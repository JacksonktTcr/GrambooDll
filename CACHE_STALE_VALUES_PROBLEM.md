# Cache Invalidation Problem: Stale Values

## Your Issue

You're getting **incorrect cached values** showing in the summary row. The cache is remembering old totals from previous operations instead of showing current data.

---

## Root Cause: Missing Cache Invalidation Points

The cache is only invalidated in **ONE place** - `dgv_CellValueChanged`. But there are many other operations that should also invalidate it:

### Events That Should Invalidate Cache But Don't

1. **Data Source Changed** ? NOT invalidating
2. **Rows Added** ? NOT invalidating  
3. **Rows Deleted** ? NOT invalidating
4. **Rows Cleared** ? NOT invalidating
5. **Rows Sorted** ? NOT invalidating
6. **Rows Filtered** ? NOT invalidating
7. **Summary Columns Changed** ? NOT invalidating

---

## Current Code Problem

```csharp
private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
{
    // ? Only place cache is invalidated!
    cacheValid = false;
    calcSingleColumnSummary(column);
}
```

This only handles cell edits, NOT these operations:

```csharp
// These DON'T invalidate cache:
grid.DataSource = newData;      // ? CACHE NOT INVALIDATED!
grid.Rows.Add(...);             // ? CACHE NOT INVALIDATED!
grid.Rows.RemoveAt(0);          // ? CACHE NOT INVALIDATED!
grid.Rows.Clear();              // ? CACHE NOT INVALIDATED!
```

---

## Solution: Add More Cache Invalidation Points

Subscribe to events that modify data:

```csharp
public SummaryControlContainer(GrbDataGridView dgv)
{
    // ... existing subscriptions ...
    
    // ? ADD THESE:
    this.dgv.DataSourceChanged += dgv_DataSourceChanged;
    this.dgv.RowsAdded += dgv_RowsAdded;
    this.dgv.RowsRemoved += dgv_RowsRemoved;
    this.dgv.UserDeletingRow += dgv_UserDeletingRow;
}

// ? NEW: Data source changed
private void dgv_DataSourceChanged(object sender, EventArgs e)
{
    cacheValid = false;  // Invalidate cache
    calcSummaries();     // Recalculate
}

// ? NEW: Rows added
private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
{
    cacheValid = false;  // Invalidate cache
    calcSummaries();     // Recalculate
}

// ? NEW: Rows removed
private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
{
    cacheValid = false;  // Invalidate cache
    calcSummaries();     // Recalculate
}

// ? NEW: User deleting row
private void dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
{
    cacheValid = false;  // Invalidate cache (will recalc after delete)
}
```

---

## Why Cache Shows Stale Values

### Scenario

```
1. Grid has 3 rows: 100 + 200 + 300 = 600 total
   Cache: { col1 = 600m }
   cacheValid: true

2. Data reloaded with different data:
   5 rows: 1000 + 2000 + 3000 + 4000 + 5000 = 15000 total
   
3. BUT event not subscribed!
   Cache NOT invalidated
   Cache still has: { col1 = 600m }
   cacheValid: still true!
   
4. refresh() called
   ?
   calcSummaries()
   ?
   if (cacheValid && summaryCache.Count > 0)  ? TRUE!
   {
       ApplyCachedSummaries()  ? Uses old cached 600m!
       ? STALE DATA DISPLAYED! ?
   }
```

---

## Also Check RefreshSummary()

The `RefreshSummary()` method doesn't invalidate cache:

```csharp
// Current code:
internal void RefreshSummary(bool reCreateSummary = false)
{
    if (!dgv.SummaryRowVisible)
        return;

    if (reCreateSummary)
    {
        reCreateSumBoxes();
        // ?? After recreating, cache is cleared but:
        // summaryCache.Clear() is called in reCreateSumBoxes()
        // But we need to ensure calcSummaries() recalculates
    }

    calcSummaries();
    // ?? This uses cache if cacheValid is true!
}

// Better:
internal void RefreshSummary(bool reCreateSummary = false)
{
    if (!dgv.SummaryRowVisible)
        return;

    if (reCreateSummary)
    {
        cacheValid = false;      // ? Invalidate cache!
        reCreateSumBoxes();
    }
    else
    {
        cacheValid = false;      // ? Always invalidate on refresh
    }

    calcSummaries();
}
```

---

## Why Your Workaround Worked

Your line:
```csharp
cacheValid = false;
```

**Forced** recalculation every time:
- Bypassed the cache check
- Always recalculated fresh totals
- Fixed the stale data problem
- But disabled ALL caching benefit!

---

## Complete Fix

### 1. Add Cache Invalidation to Dispose

```csharp
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        if (dgv != null)
        {
            // ? Unsubscribe from new events
            dgv.DataSourceChanged -= dgv_DataSourceChanged;
            dgv.RowsAdded -= dgv_RowsAdded;
            dgv.RowsRemoved -= dgv_RowsRemoved;
            dgv.UserDeletingRow -= dgv_UserDeletingRow;
            
            // ... existing unsubscribes ...
        }
        
        summaryCache.Clear();
    }

    base.Dispose(disposing);
}
```

### 2. Add Event Handlers

```csharp
private void dgv_DataSourceChanged(object sender, EventArgs e)
{
    cacheValid = false;
    calcSummaries();
}

private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
{
    cacheValid = false;
    calcSummaries();
}

private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
{
    cacheValid = false;
    calcSummaries();
}

private void dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
{
    cacheValid = false;
}
```

### 3. Fix RefreshSummary

```csharp
internal void RefreshSummary(bool reCreateSummary = false)
{
    if (!dgv.SummaryRowVisible)
        return;

    // ? Always invalidate cache on refresh
    cacheValid = false;
    
    if (reCreateSummary)
        reCreateSumBoxes();

    calcSummaries();
}
```

---

## Result

? Cache is properly invalidated when data changes  
? Fresh calculations happen when needed  
? Stale values no longer appear  
? Performance still improved (cache used when valid)  
? No more `cacheValid = false` workaround needed!

---

## Prevention

Add cache invalidation whenever:
- Data source changes
- Rows are added/removed
- Columns are reordered
- Filter is applied
- Sort is applied
- Summary columns change

**Rule:** When the data model changes, **ALWAYS invalidate the cache!**

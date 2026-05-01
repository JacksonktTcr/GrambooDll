# Auto-Show Summary Row on SummaryColumns Assignment

## Overview
When `SummaryColumns` is assigned to a `GrbDataGridView`, the summary row now **automatically shows and calculates** without requiring explicit manual configuration of `SummaryRowVisible`.

## How It Works

### The Feature
Setting `SummaryColumns` now automatically:
1. ? Sets `SummaryRowVisible = true`
2. ? Calls `RefreshSummary(true)` to create summary boxes
3. ? Triggers calculation for all assigned summary columns
4. ? Displays the summary row below the grid data

### Code Flow
```csharp
// Setting SummaryColumns automatically triggers everything
dgv.SummaryColumns = new string[] { "Amount", "Quantity", "Weight" };
// Result: Summary row is now visible and calculating automatically

// No need to manually do this anymore:
// dgv.SummaryRowVisible = true;  // Not needed!
// dgv.RefreshSummary(true);       // Not needed!
```

## Implementation Details

### Location
File: `gramboo\Controls\GrbDataGridView.cs`

### Property Setter
```csharp
public string[] SummaryColumns
{
    get { return summaryColumns; }
    set { 
        summaryColumns = value;

        // ? AUTO-CREATE: Automatically show summary row when columns are set
        if (summaryColumns != null && summaryColumns.Length > 0 && !SummaryPaused)
        {
            // Set SummaryRowVisible which triggers RefreshSummary
            SummaryRowVisible = true;
        }
        else if (summaryRowVisible && !SummaryPaused)
        {
            // Refresh if already visible
            RefreshSummary(true);
        }
    }
}
```

### The `SummaryRowVisible` Property
When set to `true`, it automatically triggers:
```csharp
public bool SummaryRowVisible
{
    get { return summaryRowVisible; }
    set
    {
        summaryRowVisible = value;
        if (summaryRowVisible)
        {
            RefreshSummary(true);  // ? Creates summary boxes and calculates
        }
    }
}
```

## Usage Examples

### Example 1: Basic Usage (Simplest)
```csharp
// Just set SummaryColumns - everything else happens automatically!
dgv.SummaryColumns = new string[] { "Amount", "Quantity" };
dgv.DataSource = data;  // Load data after setting summary columns
// Summary row is now visible and showing calculations
```

### Example 2: Correct Order
```csharp
// Set summary columns BEFORE or WITH other properties
dgv.SummaryRowVisible = false;  // Can be set, but doesn't matter
dgv.SummaryColumns = new string[] { "Amount", "Quantity" };  // This overrides to true!
dgv.SummaryRowBackColor = Color.LightGray;
dgv.FormatString = "F02";
dgv.DataSource = data;
```

### Example 3: Empty Summary Columns
```csharp
// If you pass empty array, summary row won't auto-show
dgv.SummaryColumns = new string[] { };  // Empty - no summary
dgv.DataSource = data;

// But if you already have it visible and set empty, it refreshes
dgv.SummaryRowVisible = true;
dgv.SummaryColumns = new string[] { };  // Still refreshes
```

### Example 4: With SummaryPaused
```csharp
// If SummaryPaused is true, auto-show is skipped
dgv.SummaryPaused = true;
dgv.SummaryColumns = new string[] { "Amount" };  // Won't auto-show (paused)

// To enable later:
dgv.SummaryPaused = false;
dgv.SummaryColumns = new string[] { "Amount" };  // Now it will show!
```

## Behavior Details

### When SummaryColumns is Assigned:

| Condition | Action |
|-----------|--------|
| `SummaryColumns != null && Length > 0` | Auto-set `SummaryRowVisible = true` |
| `SummaryPaused == true` | Skip auto-show (honor pause state) |
| `SummaryRowVisible already true` | Call `RefreshSummary(true)` to recalculate |
| `SummaryColumns == null || Length == 0` | Do nothing |

### Key Points
- ? **No more manual setup required** for common cases
- ? **Respects `SummaryPaused` flag** - won't show if paused
- ? **Automatic recalculation** when columns change
- ? **Backward compatible** - existing code still works
- ? **Triggers through property setter** - uses `SummaryRowVisible` for consistency

## Migration Guide

### Old Way (Still Works)
```csharp
dgv.SummaryRowVisible = true;
dgv.SummaryColumns = new string[] { "Amount", "Quantity" };
dgv.DataSource = data;
```

### New Way (Simplified)
```csharp
dgv.SummaryColumns = new string[] { "Amount", "Quantity" };
dgv.DataSource = data;
```

### Both achieve the same result - but the new way requires fewer lines!

## Advantages

1. **Reduced Code** - No need for separate `SummaryRowVisible = true` assignments
2. **Intuitive** - When you set summary columns, you expect to see the summary
3. **Less Error-Prone** - Fewer configuration steps means fewer things to forget
4. **Automatic** - Calculations start immediately without extra calls
5. **Maintainable** - Cleaner, more readable code

## Potential Issues & Solutions

### Issue: Summary Row Not Showing
**Check:**
- [ ] `SummaryColumns` is set with at least one column name
- [ ] `SummaryPaused` is `false`
- [ ] Column names in `SummaryColumns` match `DataPropertyName` exactly
- [ ] Data has been loaded (`DataSource` is set)

### Issue: Summary Showing but No Values
**Check:**
- [ ] Columns are numeric types (int, decimal, float, double)
- [ ] `DataGridView` has rows with data
- [ ] Column names in `SummaryColumns` match exactly (case-insensitive matching is used, but whitespace matters)

### Issue: Auto-Show Disabled
**If `SummaryPaused = true`:**
```csharp
dgv.SummaryPaused = false;  // Disable pause
dgv.SummaryColumns = new string[] { "Amount" };  // Now auto-show works
```

## Technical Notes

- The setter directly sets the property `summaryRowVisible` via the property instead of directly assigning the backing field
- This ensures all `SummaryRowVisible` logic runs (including the `changeParent()` layout reorganization)
- `RefreshSummary(true)` is called which recreates summary boxes and recalculates all totals
- The feature respects the `SummaryPaused` flag to allow temporary disabling

## Performance Considerations

- Auto-showing is instantaneous (no performance impact)
- Calculation performance depends on data size (unchanged from before)
- Caching mechanism still in place for optimized updates

## Backward Compatibility

? **Fully backward compatible**
- Existing code that sets `SummaryRowVisible` manually still works
- New code can omit that line for cleaner syntax
- No breaking changes to any API

## Summary

This enhancement makes `GrbDataGridView` easier to use by automatically displaying and configuring the summary row when you assign summary columns. It's a quality-of-life improvement that reduces boilerplate code while maintaining full backward compatibility.

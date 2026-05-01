# Quick Reference: Auto-Summary Row Feature

## One-Line Summary
**When you assign `SummaryColumns`, the summary row automatically shows and calculates.**

## The Basic Formula

### Before (Old Way - Still Works)
```csharp
dgv.SummaryRowVisible = true;
dgv.SummaryColumns = new string[] { "Amount", "Qty" };
dgv.DataSource = myData;
```

### Now (New Way - Recommended)
```csharp
dgv.SummaryColumns = new string[] { "Amount", "Qty" };
dgv.DataSource = myData;
```

## Three Simple Rules

1. **Set SummaryColumns** ? Summary row auto-shows ?
2. **Use correct column names** ? Must match `DataPropertyName` exactly
3. **Have numeric data** ? Summary calculations only work on numbers

## Common Scenarios

### Scenario 1: Purchase Entry
```csharp
dgv.SummaryColumns = new string[] { "Amount", "Quantity", "Weight" };
dgv.DataSource = purchaseData;  // Summary row is now visible!
```

### Scenario 2: Sales Grid
```csharp
dgv.SummaryColumns = new string[] { "NetAmount", "TaxAmount" };
dgv.SummaryRowVisible = false;  // Doesn't matter - SummaryColumns assignment overrides
dgv.DataSource = salesData;     // Summary is STILL visible
```

### Scenario 3: Disable Later
```csharp
dgv.SummaryColumns = new string[] { "Amount" };  // Shows
// ...later...
dgv.SummaryColumns = new string[] { };  // Clears - shows refresh but no summaries
dgv.SummaryRowVisible = false;          // Hide completely
```

## What Gets Triggered Automatically

When you do: `dgv.SummaryColumns = new string[] { "Amount" };`

? Summary row becomes visible  
? Summary boxes are created for each column  
? Calculations start running  
? Display refreshes with totals  

**All in one line!**

## Property Dependencies

```
SummaryColumns assignment
    ?
    ??? SummaryRowVisible = true (automatic)
            ?
            ??? RefreshSummary(true) (automatic)
                    ?
                    ??? reCreateSumBoxes() (creates UI controls)
                    ??? calcSummaries() (does calculations)
                    ??? Displays values
```

## Column Name Matching

**Must match** the `DataPropertyName` of the column:

```csharp
// In grid:
column.DataPropertyName = "Amount";

// In summary:
dgv.SummaryColumns = new string[] { "Amount" };  // ? Matches!

// NOT:
dgv.SummaryColumns = new string[] { "Amt" };     // ? Won't work
dgv.SummaryColumns = new string[] { " Amount" }; // ? Extra space won't work
```

## Troubleshooting Checklist

| Problem | Solution |
|---------|----------|
| Summary not showing | Check `SummaryPaused` is `false` |
| Summary showing but empty | Ensure columns have numeric data |
| Column name issues | Use exact `DataPropertyName` match |
| Summary disappears | Check if `SummaryRowVisible` set to `false` elsewhere |
| Values not updating | Make sure grid has data rows |

## Exception Cases

### When Auto-Show WON'T Happen
```csharp
if (dgv.SummaryPaused == true)
{
    dgv.SummaryColumns = new string[] { "Amount" };  
    // Won't auto-show (paused)
}

if (dgv.SummaryColumns == new string[] { })
{
    // Empty array - won't auto-show
}

if (dgv.SummaryColumns == null)
{
    // Null - won't auto-show
}
```

### Force Manual Control
```csharp
dgv.SummaryRowVisible = false;  // Manually hide
dgv.SummaryColumns = new string[] { "Amount" };
// Will still try to show because SummaryColumns assignment triggers it
// To truly prevent: set SummaryPaused = true

dgv.SummaryPaused = true;
dgv.SummaryColumns = new string[] { "Amount" };  // Now truly prevented
```

## Real-World Examples

### Example 1: Stock Entry
```csharp
private void LoadStockData()
{
    stockGrid.SummaryColumns = new string[] { "OpeningStock", "Inward", "Outward", "ClosingStock" };
    stockGrid.DataSource = GetStockData();  // Summary auto-shows with totals!
}
```

### Example 2: Billing System
```csharp
public void PopulateInvoiceGrid(DataTable invoiceDetails)
{
    invoiceGrid.SummaryColumns = new string[] { "ItemQty", "ItemAmount", "TaxAmount" };
    invoiceGrid.FormatString = "F02";
    invoiceGrid.DataSource = invoiceDetails;
    // User sees summary totals immediately below the items
}
```

### Example 3: Stock Comparison (from codebase)
```csharp
public void PopulateGrid()
{
    dgv.SummaryColumns = new string[] { "Qty", "Wt", "Physical No", "Physical Wt", "DiaWt", "Physical DiaWt" };
    dgv.DataSource = DBConn.GetData(cmd, "CompareClosingStock").Tables[0];
    // All summaries now calculating automatically!
}
```

## Performance Notes

- ? No performance degradation
- ? Automatic caching for repeated refreshes
- ? Only recalculates when data changes
- ? Individual column recalc on cell changes

## Code Cleanup Opportunities

Before:
```csharp
dgv.SummaryRowVisible = true;
dgv.SummaryColumns = new string[] { "Amount" };
dgv.FormatString = "F02";
dgv.DataSource = data;
dgv.RefreshSummary(true);  // ? Not needed
```

After:
```csharp
dgv.SummaryColumns = new string[] { "Amount" };
dgv.FormatString = "F02";
dgv.DataSource = data;
```

**Result:** Cleaner code, same functionality, auto-triggered calculations!

## Remember
- **Set ? Show ? Calculate** - All automatic when you assign `SummaryColumns`
- **One property, multiple effects** - Assignment triggers the whole pipeline
- **Less code, same power** - Simplified without losing functionality

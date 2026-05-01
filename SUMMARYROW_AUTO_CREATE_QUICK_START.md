# ?? Auto-Create SummaryRow - Quick Start

## One-Minute Overview

**Before (Old Way):**
```csharp
grid.SummaryColumns = new[] { "Amount" };
grid.SummaryRowVisible = true;          // Manual
grid.RefreshSummary(true);              // Manual
```

**After (New Way):**
```csharp
grid.SummaryColumns = new[] { "Amount" };
// ? Automatic! Summary row appears immediately!
```

---

## TL;DR - Just Use This

```csharp
// Set summary columns - that's all!
grid.SummaryColumns = new[] { "TotalAmount", "Quantity", "Discount" };

// The grid automatically:
// ? Makes the summary row visible
// ? Creates summary controls for each column
// ? Calculates totals
// ? Displays formatted values
```

---

## Copy-Paste Examples

### Example 1: Basic Setup
```csharp
GrbDataGridView grid = new GrbDataGridView();
grid.DataSource = GetSalesData();

// Just set columns - everything else is automatic!
grid.SummaryColumns = new[] { "Amount", "Quantity" };
```

### Example 2: Multiple Columns
```csharp
grid.SummaryColumns = new[] 
{ 
    "Sales",
    "Expenses", 
    "Profit",
    "Count"
};
```

### Example 3: Update Columns
```csharp
// Change summary columns - automatically updates
grid.SummaryColumns = new[] { "Revenue", "CostOfSales" };
// ? Summary row refreshes automatically
```

### Example 4: Disable Summary
```csharp
// Clear columns
grid.SummaryColumns = null;
// or
grid.SummaryColumns = new string[0];
```

---

## How It Works

```
Set SummaryColumns
        ?
Is it non-empty & not paused?
        ?
    ? YES ? Show summary row
              ?
              Create summary controls
              ?
              Calculate values
              ?
              Display on screen
    
    ? NO ? Don't show
```

---

## Important: What's Automatic

? **These happen automatically now:**
- Summary row visibility
- Summary control creation
- Refreshing the display
- Recalculation of totals

? **These you still need:**
- Setting the data source (`grid.DataSource = ...`)
- Configuring display format (`grid.FormatString = ...`)
- Manual refresh only if data changes after summary is set

---

## Common Tasks

### Show Summary
```csharp
grid.SummaryColumns = new[] { "Amount", "Quantity" };
```

### Change Summary Columns
```csharp
grid.SummaryColumns = new[] { "Different", "Columns" };
```

### Hide Summary
```csharp
grid.SummaryRowVisible = false;
// or
grid.SummaryColumns = null;
```

### Pause Calculations
```csharp
grid.SummaryPaused = true;
// ... make changes ...
grid.SummaryPaused = false;
```

---

## Troubleshooting

| Problem | Solution |
|:--------|:---------|
| Summary not showing | Check: `SummaryColumns != null && SummaryColumns.Length > 0` |
| Summary not updating | Ensure `SummaryPaused = false` |
| Wrong columns shown | Check column names match data fields exactly |

---

## That's It!

You're done. Just set `SummaryColumns` and enjoy automatic summary rows! ??

For more details, see: `SUMMARYROW_AUTO_CREATE_GUIDE.md`


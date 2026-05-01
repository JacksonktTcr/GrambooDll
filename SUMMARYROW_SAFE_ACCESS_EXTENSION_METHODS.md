# ??? Safe SummaryRow Access - Non-Breaking Solution

## Problem

Your code is used in many forms and you can't modify the existing `SummaryCellCollection` class. But you still get null reference exceptions when accessing `SummaryCells["column"].Text`.

## Solution: Extension Methods (Non-Breaking ?)

Instead of modifying the existing class, we add extension methods that provide safe access without changing any existing code.

---

## Implementation: Create `SummaryCellExtensions.cs`

Create a new file in your `Controls` folder:

### File: `gramboo\Controls\SummaryCellExtensions.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gramboo.Controls
{
    /// <summary>
    /// ? Safe extension methods for SummaryCellCollection
    /// Non-breaking - doesn't modify existing code
    /// Works with existing implementations
    /// </summary>
    public static class SummaryCellExtensions
    {
        // ? NEW: Safe access method - TryGet pattern
        public static bool TryGetCell(this SummaryCellCollection collection, string columnName, out ReadOnlyTextBox cell)
        {
            cell = collection[columnName];
            return cell != null;
        }

        // ? NEW: Get cell text safely with default value
        public static string GetCellText(this SummaryCellCollection collection, string columnName, string defaultText = "")
        {
            try
            {
                var cell = collection[columnName];
                return cell?.Text ?? defaultText;
            }
            catch
            {
                return defaultText;
            }
        }

        // ? NEW: Get cell value (Tag) safely with default value
        public static decimal GetCellValue(this SummaryCellCollection collection, string columnName, decimal defaultValue = 0m)
        {
            try
            {
                var cell = collection[columnName];
                if (cell != null && cell.Tag is decimal decValue)
                    return decValue;
                if (cell != null && decimal.TryParse(cell.Tag?.ToString() ?? "", out decimal parsed))
                    return parsed;
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        // ? NEW: Check if column exists
        public static bool ContainsColumn(this SummaryCellCollection collection, string columnName)
        {
            return collection[columnName] != null;
        }

        // ? NEW: Get all column names for debugging
        public static List<string> GetColumnNames(this SummaryCellCollection collection)
        {
            var names = new List<string>();
            for (int i = 0; i < collection.Count; i++)
            {
                var cell = collection[i];
                if (cell != null && !string.IsNullOrWhiteSpace(cell.DataPropertyName))
                    names.Add(cell.DataPropertyName);
            }
            return names;
        }

        // ? NEW: Get cell value with type conversion
        public static T GetCellValue<T>(this SummaryCellCollection collection, string columnName, T defaultValue = default(T))
        {
            try
            {
                var cell = collection[columnName];
                if (cell?.Tag == null)
                    return defaultValue;

                // Try direct cast
                if (cell.Tag is T tValue)
                    return tValue;

                // Try conversion
                if (typeof(T) == typeof(decimal))
                {
                    if (decimal.TryParse(cell.Tag.ToString(), out decimal decResult))
                        return (T)(object)decResult;
                }
                else if (typeof(T) == typeof(int))
                {
                    if (int.TryParse(cell.Tag.ToString(), out int intResult))
                        return (T)(object)intResult;
                }
                else if (typeof(T) == typeof(string))
                {
                    return (T)(object)(cell.Tag?.ToString() ?? "");
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        // ? NEW: Safe operation - execute if cell exists
        public static void IfCellExists(this SummaryCellCollection collection, string columnName, Action<ReadOnlyTextBox> action)
        {
            var cell = collection[columnName];
            if (cell != null)
                action(cell);
        }

        // ? NEW: Safe mapping - transform cell value if exists
        public static T IfCellExists<T>(this SummaryCellCollection collection, string columnName, Func<ReadOnlyTextBox, T> mapper, T defaultValue = default(T))
        {
            var cell = collection[columnName];
            return cell != null ? mapper(cell) : defaultValue;
        }
    }
}
```

---

## Benefits of This Approach

### ? Non-Breaking
- No changes to existing code
- Existing forms continue to work
- No compilation issues

### ? Backward Compatible
- Old code still works exactly the same
- New safe methods are additive

### ? Easy to Use
- Just call on `SummaryCells` property
- Works with existing forms

### ? No Risk
- Only adds extension methods
- Doesn't touch existing implementations

---

## Usage Examples

### Example 1: Safe Text Access (Instead of Crashing)

```csharp
// OLD (Can crash)
var text = dgv.SummaryRow.SummaryCells["Amount"].Text;

// NEW (Always safe)
var text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
```

### Example 2: Safe Numeric Access

```csharp
// NEW: Get decimal value safely
decimal total = dgv.SummaryRow.SummaryCells.GetCellValue("Total", 0m);
```

### Example 3: Check Before Using

```csharp
// NEW: Check if column exists
if (dgv.SummaryRow.SummaryCells.ContainsColumn("Amount"))
{
    var value = dgv.SummaryRow.SummaryCells.GetCellValue("Amount");
}
```

### Example 4: Debug - List All Columns

```csharp
// NEW: See what columns are available
var columns = dgv.SummaryRow.SummaryCells.GetColumnNames();
foreach (var col in columns)
    Debug.WriteLine(col);
```

### Example 5: Generic Type Conversion

```csharp
// NEW: Get with automatic type conversion
decimal amount = dgv.SummaryRow.SummaryCells.GetCellValue<decimal>("Amount", 0m);
int count = dgv.SummaryRow.SummaryCells.GetCellValue<int>("Count", 0);
string text = dgv.SummaryRow.SummaryCells.GetCellValue<string>("Text", "");
```

### Example 6: Conditional Actions

```csharp
// NEW: Execute code only if cell exists
dgv.SummaryRow.SummaryCells.IfCellExists("Amount", cell =>
{
    MessageBox.Show($"Amount: {cell.Text}");
});

// NEW: Map value if exists
var displayText = dgv.SummaryRow.SummaryCells.IfCellExists("Amount", 
    cell => $"Total: {cell.Text}",
    "No Summary");
```

---

## Real-World Scenarios

### Scenario 1: Export Summary to Excel

```csharp
public string ExportSummary(GrbDataGridView dgv)
{
    // ? SAFE: Loop through all columns without crashing
    var summaryValues = dgv.SummaryRow.SummaryCells.GetColumnNames()
        .Select(col => new 
        { 
            Column = col,
            Value = dgv.SummaryRow.SummaryCells.GetCellText(col, "")
        })
        .ToList();

    var tsv = string.Join("\n", summaryValues
        .Select(s => $"{s.Column}\t{s.Value}"));
    
    return tsv;
}
```

### Scenario 2: Update Label with Summary

```csharp
public void UpdateSummaryLabel(GrbDataGridView dgv, Label lblStatus)
{
    // ? SAFE: Get values without worrying about null
    decimal sales = dgv.SummaryRow.SummaryCells.GetCellValue("Sales", 0m);
    decimal expenses = dgv.SummaryRow.SummaryCells.GetCellValue("Expenses", 0m);
    
    lblStatus.Text = $"Sales: {sales:C} | Expenses: {expenses:C}";
}
```

### Scenario 3: Print Grid with Summary

```csharp
public void PrintGridWithSummary(GrbDataGridView dgv)
{
    var html = "<table>";
    
    // ... add rows ...
    
    // ? SAFE: Add summary row safely
    if (dgv.SummaryRow.SummaryCells.Count > 0)
    {
        html += "<tr><td colspan='100' style='font-weight:bold'>";
        
        // ? SAFE: Iterate through all columns
        dgv.SummaryRow.SummaryCells.IfCellExists("Amount", cell =>
        {
            html += $"Total: {cell.Text}";
        });
        
        html += "</td></tr>";
    }
    
    html += "</table>";
    
    // ... show print dialog ...
}
```

### Scenario 4: Validate Summary Before Saving

```csharp
public bool ValidateSummary(GrbDataGridView dgv)
{
    // ? SAFE: Check and validate values
    decimal total = dgv.SummaryRow.SummaryCells.GetCellValue("Total", 0m);
    
    if (total <= 0)
    {
        MessageBox.Show("Total must be greater than 0");
        return false;
    }
    
    return true;
}
```

---

## Implementation Steps

### Step 1: Create Extension Methods File

Create new file: `gramboo\Controls\SummaryCellExtensions.cs`

Copy the code above into it.

### Step 2: Add Using Statement

In any file that uses summary cells, add:

```csharp
using Gramboo.Controls;  // If not already there
```

### Step 3: Use Safe Methods

Replace unsafe access with safe methods:

```csharp
// OLD (can crash)
var text = dgv.SummaryRow.SummaryCells["Amount"].Text;

// NEW (never crashes)
var text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
```

### Step 4: No More Changes Needed!

That's it! Your existing forms continue to work, but you have safe methods available.

---

## Complete Example: Real Form Usage

```csharp
public partial class SalesForm : Form
{
    private GrbDataGridView dgvSales;
    
    private void LoadData()
    {
        // ... load grid ...
        dgvSales.SummaryColumns = new[] { "Amount", "Quantity", "Discount" };
    }
    
    private void DisplaySummary()
    {
        // ? SAFE: Get values without null checks
        decimal totalAmount = dgvSales.SummaryRow.SummaryCells
            .GetCellValue("Amount", 0m);
        
        decimal totalQuantity = dgvSales.SummaryRow.SummaryCells
            .GetCellValue("Quantity", 0m);
        
        decimal totalDiscount = dgvSales.SummaryRow.SummaryCells
            .GetCellValue("Discount", 0m);
        
        // Display values
        lblAmount.Text = $"Amount: {totalAmount:C}";
        lblQuantity.Text = $"Qty: {totalQuantity}";
        lblDiscount.Text = $"Discount: {totalDiscount:C}";
    }
    
    private void ExportData()
    {
        // ? SAFE: Export all summary columns
        var columns = dgvSales.SummaryRow.SummaryCells.GetColumnNames();
        
        var summary = string.Join("\t", columns
            .Select(col => dgvSales.SummaryRow.SummaryCells
                .GetCellText(col, "")));
        
        Clipboard.SetText(summary);
    }
    
    private void ValidateBeforeSave()
    {
        // ? SAFE: Validate summary values
        decimal total = dgvSales.SummaryRow.SummaryCells
            .GetCellValue("Amount", 0m);
        
        if (total <= 0)
        {
            MessageBox.Show("Total must be greater than 0");
            return;
        }
        
        // Safe to save...
    }
}
```

---

## Extension Methods Reference

### GetCellText
```csharp
string GetCellText(string columnName, string defaultText = "")
```
Get text value with default fallback.

### GetCellValue
```csharp
decimal GetCellValue(string columnName, decimal defaultValue = 0m)
decimal GetCellValue<T>(string columnName, T defaultValue = default(T))
```
Get numeric or any type value with default fallback.

### TryGetCell
```csharp
bool TryGetCell(string columnName, out ReadOnlyTextBox cell)
```
Safe try-get pattern.

### ContainsColumn
```csharp
bool ContainsColumn(string columnName)
```
Check if column exists.

### GetColumnNames
```csharp
List<string> GetColumnNames()
```
Get all available column names.

### IfCellExists
```csharp
void IfCellExists(string columnName, Action<ReadOnlyTextBox> action)
T IfCellExists<T>(string columnName, Func<ReadOnlyTextBox, T> mapper, T defaultValue)
```
Execute code if cell exists (LINQ-like syntax).

---

## Advantages Over Direct Modification

| Aspect | Direct Mod | Extension Methods |
|:---|:---|:---|
| **Breaking Changes** | Yes ? | No ? |
| **Existing Code** | May break | Works as-is ? |
| **All Forms** | Need updates | No updates needed ? |
| **Risk Level** | High | Low ? |
| **Implementation** | Complex | Simple ? |
| **Deployment** | Risky | Safe ? |

---

## FAQ

**Q: Will this break existing code?**  
A: No! Extension methods are additive. Existing code works exactly the same.

**Q: Do I need to change all my forms?**  
A: No! Use the safe methods only where you need them.

**Q: What if I forget to use safe methods?**  
A: Your existing code still works. The safe methods are optional.

**Q: Performance impact?**  
A: None! Extension methods compile to the same IL as regular methods.

**Q: Can I use with existing implementations?**  
A: Yes! Extension methods work with any class that has `SummaryCellCollection`.

---

## Summary

### Problem
Can't modify existing code used in many forms, but getting null reference exceptions.

### Solution
Create extension methods file - adds safe access without modifying existing code.

### Benefits
? Non-breaking  
? Backward compatible  
? No form changes needed  
? Optional to use  
? Safe by default  

### Next Steps
1. Create `SummaryCellExtensions.cs`
2. Copy extension methods code
3. Use safe methods where needed
4. No other changes required!

**That's it! Safe access without breaking existing code!** ??


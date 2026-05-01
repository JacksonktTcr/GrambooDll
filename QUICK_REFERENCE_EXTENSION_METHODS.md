# ? Quick Reference Card - SummaryCellExtensions

## File Location
?? `gramboo\Controls\SummaryCellExtensions.cs`

? **Status:** Created and Ready to Use

---

## 12 Extension Methods

### 1?? GetCellText() - Text Value
```csharp
string text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
```

### 2?? GetCellValue() - Decimal Value
```csharp
decimal value = dgv.SummaryRow.SummaryCells.GetCellValue("Amount", 0m);
```

### 3?? GetCellValue<T>() - Any Type
```csharp
var dec = dgv.SummaryRow.SummaryCells.GetCellValue<decimal>("Amount");
var int = dgv.SummaryRow.SummaryCells.GetCellValue<int>("Count");
var str = dgv.SummaryRow.SummaryCells.GetCellValue<string>("Text");
```

### 4?? TryGetCell() - Try Pattern
```csharp
if (dgv.SummaryRow.SummaryCells.TryGetCell("Amount", out var cell))
    MessageBox.Show(cell.Text);
```

### 5?? ContainsColumn() - Check Exists
```csharp
if (dgv.SummaryRow.SummaryCells.ContainsColumn("Amount"))
    var value = dgv.SummaryRow.SummaryCells.GetCellValue("Amount");
```

### 6?? GetColumnNames() - List All
```csharp
var columns = dgv.SummaryRow.SummaryCells.GetColumnNames();
```

### 7?? IfCellExists() - Conditional
```csharp
dgv.SummaryRow.SummaryCells.IfCellExists("Amount", cell =>
{
    MessageBox.Show($"Amount: {cell.Text}");
});
```

### 8?? IfCellExists<T>() - Map Value
```csharp
var display = dgv.SummaryRow.SummaryCells.IfCellExists("Amount",
    cell => $"Total: {cell.Text}",
    "No data");
```

### 9?? GetCellTexts() - Multiple Texts
```csharp
var values = dgv.SummaryRow.SummaryCells.GetCellTexts("A", "B", "C");
// Returns: Dictionary<string, string>
```

### ?? GetCellValues() - Multiple Values
```csharp
var values = dgv.SummaryRow.SummaryCells.GetCellValues("A", "B", "C");
// Returns: Dictionary<string, decimal>
```

### 1??1?? ExportAsLine() - Export Tab-Separated
```csharp
string line = dgv.SummaryRow.SummaryCells.ExportAsLine();
// Returns: "col1\tcol2\tcol3"
```

### 1??2?? ExportWithHeaders() - Export CSV
```csharp
string csv = dgv.SummaryRow.SummaryCells.ExportWithHeaders();
// Returns: "Col1\tCol2\tCol3\nval1\tval2\tval3"
```

---

## Common Patterns

### Pattern 1: Simple Get
```csharp
var amount = SummaryCells.GetCellText("Amount", "0");
```

### Pattern 2: Multiple Gets
```csharp
var values = SummaryCells.GetCellValues("Sales", "Expenses", "Profit");
decimal sales = values["Sales"];
```

### Pattern 3: Check First
```csharp
if (SummaryCells.ContainsColumn("Amount"))
    var value = SummaryCells.GetCellValue("Amount");
```

### Pattern 4: Conditional Action
```csharp
SummaryCells.IfCellExists("Amount", cell =>
{
    // Use cell safely
});
```

### Pattern 5: Export
```csharp
var csv = SummaryCells.ExportWithHeaders();
Clipboard.SetText(csv);
```

### Pattern 6: Debug
```csharp
var cols = SummaryCells.GetColumnNames();
foreach (var col in cols)
    Debug.WriteLine(col);
```

---

## Real Form Example

```csharp
public class MyForm : Form
{
    private GrbDataGridView dgv;
    
    private void ShowSummary()
    {
        // ? Safe - never crashes
        var total = dgv.SummaryRow.SummaryCells.GetCellValue("Total", 0m);
        lblTotal.Text = $"Total: {total:C}";
    }
    
    private void ExportSummary()
    {
        // ? Safe - handles missing columns
        var export = dgv.SummaryRow.SummaryCells.ExportWithHeaders();
        File.WriteAllText("summary.txt", export);
    }
    
    private void PrintSummary()
    {
        // ? Safe - loop safely
        foreach (var col in dgv.SummaryRow.SummaryCells.GetColumnNames())
        {
            var value = dgv.SummaryRow.SummaryCells.GetCellText(col, "");
            Console.WriteLine($"{col}: {value}");
        }
    }
}
```

---

## Error Handling

All methods are defensive:

```csharp
SummaryCells.GetCellText("NonExistent", "default")      // ? Safe
SummaryCells.GetCellValue("NonExistent", 0m)            // ? Safe
SummaryCells.GetColumnNames()                           // ? Safe
SummaryCells.ContainsColumn("Any")                      // ? Safe
```

Never throws exceptions! Always returns default or empty.

---

## Before vs After

| Before | After |
|:---|:---|
| `SummaryCells["A"].Text` ? | `SummaryCells.GetCellText("A", "")` ? |
| Can crash | Never crashes |
| No default | Has default value |
| Risky | Safe |

---

## Quick Setup

1. ? File created: `SummaryCellExtensions.cs`
2. ? Build project
3. ? Use safe methods everywhere!

---

## Remember

? Non-breaking  
? Backward compatible  
? No existing code changes needed  
? 12 safe methods available  
? Full error handling  
? Ready to deploy  

---

**You're all set! Start using the safe methods!** ??


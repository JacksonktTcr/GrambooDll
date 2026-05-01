# Practical Testing Guide: Verify What Text Returns

## Quick Test

To verify what value is actually returned, add this code:

```csharp
// After loading data
purchaseGrid.SummaryColumns = new string[] { "Amount" };
purchaseGrid.DataSource = GetPurchaseData();

// Test what the Text property returns
string textValue = purchaseGrid.SummaryRow.SummaryCells["Amount"].Text;
MessageBox.Show($"Summary Amount Text: {textValue}");
```

**Expected Results:**
- ? Shows actual total (e.g., "1000.00")
- ? Never shows blank
- ? Never shows null
- ? Shows "0" if no data

---

## Complete Test Scenarios

### Test 1: With Data (Normal Case)
```csharp
// Arrange
var grid = new GrbDataGridView();
grid.SummaryColumns = new string[] { "Amount" };

// Create sample data
DataTable data = new DataTable();
data.Columns.Add("Item", typeof(string));
data.Columns.Add("Amount", typeof(decimal));
data.Rows.Add("Item A", 100m);
data.Rows.Add("Item B", 200m);
data.Rows.Add("Item C", 300m);

// Act
grid.DataSource = data;

// Assert - Get the summary text
string text = grid.SummaryRow.SummaryCells["Amount"].Text;
decimal value = grid.SummaryRow.SummaryCells.GetCellValue("Amount", 0m);

// Verify
Debug.Assert(text == "600.00", $"Expected '600.00', got '{text}'");
Debug.Assert(value == 600m, $"Expected 600, got {value}");
MessageBox.Show("? Test 1 PASSED: Returns calculated total");
```

---

### Test 2: No Data (Empty Grid)
```csharp
// Arrange
var grid = new GrbDataGridView();
grid.SummaryColumns = new string[] { "Amount" };

// Create empty data table
DataTable data = new DataTable();
data.Columns.Add("Item", typeof(string));
data.Columns.Add("Amount", typeof(decimal));
// No rows!

// Act
grid.DataSource = data;

// Assert
string text = grid.SummaryRow.SummaryCells["Amount"].Text;

// Verify
Debug.Assert(text == "0", $"Expected '0', got '{text}'");
MessageBox.Show("? Test 2 PASSED: Returns '0' when no data");
```

---

### Test 3: Invalid Column Name
```csharp
// Arrange
var grid = new GrbDataGridView();
grid.SummaryColumns = new string[] { "Amount" };

DataTable data = new DataTable();
data.Columns.Add("Amount", typeof(decimal));
data.Rows.Add(100m);

grid.DataSource = data;

// Act - Try to get non-existent column
string text = grid.SummaryRow.SummaryCells["NonExistent"].Text;

// Assert
Debug.Assert(text == "0", $"Expected '0', got '{text}'");
MessageBox.Show("? Test 3 PASSED: Returns '0' for non-existent column");
```

---

### Test 4: Format String Applied
```csharp
// Arrange
var grid = new GrbDataGridView();
grid.FormatString = "F02";  // Two decimal places
grid.SummaryColumns = new string[] { "Amount" };

DataTable data = new DataTable();
data.Columns.Add("Amount", typeof(decimal));
data.Rows.Add(100.5m);
data.Rows.Add(200.3m);

// Act
grid.DataSource = data;

// Assert
string text = grid.SummaryRow.SummaryCells["Amount"].Text;

// Verify - Should be formatted
Debug.Assert(text == "300.80", $"Expected '300.80', got '{text}'");
MessageBox.Show("? Test 4 PASSED: Format string applied");
```

---

### Test 5: Using Extension Methods
```csharp
// Arrange
var grid = new GrbDataGridView();
grid.SummaryColumns = new string[] { "Amount", "Quantity" };

DataTable data = new DataTable();
data.Columns.Add("Item", typeof(string));
data.Columns.Add("Amount", typeof(decimal));
data.Columns.Add("Quantity", typeof(int));
data.Rows.Add("Item A", 1000m, 100);
data.Rows.Add("Item B", 500m, 50);

// Act
grid.DataSource = data;

// Assert - Using extension method
var values = grid.SummaryRow.SummaryCells.GetCellValues("Amount", "Quantity");

// Verify
Debug.Assert(values["Amount"] == 1500m, $"Amount should be 1500m");
Debug.Assert(values["Quantity"] == 150m, $"Quantity should be 150m");
MessageBox.Show("? Test 5 PASSED: Extension methods work");
```

---

### Test 6: Try-Get Pattern
```csharp
// Arrange
var grid = new GrbDataGridView();
grid.SummaryColumns = new string[] { "Amount" };

DataTable data = new DataTable();
data.Columns.Add("Amount", typeof(decimal));
data.Rows.Add(1000m);

grid.DataSource = data;

// Act
bool found = grid.SummaryRow.SummaryCells.TryGetCell("Amount", out var cell);

// Assert
Debug.Assert(found, "Cell should be found");
Debug.Assert(cell.Text == "1000.00", $"Expected '1000.00', got '{cell.Text}'");
MessageBox.Show("? Test 6 PASSED: Try-get pattern works");
```

---

## Debug Verification

Add this helper method to check all summary values:

```csharp
private void DebugSummaryValues(GrbDataGridView grid)
{
    var columnNames = grid.SummaryRow.SummaryCells.GetColumnNames();
    
    foreach (var columnName in columnNames)
    {
        string text = grid.SummaryRow.SummaryCells.GetCellText(columnName, "ERROR");
        decimal value = grid.SummaryRow.SummaryCells.GetCellValue(columnName, -1m);
        
        Debug.WriteLine($"Column: {columnName}");
        Debug.WriteLine($"  Text:  {text}");
        Debug.WriteLine($"  Value: {value}");
    }
}
```

**Usage:**
```csharp
DebugSummaryValues(myGrid);
// Output in Debug window:
// Column: Amount
//   Text:  1000.00
//   Value: 1000
// Column: Quantity
//   Text:  180
//   Value: 180
```

---

## What NOT To Expect

| Case | What NOT to get |
|------|-----------------|
| **With data** | ? Blank string |
| **With data** | ? Null reference |
| **With data** | ? "0" only |
| **No data** | ? Exception |
| **No data** | ? Null |
| **Invalid column** | ? Exception |
| **Invalid column** | ? Null |

---

## Common Issues & Verification

### Issue: Getting Blank
```csharp
// WRONG: Check if you're calling before data loads
string text = grid.SummaryRow.SummaryCells["Amount"].Text;  // Blank
MessageBox.Show(text);  // ? Data not loaded yet!

// RIGHT: Load data first
grid.DataSource = data;  // Load first
string text = grid.SummaryRow.SummaryCells["Amount"].Text;  // Now has value
```

---

### Issue: Column Name Mismatch
```csharp
// WRONG: Column name doesn't match
grid.SummaryColumns = new string[] { "Amt" };  // ? Name mismatch
var text = grid.SummaryRow.SummaryCells["Amount"].Text;  // Returns "0"

// RIGHT: Use exact column name
grid.SummaryColumns = new string[] { "Amount" };  // Exact name
var text = grid.SummaryRow.SummaryCells["Amount"].Text;  // Returns value
```

---

### Issue: Format String Not Applied
```csharp
// WRONG: Format string not set
grid.SummaryColumns = new string[] { "Amount" };
var text = grid.SummaryRow.SummaryCells["Amount"].Text;  // "1000"

// RIGHT: Set format string
grid.FormatString = "F02";
grid.SummaryColumns = new string[] { "Amount" };
var text = grid.SummaryRow.SummaryCells["Amount"].Text;  // "1000.00"
```

---

## Real-World Integration Test

```csharp
// Complete working example
public void LoadAndTestSummary()
{
    // Setup grid
    var grid = new GrbDataGridView();
    grid.SummaryColumns = new string[] { "ItemQty", "UnitPrice", "LineAmount" };
    grid.FormatString = "F02";
    
    // Load data
    var purchaseData = GetPurchaseLines();
    grid.DataSource = purchaseData;
    
    // Wait for calculations (important!)
    Application.DoEvents();
    
    // Test the values
    try
    {
        string qtyText = grid.SummaryRow.SummaryCells["ItemQty"].Text;
        decimal qtyValue = grid.SummaryRow.SummaryCells.GetCellValue("ItemQty", 0m);
        
        string amountText = grid.SummaryRow.SummaryCells["LineAmount"].Text;
        decimal amountValue = grid.SummaryRow.SummaryCells.GetCellValue("LineAmount", 0m);
        
        if (string.IsNullOrEmpty(qtyText) || qtyText == "0")
        {
            MessageBox.Show("?? Warning: Summary appears to have no data");
        }
        else
        {
            MessageBox.Show($"? Success!\n\nQty: {qtyText}\nAmount: {amountText}");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"? Error: {ex.Message}");
    }
}
```

---

## Expected Output

When everything works correctly:

```
Summary Values Returned:
? Column: ItemQty
   Text:   180
   Value:  180m

? Column: UnitPrice
   Text:   10.00
   Value:  10.00m

? Column: LineAmount
   Text:   1800.00
   Value:  1800.00m
```

---

## Summary

When you call `dgv.SummaryRow.SummaryCells["column"].Text`:

| Condition | Returns | Type |
|-----------|---------|------|
| ? After data loads | Calculated total | `string` |
| ? No data | "0" | `string` |
| ? Invalid column | "0" | `string` |
| ? Error occurs | "0" | `string` |
| ? Never | null | ? Never null |
| ? Never | blank | ? Never blank |
| ? Never | exception | ? Never throws |

**Result:** Always returns a safe, valid, meaningful value! ?

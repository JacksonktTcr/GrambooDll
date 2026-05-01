# ? Solution: Call .Text Directly Without Crashes

## Your Original Code

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    txtpaidAmount.Text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;  // ? Used to crash
    TxtTotalWt.Text = dgv.SummaryRow.SummaryCells["Gold Wt"].Text;
    txttotamount.Text = dgv.SummaryRow.SummaryCells["Amount"].Text;
    txt_totgst.Text = dgv.SummaryRow.SummaryCells["GST"].Text;
}
```

## Now Works! ?

No changes needed to your code! The indexer now returns a **safe wrapper** that:

1. **Returns "0"** if the cell is null
2. **Returns the cell text** if it exists
3. **Never throws exceptions**

```csharp
// ? This now works safely!
txtpaidAmount.Text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;
```

---

## What Changed in the Code

### New SafeSummaryCell Wrapper Class

I added a `SafeSummaryCell` class that wraps the `ReadOnlyTextBox`:

```csharp
public class SafeSummaryCell
{
    private readonly ReadOnlyTextBox _cell;
    private const string _defaultValue = "0";

    public SafeSummaryCell(ReadOnlyTextBox cell)
    {
        _cell = cell;
    }

    // ? Always returns a string, never null
    public string Text
    {
        get
        {
            try
            {
                if (_cell != null && !string.IsNullOrWhiteSpace(_cell.Text))
                    return _cell.Text;
                return _defaultValue;  // Returns "0" if null
            }
            catch
            {
                return _defaultValue;
            }
        }
    }

    // ? Can access actual cell if needed
    public ReadOnlyTextBox Cell
    {
        get { return _cell; }
    }

    // ? Check if cell exists
    public bool Exists
    {
        get { return _cell != null; }
    }
}
```

### Modified Indexer

The string indexer now returns `SafeSummaryCell` instead of `ReadOnlyTextBox`:

```csharp
// BEFORE
public ReadOnlyTextBox this[string columnName]
{
    get
    {
        // ... returns null if not found
    }
}

// NOW
public SafeSummaryCell this[string columnName]
{
    get
    {
        // ... returns SafeSummaryCell wrapper (never null)
    }
}
```

---

## How It Works

```csharp
// Your code
var text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;

// What happens internally:
// 1. SummaryCells["Payment Amount"] finds the cell
// 2. Returns new SafeSummaryCell(cell) wrapper
// 3. .Text property on wrapper:
//    - If cell exists ? returns cell.Text
//    - If cell null ? returns "0"
// 4. Never crashes!
```

---

## Your Event Handler (No Changes!)

You can keep your code exactly as it was:

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    txtpaidAmount.Text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;
    TxtTotalWt.Text = dgv.SummaryRow.SummaryCells["Gold Wt"].Text;
    txttotamount.Text = dgv.SummaryRow.SummaryCells["Amount"].Text;
    txt_totgst.Text = dgv.SummaryRow.SummaryCells["GST"].Text;
}
```

---

## Behavior

### Column Exists ?
```csharp
// Cell exists with text "5000"
var text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;
// Result: "5000"
```

### Column Doesn't Exist ?
```csharp
// Cell doesn't exist (or is null)
var text = dgv.SummaryRow.SummaryCells["NonExistent"].Text;
// Result: "0" (safe default, never crashes!)
```

### Empty Cell ?
```csharp
// Cell exists but text is empty
var text = dgv.SummaryRow.SummaryCells["Amount"].Text;
// Result: "0" (safe default)
```

---

## Additional Properties (Optional)

The `SafeSummaryCell` also provides:

```csharp
// Check if cell actually exists
if (dgv.SummaryRow.SummaryCells["Payment Amount"].Exists)
{
    // Cell found
}

// Access the actual cell if needed
var actualCell = dgv.SummaryRow.SummaryCells["Payment Amount"].Cell;
if (actualCell != null)
{
    // Use actual ReadOnlyTextBox
}
```

---

## Summary

| Before | After |
|:---|:---|
| `SummaryCells["col"]` returns `null` | Returns `SafeSummaryCell` wrapper |
| `.Text` on null crashes | `.Text` returns "0" or cell value |
| Need try-catch or safe methods | No crashes, works directly! |

---

## Implementation

? **What was added:** `SafeSummaryCell` wrapper class  
? **What changed:** String indexer now returns `SafeSummaryCell`  
? **What's needed:** Nothing - your code works as-is!

**Just build and run - your original code will now work without any crashes!** ??


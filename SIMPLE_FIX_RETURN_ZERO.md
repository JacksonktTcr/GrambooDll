# ? Simple Fix - Just Return "0"

## Your Simple Solution

Added a new method `GetTextOrZero()` that:
- Gets the cell text if it exists
- Returns `"0"` if cell is null
- Never crashes!

---

## Your Fixed Event Handler

### Replace Your Current Code

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    //txtpaidAmount.Text = "";
    //TxtTotalWt.Text = "";
    txtpaidAmount.Text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;
    TxtTotalWt.Text = dgv.SummaryRow.SummaryCells["Gold Wt"].Text;
    txttotamount.Text = dgv.SummaryRow.SummaryCells["Amount"].Text;
    txt_totgst.Text = dgv.SummaryRow.SummaryCells["GST"].Text;
}
```

### With This

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    //txtpaidAmount.Text = "";
    //TxtTotalWt.Text = "";
    txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetTextOrZero("Payment Amount");
    TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetTextOrZero("Gold Wt");
    txttotamount.Text = dgv.SummaryRow.SummaryCells.GetTextOrZero("Amount");
    txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetTextOrZero("GST");
}
```

---

## How It Works

### Old Way (Crashes ?)
```csharp
dgv.SummaryRow.SummaryCells["Payment Amount"].Text
// If cell is null ? NullReferenceException!
```

### New Way (Safe ?)
```csharp
dgv.SummaryRow.SummaryCells.GetTextOrZero("Payment Amount")
// If cell is null ? Returns "0"
// If cell exists ? Returns cell.Text
// Never crashes!
```

---

## What Changed in SummaryControlContainer.cs

Added this new method to `SummaryCellCollection` class:

```csharp
// ? NEW: Get text or "0" if null (simple solution)
public string GetTextOrZero(string columnName)
{
    try
    {
        var cell = this[columnName];
        if (cell != null && !string.IsNullOrWhiteSpace(cell.Text))
            return cell.Text;
        return "0";
    }
    catch
    {
        return "0";
    }
}
```

---

## That's It!

### Step 1: Update Event Handler
Replace your event handler with the fixed version above.

### Step 2: Build
Build ? Build Solution (Ctrl+Shift+B)

### Step 3: Test
Run your application - no more crashes! ?

---

## Before & After

### BEFORE ?
```csharp
txtpaidAmount.Text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;
// Crashes if cell null
```

### AFTER ?
```csharp
txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetTextOrZero("Payment Amount");
// Returns "0" if cell null
// Never crashes!
```

---

## Summary

- **What was added:** `GetTextOrZero()` method
- **What it does:** Returns cell text or "0" if null
- **How to use:** `GetTextOrZero("columnName")`
- **Result:** No more null reference exceptions!

**Just replace your event handler and you're done!** ??


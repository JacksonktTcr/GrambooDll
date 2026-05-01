# ?? Diagnostic: dgv_SummaryCalculated Event Handler Issue

## The Problem You're Experiencing

Your event handler is crashing because it's trying to access summary cells that don't exist or aren't initialized yet:

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    txtpaidAmount.Text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;  // ? CRASHES
    TxtTotalWt.Text = dgv.SummaryRow.SummaryCells["Gold Wt"].Text;            // ? CRASHES
    txttotamount.Text = dgv.SummaryRow.SummaryCells["Amount"].Text;           // ? CRASHES
    txt_totgst.Text = dgv.SummaryRow.SummaryCells["GST"].Text;                // ? CRASHES
}
```

---

## Root Cause Analysis

### Issue 1: Column Names Mismatch
The summary cell names you're accessing might not match your actual summary columns.

**What you're looking for:**
- "Payment Amount"
- "Gold Wt"
- "Amount"
- "GST"

**But your SummaryColumns might be:**
- "col_PaymentAmount" (no space)
- "GoldWt" (different name)
- "TotalAmount" (different name)
- "TaxAmount" (different name)

### Issue 2: Event Fires Before All Cells Created
The `dgv_SummaryCalculated` event might fire when some cells haven't been created yet.

### Issue 3: Accessing Null Without Checking
When a cell doesn't exist, `SummaryCells["columnName"]` returns `null`, and calling `.Text` on null crashes.

---

## The Solution: Debug First

### Step 1: Add Debug Output to Identify Column Names

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // ? DEBUG: See what columns actually exist
        Debug.WriteLine("=== Summary Columns Debug ===");
        Debug.WriteLine($"Total cells in summary: {dgv.SummaryRow.SummaryCells.Count}");
        
        var columnNames = dgv.SummaryRow.SummaryCells.GetColumnNames();
        Debug.WriteLine($"Available column names:");
        foreach (var colName in columnNames)
        {
            Debug.WriteLine($"  - '{colName}'");
        }
        Debug.WriteLine("=============================");
        
        // Now use safe access
        txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Payment Amount", "0");
        TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetCellText("Gold Wt", "0");
        txttotamount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
        txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetCellText("GST", "0");
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error in dgv_SummaryCalculated: {ex.Message}");
        MessageBox.Show($"Summary Error: {ex.Message}");
    }
}
```

### Step 2: Run and Check Debug Output

1. **Build project**
2. **Run application**
3. **Trigger the grid population**
4. **Open Debug Output window** (Debug ? Windows ? Output)
5. **Look for the column names** that actually exist

### Step 3: Identify Mismatch

The debug output will show you the actual column names. For example:
```
=== Summary Columns Debug ===
Total cells in summary: 4
Available column names:
  - 'PaymentAmount'
  - 'GoldWeight'
  - 'TotalAmount'
  - 'TaxAmount'
=============================
```

If you see different names than what you're accessing, that's your problem!

---

## The Safe Fix

### Before (Crashes ?)
```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    txtpaidAmount.Text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;
    TxtTotalWt.Text = dgv.SummaryRow.SummaryCells["Gold Wt"].Text;
    txttotamount.Text = dgv.SummaryRow.SummaryCells["Amount"].Text;
    txt_totgst.Text = dgv.SummaryRow.SummaryCells["GST"].Text;
}
```

### After (Safe ?)
```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // ? SAFE: Use safe methods with defaults
        txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Payment Amount", "0");
        TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetCellText("Gold Wt", "0");
        txttotamount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
        txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetCellText("GST", "0");
    }
    catch (Exception ex)
    {
        // Log error but don't crash
        Debug.WriteLine($"Error updating summary display: {ex.Message}");
    }
}
```

---

## Step-by-Step Fix Process

### Step 1: Add Debug Version

Replace your event handler with this debug version:

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // ? DEBUG: Print all available column names
        Debug.WriteLine("=== AVAILABLE SUMMARY COLUMNS ===");
        Debug.WriteLine($"SummaryCells Count: {dgv.SummaryRow.SummaryCells.Count}");
        
        var availableColumns = dgv.SummaryRow.SummaryCells.GetColumnNames();
        foreach (var col in availableColumns)
        {
            var value = dgv.SummaryRow.SummaryCells.GetCellText(col, "");
            Debug.WriteLine($"  {col}: {value}");
        }
        Debug.WriteLine("==================================");
        
        // Try to set values with what exists
        // Replace these with actual column names from debug output
        txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Payment Amount", "0");
        TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetCellText("Gold Wt", "0");
        txttotamount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
        txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetCellText("GST", "0");
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"ERROR: {ex.Message}");
        Debug.WriteLine($"STACK: {ex.StackTrace}");
    }
}
```

### Step 2: Look at Debug Output

You'll see something like:
```
=== AVAILABLE SUMMARY COLUMNS ===
SummaryCells Count: 4
  PaymentAmount: 1000
  GoldWeight: 50
  TotalAmount: 50000
  TaxAmount: 2500
==================================
```

### Step 3: Match Your Column Names

If the debug output shows:
- `PaymentAmount` (not "Payment Amount")
- `GoldWeight` (not "Gold Wt")
- `TotalAmount` (instead of "Amount")
- `TaxAmount` (not "GST")

Then update your code accordingly!

### Step 4: Replace with Correct Names

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // Use correct column names from debug output
        txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetCellText("PaymentAmount", "0");
        TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetCellText("GoldWeight", "0");
        txttotamount.Text = dgv.SummaryRow.SummaryCells.GetCellText("TotalAmount", "0");
        txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetCellText("TaxAmount", "0");
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error: {ex.Message}");
    }
}
```

---

## Protecting Against Event Timing Issues

If the event fires before all cells are created, add this check:

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // ? SAFE: Check if enough cells exist before accessing
        if (dgv.SummaryRow.SummaryCells.Count == 0)
        {
            Debug.WriteLine("Warning: Summary cells not yet created");
            return;  // Don't try to update yet
        }
        
        // Now safe to access
        txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Payment Amount", "0");
        TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetCellText("Gold Wt", "0");
        txttotamount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
        txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetCellText("GST", "0");
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error updating summary: {ex.Message}");
    }
}
```

---

## Complete Solution Template

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // ? CHECK 1: Are cells created?
        if (dgv.SummaryRow?.SummaryCells == null || dgv.SummaryRow.SummaryCells.Count == 0)
        {
            Debug.WriteLine("Summary cells not ready");
            return;
        }
        
        // ? CHECK 2: Are columns defined?
        if (dgv.SummaryColumns == null || dgv.SummaryColumns.Length == 0)
        {
            Debug.WriteLine("Summary columns not defined");
            return;
        }
        
        // ? CHECK 3: Use safe methods to access
        txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Payment Amount", "0");
        TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetCellText("Gold Wt", "0");
        txttotamount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
        txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetCellText("GST", "0");
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error in dgv_SummaryCalculated: {ex.Message}");
        Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
        
        // Optionally show to user
        // MessageBox.Show($"Summary calculation error: {ex.Message}");
    }
}
```

---

## Key Points

? **Use safe methods** - `GetCellText()` instead of direct access  
? **Check cell count** - Ensure cells are created before accessing  
? **Match column names** - Use actual column names from summary  
? **Use try-catch** - Catch and log errors gracefully  
? **Debug first** - Use `GetColumnNames()` to see what exists  

---

## Action Items

1. **Add debug version** - Replace your event handler with debug code
2. **Run application** - Reproduce the error scenario
3. **Check debug output** - See what columns actually exist
4. **Update column names** - Use correct names from debug output
5. **Replace with safe version** - Use `GetCellText()` and `GetCellValue()`
6. **Test** - Verify no more errors

---

## Summary

**Problem:** Accessing summary cells that don't exist or aren't initialized  
**Cause:** Column name mismatch or event firing before cells created  
**Solution:** Use safe methods and debug to find actual column names  
**Prevention:** Always check cell count before accessing, use try-catch


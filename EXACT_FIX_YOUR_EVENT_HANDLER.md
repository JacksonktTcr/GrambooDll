# ? EXACT FIX FOR YOUR EVENT HANDLER

## Your Current Code (Crashes ?)

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

---

## Fixed Version 1: Safe Access Only (Recommended ?)

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // ? SAFE: Use GetCellText with defaults
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

## Fixed Version 2: Debug + Safe Access (Debugging ?)

Use this version first to identify the actual column names:

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // ? DEBUG: See what columns exist
        Debug.WriteLine("=== SUMMARY COLUMNS DEBUG ===");
        Debug.WriteLine($"Total cells: {dgv.SummaryRow.SummaryCells.Count}");
        
        var columns = dgv.SummaryRow.SummaryCells.GetColumnNames();
        Debug.WriteLine("Available columns:");
        foreach (var col in columns)
        {
            var value = dgv.SummaryRow.SummaryCells.GetCellText(col, "");
            Debug.WriteLine($"  '{col}' = '{value}'");
        }
        Debug.WriteLine("=============================");
        
        // ? SAFE: Use safe methods
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

---

## Fixed Version 3: Full Protection (Most Defensive ??)

```csharp
private void dgv_SummaryCalculated(object source, EventArgs e)
{
    try
    {
        // ? CHECK 1: Ensure grid row exists
        if (dgv.SummaryRow == null)
        {
            Debug.WriteLine("Summary row is null");
            return;
        }
        
        // ? CHECK 2: Ensure summary cells collection exists
        if (dgv.SummaryRow.SummaryCells == null || dgv.SummaryRow.SummaryCells.Count == 0)
        {
            Debug.WriteLine("No summary cells created yet");
            return;
        }
        
        // ? CHECK 3: Ensure summary columns defined
        if (dgv.SummaryColumns == null || dgv.SummaryColumns.Length == 0)
        {
            Debug.WriteLine("No summary columns defined");
            return;
        }
        
        // ? SAFE: Use safe methods to access
        txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Payment Amount", "0");
        TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetCellText("Gold Wt", "0");
        txttotamount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
        txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetCellText("GST", "0");
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error in dgv_SummaryCalculated: {ex.Message}");
        Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
    }
}
```

---

## Step-by-Step Implementation

### Step 1: Choose Version

- **Simple & Safe?** ? Use Version 1
- **Want to debug?** ? Use Version 2
- **Want most protection?** ? Use Version 3

### Step 2: Replace Your Event Handler

Find your current `dgv_SummaryCalculated` method and replace it entirely with your chosen version.

### Step 3: If Using Version 2 (Debug Version)

1. Build project
2. Run application
3. Trigger the grid (enter text in TxtJoinId, etc.)
4. Open Debug Output window: **Debug ? Windows ? Output**
5. Look for the debug output showing available columns
6. Write down the **exact column names**

Example output:
```
=== SUMMARY COLUMNS DEBUG ===
Total cells: 4
Available columns:
  'PaymentAmount' = '5000'
  'GoldWt' = '100'
  'Amount' = '500000'
  'GST' = '25000'
=============================
```

### Step 4: Update Column Names (If Different)

If the debug output shows different names, update them in the code.

For example, if it shows `'PaymentAmount'` (no space), change:
```csharp
// OLD (with space)
GetCellText("Payment Amount", "0")

// NEW (matching debug output)
GetCellText("PaymentAmount", "0")
```

### Step 5: Switch to Production Version

Once you've verified column names, replace with Version 1 (Simple) for production.

---

## What Each Version Does

### Version 1: Simple & Safe
- Minimal code
- Safe access with defaults
- Good for production
- Quiet (no debug output)

### Version 2: Debug + Safe
- Shows all available columns
- Helps identify mismatches
- Good for troubleshooting
- Verbose debug output

### Version 3: Full Protection
- Multiple safety checks
- Protects against null references
- Best for critical code
- Most defensive

---

## Recommended Path

### For Quick Fix
1. Use **Version 1** 
2. Replace your event handler
3. Build and test
4. Done! ?

### For Debugging Issues
1. Use **Version 2**
2. Run application
3. Check debug output for column names
4. Update column names if different
5. Switch to **Version 1** for production
6. Done! ?

### For Production Code
1. Use **Version 3**
2. Extra safety checks
3. Better error handling
4. Production ready ?

---

## Common Column Name Patterns

Your column names might be:
- With spaces: `"Payment Amount"`, `"Gold Wt"`
- Without spaces: `"PaymentAmount"`, `"GoldWt"`
- Database names: `"col_PaymentAmount"`, `"col_GoldWt"`
- Shortened: `"Amt"`, `"Wt"`

**Use Version 2 to find the exact names!**

---

## Testing Your Fix

After applying the fix:

1. **Build:** Build ? Build Solution
2. **Run:** Press F5 or click Run
3. **Trigger error scenario:**
   - Open the form
   - Enter text in TxtJoinId field
   - Click outside the field
   - Grid should populate
4. **Check result:**
   - If summary shows values ? ? Fixed!
   - If still getting error ? Check column names in debug output
   - If no debug output ? Column names might not match

---

## Still Getting Errors?

If you still get errors after applying the fix:

1. **Use Version 2** (debug version) to see actual column names
2. **Look at debug output** for exact names
3. **Update column names** in Version 1 to match
4. **Check summary columns** - Make sure they're defined: `grid.SummaryColumns = new[] { "Amount", "GST", ... }`
5. **Verify column names in grid** match summary columns

---

## Complete Working Example

```csharp
// Your form class
public partial class FrmSchemePaymentEntry : Form
{
    private GrbDataGridView dgv;
    private TextBox txtpaidAmount;
    private TextBox TxtTotalWt;
    private TextBox txttotamount;
    private TextBox txt_totgst;
    
    private void PopulateGrid()
    {
        // ... load your grid data ...
        
        // Make sure summary columns are set!
        dgv.SummaryColumns = new[] { "Payment Amount", "Gold Wt", "Amount", "GST" };
    }
    
    // ? FIXED EVENT HANDLER
    private void dgv_SummaryCalculated(object source, EventArgs e)
    {
        try
        {
            txtpaidAmount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Payment Amount", "0");
            TxtTotalWt.Text = dgv.SummaryRow.SummaryCells.GetCellText("Gold Wt", "0");
            txttotamount.Text = dgv.SummaryRow.SummaryCells.GetCellText("Amount", "0");
            txt_totgst.Text = dgv.SummaryRow.SummaryCells.GetCellText("GST", "0");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

---

## Summary

| Version | Use When | Complexity |
|:---|:---|:---|
| Version 1 | Quick fix | Simple |
| Version 2 | Need to debug | Medium |
| Version 3 | Production | Complex |

**Recommendation:** Start with Version 2 to identify column names, then use Version 1 for production.


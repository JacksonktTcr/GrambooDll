using Gramboo.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public partial class FilterForm : Form
{
    public List<string> SelectedValues { get; private set; } = new List<string>();
    public string CustomTextFilter { get; private set; } = string.Empty;

    private string columnName;
    private List<string> distinctValues; 
    public FilterForm(IEnumerable<string> values)
    {
        distinctValues = values.Distinct().OrderBy(v => v).ToList();



        BuildUI(); // your custom initializer
    }


    private void BuildUI()
    {
        this.Text = $"Filter - {columnName}";
        this.Width = 300;
        this.Height = 400;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.Manual;

        TabControl tabs = new TabControl() { Dock = DockStyle.Fill };

        // Tab 1: Value filter
        var valueTab = new TabPage("Values");
        var panel1 = new Panel() { Dock = DockStyle.Fill };
        var chkList = new CheckedListBox() { Dock = DockStyle.Fill };
        // Get sorted values
        var sortedValues = SortByDataType(distinctValues);

        // (All) at top
        chkList.Items.Add("(All)", true);

        // Add values — sorted, blank last or first depending on your SortByDataType
        foreach (var val in sortedValues)
        {
            chkList.Items.Add(string.IsNullOrWhiteSpace(val) ? "<Empty>" : val, false);
        }

        chkList.ItemCheck += (s, e) =>
        {
            if (e.Index == 0) // (All) checkbox clicked
            {
                bool checkAll = e.NewValue == CheckState.Checked;
                for (int i = 1; i < chkList.Items.Count; i++)
                    chkList.SetItemChecked(i, checkAll);
            };
        };

            var txtSearch = new GrbTextBox() { Dock = DockStyle.Top, CueBanner = "Search..." };
        txtSearch.TextChanged += (s, e) =>
        {
            string search = txtSearch.Text.ToLower();
            chkList.Items.Clear();
            chkList.Items.Add("(All)", true);
            foreach (var val in distinctValues.Where(v => v.ToLower().Contains(search)))
                chkList.Items.Add(val, false);
        };

        panel1.Controls.Add(chkList);
        panel1.Controls.Add(txtSearch);
        valueTab.Controls.Add(panel1);

        // Tab 2: Text Filters
        var textTab = new TabPage("Text Filters");
        var panel2 = new Panel() { Dock = DockStyle.Fill };

        var combo1 = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
        combo1.Items.AddRange(new[] { "Contains", "Equals", "Starts With", "Ends With" });
        combo1.SelectedIndex = 0;

        var txt1 = new TextBox();

        var combo2 = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
        combo2.Items.AddRange(new[] { "Contains", "Equals", "Starts With", "Ends With" });
        combo2.SelectedIndex = 0;

        var txt2 = new TextBox();

        var radAnd = new RadioButton() { Text = "And", Checked = true };
        var radOr = new RadioButton() { Text = "Or" };

        var btnApply = new Button() { Text = "Apply", Width = 80, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
        btnApply.Click += (s, e) =>
        {
            string f1 = GetTextFilter(combo1.Text, txt1.Text);
            string f2 = GetTextFilter(combo2.Text, txt2.Text);
            if (!string.IsNullOrEmpty(f1) && !string.IsNullOrEmpty(f2))
                CustomTextFilter = $"{f1} {(radAnd.Checked ? "AND" : "OR")} {f2}";
            else if (!string.IsNullOrEmpty(f1))
                CustomTextFilter = f1;
            else if (!string.IsNullOrEmpty(f2))
                CustomTextFilter = f2;
            else
                CustomTextFilter = "";

            this.DialogResult = DialogResult.OK;
        };

        var btnClear = new Button() { Text = "Clear Filter", Width = 100 };
        btnClear.Click += (s, e) =>
        {
            txt1.Text = txt2.Text = string.Empty;
            CustomTextFilter = "";
        };

        var btnClose = new Button() { Text = "Close", Width = 80 };
        btnClose.Click += (s, e) => this.Close();

        var layout = new TableLayoutPanel() { Dock = DockStyle.Fill, RowCount = 6, ColumnCount = 2 };
        layout.Controls.Add(new Label() { Text = "First" }, 0, 0);
        layout.Controls.Add(combo1, 0, 1);
        layout.Controls.Add(txt1, 1, 1);
        layout.Controls.Add(new Label() { Text = "Second" }, 0, 2);
        layout.Controls.Add(combo2, 0, 3);
        layout.Controls.Add(txt2, 1, 3);
        layout.Controls.Add(radAnd, 0, 4);
        layout.Controls.Add(radOr, 1, 4);
        layout.Controls.Add(btnApply, 1, 5);
        layout.Controls.Add(btnClear, 0, 5);

        panel2.Controls.Add(layout);
        textTab.Controls.Add(panel2);

        tabs.TabPages.Add(valueTab);
        tabs.TabPages.Add(textTab);

        var footerPanel = new FlowLayoutPanel()
        {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Bottom,
            Height = 40
        };

        var btnOK = new Button() { Text = "Apply", DialogResult = DialogResult.OK };
        var btnCancel = new Button() { Text = "Close", DialogResult = DialogResult.Cancel };
        var btnClearAll = new Button() { Text = "Clear Filter" };

        btnClearAll.Click += (s, e) =>
        {
            SelectedValues.Clear();
            CustomTextFilter = "";
            this.DialogResult = DialogResult.OK;
        };

        btnOK.Click += (s, e) =>
        {
            foreach (var item in chkList.CheckedItems)
            {
                if (item.ToString() != "(All)")
                    SelectedValues.Add(item.ToString());
            }
            this.DialogResult = DialogResult.OK;
        };

        footerPanel.Controls.Add(btnOK);
        footerPanel.Controls.Add(btnClearAll);
        footerPanel.Controls.Add(btnCancel);

        this.Controls.Add(tabs);
        this.Controls.Add(footerPanel);
    }
    private List<string> SortByDataType(IEnumerable<string> values)
    {
        var blanks = values.Where(v => string.IsNullOrWhiteSpace(v)).ToList();
        var nonBlanks = values.Where(v => !string.IsNullOrWhiteSpace(v)).ToList();

        // Try detect data type
        bool allInt = nonBlanks.All(v => int.TryParse(v, out _));
        bool allDecimal = nonBlanks.All(v => decimal.TryParse(v, out _));
        bool allDate = nonBlanks.All(v => DateTime.TryParse(v, out _));

        List<string> sorted;

        try
        {
            if (allInt)
                sorted = nonBlanks.Select(int.Parse).OrderBy(x => x).Select(x => x.ToString()).ToList();
            else if (allDecimal)
                sorted = nonBlanks.Select(decimal.Parse).OrderBy(x => x).Select(x => x.ToString("0.##")).ToList();
            else if (allDate)
                sorted = nonBlanks.Select(DateTime.Parse).OrderBy(x => x).Select(x => x.ToShortDateString()).ToList();
            else
                sorted = nonBlanks.OrderBy(x => x).ToList(); // alphabetical
        }
        catch
        {
            sorted = nonBlanks.OrderBy(x => x).ToList();
        }

        // Append blanks at the end (or insert at top if you prefer)
        sorted.AddRange(blanks);
        return sorted;
    }

    private string GetTextFilter(string condition, string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return "";

        value = value.Replace("'", "''"); // SQL escape
        switch (condition)
        {
            case "Contains": return $"LIKE '%{value}%'";
            case "Equals": return $"= '{value}'";
            case "Starts With": return $"LIKE '{value}%'";
            case "Ends With": return $"LIKE '%{value}'";
            default: return "";
        }
    }
}

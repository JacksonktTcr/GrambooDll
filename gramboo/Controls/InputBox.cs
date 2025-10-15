using System;
using System.Drawing;
using System.Windows.Forms;

public class InputBox
{
    public static DialogResult Show(string title, string promptText, ref string value1, bool Between = false, bool IsDate = false)
    {
        string val2 = "";

        return Show(title, promptText, ref value1, ref val2, Between, IsDate, null);
    }

    public static DialogResult Show(string title, string promptText, ref string value1, ref string value2, bool Between = false, bool IsDate = false)
    {
        return Show(title, promptText, ref value1, ref value2, Between, IsDate, null);
    }

    public static DialogResult Show(string title, string promptText, ref string value1, ref string value2, bool Between, bool IsDate,
                                    InputBoxValidation validation)
    {

        Form form = new Form();
        Label label1 = new Label();
        Label label2 = new Label();
        TextBox TextBox1 = new TextBox();
        TextBox TextBox2 = new TextBox();
        Button buttonOk = new Button();
        Button buttonCancel = new Button();
        DateTimePicker cal1 = new DateTimePicker();
        DateTimePicker cal2 = new DateTimePicker();

        form.Text = title;        
        TextBox1.Text = value1;
        TextBox2.Text = value2;

        buttonOk.Text = "OK";
        buttonCancel.Text = "Cancel";
        buttonOk.DialogResult = DialogResult.OK;
        buttonCancel.DialogResult = DialogResult.Cancel;

        if (!Between)
        {
            label1.Text = promptText;

            label1.SetBounds(9, 20, 372, 13);
            if (!IsDate)
            {

                TextBox1.SetBounds(12, 36, 372, 20);
            }
            else
            {
                TextBox1.SetBounds(12, 36, 346, 20);
                cal1.SetBounds(358, 36, 18, 20);
            }

            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);
        }
        else
        {
            if (!IsDate)
            {
                label1.Text = "Smallest";
                label2.Text = "Largest";

                label1.SetBounds(9, 20, 180, 13);
                label2.SetBounds(9, 57, 180, 13);

                TextBox1.SetBounds(12, 36, 180, 20);
                TextBox2.SetBounds(12, 72, 180, 20);
            }
            else
            {
                label1.Text = "Oldest";
                label2.Text = "Newest";

                label1.SetBounds(9, 20, 173, 13);
                label2.SetBounds(9, 57, 173, 13);


                TextBox1.SetBounds(12, 36, 170, 20);
                TextBox2.SetBounds(12, 72, 170, 20);

                cal1.SetBounds(181, 36, 18, 20);
                cal2.SetBounds(181, 72, 18, 20);


            }
            buttonOk.SetBounds(40, 100, 75, 23);
            buttonCancel.SetBounds(118, 100, 75, 23);
        }


        label1.AutoSize = true;
        label2.AutoSize = true;
        TextBox1.Anchor = TextBox1.Anchor | AnchorStyles.Right;
        TextBox2.Anchor = TextBox2.Anchor | AnchorStyles.Right;
        cal1.Anchor =   AnchorStyles.Right;
        cal2.Anchor =   AnchorStyles.Right;
        buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;


        if (!Between)
        {
            form.ClientSize = new Size(396, 107);
            if (!IsDate)
            {
                form.Controls.AddRange(new Control[] { label1, label2, TextBox1, buttonOk, buttonCancel });
            }
            else
            {
                form.Controls.AddRange(new Control[] { label1, TextBox1, cal1, buttonOk, buttonCancel });
            }
        }
        else
        {
            form.ClientSize = new Size(200, 130);

            if (!IsDate)
            {
                form.Controls.AddRange(new Control[] { label1, label2, TextBox1, TextBox2, buttonOk, buttonCancel });
            }
            else
            {
                form.Controls.AddRange(new Control[] { label1, label2, TextBox1, TextBox2,cal1,cal2, buttonOk, buttonCancel });
            }
            form.ClientSize = new Size(Math.Max(300, label1.Right + 10), form.ClientSize.Height);
        }

        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimizeBox = false;
        form.MaximizeBox = false;
        form.AcceptButton = buttonOk;
        form.CancelButton = buttonCancel;
        if (IsDate)
        {

            cal1.ValueChanged += delegate(object sender, EventArgs e)
            {
                TextBox1.Text = cal1.Value.Date.ToShortDateString();
            };

            cal2.ValueChanged += delegate(object sender, EventArgs e)
            {
                TextBox2.Text = cal2.Value.Date.ToShortDateString();
            };
        }
        if (validation != null)
        {
            form.FormClosing += delegate(object sender, FormClosingEventArgs e)
            {
                if (form.DialogResult == DialogResult.OK)
                {
                    string errorText = validation(TextBox1.Text);
                    if (e.Cancel = (errorText != ""))
                    {
                        MessageBox.Show(form, errorText, "Validation Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextBox1.Focus();
                    }
                     errorText = validation(TextBox2.Text);
                    if (e.Cancel = (errorText != ""))
                    {
                        MessageBox.Show(form, errorText, "Validation Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextBox2.Focus();
                    }
                }
            };

          
        }
        DialogResult dialogResult = form.ShowDialog();
        value1 = TextBox1.Text;
        value2 = TextBox2.Text;
        return dialogResult;
    }
}
public delegate string InputBoxValidation(string errorMessage);
namespace Gramboo.Controls
{
    partial class DataGridPrintOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOk = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rTxtTitleText = new System.Windows.Forms.RichTextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkFitToWidth = new System.Windows.Forms.CheckBox();
            this.chkHLine = new System.Windows.Forms.CheckBox();
            this.chkVLine = new System.Windows.Forms.CheckBox();
            this.chkGridLine = new System.Windows.Forms.CheckBox();
            this.lnkTitleFont = new System.Windows.Forms.LinkLabel();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.lblLineColor = new System.Windows.Forms.Label();
            this.lblLineColorText = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.chkIstCln = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(263, 284);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(348, 284);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(438, 284);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rTxtTitleText
            // 
            this.rTxtTitleText.Location = new System.Drawing.Point(214, 23);
            this.rTxtTitleText.Name = "rTxtTitleText";
            this.rTxtTitleText.Size = new System.Drawing.Size(299, 96);
            this.rTxtTitleText.TabIndex = 4;
            this.rTxtTitleText.Text = "";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(214, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Title";
            // 
            // chkFitToWidth
            // 
            this.chkFitToWidth.AutoSize = true;
            this.chkFitToWidth.Location = new System.Drawing.Point(214, 135);
            this.chkFitToWidth.Name = "chkFitToWidth";
            this.chkFitToWidth.Size = new System.Drawing.Size(84, 17);
            this.chkFitToWidth.TabIndex = 6;
            this.chkFitToWidth.Text = "Fit To Width";
            this.chkFitToWidth.UseVisualStyleBackColor = true;
            // 
            // chkHLine
            // 
            this.chkHLine.AutoSize = true;
            this.chkHLine.Location = new System.Drawing.Point(214, 158);
            this.chkHLine.Name = "chkHLine";
            this.chkHLine.Size = new System.Drawing.Size(96, 17);
            this.chkHLine.TabIndex = 7;
            this.chkHLine.Text = "Horizontal Line";
            this.chkHLine.UseVisualStyleBackColor = true;
            // 
            // chkVLine
            // 
            this.chkVLine.AutoSize = true;
            this.chkVLine.Location = new System.Drawing.Point(214, 181);
            this.chkVLine.Name = "chkVLine";
            this.chkVLine.Size = new System.Drawing.Size(84, 17);
            this.chkVLine.TabIndex = 8;
            this.chkVLine.Text = "Vertical Line";
            this.chkVLine.UseVisualStyleBackColor = true;
            // 
            // chkGridLine
            // 
            this.chkGridLine.AutoSize = true;
            this.chkGridLine.Location = new System.Drawing.Point(214, 204);
            this.chkGridLine.Name = "chkGridLine";
            this.chkGridLine.Size = new System.Drawing.Size(68, 17);
            this.chkGridLine.TabIndex = 9;
            this.chkGridLine.Text = "Grid Line";
            this.chkGridLine.UseVisualStyleBackColor = true;
            // 
            // lnkTitleFont
            // 
            this.lnkTitleFont.AutoSize = true;
            this.lnkTitleFont.Location = new System.Drawing.Point(455, 7);
            this.lnkTitleFont.Name = "lnkTitleFont";
            this.lnkTitleFont.Size = new System.Drawing.Size(51, 13);
            this.lnkTitleFont.TabIndex = 10;
            this.lnkTitleFont.TabStop = true;
            this.lnkTitleFont.Text = "Title Font";
            this.lnkTitleFont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTitleFont_LinkClicked);
            // 
            // fontDialog1
            // 
            this.fontDialog1.Color = System.Drawing.SystemColors.ControlText;
            // 
            // lblLineColor
            // 
            this.lblLineColor.BackColor = System.Drawing.Color.Black;
            this.lblLineColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLineColor.Location = new System.Drawing.Point(360, 136);
            this.lblLineColor.Name = "lblLineColor";
            this.lblLineColor.Size = new System.Drawing.Size(20, 18);
            this.lblLineColor.TabIndex = 11;
            this.lblLineColor.Click += new System.EventHandler(this.lblLineColor_Click);
            // 
            // lblLineColorText
            // 
            this.lblLineColorText.AutoSize = true;
            this.lblLineColorText.Location = new System.Drawing.Point(387, 139);
            this.lblLineColorText.Name = "lblLineColorText";
            this.lblLineColorText.Size = new System.Drawing.Size(54, 13);
            this.lblLineColorText.TabIndex = 12;
            this.lblLineColorText.Text = "Line Color";
            // 
            // chkIstCln
            // 
            this.chkIstCln.CheckOnClick = true;
            this.chkIstCln.FormattingEnabled = true;
            this.chkIstCln.Location = new System.Drawing.Point(12, 23);
            this.chkIstCln.Name = "chkIstCln";
            this.chkIstCln.Size = new System.Drawing.Size(180, 274);
            this.chkIstCln.TabIndex = 13;
            // 
            // DataGridPrintOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 319);
            this.Controls.Add(this.chkIstCln);
            this.Controls.Add(this.lblLineColorText);
            this.Controls.Add(this.lblLineColor);
            this.Controls.Add(this.lnkTitleFont);
            this.Controls.Add(this.chkGridLine);
            this.Controls.Add(this.chkVLine);
            this.Controls.Add(this.chkHLine);
            this.Controls.Add(this.chkFitToWidth);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.rTxtTitleText);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOk);
            this.Name = "DataGridPrintOptions";
            this.Text = "Print Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox rTxtTitleText;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkFitToWidth;
        private System.Windows.Forms.CheckBox chkHLine;
        private System.Windows.Forms.CheckBox chkVLine;
        private System.Windows.Forms.CheckBox chkGridLine;
        private System.Windows.Forms.LinkLabel lnkTitleFont;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label lblLineColor;
        private System.Windows.Forms.Label lblLineColorText;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.CheckedListBox chkIstCln;
    }
}
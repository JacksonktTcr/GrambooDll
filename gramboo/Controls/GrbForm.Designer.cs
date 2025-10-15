using System.Drawing;
namespace Gramboo.Controls
{
    partial class GrbForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkSaveQuery = new Gramboo.Controls.GrbCheckBox();
            this.chkAuditTrial = new Gramboo.Controls.GrbCheckBox();
            this.txtCounterId = new Gramboo.Controls.GrbTextBox();
            this.txtBranchID = new Gramboo.Controls.GrbTextBox();
            this.txtModifiedDate = new Gramboo.Controls.GrbTextBox();
            this.txtcreatedDate = new Gramboo.Controls.GrbTextBox();
            this.txtcompId = new Gramboo.Controls.GrbTextBox();
            this.txtModuserID = new Gramboo.Controls.GrbTextBox();
            this.txtCrUserId = new Gramboo.Controls.GrbTextBox();
            this.txtUserName = new Gramboo.Controls.GrbTextBox();
            this.txtDbId = new Gramboo.Controls.GrbTextBox();
            this.SuspendLayout();
            // 
            // chkSaveQuery
            // 
            this.chkSaveQuery.AcceptBlankValue = false;
            this.chkSaveQuery.BindingProperty = "Value";
            this.chkSaveQuery.Checked = true;
            this.chkSaveQuery.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveQuery.Location = new System.Drawing.Point(319, 396);
            this.chkSaveQuery.Name = "chkSaveQuery";
            this.chkSaveQuery.Size = new System.Drawing.Size(104, 24);
            this.chkSaveQuery.TabIndex = 18;
            this.chkSaveQuery.TableName = null;
            this.chkSaveQuery.Text = "SaveQuery";
            this.chkSaveQuery.UseVisualStyleBackColor = true;
            this.chkSaveQuery.Visible = false;
            // 
            // chkAuditTrial
            // 
            this.chkAuditTrial.AcceptBlankValue = false;
            this.chkAuditTrial.BindingProperty = "Value";
            this.chkAuditTrial.Checked = true;
            this.chkAuditTrial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuditTrial.Location = new System.Drawing.Point(536, 393);
            this.chkAuditTrial.Name = "chkAuditTrial";
            this.chkAuditTrial.Size = new System.Drawing.Size(104, 24);
            this.chkAuditTrial.TabIndex = 17;
            this.chkAuditTrial.TableName = null;
            this.chkAuditTrial.Text = "grbCheckBox1";
            this.chkAuditTrial.UseVisualStyleBackColor = true;
            this.chkAuditTrial.Visible = false;
            // 
            // txtCounterId
            // 
            this.txtCounterId.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtCounterId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCounterId.DataField = "Counter_id";
            this.txtCounterId.Location = new System.Drawing.Point(921, 403);
            this.txtCounterId.Margin = new System.Windows.Forms.Padding(2);
            this.txtCounterId.Name = "txtCounterId";
            this.txtCounterId.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtCounterId.Size = new System.Drawing.Size(10, 20);
            this.txtCounterId.TabIndex = 9;
            this.txtCounterId.Visible = false;
            this.txtCounterId.WordWrap = false;
            // 
            // txtBranchID
            // 
            this.txtBranchID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtBranchID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBranchID.DataField = "Branch_id";
            this.txtBranchID.Location = new System.Drawing.Point(924, 403);
            this.txtBranchID.Margin = new System.Windows.Forms.Padding(2);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtBranchID.Size = new System.Drawing.Size(10, 20);
            this.txtBranchID.TabIndex = 10;
            this.txtBranchID.Visible = false;
            this.txtBranchID.WordWrap = false;
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Alias = "[Last Modified Date]";
            this.txtModifiedDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtModifiedDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtModifiedDate.DataField = "Last_modified_date";
            this.txtModifiedDate.Location = new System.Drawing.Point(898, 377);
            this.txtModifiedDate.Margin = new System.Windows.Forms.Padding(2);
            this.txtModifiedDate.Name = "txtModifiedDate";
            this.txtModifiedDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtModifiedDate.Size = new System.Drawing.Size(10, 20);
            this.txtModifiedDate.TabIndex = 11;
            this.txtModifiedDate.Visible = false;
            this.txtModifiedDate.WordWrap = false;
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Alias = "[Created Date]";
            this.txtcreatedDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtcreatedDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcreatedDate.DataField = "Created_date";
            this.txtcreatedDate.Location = new System.Drawing.Point(921, 375);
            this.txtcreatedDate.Margin = new System.Windows.Forms.Padding(2);
            this.txtcreatedDate.Name = "txtcreatedDate";
            this.txtcreatedDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtcreatedDate.Size = new System.Drawing.Size(10, 20);
            this.txtcreatedDate.TabIndex = 12;
            this.txtcreatedDate.Visible = false;
            this.txtcreatedDate.WordWrap = false;
            // 
            // txtcompId
            // 
            this.txtcompId.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtcompId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcompId.DataField = "Company_id";
            this.txtcompId.Location = new System.Drawing.Point(935, 377);
            this.txtcompId.Margin = new System.Windows.Forms.Padding(2);
            this.txtcompId.Name = "txtcompId";
            this.txtcompId.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtcompId.Size = new System.Drawing.Size(10, 20);
            this.txtcompId.TabIndex = 13;
            this.txtcompId.Visible = false;
            this.txtcompId.WordWrap = false;
            // 
            // txtModuserID
            // 
            this.txtModuserID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtModuserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtModuserID.DataField = "Last_modified_by";
            this.txtModuserID.Location = new System.Drawing.Point(935, 351);
            this.txtModuserID.Margin = new System.Windows.Forms.Padding(2);
            this.txtModuserID.Name = "txtModuserID";
            this.txtModuserID.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtModuserID.Size = new System.Drawing.Size(10, 20);
            this.txtModuserID.TabIndex = 14;
            this.txtModuserID.Visible = false;
            this.txtModuserID.WordWrap = false;
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtCrUserId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCrUserId.DataField = "Created_by";
            this.txtCrUserId.Location = new System.Drawing.Point(898, 351);
            this.txtCrUserId.Margin = new System.Windows.Forms.Padding(2);
            this.txtCrUserId.Name = "txtCrUserId";
            this.txtCrUserId.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtCrUserId.Size = new System.Drawing.Size(10, 20);
            this.txtCrUserId.TabIndex = 15;
            this.txtCrUserId.Visible = false;
            this.txtCrUserId.WordWrap = false;
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Location = new System.Drawing.Point(921, 351);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtUserName.Size = new System.Drawing.Size(10, 20);
            this.txtUserName.TabIndex = 16;
            this.txtUserName.Visible = false;
            this.txtUserName.WordWrap = false;
            // 
            // txtDbId
            // 
            this.txtDbId.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtDbId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDbId.DataField = "DbId";
            this.txtDbId.Location = new System.Drawing.Point(502, 304);
            this.txtDbId.Margin = new System.Windows.Forms.Padding(2);
            this.txtDbId.Name = "txtDbId";
            this.txtDbId.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtDbId.Size = new System.Drawing.Size(10, 20);
            this.txtDbId.TabIndex = 19;
            this.txtDbId.Visible = false;
            this.txtDbId.WordWrap = false;
            // 
            // GrbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(235)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1014, 628);
            this.Controls.Add(this.txtDbId);
            this.Controls.Add(this.chkSaveQuery);
            this.Controls.Add(this.chkAuditTrial);
            this.Controls.Add(this.txtCounterId);
            this.Controls.Add(this.txtBranchID);
            this.Controls.Add(this.txtModifiedDate);
            this.Controls.Add(this.txtcreatedDate);
            this.Controls.Add(this.txtcompId);
            this.Controls.Add(this.txtModuserID);
            this.Controls.Add(this.txtCrUserId);
            this.Controls.Add(this.txtUserName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GrbForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected GrbTextBox txtModifiedDate;
        protected GrbTextBox txtcreatedDate;
        protected GrbTextBox txtcompId;
        protected GrbTextBox txtModuserID;
        protected GrbTextBox txtCrUserId;
        protected GrbTextBox txtUserName;
        protected GrbTextBox txtCounterId;
        protected GrbTextBox txtBranchID;
        private GrbCheckBox chkAuditTrial;
        private GrbCheckBox chkSaveQuery;
        protected GrbTextBox txtDbId;
    }
}

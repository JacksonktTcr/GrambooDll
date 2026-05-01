using System;

namespace Gramboo.Controls
{
    partial class GrbDataGridView
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
            if (_isDisposing || IsDisposed)
                return;

            _isDisposing = true;

            if (disposing)
            {
                try
                {
                    this.SuspendLayout();

                    Resize -= DataGridControlSum_Resize;
                    RowHeadersWidthChanged -= DataGridControlSum_Resize;
                    ColumnAdded -= DataGridControlSum_ColumnAdded;
                    ColumnRemoved -= DataGridControlSum_ColumnRemoved;
                    hScrollBar.Scroll -= hScrollBar_Scroll;
                    hScrollBar.VisibleChanged -= hScrollBar_VisibleChanged;
                    this.MouseMove -= this_MouseMove;

                    if (summaryControl != null)
                    {
                        summaryControl.SummaryVisibilityChanged -= summaryControl_VisibilityChanged;

                        if (summaryControl.Parent != null)
                            summaryControl.Parent.Controls.Remove(summaryControl);

                        summaryControl.Dispose();
                        summaryControl = null;
                    }

                    if (hScrollBar != null)
                    {
                        if (hScrollBar.Parent != null)
                            hScrollBar.Parent.Controls.Remove(hScrollBar);

                        hScrollBar.Dispose();
                        hScrollBar = null;
                    }

                    if (spacePanel != null)
                    {
                        if (spacePanel.Parent != null)
                            spacePanel.Parent.Controls.Remove(spacePanel);

                        spacePanel.Dispose();
                        spacePanel = null;
                    }

                    if (panel != null)
                    {
                        if (panel.Parent != null)
                            panel.Parent.Controls.Remove(panel);

                        panel.Dispose();
                        panel = null;
                    }

                    if (refBox != null)
                    {
                        refBox.Dispose();
                        refBox = null;
                    }

                    // make sure no more binding/layout happens
                    this.DataSource = null;
                }
                catch
                {
                    // ignore errors in our cleanup
                }
            }

            try
            {
                base.Dispose(disposing);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Known DataGridView disposal bug – ignore
                // (optional: log once somewhere if you want)
            }
            catch
            {
                // If you want to be ultra-strict, you can rethrow here,
                // but I'd probably ignore all dispose-time errors for this control.
            }
        }


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }
}

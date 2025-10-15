using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gramboo.Controls;

namespace REGAL.Forms.CNRL
{
    public partial class AddNewMasterData : UserControl
    {
        #region Private Variables

       // GrbForm masterForm=new GrbForm();

        #endregion Private Variables

        #region Properties

        [Browsable (false)]
        public Control ParentControl { get; set; }

        [Browsable(false)]
        public GrbForm MasterForm { get; set; }
        
        [Browsable(false)]
        public new GrbForm ParentForm { get; set; }

        #endregion Properties

        #region Constructors

        public AddNewMasterData()
        {
            InitializeComponent();
            btnNew.Location = new Point(0, 0);
            btnNew.Height = this.Height;
            btnNew.Width = this.Width;
            btnNew.Click += btnNew_Click;

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            btnNew.Height = this.Height;
            btnNew.Width = this.Width;

        }

       

        #endregion Constructors

        #region Events

        void btnNew_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var frm = Application.OpenForms.OfType<frmmenu>();

            //    frmMain main = GetMainForm();
            //    if (main == null)
            //        return;
            //    else
            //        main.AfterSave += main_AfterSave;

            //    if (frm.Count() > 0)
            //    {
            //        frmmenu mnuFrm = frm.First();
            //        if(this.MasterForm!=null && this.MasterForm.IsDisposed)
            //            MasterForm = Activator.CreateInstance(MasterForm.GetType()) as GrbForm;
            //        mnuFrm.ShowForm(this.MasterForm, this.MasterForm.Text);
            //    }

                
            //}
            //catch (Exception ex)
            //{
            //    Gramboo.General.ShowMessage(ex.Message);
            //}
        }

        void main_AfterSave(object sender, EventArgs e)
        {
            try
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.RefreshData();
                    SetParentControlValue(this.MasterForm.TableName.IdTextBox.Text);
                }
            }
            catch (Exception)
            {
               //Gamboo.General.ShowMessage(ex.Message);
            }
        }

        #endregion Events

        #region Methods

        private Form GetMainForm()
        {
            var frm = Application.OpenForms.OfType<Form>();

            if (frm.Count() > 0)
            {
                return frm.First();
            }
            return null;
        }

        public void SetParentControlValue(object value)
        {
            if (this.ParentControl != null)
            {
                if (ParentControl is GrbComboBox)
                    (ParentControl as GrbComboBox).SelectedValue = value;
                else
                    ParentControl.Text = Convert.ToString(value);
            }
        }

        #endregion Methods
    }
}

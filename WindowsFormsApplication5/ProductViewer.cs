using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gramboo;
using System.IO;
using Gramboo.Controls.PopupControl;

namespace REGAL.Classes
{
    public partial class ProductViewer : UserControl
    {
        Gramboo.Controls.GrbComboBox cmbUCCode;

        private  ProductViewer()
        {
            InitializeComponent();
        }
        Int64 _ProdCodeId;
        DataController DBConn=new DataController();
        public ProductViewer(Int64 prodcodeid,DataController dc)
        {
            InitializeComponent();
            ProdCodeId = prodcodeid;
            DBConn = dc;
        }

        public Int64 ProdCodeId 
        {
            get
            {
                return _ProdCodeId;
            }
            set
            {
                _ProdCodeId = value;
                if (value != null)
                {
                    GetItemDetails();
                }
               
            }
        }

        private void GetItemDetails()
        {

            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * From STK.VProdcodeMaster WHERE ProdCodeId=" + ProdCodeId.ToString())).Tables[0])
            {
                 if (dt.Rows.Count > 0)
                {
                    lblItem.Text = dt.Rows[0]["ItemName"].ToString();
                    lblJobNO.Text = dt.Rows[0]["ProdCode"].ToString();
                    if (dt.Rows[0]["UcCode"] != DBNull.Value)
                    {
                        lnkuccode.Text = dt.Rows[0]["UcCode"].ToString();
                        
                    }
                    if(  dt.Rows[0]["ProductImage"] != DBNull.Value  )
                        grbPictureBox1.Image = byteArrayToImage((byte[])dt.Rows[0]["ProductImage"]);
                }
            }

        }
        private  byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public bool Save()
        {
            try
            {
                DBConn.ExecuteSqlTransaction(new System.Data.SqlClient.SqlCommand("Update STK.ProductCodeMaster Set ImageId=" + cmbUCCode.SelectedValue + " Where ProdCodeid=" + ProdCodeId), "save");

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void lnkuccode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbUCCode = new Gramboo.Controls.GrbComboBox();
            this.Controls.Add(cmbUCCode);
            cmbUCCode.SelectedValueChanged += new EventHandler(cmbUCCode_SelectedValueChanged);
            cmbUCCode.LostFocus += new EventHandler(cmbUCCode_LostFocus);

            cmbUCCode.DisplayMember = "UCCode";
            cmbUCCode.ValueMember = "ImageId";
            cmbUCCode.DataSource = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select UCCode,ImageId FROM JMSIMG.dbo.VProductImages")).Tables[0]; ;

            cmbUCCode.Location = lnkuccode.Location;
            lnkuccode.Visible = false;
        }

        private void cmbUCCode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbUCCode.SelectedValue != null)
            {
                lnkuccode.Text = cmbUCCode.Text;
                grbPictureBox1.BinaryValue = (byte[])DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select ProductImage From JMSIMG.dbo.VProductImages WHERE ImageId=" + cmbUCCode.SelectedValue)).Tables[0].Rows[0][0];
            }
            else
            {
                lnkuccode.Text = "Select UC Code";
                grbPictureBox1.BinaryValue = null;
            }
        }

        private void cmbUCCode_LostFocus(object sender, EventArgs e)
        {
            cmbUCCode.Visible = false;
            lnkuccode.Visible = true;
        }
       
    }
}

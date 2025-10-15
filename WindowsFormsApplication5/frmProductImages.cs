using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Transactions;
using System.Windows.Forms;

namespace JMS.Forms.PROD
{
    public partial class frmProductImages : Gramboo.Controls.GrbForm
    {
      private static frmProductImages instance;
 
        public static frmProductImages Instance
       {

            get
            {
                if (instance == null)
                {
                    instance = new frmProductImages();
                }
                else if (instance.IsDisposed)
                {
                    instance = new frmProductImages();
                }
                return instance;
            }
        }
        public frmProductImages()
        {
            InitializeComponent();
            this.DefaultPrimaryKeys = new List<string>();
        }

        public override void Init()
        {
            base.Init();
            grbDataGridView1.EntryFormName = this ;
            TxtIsactive.Text = "1";
            grbPictureBox1.Image = null;
        }

        public override bool InitializeTables()
        {

            this.TableName = new Table("JMSIMG", "dbo", "ProductImages");
            this.TableName.PrimaryKeys.Add("ImageId");
            this.TableName.IdTextBox = txtImageId;
            return true;
        }

        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            if (base.FillData(PrimaryValues))
            {
                if (this.TableName.IdTextBox.Text.Trim().Length > 0)
                {
                    grbPictureBox1.BinaryValue = (byte [] )DBConn.GetData(new SqlCommand("Select ProductImage FROM JMSIMG.dbo.ProductImages WHERE ImageID=" + this.TableName.IdTextBox.Text)).Tables[0].Rows[0][0];

                   
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public override void RefreshData()
        {
            grbDataGridView1.ShowSerialNo = true;
            grbDataGridView1.HiddenDataFields = new List<string> { "ImageId", "ItemId" };
            grbDataGridView1.Fill(new Table("JMSIMG", "dbo", "VProductImages", true));
            ((DataGridViewImageColumn)grbDataGridView1.Columns["ProductImage"]).ImageLayout = DataGridViewImageCellLayout.Stretch;
            foreach (DataGridViewRow r in grbDataGridView1.Rows)
            {
                r.Height = 100;
            }

            Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VOrnaments", "[Item Name]", "ItemId", "IsActive='True'");

            base.RefreshData();
        }

        public override bool GenerateID(Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text = JMS.Classes.Common.GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, Convert.ToInt32(base.txtcompId.Text), Convert.ToInt32(base.txtBranchID.Text)).ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }




        public override bool Save()
        {
        //    //if (ValidateControls())
        //    //{
        //    //    using (TransactionScope transactionScope = new TransactionScope())
        //    //    {
        //    //        if ( IsEditMode)
        //    //        {
        //    //            SqlConnection sqlConnectiondel= new SqlConnection("Data Source=" + DBConn.ConnectionProperties.ServerName + ";Initial Catalog=JMSIMG;Integrated Security=false;User ID=" +
        //    //                DBConn.ConnectionProperties.DBUsername + ";Password=" + DBConn.ConnectionProperties.DBPassword + ";");
        //    //            SqlCommand sqlCommandDel = sqlConnectiondel.CreateCommand();
        //    //            sqlCommandDel.CommandText = "Delete FROM ProductImages WHERE ImageId="+ txtImageId.Text.Trim() ;
        //    //            sqlConnectiondel.Open();
        //    //            sqlCommandDel.ExecuteNonQuery();
        //    //        }
        //    //            SqlConnection sqlConnection1 = new SqlConnection("Data Source=" + DBConn.ConnectionProperties.ServerName + ";Initial Catalog=JMSIMG;Integrated Security=false;User ID=" +
        //    //                DBConn.ConnectionProperties.DBUsername + ";Password=" + DBConn.ConnectionProperties.DBPassword + ";");
        //    //            SqlCommand sqlCommand1 = sqlConnection1.CreateCommand();
        //    //            sqlCommand1.CommandText = "Insert Into ProductImages	(Id,Description,ProductImage) values('" + Guid.NewGuid().ToString() +
        //    //                "','',Cast('' As varbinary(Max)) ); Select ProductImage.PathName() As Path From ProductImages Where Id =@@Identity";
        //    //            sqlConnection1.Open();
        //    //            string filePath1 = (string)sqlCommand1.ExecuteScalar();
        //    //            SqlConnection sqlConnection2 = new SqlConnection("Data Source=" + DBConn.ConnectionProperties.ServerName + ";Initial Catalog=JMSIMG;Integrated Security=false;User ID=" +
        //    //                DBConn.ConnectionProperties.DBUsername + ";Password=" + DBConn.ConnectionProperties.DBPassword + ";");
        //    //            SqlCommand sqlCommand2 = sqlConnection2.CreateCommand();
        //    //            sqlCommand2.CommandText = "Select GET_FILESTREAM_TRANSACTION_CONTEXT() As TransactionContext";
        //    //            sqlConnection2.Open();
        //    //            byte[] transactionContext1 = (byte[])sqlCommand2.ExecuteScalar();
        //    //            SqlFileStream sqlFileStream1 = new SqlFileStream
        //    //                (filePath1, transactionContext1, FileAccess.Write);
        //    //            byte[] ProductImage = Guid.NewGuid().ToByteArray();
        //    //            sqlFileStream1.Write(ProductImage, 0, ProductImage.Length);
        //    //            sqlFileStream1.Close();
        //    //            transactionScope.Complete();
                    
        //    //    }

            base.txtcompId.Text = "1";
            base.txtBranchID.Text = "101";

            byte[] temp;
            if (grbPictureBox1.BinaryValue  == null)
            {
                grbPictureBox1.ShowMessage("Select Image");
                return false;
            }
            else
            {
                temp = grbPictureBox1.BinaryValue;
                grbPictureBox1.BinaryValue = new byte[] { 0 };
            }
            if (base.Save())
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    grbPictureBox1.BinaryValue = temp;

                    string filePath1 = (string)DBConn.GetData(new SqlCommand(" Select ISNULL(ProductImage.PathName(),'') As Path From JMSIMG.dbo.ProductImages Where ImageId =" + txtImageId.Text)).Tables[0].Rows[0][0];

                    if (filePath1.Length > 0)
                    {

                        //WrapperImpersonationContext context = new WrapperImpersonationContext(System.Environment.MachineName, "guest", "");
                        //context.Enter();
                        //Console.WriteLine("Current user: " + WindowsIdentity.GetCurrent().Name);
                       // SqlConnection sqlConnection2 = new SqlConnection("Data Source=" + DBConn.ConnectionProperties.ServerName + ";Database=JMSIMG;User ID=sa;Password=P@SSW0RD;Trusted_Connection=False;Integrated Security = true;");
                        SqlConnection sqlConnection2 = new SqlConnection("Server=" + DBConn.ConnectionProperties.ServerName + ";Database=JMSIMG;Integrated Security = true");
                        // Server=LOCALHOST\SQL2008
                        SqlCommand sqlCommand2 = sqlConnection2.CreateCommand();
                        sqlCommand2.CommandText = "Select GET_FILESTREAM_TRANSACTION_CONTEXT() As TransactionContext";
                        sqlConnection2.Open();
                        //sqlConnection2.Close();
                        //DBConn.ConnectionProperties.ConnectionString = "Data Source=" + DBConn.ConnectionProperties.ServerName + ";Initial Catalog=JMSIMG;Integrated Security=true;User ID=" +
                        //       DBConn.ConnectionProperties.DBUsername + ";Password=" + DBConn.ConnectionProperties.DBPassword + ";";
                        //byte[] transactionContext1 = (byte[])DBConn.GetData(new SqlCommand("Select GET_FILESTREAM_TRANSACTION_CONTEXT() As TransactionContext")).Tables[0].Rows[0][0];
                        byte[] transactionContext1 = (byte[])sqlCommand2.ExecuteScalar();
                       // Gramboo.General.ShowMessage(filePath1);
                        SqlFileStream sqlFileStream1 = new SqlFileStream
                            (filePath1, transactionContext1, FileAccess.Write);

                        byte[] ProductImage = grbPictureBox1.BinaryValue;
                        sqlFileStream1.Write(ProductImage, 0, ProductImage.Length);
                        sqlFileStream1.Close();
                        transactionScope.Complete();
                        //context.Leave();

                    }
                }


                RefreshData();
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //}
        private void grbDataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}

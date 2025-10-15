using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gramboo.Database;
using System.Data.SqlClient;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using System.IO;
using Gramboo.Classes;

namespace Gramboo.Controls
{
    public partial class GrbForm : DockContent
    {

        Dictionary<string,object> cols = new Dictionary<string, object>();
        DataTable PropTable = new DataTable("PropertyTable");
        DataTable MainPropTable = new DataTable("MainPropTable");
        DataTable OldValues;
        DataController dc;
        List<string> _DefaultPrimaryKeys = new List<string>();
        Dictionary<string ,Int64> TableKeys=new Dictionary<string,Int64>();
        private List<string> _logrefcols;
        //IEnumerable<GrbTextBox> Idctrls;
       internal     bool LoadFlag = false;

        public GrbForm()
        {
          InitializeComponent();
            
            if (!DesignMode)
              dc = new DataController(this);
          this.KeyPreview = true;
          TabText = Text;
          _DefaultPrimaryKeys.Add("Company_id");
          _logrefcols = new List<string>();
        }   
       [DefaultValue("")]
        public string  LogView { get; set; }

        public List<string> DefaultPrimaryKeys
        {
            get
            {
                return _DefaultPrimaryKeys;
            }
            set
            {
                _DefaultPrimaryKeys = value;
            }
        }
        /// <summary>
        /// Reference columns for audit trail log desscription
        /// </summary>
        [Browsable(true)]
        [Description("  Gets or Sets  Reference columns for audit trail log desscription")]
        [DefaultValue(null)]
        public List<string> LogReferenceColumns
        {
            get
            {
                return _logrefcols;
            }
            set
            {
                _logrefcols = value;
            }
        }

        [DefaultValue(false)]
        public  bool EditMode { get; set; }
        public bool IsEditMode
        {
            get
            {
                return EditMode;
            }
        }
        [DefaultValue(null)]
        public Table  TableName { get; set; }

        [DefaultValue(null)]
        public GrbForm ListForm { get; set; }

        [DefaultValue(null)]
        public Control AcceptControl { get; set; }
       
        public DataController DBConn
        {
            get
            {
                return dc;
            }
            set
            {
                dc = value;
            }
        }

        public virtual bool ValidateControls( )
        {
            bool flag=true ;
            bool isID = false;
            IEnumerable<Control> ctrllst;
            ctrllst = GetAll(this);
            foreach (Control c in ctrllst)
            {
             
                    if (c.GetType().GetProperty("AcceptBlankValue") != null)
                    {
                        if (c.GetType().GetProperty("IsIDField") != null)
                        {
                            isID = Convert.ToBoolean(c.GetType().GetProperty("IsIDField").GetValue(c, null));
                        }

                        if (Convert.ToBoolean(c.GetType().GetProperty("AcceptBlankValue").GetValue(c, null)) == false && Convert.ToBoolean(c.GetType().GetProperty("BindToDataGridview").GetValue(c, null)) == false)
                        {
                            if (Convert.ToBoolean(c.GetType().GetMethod("IsBlank").Invoke(c, null)) == true && isID == false)
                            {
                                flag = false;
                            }

                            if (c.GetType() == typeof(GrbTextBox))
                            {
                                if (Convert.ToBoolean(c.GetType().GetProperty("AcceptZero").GetValue(c, null)) == false  )
                                {
                                    string s = c.GetType().GetProperty(c.GetType().GetProperty("BindingProperty").GetValue(c, null).ToString()).GetValue(c, null).ToString();
                                    double d;

                                    if (Double.TryParse(s, out d) == false)
                                    {
                                        d = 0;
                                    }
                                         


                                    if (d == 0)
                                    {
                                        ((GrbTextBox)c).ShowMessage("Can not accept zero");
                                        flag = false;
                                    }
                                }
                            }

                             
                        }
                    }
                    else if (c.GetType() == typeof(GrbDataGridView))
                    {
                        if (((GrbDataGridView)c).Rows.Count == 0 && ((GrbDataGridView)c).IsList == false && ((GrbDataGridView)c).AllowEmptyRows ==false )
                        {
                            flag = false;
                            General.ShowMessage("Add Details to Grid");
                        }
                    }

                }

            
            return flag;
        }




 
        public virtual bool GenerateID(Table table_name)
        {
            //IEnumerable<Control> ctrls;
            //ctrls = GetAllIDTextBoxes(this);
            //DataTable dt = new DataTable();

            //dt = GetTableColumns(table_name);

            //foreach (Control c in ctrls)
            //{
            //    if (dt.Select("COLUMN_NAME =" + ((GrbTextBox)c).DataField).Length > 0)
            //    {
            //        if (table_name != this.TableName)
            //        {
            //            if (MainPropTable.Select("DataField =" + ((GrbTextBox)c).DataField).Length <= 0)
            //            {

                        

            //            }

            //        }
            //    }
 
            //}
           

            return true;
        }

        //public virtual bool GenerateID(Table table_name, out object value)
        //{
        //    value = null;
        //    return true;
        //}
        public virtual bool Save()
        {
            return Save(true);
        }
        public virtual bool Save(bool Confirm = true)
        {
            
                bool saveflag = false;
            InitializeTables();
            GetCommonFieldValues();
            Gramboo.DataController.CommandCollection cmdColl = new Gramboo.DataController.CommandCollection();
            TableKeys = new Dictionary<string, long>();

            //Idctrls = GetAllIdTexts(this); ;
            if (!EditMode)
            {
                if (this.TableName.IdTextBox != null)
                {
                    if (!GenerateID(this.TableName))
                        return false;
                    else
                    {
                        TableKeys.Add(this.TableName.GetName(), Convert.ToInt64(this.TableName.IdTextBox.Text));
                    }

                }
            }


            if (ValidateControls())
            {

                CreatePropertyTable(this.TableName);
                MainPropTable = PropTable.Copy();


                //if (EditMode == false)
                //    EditMode = GetEditMode(this.TableName);
                if (EditMode)
                {
                    if (Confirm)
                    {
                        if (ShowMessage("Do You Want To Update This Entry ?", "", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }



                if (!CheckDuplicates())
                    return false;



                if (this.TableName.IsDatagridView == false)
                {
                    string qry;
                    if (EditMode)
                        qry = GenerateUpdateQuery(this.TableName);
                    else
                    {
                        GenerateID(this.TableName);
                        foreach (DataRow r in MainPropTable.Rows)
                        {
                            if (r["DataField"].ToString() == this.TableName.IdTextBox.DataField)
                            {
                                r["Value"] = this.TableName.IdTextBox.Text;
                            }
                        }
                        foreach (DataRow r in PropTable.Rows)
                        {
                            if (r["DataField"].ToString() == this.TableName.IdTextBox.DataField)
                            {
                                r["Value"] = this.TableName.IdTextBox.Text;
                            }
                        }
                        qry = GenerateInsertQuery(this.TableName);
                    }
                    cmdColl.Add(qry);
                }
                else
                {

                    cmdColl.Add(GetDataGridviewValues(this.TableName));
                }
                //if (this.TableName.ChildTables.Count == 0)
                //{
                //    if (DBConn.ExecuteSqlTransaction(new SqlCommand(qry), this.TableName.GetName()))
                //    {
                //        EditMode = true;
                //        RefreshData();
                //        General.ShowMessage("Data Saved");
                //        return true;
                //    }
                //    else
                //    {
                //        General.ShowMessage("Failed to Save", "", MessageBoxIcon.Error);
                //        return false;
                //    }
                //}
                //else
                //{

                //cmdColl.Add(new SqlCommand(qry));



                if (chkAuditTrial.Checked)
                {

                    string auditstr = "", editremarks = "Data Modified ";
                    if (GeneralConfig.AskForPasswordForChangeData)
                    {
                        frmAuth Auth = new frmAuth();
                        if (EditMode)
                        {
                            Auth.ShowDialog();
                            if (Auth.DialogResult != DialogResult.OK)
                            {
                                return false;
                            }
                        }
                        editremarks = Auth.txtRemarks.Text;
                    }

                    SqlCommand audit = new SqlCommand();

                    auditstr = "INSERT INTO SYST.Log ([Date],[user_Id],[Action],[Description],XMLData,TableName,EntryID" +
          " ,[Company_id],[Branch_id],[Counter_id],[IsActive])" +
           " Values ('" + DateTime.Now + "'," +
          GeneralConfig.UserId  + "," +
          "'" + (EditMode ? "Update" : "Insert") + "','" + (EditMode ? editremarks : "New Data Added") + "'," +
             "@Desc,'" + this.TableName.OwnerName + "." + TableName.Name + "'," + this.TableName.IdTextBox.Text + "," + txtcompId.Text + "," + txtBranchID.Text + "," + GeneralConfig.CounterId + ",'TRUE')";
                    audit.CommandText = auditstr;
                    audit.Parameters.AddWithValue("@Desc", (EditMode ? (GeneralConfig.AskForPasswordForChangeData ? GenerateLogDescription() : "") : ""));
                    cmdColl.Add(audit);

                }
                foreach (Table t in this.TableName.ChildTables)
                {

                    SqlCommand cmddel = new SqlCommand(GenerateDeleteQuery(t));

                    cmdColl.Add(cmddel);
                    if (chkSaveQuery.Checked)
                    {
                        SaveQueryStack(cmddel);
                    }
                }
                foreach (Table t in this.TableName.ChildTables)
                {



                    if (t.IsDatagridView == false)
                    {
                        if (t.IdTextBox != null)
                        {
                            if (TableKeys.ContainsKey(t.GetName()))
                            {
                                long idval;

                                TableKeys.TryGetValue(t.GetName(), out idval);
                                t.IdTextBox.Text = (idval + 1).ToString();

                            }
                            else
                            {

                                if (!GenerateID(t))
                                    return false;
                                else
                                {
                                    TableKeys.Add(t.GetName(), Convert.ToInt64(t.IdTextBox.Text));
                                }
                            }
                        }
                        CreatePropertyTable(t);

                        cmdColl.Add(new SqlCommand(GenerateInsertQuery(t)));
                    }
                    else
                    {
                        cmdColl.Add(GetDataGridviewValues(t));
                    }
                }


                try
                {
                    bool p = false;

                    saveflag= DBConn.ExecuteSqlTransaction(cmdColl, this.TableName.GetName(), ref  p);
                     
                }
                catch (SqlException ex)
                {
                    ShowMessage(ex.Message);

                    MessageBox.Show(ex.ToString());


                    saveflag = false;
                }
                catch (Exception ex1)
                {
                    ShowMessage("Error while saving " + ex1.Message);
                    saveflag = false;
                }
                if (saveflag  )
                {

                    if (chkSaveQuery.Checked)
                    {
                        SaveQueryStack(cmdColl);
                    }
                    AfterSave();
                    EditMode = true;
                    if (Confirm)
                    {
                        General.ShowMessage("Data Saved");
                    }


                } 

                //}
            }
            

            return saveflag;

        }
         
        public virtual void List()
        {
            SetListForm();
            if (this.ListForm == null)
                return;
             

            if (this.FindForm().MdiParent != null)
            {

                IEnumerable<Control> ctrllst;
                ctrllst = GetAll(this.FindForm().MdiParent);
                foreach (Control c in ctrllst)
                {
                    if (c.GetType() == typeof(DockPanel))
                    {
                        if (!this.ListForm.Visible)
                        {

                            GrbForm frm = this.ListForm;
                            if (this.ListForm.IsDisposed)
                            {

                                frm = (GrbForm)Activator.CreateInstance(this.ListForm.GetType());
                                frm.MdiParent = this.FindForm().ParentForm;
                                this.ListForm = frm;
                            }
                            // this.EntryFormName.MdiParent = this.FindForm().ParentForm;
                            frm.Show((DockPanel)c);
                        }
                    }
                }
            }
            else
            {
                if (!this.ListForm.Visible)
                    this.ListForm.Show();
            }
            this.ListForm.Activate();
             
        }
        public virtual void SetListForm()
        {

        }
        public virtual void RefreshData()
        {

        }
        public virtual void AfterSave()
        {

        }
        public virtual bool Delete()
        {
            return Delete(true);
        }
        public virtual bool Delete(bool Confirm = true)
        {
            InitializeTables();
            bool flag=true;
            if (Confirm)
            {
                if (General.ShowMessage("Do You Want to Delete This Entry ?", "", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

            }
            else
            {
                flag = true;
            }
            if (flag )
            {
                if (this.ActiveControl.GetType() == typeof(GrbDataGridView) )
                {
                    GrbDataGridView dgv = (GrbDataGridView)this.ActiveControl;
                    if (dgv.IsList && dgv.EntryFormName==this )
                    {
                        dgv.Edit(dgv);
                       
                    }
                    else if (dgv.IsList && dgv.EntryFormName != this)
                    {
                        dgv.Delete(dgv);
                        return true ;
                    }
                }

                CreatePropertyTable(this.TableName);
              Gramboo.DataController.CommandCollection cmdcol = new Gramboo.DataController.CommandCollection();

                cmdcol.Add  ( new SqlCommand(GenerateDeleteQuery(this.TableName)));

             
              
                if (chkAuditTrial.Checked)
                {
                    string auditstr = "",editremarks="";
                        if (GeneralConfig.AskForPasswordForChangeData)
                        {
                            frmAuth Auth = new frmAuth();
                            if (EditMode)
                            {
                                Auth.ShowDialog();
                                if (Auth.DialogResult != DialogResult.OK)
                                {
                                    return false;
                                }
                            }
                            editremarks = Auth.txtRemarks.Text;
                        }


                    SqlCommand audit = new SqlCommand();
                    auditstr = "INSERT INTO SYST.Log ([Date],[user_Id],[Action],[Description],XMLData,TableName,EntryID" +
          " ,[Company_id],[Branch_id],[Counter_id],[IsActive])" +

           " Values ('" + DateTime.Now + "'," +
          GeneralConfig.UserId + "," +
          "'" +  " Delete"+ "','" +(GeneralConfig.AskForPasswordForChangeData ? editremarks : "Data Deleted") +"',"+
             "@Desc,'" + this.TableName.OwnerName + "." + TableName.Name  + "'," + this.TableName.IdTextBox.Text + "," + txtcompId.Text + "," + txtBranchID.Text + "," + GeneralConfig.CounterId  + ",'TRUE')";

                    audit.CommandText = auditstr;
                    audit.Parameters.AddWithValue("@Desc", (GeneralConfig.AskForPasswordForChangeData ? GenerateLogDescription() : ""));
                    cmdcol.Add(audit);
                }
                foreach (Table t in this.TableName.ChildTables)
                {
                    cmdcol.Add(new SqlCommand(GenerateDeleteQuery(t)));
                }

                if (DBConn.ExecuteSqlTransaction(cmdcol, this.TableName.GetName()))
                {
                    
                    if (chkSaveQuery.Checked)
                    {
                        SaveQueryStack(cmdcol);
                    }
                    General.ShowMessage("Data Deleted");
                    Init();

                    return true;
                }
                else
                {
                    General.ShowMessage("Failed to Delete", "", MessageBoxIcon.Error);
                    return false;
                }


            }
            else
            {
                return false;
            }

        }
        public object GetDefaultKeyVal(string datafield)
        {
            if (datafield == txtcompId.DataField)
                return txtcompId.Text;
            else if (datafield == txtCrUserId.DataField)
                return txtCrUserId.Text;
            else if (datafield == txtBranchID.DataField)
                return txtBranchID.Text;
            else if (datafield == txtCounterId.DataField)
                return txtCounterId.Text;
            else
                return 0;
        }
        public virtual string GenerateDeleteQuery(Table table_name)
        {
            string qry = "";
            qry = "DELETE FROM " + table_name.GetName() + " WHERE " + GetCriteria(this.TableName );
            return qry;
        }

        public  virtual string GenerateInsertQuery(Table table_name)
        {
            string qry="";

            string fieldlst="";
            string vallst="";
            string quote="";
            SetDefaultValues();
            foreach (DataRow c in PropTable.Rows )
            {
                quote = (General.TypesWithQuotes.Contains(c["DataType"].ToString(), StringComparer.OrdinalIgnoreCase) ? "'" : "");

                fieldlst += (fieldlst.Length == 0 ? "" : " , ") + c["DataField"];

                vallst += (vallst.Length == 0 ? "" : " , ") + quote + c["Value"].ToString().Replace("'","''") + quote;
            }
            qry = "INSERT INTO " + table_name.GetName() + "(" + fieldlst + ")" + " VALUES (" + vallst + ")";


            return  qry;

        }

        public virtual string GenerateUpdateQuery(Table table_name)
        {
            string qry = "";

            string fieldlst = "";
            string criteria = GetCriteria(table_name);
            string quote = ""; 
            object  defval = "";

            SetDefaultValues();

            table_name.NotUpdatables.Add("DBID");
            foreach (DataRow c in PropTable.Rows)
            { 
                if (table_name.NotUpdatables.Contains ( c["DataField"].ToString(),StringComparer.OrdinalIgnoreCase)==false  )
                {
                    quote = (General.TypesWithQuotes.Contains(c["DataType"].ToString(), StringComparer.OrdinalIgnoreCase) ? "'" : "");

                    fieldlst += (fieldlst.Length == 0 ? "" : " , ") + c["DataField"] + "=" + quote + c["Value"].ToString().Replace("'", "''") + quote;

                }
            }

            qry = "UPDATE  " + table_name.GetName() + " SET " + fieldlst +  (criteria.Length==0?"": " WHERE " + criteria );


            return qry;

        }

        private void SetDefaultValues()
        {
            foreach (DataRow r in PropTable.Rows)
            {
                if (r["Value"] == null || r["Value"].ToString().Trim().Length == 0)
                {
                    r["Value"] = (General.TypesWithQuotes.Contains(r["DataType"].ToString(), StringComparer.OrdinalIgnoreCase) ? General.SqlDefaultValues[r["DataType"].ToString()] : 0);
                    r.AcceptChanges();
                    
                }
            }
            PropTable.AcceptChanges();
        }


        ///<summary>
        /// Gets Column Details from given table 
        /// </summary>
        /// <param name="table_name"> Table Name </param>
        /// <param name="table_owner">Schema Name <example> dbo </example> </param>
        /// <returns></returns>
        public DataTable GetTableColumns(Table table_name)
        {
            DataTable dt = null;

             try
            {


                DataSet dset = new DataSet();
                SqlCommand cmd = new SqlCommand(table_name.DatabaseName +".sys.sp_columns");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@table_name", table_name.Name);
                cmd.Parameters.AddWithValue("@table_owner", table_name.OwnerName );
                DataController dc = new DataController(this);
                dset = dc.GetData(cmd, table_name.GetName(), dc.ConnectionProperties.ConnectionString);


                if ((dset != null) && (dset.Tables.Count > 0))
                    dt = dset.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while getting columns " + ex.ToString());
                throw new Exception("Error Occured While Fetching Table Details : \n" + ex.Message);
            }

            return dt;

        }

        private void CreatePropertyTable(Table table_name)
        {
            PropTable.Clear();
            PropTable.Columns.Clear();
            PropTable.Columns.Add("ControlName",typeof(string) );
            PropTable.Columns.Add("ControlType",typeof(string)  );
            PropTable.Columns.Add("TableName",typeof(string)  );
            PropTable.Columns.Add("DataField", typeof(string));
            PropTable.Columns.Add("DataType", typeof(string));
            PropTable.Columns.Add("Value", typeof(object));
            PropTable.Columns.Add("CheckDuplicate", typeof(bool));

            GetValues(table_name);

        }
        public IEnumerable<Control> GetAll(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            
                return controls.SelectMany(ctrl => GetAll(ctrl))
                                          .Concat(controls);
            

 
        }
        //public IEnumerable<GrbTextBox> GetAllIdTexts(Control control)
        //{
        //    var controls = control.Controls.Cast<Control>();

        //    return controls.SelectMany(ctrl => GetAll(ctrl))
        //                              .Concat(controls).OfType<GrbTextBox>().Where(I => I.IsIDField ==true) ;


        //}

        public virtual bool InitializeTables()
        {
            return true;
        }
        private void GetValues(Table table_name)
        {


            //if (File.Exists(System.IO.Path.GetTempPath() + "PropTable" + table_name.GetName()))
            //{
            //    PropTable.ReadXml(System.IO.Path.GetTempPath() + "PropTable" + table_name.GetName());
            //}
            //else
            //{
            string ctrlname="";
            //try
            //{
                var c = GetAll(this);
                DataTable cols = new DataTable();
                cols = GetTableColumns(table_name);
                foreach (DataRow colName in cols.Rows)
                {

                try
                {
                    foreach (Control ctrl in c)
                    {

                        var type = ctrl.GetType();
                        ctrlname = ctrl.Name;
                        if (type.GetProperty("DataField") != null)
                        {
                            if (((string)ctrl.GetType().GetProperty("DataField").GetValue(ctrl, null)) != null)
                            {

                                if (((string)ctrl.GetType().GetProperty("DataField").GetValue(ctrl, null)).ToUpper().Trim() != "" && ((string)ctrl.GetType().GetProperty("DataField").GetValue(ctrl, null)).ToUpper().Trim() != "None")
                                {

                                    if (colName["COLUMN_NAME"].ToString().ToUpper().Trim() == ((string)ctrl.GetType().GetProperty("DataField").GetValue(ctrl, null)).ToUpper().Trim() || colName["COLUMN_NAME"].ToString() == ((string)ctrl.GetType().GetProperty("Alias").GetValue(ctrl, null)).ToUpper().Trim())
                                    {
                                        DataRow dr = PropTable.NewRow();
                                        string bindprop;

                                        bindprop = (string)ctrl.GetType().GetProperty("BindingProperty").GetValue(ctrl, null);
                                        ctrlname = ctrl.Name;
                                        dr["ControlName"] = ctrl.Name;
                                        dr["ControlType"] = type.Name;
                                        dr["TableName"] = (string)ctrl.GetType().GetProperty("TableName").GetValue(ctrl, null);


                                        dr["DataField"] = (string)ctrl.GetType().GetProperty("DataField").GetValue(ctrl, null);


                                        dr["DataType"] = colName["TYPE_NAME"];

                                        dr["Value"] = (object)ctrl.GetType().GetProperty(bindprop).GetValue(ctrl, null);

                                        if (type.GetProperty("CheckDuplicates") != null)
                                        {
                                            dr["CheckDuplicate"] = (bool)ctrl.GetType().GetProperty("CheckDuplicates").GetValue(ctrl, null);
                                        }
                                        else
                                        {
                                            dr["CheckDuplicate"] = false;
                                        }

                                        PropTable.Rows.Add(dr);

                                    }
                                }
                            }
                        }
                    }

                    //    PropTable.WriteXml(System.IO.Path.GetTempPath() + "PropTable" + table_name.GetName(), true);
                    //}
                }


                catch (Exception ex)
                {
                    General.ShowMessage("Error Occured : Check " + ctrlname + ex.ToString(), "", MessageBoxIcon.Error, MessageBoxButtons.OK);
                }
                }
            //}

            //catch (Exception ex)
            //{
            //    General.ShowMessage("Error occured during getting values\n" + ex.Message.ToString(), "", MessageBoxIcon.Error, MessageBoxButtons.OK);

            //}
        }


        private string GetCriteria(Table table_name)
        {
            string criteria = "";
            string Quotes="";
             
            foreach (string defPK in DefaultPrimaryKeys)
            {
                if (table_name.PrimaryKeys.Contains(defPK.ToString(), StringComparer.OrdinalIgnoreCase) == false)
                {
                    table_name.PrimaryKeys.Add(defPK);
                    table_name.NotUpdatables.Add(defPK);
                }
            }


            
            if (table_name != this.TableName)
            {
                foreach (string defPK in this.TableName.PrimaryKeys )
                {
                    if (table_name.PrimaryKeys.Contains(defPK.ToString(), StringComparer.OrdinalIgnoreCase) == false)
                    {
                        table_name.PrimaryKeys.Add(defPK);
                        table_name.NotUpdatables.Add(defPK);
                    }
                }
            }

            foreach (string pk in table_name.PrimaryKeys)
            {
                
                if (pk.Length > 0)
                {
                   
                    DataRow r;
                    r = PropTable.NewRow();
                    
                    if (PropTable.Select("DataField='" + pk +"'").Length > 0)
                    {
                        r = PropTable.Select("DataField='" + pk + "'")[0];
                        Quotes = (General.TypesWithQuotes.Contains(r["DataType"].ToString(), StringComparer.OrdinalIgnoreCase) ? "'" : "");
                        criteria += (criteria.Length == 0 ? "" : " and ");
                        criteria += r["DataField"] + "=" + Quotes + r["Value"].ToString().Replace("'","''") + Quotes;
                    }
                    table_name.NotUpdatables.Add(pk);
                }
            }




            return criteria;
             
        }

        private string GetDuplicateCheckCriteria(Table table_name)
        {
            string criteria = "";
            string Quotes = "";
            List<string> primkys =new List<string>();
 
            foreach (string defPK in DefaultPrimaryKeys)
            {
               
                    primkys.Add(defPK);
                
            }
            foreach (string pk in primkys)
            {
                if (pk.Length > 0)
                {
                    DataRow r;
                    r = PropTable.NewRow();

                    if (PropTable.Select("DataField='" + pk + "'").Length > 0)
                    {
                        r = PropTable.Select("DataField='" + pk + "'")[0];
                        Quotes = (General.TypesWithQuotes.Contains(r["DataType"].ToString(), StringComparer.OrdinalIgnoreCase) ? "'" : "");
                        criteria += (criteria.Length == 0 ? "" : " and ");
                        criteria += r["DataField"] + "=" + Quotes + r["Value"].ToString().Replace("'","''")  + Quotes;
                    }

                }
            }

            primkys.Clear();
            if (EditMode)
            {
                foreach (string PK in table_name.PrimaryKeys )
                {
                    if (primkys.Contains(PK) == false)
                    {
                        primkys.Add(PK);
                    }
                }
                foreach (string pk in primkys)
                {
                    if (pk.Length > 0)
                    {
                        DataRow r;
                        r = PropTable.NewRow();

                        if (PropTable.Select("DataField='" + pk + "'").Length > 0)
                        {
                            r = PropTable.Select("DataField='" + pk + "'")[0];
                            Quotes = (General.TypesWithQuotes.Contains(r["DataType"].ToString(), StringComparer.OrdinalIgnoreCase) ? "'" : "");
                            criteria += (criteria.Length == 0 ? "" : " and ");
                            criteria += r["DataField"] + "!=" + Quotes + r["Value"].ToString().Replace("'","''")  + Quotes;
                        }

                    }
                }
            }

           
            return criteria;
        }

        private bool GetEditMode(Table table_name)
        {
            try
            {
                string criteria = GetCriteria(table_name);

                if (criteria.Length > 0)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "select top 1 * from " + table_name.GetName() + " Where " + criteria;
                    cmd.CommandTimeout = 30;

                    if (dc.GetData(cmd, table_name.GetName()).Tables[0].Rows.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                else
                {

                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());

                return false;
            } 

        }

        public DialogResult ShowMessage(string Message = "", string Title = "", MessageBoxIcon MsgIcon = MessageBoxIcon.Information, MessageBoxButtons MsgButtons = MessageBoxButtons.OK, MessageBoxDefaultButton MsgDefaultButton = MessageBoxDefaultButton.Button1)
        {
            return General.ShowMessage(Message, Title, MsgIcon, MsgButtons, MsgDefaultButton);
        }



        public virtual bool  FillData(Dictionary<string,object> PrimaryValues )
        {
            try
            {
              
                InitializeTables();
                Init();

                if (FillFromTable(this.TableName,PrimaryValues ))
                { 
                    foreach (Table t in this.TableName.ChildTables)
                    {
                        FillFromTable(t, PrimaryValues);
                    }
                }
                //if (this.MdiParent != null)
                //{
                //    bool flag = false;
                //    IEnumerable<Control> ctrllst;
                //    ctrllst = GetAll(this.FindForm().MdiParent);

                //    foreach (Control c in ctrllst)
                //    {
                //        if (c.GetType() == typeof(DockPanel))
                //        {
                //            flag = true;
                            
                //            this.Visible = false;
                //            this.Show(c);
                       
                //        }
                //    }
                //    if (!flag)
                //    {
                //        this.Visible = false;
                //        this.Show();
                //    }
                //}
                this.EditMode = true;
                this.Activate();
                return true;
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                return false;
            }


        }

        public virtual void Print()
        {
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadFlag = false  ;
        }

        private bool FillFromTable(Table table_name, Dictionary<string, object> PrimaryValues)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                string critertia = GetCriteria(table_name, PrimaryValues);
                string OrderBy = "";

                if (table_name.PrimaryKeys.Count > 0)
                {
                    foreach (string s in table_name.PrimaryKeys)
                    {
                        if (s.Length > 0)
                        {
                            if (OrderBy.Length == 0)
                            {
                                OrderBy = s;
                            }
                            else
                            {
                                if( table_name.PrimaryKeys.FindAll(ss => ss.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0).Count==0 )
                                OrderBy += "," + s;
                            }
                        }
                    }
                }

                if (table_name.FillView == null)
                {
                    cmd.CommandText = "select * from " + table_name.GetName() + " WHERE " + critertia  + ( OrderBy.Length>0 ? " Order By "+ OrderBy:"");
                    cmd.CommandType = CommandType.Text;
                }
                else
                {
                    cmd.CommandText = "select * from " + table_name.FillView.GetName() + " WHERE " + critertia  +(OrderBy.Length > 0 ? " Order By " + OrderBy : "");
                    cmd.CommandType = CommandType.Text;
                }

                if (table_name.IsDatagridView)
                {

                    table_name.DatagridView.Fill((table_name.FillView == null ? table_name : table_name.FillView), critertia,OrderBy );
                }
                else
                {
                    IEnumerable<Control> ctrllst;
                    ctrllst = GetAll(this);
                    DataTable dt1 = new DataTable();

                    dt1 = dc.GetData(cmd, table_name.GetName()).Tables[0];

                    if (TableName.Name == this.TableName.Name)
                        OldValues = dt1.Copy();
                    foreach (DataRow r in dt1.Rows)
                    {


                        foreach (Control ctl in ctrllst)
                        {
                            if (ctl.GetType().GetProperty("DataField") != null)
                            {
                                foreach (DataColumn c in dt1.Columns)
                                {
                                    if (Convert.ToString(ctl.GetType().GetProperty("DataField").GetValue(ctl, null)).ToUpper().Trim() == c.ColumnName.ToString().ToUpper().Trim() || Convert.ToString(ctl.GetType().GetProperty("Alias").GetValue(ctl, null)).ToUpper().Trim() == c.ColumnName.ToString().ToUpper().Trim())
                                    {

                                        string bprop = ctl.GetType().GetProperty("BindingProperty").GetValue(ctl, null).ToString();
                                        PropertyInfo propertyInfo = ctl.GetType().GetProperty(bprop);
                                        try
                                        {
                                            propertyInfo.SetValue(ctl, Convert.ChangeType(r[c], propertyInfo.PropertyType), null);
                                        }
                                        catch (InvalidCastException ex )
                                        {
                                            MessageBox.Show( ctl.Name +" "+ ex.ToString());

                                        }
                                        catch (FormatException ex)
                                        {
                                            MessageBox.Show(ctl.Name + " " + ex.ToString());

                                        }
                                        catch (NotImplementedException ex)
                                        {
                                            MessageBox.Show(ctl.Name + " " + ex.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            General.ShowMessage(ctl.Name + " " + ex.Message.ToString());
                                        }

                                    }
                                }
                            }
                        }

                    }                 //}
                }
                return true;
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                return false;
            }
        }
    

        private string GetCriteria(Table table_name,Dictionary<string, object> primarykeys)
        {


            string criteria = "";
            string Quotes = "";

            DataTable dt = new DataTable();
            dt = GetTableColumns(table_name);

      
            foreach (KeyValuePair<string, object> pk in primarykeys)
            {

                if (pk.Key.Length  > 0)
                {
                    DataRow r;
                    r = dt.NewRow();

                    if (dt.Select("COLUMN_NAME='" + pk.Key  + "'").Length > 0)
                    {
                        r = dt.Select("COLUMN_NAME='" + pk.Key  + "'")[0];
                        Quotes = (General.TypesWithQuotes.Contains(r["TYPE_NAME"].ToString(), StringComparer.OrdinalIgnoreCase) ? "'" : "");
                        criteria += (criteria.Length == 0 ? "" : " and ");
                        criteria += r["COLUMN_NAME"] + "=" + Quotes + pk.Value.ToString().Replace("'","''")   + Quotes;
                    }

                }
            }
            return criteria;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (!LoadFlag)
            {
                LoadFlag = true;
                Init();
            }
          

        }

        public void SwitchAccount(int ToCompany,int ToBranch )
        {
            txtBranchID.Text = ToBranch.ToString();
            txtcompId.Text = ToCompany.ToString(); 
            Init();
            RefreshData();

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (this.ActiveControl == null)
                return;



            if (this.ActiveControl is TextBox || this.ActiveControl.GetType().BaseType is TextBox  )
            {
                if (((TextBox)this.ActiveControl).Multiline == true)
                    return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.AcceptControl == this.ActiveControl)
                {
                    this.Save();
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }
            }
            else if (e.Alt && e.KeyCode == Keys.N)
            {
                Init();
            }
            else if (e.KeyCode == Keys.F2)
            {
                // ToolStripSave_Click(sender, e);
            }

        }
        public  virtual void Init()
        {


            if (!DesignMode)
            {
               

                this.EditMode = false;
                General.ClearControls(this);
                GetCommonFieldValues();

                RefreshData();
            }
        }
        public virtual void Edit()
        {
            if (this.ActiveControl ==  null)
                return;

            if (this.ActiveControl.GetType() == typeof(GrbDataGridView))
            {
                if (((GrbDataGridView)this.ActiveControl).IsList)
                {
                    ((GrbDataGridView)this.ActiveControl).Edit((GrbDataGridView)this.ActiveControl);
                }
            }
        }
        public virtual void GetCommonFieldValues()
        {
            //GRBConfig config = GRBConfig.Open();
            

            try
            {
                if (EditMode == false)
                {
                    txtDbId.Text = GeneralConfig.DbId.ToString();
                    txtCrUserId.Text = GeneralConfig.UserId.ToString(); 
                    txtcreatedDate.Text = DateTime.Now.ToString();
                }
                txtBranchID.Text = GeneralConfig .BranchId.ToString();
                txtcompId.Text = GeneralConfig.CompanyID.ToString();
                txtCounterId.Text = GeneralConfig.CounterId.ToString();
               
                txtModuserID.Text = GeneralConfig.UserId.ToString();
                txtUserName.Text =(GeneralConfig.UserName==null?"": GeneralConfig.UserName.ToString());
                chkAuditTrial.Checked = GeneralConfig.EnableAuditTrail;
                chkSaveQuery.Checked = GeneralConfig.SaveQuery;
                txtModifiedDate.Text = DateTime.Now.ToString(); 
            }
            catch (Exception ex )
            {
                MessageBox.Show("Error while fetching Common Field values"+ ex.ToString());
            }
        }

        private bool CheckDuplicates()
        {
            bool flag=true ;
            string criteria = GetDuplicateCheckCriteria(this.TableName);
            string Quotes="";

            foreach (DataRow r in PropTable.Select("CheckDuplicate='True'"))
            {
                Quotes = (General.TypesWithQuotes.Contains(r["DataType"].ToString(), StringComparer.OrdinalIgnoreCase) ? "'" : "");
                 if (dc.GetData(new SqlCommand("Select top 1 * FROM " + TableName.GetName() +
                     " WHERE " + r["DataField"] + "=" + Quotes + r["Value"].ToString().Trim().Replace("'","''")  + Quotes + (criteria.Length>0 ? " AND " + criteria:""))).Tables[0].Rows.Count > 0)
                 {
                    if (this.Controls.Find(r["ControlName"].ToString(), true)[0].GetType() == typeof(GrbTextBox))
                    {
                        ((GrbTextBox)this.Controls.Find(r["ControlName"].ToString(), true)[0]).ShowMessage(r["Value"].ToString() + " already exists ");

                        flag = false;
                    }
                    else if (this.Controls.Find(r["ControlName"].ToString(), true)[0].GetType() == typeof(GrbComboBox))
                    {
                        ((GrbComboBox)this.Controls.Find(r["ControlName"].ToString(), true)[0]).ShowMessage(r["Value"].ToString() + " already exists ");

                        flag = false;
                    }
                 }
                
            }

            return flag;
        }
         
        private SqlCommand[] GetDataGridviewValues(Table table_name)
        {

            try
            {

                SqlCommand[] cmdcol = new SqlCommand[0];

                if (table_name.DatagridView.Rows.Count == 0)
                    return cmdcol;

               cmdcol = new SqlCommand[table_name.DatagridView.Rows.Count];


                DataTable dt = new DataTable();
                PropTable.Rows.Clear();
                dt = GetTableColumns(table_name);

                DataRow dr;

                foreach (DataRow r in MainPropTable.Rows)
                {
                    dr = PropTable.NewRow();

                    if ((dt.Select("COLUMN_NAME='" + r["DataField"] + "'")).GetLength(0) > 0)
                    {
                        dr.ItemArray=r.ItemArray ;
                        dr["TableName"] = MainPropTable.TableName;
                        PropTable.Rows.Add(dr);
                    }
                }

                foreach (DataGridViewColumn c in table_name.DatagridView.Columns)
                {

                    try
                    {
                        if ((PropTable.Select("DataField='" + c.DataPropertyName.Replace(" ", "") + "'")).GetLength(0) <= 0)
                        {

                            dr = PropTable.NewRow();
                            if (c.DataPropertyName != null)
                            {
                                if ((dt.Select("COLUMN_NAME='" + c.DataPropertyName.Replace(" ", "") + "'")).GetLength(0) > 0)
                                {
                                    dr = PropTable.NewRow();
                                    dr["ControlName"] = c.Name;
                                    dr["ControlType"] = c.CellType.Name;
                                    dr["TableName"] = table_name.GetName();
                                    dr["DataField"] = c.DataPropertyName.Replace(" ", "");
                                    dr["DataType"] = Convert.ToString(((dt.Select("COLUMN_NAME='" + c.DataPropertyName.Replace(" ", "") + "'"))[0]["TYPE_NAME"]));
                                    dr["Value"] = null;
                                    dr["CheckDuplicate"] = false;


                                    PropTable.Rows.Add(dr);
                                    PropTable.AcceptChanges();
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message + c.Name);
                    }
                }
                int i = 0;
                //GrbTextBox idtext=new GrbTextBox();
                try
                {
                    //idtext = GetIdentityTextbox(table_name);
                    if (table_name.IdTextBox != null)
                    {

                        if (TableKeys.ContainsKey(table_name.GetName()))
                        {
                            long idval;

                            TableKeys.TryGetValue(table_name.GetName(), out idval);
                            table_name.IdTextBox.Text = (idval + 1).ToString();

                        }
                        else
                        {

                            if (!GenerateID(table_name))
                                return null;
                            else
                            {
                                TableKeys.Add(table_name.GetName(), Convert.ToInt64(table_name.IdTextBox.Text));
                            }
                        }
                    }

                }
                catch(Exception ex )
                {
                    MessageBox.Show(table_name.IdTextBox.Text + " " + table_name.IdTextBox.Name);

                }

                foreach (DataGridViewRow r in table_name.DatagridView.Rows)
                {



                    foreach (DataGridViewCell c in r.Cells)
                    {
                        try
                        {
                            if (c.OwningColumn.DataPropertyName != null)
                            {

                                if ((PropTable.Select("DataField='" + c.OwningColumn.DataPropertyName + "'")).GetLength(0) > 0)
                                {
                                    if (PropTable.Select("DataField='" + c.OwningColumn.DataPropertyName + "'")[0]["TableName"].ToString() != MainPropTable.TableName)
                                    {
                                        if (table_name.IdTextBox.DataField.ToUpper() == c.OwningColumn.DataPropertyName.ToUpper())
                                        {

                                            if (!EditMode)
                                            {
                                                c.DataGridView[c.ColumnIndex, c.RowIndex].Value = table_name.IdTextBox.Text;
                                            }
                                            else if (c.DataGridView[c.ColumnIndex, c.RowIndex].Value.ToString().Length <= 1)
                                            {
                                                c.DataGridView[c.ColumnIndex, c.RowIndex].Value = table_name.IdTextBox.Text;
                                            }
                                        }

                                        PropTable.Select("DataField='" + c.OwningColumn.DataPropertyName + "'")[0]["Value"] = c.DataGridView[c.ColumnIndex, c.RowIndex].Value;
                                        PropTable.AcceptChanges();
                                    }
                                }


                                else if ((PropTable.Select("DataField='" + c.OwningColumn.DataPropertyName.Replace(" ", "") + "'")).GetLength(0) > 0 && table_name.DatagridView.Columns.Contains(c.OwningColumn.DataPropertyName.Replace(" ", "")) == false)
                                {
                                    if (PropTable.Select("DataField='" + c.OwningColumn.DataPropertyName.Replace(" ", "") + "'")[0]["TableName"].ToString() != MainPropTable.TableName)
                                    {
                                        if (table_name.IdTextBox.DataField.ToUpper() == c.OwningColumn.DataPropertyName.Replace(" ", "").ToUpper())
                                        {
                                            if (!EditMode)
                                            {
                                                c.DataGridView[c.ColumnIndex, c.RowIndex].Value = table_name.IdTextBox.Text;
                                            }
                                            else if (c.DataGridView[c.ColumnIndex, c.RowIndex].Value.ToString().Length <= 1)
                                            {
                                                c.DataGridView[c.ColumnIndex, c.RowIndex].Value = table_name.IdTextBox.Text;
                                            }
                                        }

                                        PropTable.Select("DataField='" + c.OwningColumn.DataPropertyName.Replace(" ", "") + "'")[0]["Value"] = c.DataGridView[c.ColumnIndex, c.RowIndex].Value;
                                        PropTable.AcceptChanges();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(c.OwningColumn.DataPropertyName + " " + ex.Message);

                        }

                    }
                    

                        //if (!GenerateID(table_name))
                        //    return null;

                        double n = 0;
                        bool p = false;
                        p = Double.TryParse(table_name.IdTextBox.Text, out n);
                        if (p)
                        {
                            table_name.IdTextBox.Text = (n + 1).ToString();
                            TableKeys[table_name.GetName()] = (long)n + 1;
                        }
                        cmdcol[i] = new SqlCommand(GenerateInsertQuery(table_name));
                        i++;
                     
                }
                return cmdcol;
            }

            catch (Exception ex)
            {
                General.ShowMessage("Error While getting grid values "+ table_name.ToString()+ ex.Message, "", MessageBoxIcon.Error);
                return null;
            }
        }

        //private GrbTextBox GetIdentityTextbox(Table table_name)
        //{
        //    GrbTextBox idtext = new GrbTextBox();

        //    if (table_name.PrimaryKeys.Count > 0)
        //    {
        //        foreach (Control idctrl in Idctrls)
        //        {
        //            if (table_name == this.TableName)
        //            {
        //                if (table_name.PrimaryKeys.Contains(((GrbTextBox)idctrl).DataField.ToString(), StringComparer.OrdinalIgnoreCase) && this.TableName.PrimaryKeys.Contains(((GrbTextBox)idctrl).DataField.ToString(), StringComparer.OrdinalIgnoreCase) == false)
        //                {

        //                    idtext = (GrbTextBox)idctrl;
        //                }
        //            }
        //            else
        //            {
        //                if (table_name.PrimaryKeys.Contains(((GrbTextBox)idctrl).DataField))
        //                {
        //                    idtext = (GrbTextBox)idctrl;
        //                }
        //            }
        //        }
        //    }
        //    return idtext;
        //}


        private string GenerateLogDescription()
        {


            try
            {
                if(LogView!=null)
                {
                    if(LogView.Length>0)
                    {

                        OldValues = DBConn.GetData(new SqlCommand("Select * from " + LogView + " Where " + TableName.IdTextBox.DataField + "=" + TableName.IdTextBox.Text)).Tables[0];
                        
                    }
                    else if(TableName.FillView.Name.Length>0)
                    {

                        OldValues = DBConn.GetData(new SqlCommand("Select * from " + TableName.FillView.Name + " Where " + TableName.IdTextBox.DataField + "=" + TableName.IdTextBox.Text)).Tables[0];
                    }
                }

                MemoryStream str = new MemoryStream();
                OldValues.WriteXml(str, true);
                str.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(str);
                string xmlstr;
                xmlstr = sr.ReadToEnd();
                return xmlstr;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating log " + ex.ToString());
                return "";
            }

            
            
        }

        public void ShowHistory() 
        {
            try
            {
                frmHistory history = new frmHistory();
                history.TableName = TableName.OwnerName + "." + TableName.Name;
                history.EntryID = Convert.ToInt64(TableName.IdTextBox.Text);
                history.StartPosition = FormStartPosition.CenterScreen;
                history.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }
        private bool SaveQueryStack(Gramboo.DataController.CommandCollection cmdColl)
        {
            try
            {
                cmdColl.Reset();
                foreach (SqlCommand cmd in cmdColl)
                {
                    SaveQueryStack(cmd);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving query stack " + ex.ToString());

                return false;
            }
        }
        private bool SaveQueryStack(SqlCommand cmdColl)
        {
            SqlCommand cmd = new SqlCommand("Insert INTO SYST.QueryStackMaster(Query,TransferStatus) VALUES(@Query,'FALSE')");
            cmd.Parameters.AddWithValue("@Query", cmdColl.CommandText);

            return dc.ExecuteSqlTransaction( cmd, "SaveQuery");

        }
       
    }


}

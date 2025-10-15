using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using GlassMessage;
using Gramboo.Database;
using System.Data;
namespace Gramboo
{
    public static class General
    {


        public static List<string> TypesWithQuotes = new List<string>() { "bit","text", "date", "time", "datetime2", "datetimeoffset", "smalldatetime", "datetime", "ntext", "varchar", "char", "nvarchar", "nchar", "xml", "sysname" };

        public static Dictionary<string, object> SqlDefaultValues = new Dictionary<string, object>() 
    { { "text", "-" }, { "date", "01-JAN-1900" }, { "time", "01-JAN-1900 00:00:00"  }, { "datetime2", "01-JAN-1900 00:00:00"  },
    { "datetimeoffset","01-JAN-1900 00:00:00" }, { "smalldatetime", "01-JAN-1900 00:00:00" }, { "datetime", "01-JAN-1900 00:00:00" }, { "ntext", "-" }, { "varchar", "-" }, 
    { "char", "-" }, { "nvarchar", "-" }, { "nchar", "-" }, { "xml", "-" }, { "sysname", "-" }, { "bit", "false" } ,{"uniqueidentifier" , "default"} };



        /// <summary>
        /// This function clears all controls in a form or a container (groupbox,panel etc.)
        /// 
        /// </summary>
        /// <param name="FormName"> Form or Container Control </param>
        public static void ClearControls(Control FormName)
        {
           
            try
            {
                foreach (Control Obj in FormName.Controls)
                {

                    if (Obj.HasChildren == true)
                    {
                        ClearControls(Obj);
                        if (Obj is Gramboo.Controls.GrbRadioButtonGroup)
                        {
                            ((Gramboo.Controls.GrbRadioButtonGroup)Obj).Value = "";
                        }
                        else if (Obj is Gramboo.Controls.GrbDataGridView)
                        {
                            bool summvisible = ((Gramboo.Controls.GrbDataGridView)Obj).SummaryRowVisible;
                            ((Gramboo.Controls.GrbDataGridView)Obj).SummaryRowVisible = false;


                            if (((Gramboo.Controls.GrbDataGridView)Obj).IsList == false)
                                ((Gramboo.Controls.GrbDataGridView)Obj).DataSource = null;


                            ((Gramboo.Controls.GrbDataGridView)Obj).SummaryRowVisible= summvisible;
                        }
                    }
                    else if (Obj is TextBox)
                    {
                        ((TextBox)Obj).Text = "";
                    }
                    else if (Obj is ComboBox)
                    {

                        ((ComboBox)Obj).Text = "";
                        ((ComboBox)Obj).SelectedIndex = -1;
                    }
                    else if (Obj is DateTimePicker)
                    {
                        ((DateTimePicker)Obj).Value = DateTime.Now.Date;
                    }
                    else if (Obj is CheckBox)
                    {
                        ((CheckBox)Obj).Checked = false;
                    }
                    else if (Obj is RadioButton)
                    {
                        ((RadioButton)Obj).Checked = false;

                    }
                    else if (Obj is MaskedTextBox)
                    {
                        ((MaskedTextBox)Obj).Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", MessageBoxIcon.Error);
            }
        }


        public static void SetAutoComplete(TextBox TextBoxName, string SqlQuery)
        {


            Gramboo.DataController dc = new Gramboo.DataController();

            AutoCompleteStringCollection col = new AutoCompleteStringCollection();

            foreach (DataRow r in dc.GetData(new SqlCommand(SqlQuery)).Tables[0].Rows)
            {
                col.Add(r[0].ToString());
            }

            TextBoxName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TextBoxName.AutoCompleteCustomSource = col;
            TextBoxName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        public static void Setupcombo(object ComboBoxName, string TableName, string DisplayMember, string ValueMember = "", string Criteria = "1=1" )
        {
            Gramboo.DataController dc = ((Gramboo.Controls.GrbForm)((ComboBox)ComboBoxName).FindForm()).DBConn;
            Setupcombo(dc, ComboBoxName, TableName, DisplayMember, ValueMember, Criteria);
        }

        public static void Setupcombo(Gramboo.DataController dc,object ComboBoxName, string TableName, string DisplayMember, string ValueMember = "", string Criteria = "1=1")
        {
            string SqlQuery;
            string Existing;
          ((ComboBox) ComboBoxName).BeginUpdate();
            SqlQuery = "Select Distinct " + DisplayMember + " as  DisplayMember " + (ValueMember != "" ? "," + ValueMember + " as ValueMember" : "") + " From " + TableName + " WHERE " + Criteria + " Order By DisplayMember";


            Existing = ((ComboBox)ComboBoxName).Text;

            ((ComboBox)ComboBoxName).Text = "";
            ((ComboBox)ComboBoxName).DisplayMember = "DisplayMember";
            if (ValueMember != "")
                ((ComboBox)ComboBoxName).ValueMember = "ValueMember";

            ((ComboBox)ComboBoxName).DataSource = dc.GetData(new SqlCommand(SqlQuery)).Tables[0];


            ((ComboBox)ComboBoxName).EndUpdate();

            ((ComboBox)ComboBoxName).SelectedIndex  = -1;
            if (Existing != "")
                ((ComboBox)ComboBoxName).Text = Existing;

        }

        /// <summary>
        /// Displays Custom  Messages
        /// </summary>
        /// <param name="Message">What you want to Display as Message</param>
        /// <param name="Title">The Title of the message box </param>
        /// <param name="MsgButtons"> butttons that you want in message box  </param>
        /// <param name="MsgIcon">The Icon Displayed in the message box  </param>
        public static DialogResult ShowMessage(string Message = "", string Title = "", MessageBoxIcon MsgIcon = MessageBoxIcon.Information, MessageBoxButtons MsgButtons = MessageBoxButtons.OK, MessageBoxDefaultButton MsgDefaultButton = MessageBoxDefaultButton.Button1)
        {
            if (Title.Length == 0)
            {
                Title = Application.ProductName;

            }

            return Glass.Show(Message, Title, MsgIcon, MsgButtons, MsgDefaultButton);
        }



    }
}

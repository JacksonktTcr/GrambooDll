using Gramboo.Controls;
using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class test : GrbForm
    {
        public test()
        {
            InitializeComponent();
        }
        public override void Init()
        {
            base.Init();
            dgv.DataFields = new List<string>() { "EntryId", "TransId", "ItemId", "[Item Name]","OldType", "Gwt","LessPerc", "LessWt","Mud","StWt","StoneAmount", "NetWt",
           "Rate", "Amount","DiaNo" ,"DiaWt","DiaRate","DiaAmount","TotalCash","IsReceipt" };
            dgv.HiddenDataFields = new List<string>() { "EntryId", "TransId", "IsReceipt", "ItemId", "LessPerc" };
            dgv.ShowSerialNo = true;
            dgv.SummaryColumns = new string[] { "Gwt", "NetWt", "Amount", "LessWt", "DiaAmount", "DiaNo", "DiaAmount", "TotalCash" };
            dgv.Fill(new Table(SAFA.Classes.Common.DbName, "SALE", "VOldGoldReceiptMaterialsNewEst", true), "1=2");
           

            dgv.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
        }
    }
}

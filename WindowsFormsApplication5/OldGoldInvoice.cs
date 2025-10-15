using Gramboo;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Kallans.Classes
{
    public static class OldGoldInvoice
    {
        public static Kallans.Forms.GENERALFORMS.Login log;
        private static DataController dc = new DataController();



        public static int Comp_pin, Qty, SumQty, DiaNo, ExQty, Qty_Ret;
        public static string Company, Comp_Add1, Comp_Add2, Comp_Place, Comp_City, Comp_Dist, Comp_State, Comp_phone, ProdCode, ItemName,
        TinNo, CstNo, InvoiceNo, CustomerName, Address1, PhoneNo, InvoiceDate, oldGoldNo, BranchName, oldGoldNoSales;
        public static float DisAmt = 0, oldGoldRpt = 0, netTotal = 0, TaxAmt = 0, DiaWt = 0, DiaCash = 0, CashPaid = 0, AmtPayable = 0,
        FinalAmt = 0, Gwt = 0, StWt = 0, StoneCash = 0, Total = 0, VaPerc = 0, VaPercAftDis = 0, NetWt = 0,
        SGwt = 0, SStWt = 0, SStoneCash = 0, SNetWt = 0, STotAmt = 0, GrandTotal = 0, TaxPerc = 0, MC = 0, Wst = 0, WstCash = 0;
        public static string ExItemname, SalesMan;
        public static float ExGwt = 0, ExStWt = 0, ExStCash = 0, ExNetWt = 0, ExAmount = 0,
          SExGwt = 0, SExStWt = 0, SExStCash = 0, SExNetWt = 0, SExAmount = 0, SMC = 0, SWst = 0, SWstCash = 0;
        public static string ProdCode_Ret, ItemName_Ret;
        public static float Gwt_Ret = 0, StWt_Ret = 0, NetWt_Ret = 0, VAPerc_Ret = 0, StCash_Ret = 0, Amount_Ret = 0,
        SGwt_Ret = 0, SStWt_Ret = 0, SNetWt_Ret = 0, SStCash_Ret = 0, SAmount_Ret = 0, MC_Ret = 0,
        Wst_Ret = 0, WstCash_Ret = 0, SMC_Ret = 0, SWst_Ret = 0, SWstCash_Ret = 0,
        TotalCashPaid = 0, TotalWtPaid = 0, TotalOldGoldReceipt = 0, BalanceAmount = 0, totalProdVal = 0, CshPaid = 0, CreditPaid = 0,
        CreditCardPaid = 0, AdvancePaid = 0, SchemeAmount1;
        public static string InvNo, InvDate, CustName, CreatedBy, OldNo;
        public static int compId;
        public static float SchemeAmount;
        // public static string SchemeAmount;
        //     OldGold
        public static string InvNo_Old, InvDate_Old, CustName_Old, CreatedBy_Old, VchDate_Old, VchNo_Old, PSalesNo_Old;

        #region OldGold
        public static void NotePadInvoiceOldGold(string EntyId)
        {
            Gramboo.GRBConfig c = Gramboo.GRBConfig.Open();
            string UserId = Convert.ToString(c.Login.UserId);
            compId = c.Login.CompanyID;

            NotepadPrintHelper p = new NotepadPrintHelper();
            p.OpenWriter(Path.GetTempPath() + "OldGoldinvoice.txt");
            p.FontStyle = NotepadPrintHelper.FontStyles.Big;

            string[] entryIds = EntyId.Split(',');

            
            foreach (string EntryId in entryIds)
            {

                using (DataTable dt = dc.GetData(new System.Data.SqlClient.SqlCommand
                  ("Select [Voucher No],[Sales No],BranchName,[Voucher Date],Comp_Phone,[Customer Name],Comp_Name,Comp_Addr1,Comp_Addr2,Comp_Place,Comp_City,Comp_District,Comp_State,Comp_Pin,Comp_Phone,Comp_TIN,Comp_CST FROM SALE.VOldGoldReceiptPrint WHERE EntryId='" + EntryId + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        Company = (dt.Rows[0]["Comp_Name"] == null ? "" : dt.Rows[0]["Comp_Name"].ToString());
                        Comp_Add1 = (dt.Rows[0]["Comp_Addr1"] == null ? "" : dt.Rows[0]["Comp_Addr1"].ToString());
                        Comp_Add2 = (dt.Rows[0]["Comp_Addr2"] == null ? "" : dt.Rows[0]["Comp_Addr2"].ToString());
                        Comp_Place = (dt.Rows[0]["Comp_Place"] == null ? "" : dt.Rows[0]["Comp_Place"].ToString());
                        Comp_City = (dt.Rows[0]["Comp_City"] == null ? "" : dt.Rows[0]["Comp_City"].ToString());
                        Comp_Dist = (dt.Rows[0]["Comp_District"] == null ? "" : dt.Rows[0]["Comp_District"].ToString());
                        Comp_State = (dt.Rows[0]["Comp_State"] == null ? "" : dt.Rows[0]["Comp_State"].ToString());

                        Comp_phone = (dt.Rows[0]["Comp_Phone"] == null ? "" : dt.Rows[0]["Comp_Phone"].ToString());
                        TinNo = (dt.Rows[0]["Comp_TIN"] == null ? "" : dt.Rows[0]["Comp_TIN"].ToString());
                        CstNo = (dt.Rows[0]["Comp_CST"] == null ? "" : dt.Rows[0]["Comp_CST"].ToString());
                        VchDate_Old = (dt.Rows[0]["Voucher Date"] == null ? "" : dt.Rows[0]["Voucher Date"].ToString());
                        VchNo_Old = (dt.Rows[0]["Voucher No"] == null ? "" : dt.Rows[0]["Voucher No"].ToString());
                        CustName_Old = (dt.Rows[0]["Customer Name"] == null ? "" : dt.Rows[0]["Customer Name"].ToString());
                        BranchName = (dt.Rows[0]["BranchName"] == null ? "" : dt.Rows[0]["BranchName"].ToString());
                        PSalesNo_Old = (dt.Rows[0]["Sales No"] == null ? "" : dt.Rows[0]["Sales No"].ToString());
                    }
                }

                //NotepadPrintHelper p = new NotepadPrintHelper();
                p.OpenWriter(Path.GetTempPath() + "OldGoldinvoice.txt");
                p.FontStyle = NotepadPrintHelper.FontStyles.Big;
                p.NewLine = true;
                p.PrintAlignment = PrintAlign.Center;

                if (compId == 1)
                {
                    p.PrintString(Company, 80);
                    p.FontStyle = NotepadPrintHelper.FontStyles.Regular;
                    p.PrintString(Comp_Add1 + "," + Comp_Add2, 80);
                    p.PrintString("PHONE:" + Comp_phone, 80);
                    p.PrintAlignment = PrintAlign.Left;
                    p.PrintString("TIN :" + TinNo);
                    p.PrintString("");
                    p.PrintString("");
                    p.PrintAlignment = PrintAlign.Left;
                    p.PrintString("");
                    p.PrintAlignment = PrintAlign.Center;

                    p.PrintString("THE KERALA VALUE ADDED TAX RULES,2005");
                    p.PrintString("FORM NO. 8E");
                    p.PrintString("PURCHASE BILL");
                    p.PrintString("CASH");

                }
                else
                {

                    p.PrintString("OLD GOLD PURCHASE BILL");
                }
                p.FontStyle = NotepadPrintHelper.FontStyles.Regular;
                p.PrintString(" ");
                p.PrintString(" ");

                p.PrintAlignment = PrintAlign.Left;

                p.NewLine = false;
                p.PrintString("BILL No.    :" + VchNo_Old, 50);
                p.PrintString("Date : " + VchDate_Old);
                p.NewLine = true;
                p.PrintAlignment = PrintAlign.Left;
                p.PrintString("Customer Name  :" + CustName_Old);
                p.NewLine = true;
                p.PrintAlignment = PrintAlign.Left;
                p.PrintLine('=', 80);

                p.NewLine = false;
                p.PrintAlignment = PrintAlign.Left;
                p.PrintString("ItemName", 20);
                p.PrintAlignment = PrintAlign.Right;
                p.PrintString("Gwt", 10);
                p.PrintString("LessWt", 10);
                p.PrintString("NetWt", 10);
                p.PrintString("Rate", 10);
                p.PrintString("Amount", 20);
                p.NewLine = true;
                p.PrintAlignment = PrintAlign.Left;
                p.PrintLine('-', 80);
                p.NewLine = true;
                p.PrintAlignment = PrintAlign.Left;
                using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
                ("Select [Item Name],LessWt,Gwt,Rate,NetWt,Amount FROM SALE.VOldGoldReceiptMaterialsNew WHERE EntryId=" + EntryId + "")).Tables[0])
                {
                    foreach (DataRow r in dtdetails.Rows)
                    {

                        p.PrintAlignment = PrintAlign.Left;

                        p.NewLine = false;
                        p.PrintString("" + r["Item Name"].ToString(), 20);
                        p.PrintAlignment = PrintAlign.Right;
                        p.PrintString("" + Convert.ToSingle(r["Gwt"].ToString()).ToString("F2"), 10);
                        p.PrintString("" + Convert.ToSingle(r["LessWt"].ToString()).ToString("F2"), 10);
                        p.PrintString("" + Convert.ToSingle(r["NetWt"].ToString()).ToString("F2"), 10);
                        p.PrintString("" + Convert.ToSingle(r["Rate"].ToString()).ToString("F2"), 10);
                        p.PrintString("" + Convert.ToSingle(r["Amount"].ToString()).ToString("F2"), 20);
                        p.NewLine = true;

                    }
                    p.NewLine = true;
                    p.PrintAlignment = PrintAlign.Left;
                    p.PrintLine('-', 80);
                    p.NewLine = false;
                    p.PrintString("Total", 20);

                }
                float SGwt = 0, SLessWt = 0, SNetWt = 0, SRate = 0, SAmount = 0;
                using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
               ("Select ISNULL(SUM(LessWt),0) as TotLessWt,ISNULL(SUM(Gwt),0) as TotGwt, ISNULL(SUM(NetWt),0) as TotNetWt,ISNULL(SUM(Amount),0)  as TotAmount FROM SALE.VOldGoldReceiptMaterialsNew WHERE EntryId= " + EntryId + " ")).Tables[0])
                {
                    DataRow r = dtdetails.Rows[0];
                    SGwt = Convert.ToSingle(r["TotGwt"].ToString());
                    SLessWt = Convert.ToSingle(r["TotLessWt"].ToString());
                    SNetWt = Convert.ToSingle(r["TotNetWt"].ToString());
                    SAmount = Convert.ToSingle(r["TotAmount"].ToString());

                    p.PrintAlignment = PrintAlign.Right;
                    p.PrintString("" + SGwt.ToString("F2") , 10);
                    p.PrintString("" + SLessWt.ToString("F2") , 10);
                    p.PrintString("" + SNetWt.ToString("F2"), 10);
                    p.PrintString("" + SAmount.ToString("F2"), 30);
                }
                p.NewLine = true;
                p.PrintAlignment = PrintAlign.Left;
                p.PrintLine('-', 80);
                p.PrintAlignment = PrintAlign.Right;

                float TotAmt, LessAmt, NetAmt;
                using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
         ("Select  [Sales No],TotalAmount,LessAmount,NetAmount  FROM SALE.VOldGoldReceiptList WHERE EntryId= " + EntryId + " ")).Tables[0])
                {
                    DataRow r = dtdetails.Rows[0];

                    TotAmt = Convert.ToSingle(r["TotalAmount"].ToString());
                    LessAmt = Convert.ToSingle(r["LessAmount"].ToString());
                    NetAmt = Convert.ToSingle(r["NetAmount"].ToString());


                    p.PrintString("Total Value:".PadRight(40 - TotAmt.ToString("f2").Length) + TotAmt.ToString("f2"));
                    p.PrintString("Discount:".PadRight(40 - LessAmt.ToString("f2").Length) + LessAmt.ToString("f2"));
                    p.PrintString("Net Purchase Cost:".PadRight(40 - NetAmt.ToString("f2").Length) + NetAmt.ToString("f2"));
                    if (r["Sales No"].ToString().Length > 0)
                        p.PrintString("SALES INVOICE:".PadRight(40 - r["Sales No"].ToString().Length) + r["Sales No"].ToString());
                }

                p.PrintAlignment = PrintAlign.Left;
                p.PrintLine('-', 100);
                p.PrintString("NET PURCHASE COST IN WORDS : " + ToWords.ConvertToWords(NetAmt));
                p.PrintLine('-', 100);
                p.PrintString("E&OE");
                p.PrintString(" ");
                p.PrintAlignment = PrintAlign.Right;
                p.PrintString("Authorised Signatory ");
                p.PrintAlignment = PrintAlign.Left;
                p.Print();


            }
           // p.CloseWriter();
        }
       


        #endregion
       
    }
}

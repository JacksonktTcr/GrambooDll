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
    public static class Invoice
    {
        // public static DataController DBConn;
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
        public static int count;
        // public static string SchemeAmount;
        //     OldGold
        public static string InvNo_Old, InvDate_Old, CustName_Old, CreatedBy_Old, VchDate_Old, VchNo_Old, PSalesNo_Old;


    
        #region Sales Entry
        public static bool PrintDotMatrixOld()
        {
            if (oldGoldNoSales == "")
                return false;

            using (DataTable dt = dc.GetData(new System.Data.SqlClient.SqlCommand
             ("Select [Voucher No],[Sales No],BranchName,[Voucher Date],[Sales Man Name],Comp_Phone,[Customer Name],Comp_Name,Comp_Addr1,Comp_Addr2,Comp_Place,Comp_City,Comp_District,Comp_State,Comp_Pin,Comp_Phone,Comp_TIN,Comp_CST FROM SALE.VOldGoldReceiptPrint WHERE EntryId=" + oldGoldNoSales + "")).Tables[0])
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
                    InvoiceDate = (dt.Rows[0]["Voucher Date"] == null ? "" : dt.Rows[0]["Voucher Date"].ToString());
                    InvoiceNo = (dt.Rows[0]["Voucher No"] == null ? "" : dt.Rows[0]["Voucher No"].ToString());
                    CustomerName = (dt.Rows[0]["Customer Name"] == null ? "" : dt.Rows[0]["Customer Name"].ToString());
                    BranchName = (dt.Rows[0]["BranchName"] == null ? "" : dt.Rows[0]["BranchName"].ToString());
                    oldGoldNo = (dt.Rows[0]["Sales No"] == null ? "" : dt.Rows[0]["Sales No"].ToString());
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //#endregion

   
        public static void NotepadInvoiceSales(string SalsId)
        {
          

            NotepadPrintHelper p = new NotepadPrintHelper();
            p.OpenWriter(Path.GetTempPath() + "invoice.txt");

            Gramboo.GRBConfig c = Gramboo.GRBConfig.Open();
            string UserId = Convert.ToString(c.Login.UserId);
            compId = c.Login.CompanyID;

            string[] entryIds = SalsId.Split(',');
            count = entryIds.Length;


            foreach (string SalesId in entryIds)
            {
                if (!PrintDotMatrix(SalesId)) return;

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
                    p.PrintString("FORM NO. 8J");
                    p.PrintString("RETAIL INVOICE");
                    p.PrintString("CASH");
                }
                else
                {

                    p.FontStyle = NotepadPrintHelper.FontStyles.Regular;
                    p.PrintString("SALES ORDER/INVOICE");
                    p.PrintAlignment = PrintAlign.Left;
                    p.PrintString("TIN :" + TinNo);
                }
                p.PrintString("  ");
                p.PrintString("  ");
                p.PrintAlignment = PrintAlign.Left;

                p.NewLine = false;
                p.PrintString("INVOICE No: " + InvoiceNo, 30);

                p.PrintAlignment = PrintAlign.Right;
                p.PrintString("Date :  " + InvoiceDate, 50);
                p.NewLine = true;
                p.PrintAlignment = PrintAlign.Left;
                p.PrintString("Name & Address  " + CustomerName);
                p.PrintString("Phone:  ...........");

                p.PrintLine('-', 80);

                p.NewLine = false;

                p.PrintAlignment = PrintAlign.Left;
                p.PrintString("ProdCode", 12);
                p.PrintString("ItemName", 12);

                p.PrintAlignment = PrintAlign.Right;
                p.PrintString("Qty", 5);
                p.PrintString("Wt", 8);
                p.PrintString("St.Wt", 8);
                p.PrintString("Gr.Wt", 8);
                p.PrintString("VA", 8);
                p.PrintString("St.Cash", 8);
                p.PrintString("Amount", 10);
                p.NewLine = true;
                p.PrintLine('-', 80);


                #region SALES

                float TotQty = 0, TotGwt = 0, TotNetWt = 0, TotStWt = 0, TotVa = 0, TotAmt = 0, TotStCash = 0;

                using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
            ("Select prodCode,ItemName,Qty,Gwt,StoneWt,NetWt,StoneCash,VA,VAPerc,TotalAmount,VApercAftDis,MC,Wastage,WastageCash FROM SALE.SalesDotPrint WHERE SalesId='" + SalesId + "' and Type='S'")).Tables[0])
                {
                    if (dtdetails.Rows.Count > 0)
                    {

                        foreach (DataRow r in dtdetails.Rows)
                        {

                            ProdCode = r["prodCode"].ToString();
                            ItemName = r["ItemName"].ToString();
                            Qty = Convert.ToInt32(r["Qty"].ToString());
                            Gwt = Convert.ToSingle(r["Gwt"].ToString());
                            StWt = Convert.ToSingle(r["StoneWt"].ToString());
                            NetWt = Convert.ToSingle(r["NetWt"].ToString());
                            StoneCash = Convert.ToSingle(r["StoneCash"].ToString());
                            VaPerc = Convert.ToSingle(r["VAPerc"].ToString());
                            Total = Convert.ToSingle(r["TotalAmount"].ToString());
                            VaPercAftDis = Convert.ToSingle(r["VApercAftDis"].ToString());
                            MC = Convert.ToSingle(r["MC"].ToString());
                            Wst = Convert.ToSingle(r["Wastage"].ToString());
                            WstCash = Convert.ToSingle(r["WastageCash"].ToString());
                            p.PrintAlignment = PrintAlign.Left;

                            p.NewLine = false;
                            p.PrintString((ProdCode.Length == 0 ? " " : ProdCode), 12);

                            p.PrintString(ItemName, 12);
                            p.PrintAlignment = PrintAlign.Right;
                            p.PrintString(Qty.ToString(), 5);
                            p.PrintString(NetWt.ToString("f2"), 8);
                            p.PrintString(StWt.ToString("f2"), 8);
                            p.PrintString(Gwt.ToString("f2"), 8);
                            p.PrintString(r["VA"].ToString(), 8);
                            p.PrintString(StoneCash.ToString("f2"), 8);
                            p.PrintString(Total.ToString("f2"), 10);
                            p.NewLine = true;

                            TotQty += Qty; TotGwt += Gwt; TotNetWt += NetWt; TotStWt += StWt; TotVa += Convert.ToSingle(r["VA"]); TotAmt += Total; TotStCash += StoneCash;

                        }

                        p.NewLine = true;
                        p.PrintAlignment = PrintAlign.Left;
                        p.PrintLine('-', 80);
                        p.NewLine = false;
                        p.PrintAlignment = PrintAlign.Right;
                        p.PrintString(TotQty.ToString(), 29);
                        p.PrintString(TotNetWt.ToString("f2"), 8);
                        p.PrintString(TotStWt.ToString("f2"), 8);
                        p.PrintString(TotGwt.ToString("f2"), 8);
                        p.PrintString(TotVa.ToString(), 8);
                        p.PrintString(TotStCash.ToString("f2"), 8);
                        p.PrintString(TotAmt.ToString("f2"), 10);
                        p.NewLine = true;
                        p.PrintAlignment = PrintAlign.Left;
                        p.PrintLine('-', 80);
                        p.PrintAlignment = PrintAlign.Right;
                    }
                }
                #endregion

                #region SALES RETURN

                TotQty = 0; TotGwt = 0; TotNetWt = 0; TotStWt = 0; TotVa = 0; TotAmt = 0; TotStCash = 0;

                using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
            ("Select prodCode,ItemName,Qty,Gwt,StoneWt,NetWt,StoneCash,VA,VAPerc,TotalAmount,VApercAftDis,MC,Wastage,WastageCash FROM SALE.SalesDotPrint WHERE SalesId='" + SalesId + "' and Type='R'")).Tables[0])
                {

                    p.PrintAlignment = PrintAlign.Center;
                    if (dtdetails.Rows.Count > 0)
                    {
                        p.PrintString("SALES RETURN DETAILS");
                        p.PrintString(" ");
                        p.PrintAlignment = PrintAlign.Left;
                        foreach (DataRow r in dtdetails.Rows)
                        {

                            ProdCode = r["prodCode"].ToString();
                            ItemName = r["ItemName"].ToString();
                            Qty = Convert.ToInt32(r["Qty"].ToString());
                            Gwt = Convert.ToSingle(r["Gwt"].ToString());
                            StWt = Convert.ToSingle(r["StoneWt"].ToString());
                            NetWt = Convert.ToSingle(r["NetWt"].ToString());
                            StoneCash = Convert.ToSingle(r["StoneCash"].ToString());
                            VaPerc = Convert.ToSingle(r["VAPerc"].ToString());
                            Total = Convert.ToSingle(r["TotalAmount"].ToString());
                            VaPercAftDis = Convert.ToSingle(r["VApercAftDis"].ToString());
                            MC = Convert.ToSingle(r["MC"].ToString());
                            Wst = Convert.ToSingle(r["Wastage"].ToString());
                            WstCash = Convert.ToSingle(r["WastageCash"].ToString());
                            p.PrintAlignment = PrintAlign.Left;

                            p.NewLine = false;
                            p.PrintString((ProdCode.Length == 0 ? " " : ProdCode), 12);

                            p.PrintString(ItemName, 12);
                            p.PrintAlignment = PrintAlign.Right;
                            p.PrintString(Qty.ToString(), 5);
                            p.PrintString(NetWt.ToString("f2"), 8);
                            p.PrintString(StWt.ToString("f2"), 8);
                            p.PrintString(Gwt.ToString("f2"), 8);
                            p.PrintString(r["VA"].ToString(), 8);
                            p.PrintString(StoneCash.ToString("f2"), 8);
                            p.PrintString(Total.ToString("f2"), 10);
                            p.NewLine = true;
                            TotQty += Qty; TotGwt += Gwt; TotNetWt += NetWt; TotStWt += StWt; TotVa += Convert.ToSingle(r["VA"]); TotAmt += Total; TotStCash += StoneCash;

                        }

                        p.NewLine = true;
                        p.PrintAlignment = PrintAlign.Left;
                        p.PrintLine('-', 80);
                        p.NewLine = false;
                        p.PrintAlignment = PrintAlign.Right;
                        p.PrintString(TotQty.ToString(), 29);
                        p.PrintString(TotNetWt.ToString("f2"), 8);
                        p.PrintString(TotStWt.ToString("f2"), 8);
                        p.PrintString(TotGwt.ToString("f2"), 8);
                        p.PrintString(TotVa.ToString(), 8);
                        p.PrintString(TotStCash.ToString("f2"), 8);
                        p.PrintString(TotAmt.ToString("f2"), 10);
                        p.NewLine = true;
                        p.PrintAlignment = PrintAlign.Left;
                        p.PrintLine('-', 80);
                        p.PrintAlignment = PrintAlign.Right;

                    }
                }
                #endregion


                #region  OLD GOLD

                if (compId == 2)
                {
                    if (oldGoldNo != "")
                    {
                        p.NewLine = true;

                        p.PrintAlignment = PrintAlign.Center;
                        p.PrintString("OLD GOLD DETAILS");
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
                        ("Select [Item Name],LessWt,Gwt,Rate,NetWt,Amount FROM SALE.VOldGoldReceiptMaterialsNew WHERE EntryId='" + oldGoldNo + "'")).Tables[0])
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


                            }
                            p.NewLine = true;
                            p.PrintAlignment = PrintAlign.Left;
                            p.PrintLine('-', 80);
                            p.NewLine = false;
                            p.PrintString("Total", 20);

                        }
                        float SGwt = 0, SLessWt = 0, SNetWt = 0, SRate = 0, SAmount = 0;
                        using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
                       ("Select ISNULL(SUM(LessWt),0) as TotLessWt,ISNULL(SUM(Gwt),0) as TotGwt, ISNULL(SUM(NetWt),0) as TotNetWt,ISNULL(SUM(Amount),0)  as TotAmount FROM SALE.VOldGoldReceiptMaterialsNew WHERE EntryId= " + oldGoldNo + " ")).Tables[0])
                        {
                            DataRow r = dtdetails.Rows[0];
                            SGwt = Convert.ToSingle(r["TotGwt"].ToString());
                            SLessWt = Convert.ToSingle(r["TotLessWt"].ToString());
                            SNetWt = Convert.ToSingle(r["TotNetWt"].ToString());
                            SAmount = Convert.ToSingle(r["TotAmount"].ToString());

                            p.PrintAlignment = PrintAlign.Right;
                            p.PrintString("" + SGwt, 10);
                            p.PrintString("" + SLessWt, 10);
                            p.PrintString("" + SNetWt, 10);
                            p.PrintString("" + SAmount, 30);
                        }

                        //       float TotAmt, LessAmt, NetAmt;
                        //       using (DataTable dtdetails = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                        //("Select  [Sales No],TotalAmount,LessAmount,NetAmount  FROM SALE.VOldGoldReceiptList WHERE EntryId= " + txt_OldGoldReceipt.Tag + " ")).Tables[0])
                        //       {
                        //           DataRow r = dtdetails.Rows[0];

                        //           TotAmt = Convert.ToSingle(r["TotalAmount"].ToString());
                        //           LessAmt = Convert.ToSingle(r["LessAmount"].ToString());
                        //           NetAmt = Convert.ToSingle(r["NetAmount"].ToString());


                        //           p.PrintString("Total Value:".PadRight(40 - TotAmt.ToString("f2").Length) + TotAmt.ToString("f2"));
                        //           p.PrintString("Discount:".PadRight(40 - LessAmt.ToString("f2").Length) + LessAmt.ToString("f2"));
                        //           p.PrintString("Net Purchase Cost:".PadRight(40 - NetAmt.ToString("f2").Length) + NetAmt.ToString("f2"));
                        //       }
                    }
                }
                #endregion
                p.NewLine = true;
                p.PrintLine('-');
                p.PrintAlignment = PrintAlign.Right;
                p.PrintString("Net Value ".PadRight(30 - totalProdVal.ToString("f2").Length) + totalProdVal.ToString("f2"));
                p.PrintString("Cash Discount ".PadRight(30 - DisAmt.ToString("f2").Length) + DisAmt.ToString("f2"));
                if (Convert.ToSingle(TaxAmt) != 0)
                    p.PrintString("Vat (@ of " + TaxPerc + "%) ".PadRight(15 - TaxAmt.ToString("f2").Length) + TaxAmt.ToString("f2"));
                //p.PrintLine('-', 40);
                //p.PrintString("Grand Total  ".PadRight(30 - txtNetTotal.Text.Length) + txtNetTotal.Text);
                //p.PrintLine('-', 40);
                if (SAmount_Ret != 0)
                    p.PrintString(("Return Amount ").PadRight(30 - SAmount_Ret.ToString("f2").Length) + SAmount_Ret.ToString("f2"));

                if (oldGoldRpt != 0)
                    p.PrintString(("Old Bill No " + OldNo).PadRight(30 - oldGoldRpt.ToString("f2").Length) + oldGoldRpt.ToString("f2"));

                double AdvPaid = Convert.ToDouble((AdvancePaid.ToString() == "" ? "0" : AdvancePaid.ToString("f2")));

                if (AdvPaid != 0)
                    p.PrintString(("Advance Amount ").PadRight(30 - AdvPaid.ToString("f2").Length) + AdvPaid.ToString("f2"));

                double SchemeAmt = Convert.ToDouble((SchemeAmount.ToString() == "" ? "0" : SchemeAmount.ToString("f2")));

                if (SchemeAmt != 0)
                    p.PrintString(("Scheme Amount ").PadRight(30 - SchemeAmt.ToString("f2").Length) + SchemeAmt.ToString("f2"));

                p.PrintLine('-', 20);
                if (CreditCardPaid > 0)
                {
                    if (CreditCardPaid < CashPaid)
                        p.PrintString(("Cash Paid ").PadRight(30 - (CashPaid - CreditCardPaid).ToString("f2").Length) + (CashPaid - CreditCardPaid).ToString("f2"));
                    if (SchemeAmt != 0)
                        p.PrintString(("Credit Card Paid ").PadRight(30 - CreditCardPaid.ToString("f2").Length) + CreditCardPaid.ToString("f2"));

                }
                p.PrintLine('-', 20);
                double netamt = Convert.ToDouble(netTotal) - (SAmount_Ret + oldGoldRpt + AdvPaid + SchemeAmt);

                p.FontStyle = NotepadPrintHelper.FontStyles.Big;

                if (netamt >= 0)
                    p.PrintString("Net Total  ".PadRight(30 - netamt.ToString("f2").Length) + netamt.ToString("f2"));
                else
                    p.PrintString("Refund Amount ".PadRight(30 - netamt.ToString("f2").Length) + netamt.ToString("f2"));

                //p.PrintLine('-', 20);
                p.FontStyle = NotepadPrintHelper.FontStyles.Regular;

                p.PrintAlignment = PrintAlign.Left;
                p.PrintLine('-', 100);
                p.PrintString("GRAND TOTAL IN WORDS : " + ToWords.ConvertToWords(netamt));
                p.PrintLine('-', 100);
                p.PrintString("Sales Man :" + SalesMan);
                p.PrintString("E&OE");
                p.PrintString(" ");
                p.PrintAlignment = PrintAlign.Right;
                p.PrintString("Authorised Signatory ");
                p.PrintAlignment = PrintAlign.Left;
                p.PrintString(CreatedBy);
                p.PrintString("THANKS, VISIT AGAIN");
                p.PrintString(" ");
                p.PrintString(" ");
                p.FontStyle = NotepadPrintHelper.FontStyles.Regular;
                p.PrintString("Note : For exchange, making charge will be DEDUCTED after ");
                p.PrintString("seven days from the date of purchase");

                p.Print();


                

                if (compId == 1 && count ==1)
                {
                    printOldGoldSales();
                }
            }
            p.CloseWriter();
        }


        public static bool PrintDotMatrix(string SalesId)
        {


            using (DataTable dt = dc.GetData(new System.Data.SqlClient.SqlCommand
              ("Select [Invoice No],[Created By],OldGoldId,[Sales Man Name],AdvancePaid,BranchName,[Invoice Date],PhoneNo,CreditPaid,DiscAmount,GrandTotal,[old Gold No],TaxPerc,TotalTaxAmount,NetTotal,TotalProdVal,TotalCashPaid,TotalOldGoldReceipt,FinalAmount,[Customer Name],Address1,Address2,Comp_Name,Comp_Addr1,Comp_Addr2,Comp_Place,Comp_City,Comp_District,Comp_State,Comp_Pin,Comp_Phone,Comp_TIN,Comp_CST,TotalReturnAmount,SchemeAmount FROM SALE.VSalesMaster WHERE SalesId='" + SalesId + "'")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    Company = (dt.Rows[0]["Comp_Name"] == null ? "" : dt.Rows[0]["Comp_Name"].ToString());
                    SalesMan = (dt.Rows[0]["Comp_Name"] == null ? "" : dt.Rows[0]["Sales Man Name"].ToString());
                    Comp_Add1 = (dt.Rows[0]["Comp_Addr1"] == null ? "" : dt.Rows[0]["Comp_Addr1"].ToString());
                    Comp_Add2 = (dt.Rows[0]["Comp_Addr2"] == null ? "" : dt.Rows[0]["Comp_Addr2"].ToString());
                    Comp_Place = (dt.Rows[0]["Comp_Place"] == null ? "" : dt.Rows[0]["Comp_Place"].ToString());
                    Comp_City = (dt.Rows[0]["Comp_City"] == null ? "" : dt.Rows[0]["Comp_City"].ToString());
                    Comp_Dist = (dt.Rows[0]["Comp_District"] == null ? "" : dt.Rows[0]["Comp_District"].ToString());
                    Comp_State = (dt.Rows[0]["Comp_State"] == null ? "" : dt.Rows[0]["Comp_State"].ToString());
                    Comp_pin = Convert.ToInt32(dt.Rows[0]["Comp_Pin"] == null ? "" : dt.Rows[0]["Comp_Pin"].ToString());
                    Comp_phone = (dt.Rows[0]["Comp_Phone"] == null ? "" : dt.Rows[0]["Comp_Phone"].ToString());
                    TinNo = (dt.Rows[0]["Comp_TIN"] == null ? "" : dt.Rows[0]["Comp_TIN"].ToString());
                    CstNo = (dt.Rows[0]["Comp_CST"] == null ? "" : dt.Rows[0]["Comp_CST"].ToString());
                    InvoiceDate = (dt.Rows[0]["Invoice Date"] == null ? "" : dt.Rows[0]["Invoice Date"].ToString());
                    DisAmt = Convert.ToSingle(dt.Rows[0]["DiscAmount"] == null ? "" : dt.Rows[0]["DiscAmount"].ToString());
                    oldGoldRpt = Convert.ToSingle(dt.Rows[0]["TotalOldGoldReceipt"] == null ? "" : dt.Rows[0]["TotalOldGoldReceipt"].ToString());
                    netTotal = Convert.ToSingle(dt.Rows[0]["NetTotal"] == null ? "" : dt.Rows[0]["NetTotal"].ToString());
                    SAmount_Ret = Convert.ToSingle(dt.Rows[0]["TotalReturnAmount"] == null ? "" : dt.Rows[0]["TotalReturnAmount"].ToString());
                    TaxAmt = Convert.ToSingle(dt.Rows[0]["TotalTaxAmount"] == null ? "" : dt.Rows[0]["TotalTaxAmount"].ToString());
                    FinalAmt = Convert.ToSingle(dt.Rows[0]["FinalAmount"] == null ? "" : dt.Rows[0]["FinalAmount"].ToString());
                    InvoiceNo = (dt.Rows[0]["Invoice No"] == null ? "" : dt.Rows[0]["Invoice No"].ToString());
                    CustomerName = (dt.Rows[0]["Customer Name"] == null ? "" : dt.Rows[0]["Customer Name"].ToString());
                    Address1 = (dt.Rows[0]["Address1"] == null ? "" : dt.Rows[0]["Address1"].ToString());
                    PhoneNo = (dt.Rows[0]["PhoneNo"] == null ? "" : dt.Rows[0]["PhoneNo"].ToString());
                    GrandTotal = Convert.ToSingle(dt.Rows[0]["GrandTotal"] == null ? "" : dt.Rows[0]["GrandTotal"].ToString());
                    TaxPerc = Convert.ToSingle(dt.Rows[0]["TaxPerc"] == null ? "" : dt.Rows[0]["TaxPerc"].ToString());
                    OldNo = (dt.Rows[0]["old Gold No"] == null ? "" : dt.Rows[0]["old Gold No"].ToString());
                    BranchName = (dt.Rows[0]["BranchName"] == null ? "" : dt.Rows[0]["BranchName"].ToString());
                    CashPaid = Convert.ToSingle(dt.Rows[0]["TotalCashPaid"] == null ? "" : dt.Rows[0]["TotalCashPaid"].ToString());
                    CreditCardPaid = Convert.ToSingle(dt.Rows[0]["CreditPaid"] == null ? "" : dt.Rows[0]["CreditPaid"].ToString());
                    oldGoldNoSales = (dt.Rows[0]["OldGoldId"] == null ? "" : dt.Rows[0]["OldGoldId"].ToString());
                    CreatedBy = (dt.Rows[0]["Created By"] == null ? "" : dt.Rows[0]["Created By"].ToString());
                    totalProdVal = Convert.ToSingle(dt.Rows[0]["TotalProdVal"] == null ? "" : dt.Rows[0]["TotalProdVal"].ToString());
                    AdvancePaid = Convert.ToSingle(dt.Rows[0]["AdvancePaid"] == null ? "" : dt.Rows[0]["AdvancePaid"].ToString());
                    // SchemeAmount = Convert.ToSingle    (dt.Rows[0]["SchemeAmount"] == null ? "" : dt.Rows[0]["SchemeAmount"].ToString());
                    AmtPayable = (netTotal - CashPaid);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public static void printOldGoldSales()
        {
            if (oldGoldNoSales == "0")
                return;

            if (!PrintDotMatrixOld())
                return;

            NotepadPrintHelper p = new NotepadPrintHelper();
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
            p.PrintString("BILL No.    :" + InvoiceNo, 50);
            p.PrintString("Date : " + InvoiceDate);
            p.NewLine = true;
            p.PrintAlignment = PrintAlign.Left;
            p.PrintString("Customer Name  :" + CustomerName);
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
            ("Select [Item Name],LessWt,Gwt,Rate,NetWt,Amount FROM SALE.VOldGoldReceiptMaterialsNew WHERE EntryId='" + oldGoldNoSales + "'")).Tables[0])
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
           ("Select ISNULL(SUM(LessWt),0) as TotLessWt,ISNULL(SUM(Gwt),0) as TotGwt, ISNULL(SUM(NetWt),0) as TotNetWt,ISNULL(SUM(Amount),0)  as TotAmount FROM SALE.VOldGoldReceiptMaterialsNew WHERE EntryId= " + oldGoldNoSales + " ")).Tables[0])
            {
                DataRow r = dtdetails.Rows[0];
                SGwt = Convert.ToSingle(r["TotGwt"].ToString());
                SLessWt = Convert.ToSingle(r["TotLessWt"].ToString());
                SNetWt = Convert.ToSingle(r["TotNetWt"].ToString());
                SAmount = Convert.ToSingle(r["TotAmount"].ToString());

                p.PrintAlignment = PrintAlign.Right;
                p.PrintString("" + SGwt, 10);
                p.PrintString("" + SLessWt, 10);
                p.PrintString("" + SNetWt, 10);
                p.PrintString("" + SAmount, 30);
            }
            p.NewLine = true;
            p.PrintAlignment = PrintAlign.Left;
            p.PrintLine('-', 80);
            p.PrintAlignment = PrintAlign.Right;

            float TotAmt, LessAmt, NetAmt;
            using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
     ("Select  [Sales No],TotalAmount,LessAmount,NetAmount  FROM SALE.VOldGoldReceiptList WHERE EntryId= " + oldGoldNoSales + " ")).Tables[0])
            {
                DataRow r = dtdetails.Rows[0];

                TotAmt = Convert.ToSingle(r["TotalAmount"].ToString());
                LessAmt = Convert.ToSingle(r["LessAmount"].ToString());
                NetAmt = Convert.ToSingle(r["NetAmount"].ToString());


                p.PrintString("Total Value:".PadRight(40 - TotAmt.ToString("f2").Length) + TotAmt.ToString("f2"));
                p.PrintString("Discount:".PadRight(40 - LessAmt.ToString("f2").Length) + LessAmt.ToString("f2"));
                p.PrintString("Net Purchase Cost:".PadRight(40 - NetAmt.ToString("f2").Length) + NetAmt.ToString("f2"));

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

            p.CloseWriter();
        }
        #endregion




        //#region OldGold
        //public static void NotePadInvoiceOldGold(string EntyId)
        //{
        //    Gramboo.GRBConfig c = Gramboo.GRBConfig.Open();
        //    string UserId = Convert.ToString(c.Login.UserId);
        //    compId = c.Login.CompanyID;

        //    NotepadPrintHelper p = new NotepadPrintHelper();
        //    p.OpenWriter(Path.GetTempPath() + "OldGoldinvoice.txt");
        //    p.FontStyle = NotepadPrintHelper.FontStyles.Big;

        //    string[] entryIds = EntyId.Split(',');

        //    foreach (string EntryId in entryIds)
        //    {

        //        using (DataTable dt = dc.GetData(new System.Data.SqlClient.SqlCommand
        //          ("Select [Voucher No],[Sales No],BranchName,[Voucher Date],Comp_Phone,[Customer Name],Comp_Name,Comp_Addr1,Comp_Addr2,Comp_Place,Comp_City,Comp_District,Comp_State,Comp_Pin,Comp_Phone,Comp_TIN,Comp_CST FROM SALE.VOldGoldReceiptPrint WHERE EntryId='" + EntryId + "'")).Tables[0])
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                Company = (dt.Rows[0]["Comp_Name"] == null ? "" : dt.Rows[0]["Comp_Name"].ToString());
        //                Comp_Add1 = (dt.Rows[0]["Comp_Addr1"] == null ? "" : dt.Rows[0]["Comp_Addr1"].ToString());
        //                Comp_Add2 = (dt.Rows[0]["Comp_Addr2"] == null ? "" : dt.Rows[0]["Comp_Addr2"].ToString());
        //                Comp_Place = (dt.Rows[0]["Comp_Place"] == null ? "" : dt.Rows[0]["Comp_Place"].ToString());
        //                Comp_City = (dt.Rows[0]["Comp_City"] == null ? "" : dt.Rows[0]["Comp_City"].ToString());
        //                Comp_Dist = (dt.Rows[0]["Comp_District"] == null ? "" : dt.Rows[0]["Comp_District"].ToString());
        //                Comp_State = (dt.Rows[0]["Comp_State"] == null ? "" : dt.Rows[0]["Comp_State"].ToString());

        //                Comp_phone = (dt.Rows[0]["Comp_Phone"] == null ? "" : dt.Rows[0]["Comp_Phone"].ToString());
        //                TinNo = (dt.Rows[0]["Comp_TIN"] == null ? "" : dt.Rows[0]["Comp_TIN"].ToString());
        //                CstNo = (dt.Rows[0]["Comp_CST"] == null ? "" : dt.Rows[0]["Comp_CST"].ToString());
        //                VchDate_Old = (dt.Rows[0]["Voucher Date"] == null ? "" : dt.Rows[0]["Voucher Date"].ToString());
        //                VchNo_Old = (dt.Rows[0]["Voucher No"] == null ? "" : dt.Rows[0]["Voucher No"].ToString());
        //                CustName_Old = (dt.Rows[0]["Customer Name"] == null ? "" : dt.Rows[0]["Customer Name"].ToString());
        //                BranchName = (dt.Rows[0]["BranchName"] == null ? "" : dt.Rows[0]["BranchName"].ToString());
        //                PSalesNo_Old = (dt.Rows[0]["Sales No"] == null ? "" : dt.Rows[0]["Sales No"].ToString());
        //            }
        //        }

        //        //NotepadPrintHelper p = new NotepadPrintHelper();
        //        p.OpenWriter(Path.GetTempPath() + "OldGoldinvoice.txt");
        //        p.FontStyle = NotepadPrintHelper.FontStyles.Big;
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Center;

        //        if (compId == 1)
        //        {
        //            p.PrintString(Company, 80);
        //            p.FontStyle = NotepadPrintHelper.FontStyles.Regular;
        //            p.PrintString(Comp_Add1 + "," + Comp_Add2, 80);
        //            p.PrintString("PHONE:" + Comp_phone, 80);
        //            p.PrintAlignment = PrintAlign.Left;
        //            p.PrintString("TIN :" + TinNo);
        //            p.PrintString("");
        //            p.PrintString("");
        //            p.PrintAlignment = PrintAlign.Left;
        //            p.PrintString("");
        //            p.PrintAlignment = PrintAlign.Center;

        //            p.PrintString("THE KERALA VALUE ADDED TAX RULES,2005");
        //            p.PrintString("FORM NO. 8E");
        //            p.PrintString("PURCHASE BILL");
        //            p.PrintString("CASH");

        //        }
        //        else
        //        {

        //            p.PrintString("OLD GOLD PURCHASE BILL");
        //        }
        //        p.FontStyle = NotepadPrintHelper.FontStyles.Regular;
        //        p.PrintString(" ");
        //        p.PrintString(" ");

        //        p.PrintAlignment = PrintAlign.Left;

        //        p.NewLine = false;
        //        p.PrintString("BILL No.    :" + VchNo_Old, 50);
        //        p.PrintString("Date : " + VchDate_Old);
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintString("Customer Name  :" + CustName_Old);
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintLine('=', 80);

        //        p.NewLine = false;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintString("ItemName", 20);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("Gwt", 10);
        //        p.PrintString("LessWt", 10);
        //        p.PrintString("NetWt", 10);
        //        p.PrintString("Rate", 10);
        //        p.PrintString("Amount", 20);
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintLine('-', 80);
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Left;
        //        using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
        //        ("Select [Item Name],LessWt,Gwt,Rate,NetWt,Amount FROM SALE.VOldGoldReceiptMaterialsNew WHERE EntryId=" + EntryId + "")).Tables[0])
        //        {
        //            foreach (DataRow r in dtdetails.Rows)
        //            {

        //                p.PrintAlignment = PrintAlign.Left;

        //                p.NewLine = false;
        //                p.PrintString("" + r["Item Name"].ToString(), 20);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + Convert.ToSingle(r["Gwt"].ToString()).ToString("F2"), 10);
        //                p.PrintString("" + Convert.ToSingle(r["LessWt"].ToString()).ToString("F2"), 10);
        //                p.PrintString("" + Convert.ToSingle(r["NetWt"].ToString()).ToString("F2"), 10);
        //                p.PrintString("" + Convert.ToSingle(r["Rate"].ToString()).ToString("F2"), 10);
        //                p.PrintString("" + Convert.ToSingle(r["Amount"].ToString()).ToString("F2"), 20);
        //                p.NewLine = true;

        //            }
        //            p.NewLine = true;
        //            p.PrintAlignment = PrintAlign.Left;
        //            p.PrintLine('-', 80);
        //            p.NewLine = false;
        //            p.PrintString("Total", 20);

        //        }
        //        float SGwt = 0, SLessWt = 0, SNetWt = 0, SRate = 0, SAmount = 0;
        //        using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
        //       ("Select ISNULL(SUM(LessWt),0) as TotLessWt,ISNULL(SUM(Gwt),0) as TotGwt, ISNULL(SUM(NetWt),0) as TotNetWt,ISNULL(SUM(Amount),0)  as TotAmount FROM SALE.VOldGoldReceiptMaterialsNew WHERE EntryId= " + EntryId + " ")).Tables[0])
        //        {
        //            DataRow r = dtdetails.Rows[0];
        //            SGwt = Convert.ToSingle(r["TotGwt"].ToString());
        //            SLessWt = Convert.ToSingle(r["TotLessWt"].ToString());
        //            SNetWt = Convert.ToSingle(r["TotNetWt"].ToString());
        //            SAmount = Convert.ToSingle(r["TotAmount"].ToString());

        //            p.PrintAlignment = PrintAlign.Right;
        //            p.PrintString("" + SGwt, 10);
        //            p.PrintString("" + SLessWt, 10);
        //            p.PrintString("" + SNetWt, 10);
        //            p.PrintString("" + SAmount, 30);
        //        }
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintLine('-', 80);
        //        p.PrintAlignment = PrintAlign.Right;

        //        float TotAmt, LessAmt, NetAmt;
        //        using (DataTable dtdetails = dc.GetData(new System.Data.SqlClient.SqlCommand
        // ("Select  [Sales No],TotalAmount,LessAmount,NetAmount  FROM SALE.VOldGoldReceiptList WHERE EntryId= " + EntryId + " ")).Tables[0])
        //        {
        //            DataRow r = dtdetails.Rows[0];

        //            TotAmt = Convert.ToSingle(r["TotalAmount"].ToString());
        //            LessAmt = Convert.ToSingle(r["LessAmount"].ToString());
        //            NetAmt = Convert.ToSingle(r["NetAmount"].ToString());


        //            p.PrintString("Total Value:".PadRight(40 - TotAmt.ToString("f2").Length) + TotAmt.ToString("f2"));
        //            p.PrintString("Discount:".PadRight(40 - LessAmt.ToString("f2").Length) + LessAmt.ToString("f2"));
        //            p.PrintString("Net Purchase Cost:".PadRight(40 - NetAmt.ToString("f2").Length) + NetAmt.ToString("f2"));
        //            if (r["Sales No"].ToString().Length > 0)
        //                p.PrintString("SALES INVOICE:".PadRight(40 - r["Sales No"].ToString().Length) + r["Sales No"].ToString());
        //        }

        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintLine('-', 100);
        //        p.PrintString("NET PURCHASE COST IN WORDS : " + ToWords.ConvertToWords(NetAmt));
        //        p.PrintLine('-', 100);
        //        p.PrintString("E&OE");
        //        p.PrintString(" ");
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("Authorised Signatory ");
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.Print();


        //    }
        //    p.CloseWriter();
        //}
        //    //public void PrintDotMatrix()
        //    //{

        //    //}


        //#endregion





        
    }

}

using Gramboo.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Kallans.Classes
{
    class MenuControl
    {

        public static void LoadForm(string Menu, string Group,GrbTreeview treeview,Form frm)
        {
            if (Group == "Master Files")
            {
                switch (Menu )
                {
                    case "Item Category Master":
                        ShowForm(Kallans.Forms.ITEM.CategoryMaster.Instance, treeview.SelectedNode.Text,frm);
                        break;
                    case "Item Group Master":
                        ShowForm(Kallans.Forms.ITEM.ItemGroupMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Item Type Master":
                        ShowForm(Kallans.Forms.ITEM.ItemTypeMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Purity Master":
                        ShowForm(Kallans.Forms.ITEM.PurityMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Model Master":
                        ShowForm(Kallans.Forms.ITEM.ModelMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Item Master":
                        ShowForm(Kallans.Forms.ITEM.ItemMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Menu Group Master":
                        ShowForm(Kallans.Forms.SYST.FormMenuGroupMaster.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Menu Master":
                        ShowForm(Kallans.Forms.SYST.FormMenuMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Change Password":
                        ShowForm(Kallans.Forms.SYST.ChangePassword.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "User Category":
                        ShowForm(Kallans.Forms.SYST.UserCategory.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "User Registration":
                        ShowForm(Kallans.Forms.SYST.UserRegistration.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Branch Master":
                        ShowForm(Kallans.Forms.SYST.BranchMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Company Master":
                        ShowForm(FA.CompanyMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Counter Master":
                        ShowForm(Kallans.Forms.SYST.Counter_Master.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Supplier Master":
                        ShowForm(Kallans.Forms.PUR.SupplierMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Supplier Type Master":
                        ShowForm(Kallans.Forms.PUR.SupplierTypeMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Supplier Group Master":
                        ShowForm(Kallans.Forms.PUR.SupplierGroupMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Customer Group Master":
                        ShowForm(Kallans.Forms.CRM.CustomerGroupMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Customer Type Master":
                        ShowForm(Kallans.Forms.CRM.CustomerTypeMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Customer Master":
                        ShowForm(Kallans.Forms.CRM.CustomerMasterr.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Miscellaneous Charge Master":
                        ShowForm(Kallans.Forms.PUR.MiscelleniousChargeMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Purchase Type Master":
                        ShowForm(Kallans.Forms.PUR.PurchaseTaxApplicableTaxes.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Metal Rate":
                        ShowForm(Kallans.Forms.GENERALFORMS.MetalRate.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Tax Master":
                        ShowForm(Kallans.Forms.PUR.TaxMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Department Master":
                        ShowForm(Kallans.Forms.STOCK.DepartmentMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Credit Card Master":
                        ShowForm(Kallans.Forms.GENERALFORMS.CreditCardMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Category Master":
                        ShowForm(FA.CategoryMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Group Master":
                        ShowForm(FA.GroupMaster.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Ledger Master":
                        ShowForm(FA.LedgerMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Bank Account Type Master":
                        ShowForm(FA.FORMS.BankAccountTypeMasterFrm.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Bank Account Master":
                        ShowForm(FA.FORMS.BankAccountMasterEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Opening Balance Master":
                        ShowForm(FA.OpeningBalanceEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "":

                    case "Sieve Group Master":
                        ShowForm(Kallans.Forms.ITEM.SieveGroupMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Sieve Master":
                        ShowForm(Kallans.Forms.ITEM.SieveMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Favorite Menu Master":
                        ShowForm(Kallans.Forms.SYST.FavFrmmenuMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Leave Master":
                           ShowForm(Kallans.Forms.HR.LeaveMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;


                    case "Employee Master":
                         ShowForm(Kallans.Forms.EMP.frmEmployeeMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Employee Group Master":
                          ShowForm(Kallans.Forms.HR.HRGroupMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Employee Department Master":
                        ShowForm(Kallans.Forms.EMP.frmDepartmentMaster.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Employee Designation Master":
                            ShowForm(Kallans.Forms.EMP.frmDesignationMaster.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Employee Type Master":
                        ShowForm(Kallans.Forms.EMP.frmEmployeeTypeMaster.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Holiday Master":
                           ShowForm(Kallans.Forms.HR.HolidayMaster.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Deposit Master":
                        ShowForm(Kallans.Forms.CRM.DepositMster.Instance, treeview.SelectedNode.Text, frm);
                        break;
         

                    case "Floor Master":
                         ShowForm(Kallans.Forms.STOCK.FloorMaster.Instance, treeview.SelectedNode.Text, frm);
                        break;


                         case "Budget Master":
                        ShowForm(new FA.FORMS.BudgetMaster1(), treeview.SelectedNode.Text, frm);
                        break;

                         case "Minimum VA Setting":
                        ShowForm(Kallans.Forms.ITEM.MinimumVASetting.Instance,treeview.SelectedNode.Text,frm);
                        break;

                         case "Item Reorder Master":
                        ShowForm(Kallans.Forms.ITEM.ItemReorderMaster.Instance,treeview.SelectedNode.Text,frm);
                        break;

                    case "Opening Cash Master":
                         ShowForm(Kallans.Forms.ACC.OpeningCashMaster.Instance,treeview.SelectedNode.Text,frm);
                        break;


                    default:
                        treeview.SelectedNode = treeview.Nodes[0];
                        break;



                }
            }
            else if (Group == "Transactions")
            {
                switch (Menu)
                {


                    case "Saving Scheme":
                        ShowForm(Kallans.Forms.SALE.SavingsScheme.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Scheme Cancellation Entry":
                        ShowForm(Kallans.Forms.SALE.SchemeCancellationEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Scheme Joining Entry":
                        ShowForm(Kallans.Forms.SALE.SchemeJoiningEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Scheme Payment Entry":
                        ShowForm(Kallans.Forms.SALE.SchemePaymentEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;


                    case "Testing Return Entry":
                        ShowForm(Kallans.Forms.STOCK.TestingReturnEntry.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Purchase Entry":
                        ShowForm(Kallans.Forms.PUR.PurchaseEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Stock Transfer Issue":
                        ShowForm(new Kallans.Forms.STOCK.StockTransferEntryNew(15),  treeview.SelectedNode.Text,frm);
                        break;
                    case "Stock Transfer Receipt":
                        ShowForm(new Kallans.Forms.STOCK.StockTransferEntryNew(6),  treeview.SelectedNode.Text,frm);
                        break;

                    case "Purchase Return Entry":
                        ShowForm(Kallans.Forms.PUR.PurchaseReturnEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Stock Entry":
                        ShowForm(Kallans.Forms.STOCK.StockEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Gold Booking":
                        ShowForm(Kallans.Forms.SALE.GoldBookingMaster.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Sales Entry":
                        ShowForm(Kallans.Forms.SALE.frmSalesEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Sales Order Entry":
                        ShowForm(Kallans.Forms.SALE.frmSalesOrder.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Metal Mix Entry":
                        ShowForm(Kallans.Forms.PROD.MetalMixEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Supplier Settlement":
                        ShowForm(Kallans.Forms.PUR.SupplierSettlement.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Supplier Opening Balance Entry":
                        ShowForm(Kallans.Forms.PUR.SupplierOpeningBalanceEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Item Conversion":
                        ShowForm(Kallans.Forms.STOCK.ItemConversion.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Aciding Entry":
                        ShowForm(Kallans.Forms.PROD.AcidingEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Testing Issue Entry":
                        ShowForm(Kallans.Forms.STOCK.TestingIssueEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Repair Receipt Entry":
                        ShowForm(Kallans.Forms.STOCK.RepairEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Repair Issue Entry":
                        ShowForm(Kallans.Forms.STOCK.RepairIssueEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    //case "BarCode Print":
                    //    ShowForm(Kallans.REPORTS.STOCK.BarCodePrint.Instance, treeview.SelectedNode.Text, frm);
                    //    break;

                    case "Colouring Return Entry":
                        ShowForm(Kallans.Forms.PROD.ColouringReturn.Instance, treeview.SelectedNode.Text,frm);
                        break;

                    case "Old Gold Receipt Entry":
                        ShowForm(Kallans.Forms.SALE.OldGoldReceiptNew.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Salary Details Entry":
                        ShowForm(Kallans.Forms.HR.SalaryDetails.Instance,treeview.SelectedNode.Text,frm);
                        break;

                   
                    //case "Repair Receipt Entry":
                    //      ShowForm(Kallans.Forms.STOCK.RepairEntry.Instance,  treeview.SelectedNode.Text,frm);
                    //      break;

                    case "Payment":
                       FA.FORMS.IssueOrReciept frmacci = new  FA.FORMS.IssueOrReciept( );
                      //frmacci.ShowForm(
                        break;

                    case "Receipt":
                          FA.FORMS.IssueOrReciept frmaccR = new  FA.FORMS.IssueOrReciept( );
                          //ShowForm(frmaccR, treeview.SelectedNode.Text, frmaccR);   
                        break;

                    //case "Contra":
                    //    ShowForm(Kallans.Forms.ACC.AccountingVoucher.ContraInstance,  treeview.SelectedNode.Text,frm);
                    //    break;

                    //case "Journal":
                    //    ShowForm(Kallans.Forms.ACC.AccountingVoucher.JournalInstance,  treeview.SelectedNode.Text,frm);
                    //    break;
                    case "Stock Compare Entry":
                        ShowForm(Kallans.Forms.STOCK.StockCompareEntry.Instance, treeview.SelectedNode.Text, frm);
                        break;


                    case "Complements Purchase":
                    ShowForm(new Kallans.Forms.PUR.ComplementsPurchase(),  treeview.SelectedNode.Text,frm);
                    break;


                    case "Department Opening Stock Entry":
                        ShowForm(Kallans.Forms.STOCK.DepartmentOpeningStkEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Adjust Entry":
                        ShowForm(Kallans.Forms.STOCK.AdjustEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Continuous Bill Print":

                        ShowForm(Kallans.Forms.PUR.ContinuousBillPrint.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Gold Booking Cancelation":
                        ShowForm(Kallans.Forms.SALE.GoldBookingCancelationEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case"Advance Payment":
                           ShowForm(Kallans.Forms.HR.AdvancePayment.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Leave Application":
                          ShowForm(Kallans.Forms.HR.LeaveApplication.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Staff Arrears":
                          ShowForm(Kallans.Forms.HR.StaffArrears.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Manual PunchEntry":
                           ShowForm(Kallans.Forms.HR.ManualPunchEntry.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Salary Generation":
                          ShowForm(Kallans.Forms.HR.SalaryGeneration1.Instance,  treeview.SelectedNode.Text,frm);
                        break;

                    case "Must Roll Generation":
                          ShowForm(Kallans.Forms.HR.GenerateMustRoll.Instance,  treeview.SelectedNode.Text,frm);
                          break;

                    case "Complement Issue":
                         ShowForm(Kallans.Forms.SALE.ComplemetIssueEntry.Instance,  treeview.SelectedNode.Text,frm);
                         break;

                    case "Accounts Generation":
                         ShowForm(new Kallans.Forms.ACC.AccountsGeneration(), treeview.SelectedNode.Text, frm);
                         break;

                    case "Budget Variance":
                         ShowForm(new FA.FORMS.BudgetVariance(), treeview.SelectedNode.Text, frm);
                         break;


                    
                    case "Cash Transaction Entry":
                         ShowForm(Kallans.Forms.SALE.CashTransactionEntry.Instance,  treeview.SelectedNode.Text,frm);
                         break;

                    case "Melting Entry":
                         ShowForm(Kallans.Forms.PROD.MeltingEntry.Instance,treeview.SelectedNode.Text,frm);
                         break;

                    case "Sales Bill Settings":
                         ShowForm(Kallans.Forms.SALE.SalesBillSettings.Instance, treeview.SelectedNode.Text, frm);
                         break;

                    case "colouring Issue Entry":
                         ShowForm(Kallans.Forms.PROD.ColoringIssueEntry.Instance, treeview.SelectedNode.Text, frm);
                         break;




                    case "Sales Balance Receipt Entry":
                         ShowForm(Kallans.Forms.SALE.SalesBalanceReceiptEntry.Instance, treeview.SelectedNode.Text, frm);
                         break;

                    case "Stock Comparison":
                         ShowForm(Kallans.Forms.STOCK.StockCompareEntry.Instance, treeview.SelectedNode.Text, frm);
                         break;

                    case "Product Code Verification":
                         ShowForm(KALLANS.Forms.STOCK.frmStockVerification.Instance, treeview.SelectedNode.Text, frm);
                         break;


                    default:
                        treeview.SelectedNode = treeview.Nodes[0];
                        break;
                }
            }
            else if (Group == "Reports")
            {
                switch (Menu)
                {

 //saving Scheme
                    case "Saving Scheme Joining Report":
                        ShowForm(Kallans.REPORTS.SALE.SavingSchemeJoiningGridReport.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Saving Scheme Payment Report":
                        ShowForm(Kallans.REPORTS.SALE.SavingSchemePaymentGridReport.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Saving Scheme Cancellation Report":
                        ShowForm(Kallans.REPORTS.SALE.SchemeCancellationEntryGridReport.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    
                    case "Saving Scheme Reports":
                        ShowForm(Kallans.REPORTS.SALE.SupplierBalnce.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Sales Order Details Report":
                        ShowForm(Kallans.REPORTS.SALE.SalesOrderDetailsReport.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Sales Order Master Report":
                        ShowForm(Kallans.Forms.SALE.SalesOrderMasterReport.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Saving Scheme Total Payments Report":
                          ShowForm(Kallans.REPORTS.SALE.SavingSchemeTotalPayment.Instance, treeview.SelectedNode.Text, frm);
                        break;

//Purchase
                    case "Purchase Return Details Report":
                        ShowForm(Kallans.REPORTS.PUR.PurchaseReturnDetailsReport.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Purchase Return Master Report":
                        ShowForm(Kallans.REPORTS.PUR.PurchaseReturnEntryMasterReport.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Puchase Master  Report":
                        ShowForm(Kallans.REPORTS.PUR.PurchaseEntryMasterReport.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Purchase Details  Report":
                        ShowForm(Kallans.REPORTS.PUR.PurchaseDetailsReport.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Supplier Settlement Master Report":
                        ShowForm(Kallans.REPORTS.PUR.SupplierSettlementMasterReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Supplier Settlement Details Report":
                        ShowForm(Kallans.REPORTS.PUR.SupplierSettlementDetailsReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Supplier Due Report":
                        ShowForm(Kallans.REPORTS.PUR.SupplierDueReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Supplier Balance Report":
                        ShowForm(Kallans.REPORTS.PUR.SupplierBalnce.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Gold Transaction Report":
                        ShowForm(Kallans.REPORTS.PUR.GoldTransactionReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Silver Transaction Report":
                        ShowForm(Kallans.REPORTS.PUR.SilverTransactionReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Complements Purchase Master Report":
                        ShowForm(Kallans.REPORTS.PUR.ComplementsPurchaseMasterReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Complements Purchase Details Report":
                        ShowForm(Kallans.REPORTS.PUR.ComplementsPurchaseDetailsReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Suppler Balance Report -Itemwise":
                        ShowForm(Kallans.REPORTS.PUR.frmSupplierBalance.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Salary Detailed Report":
                        ShowForm(Kallans.REPORTS.HR.SalaryDetailsReport.Instance,treeview.SelectedNode.Text,frm);
                        break;

                    case"Advance Payment Report":
                        ShowForm(Kallans.REPORTS.HR.AdvancePaymentReport.Instance, treeview.SelectedNode.Text,frm);
                        break;

//Production
                    case "Melting Report":
                        ShowForm(Kallans.REPORTS.PROD.RptMetalMixEntry.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Aciding Report":
                        ShowForm(Kallans.REPORTS.PROD.AcidingReport.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Employee Wise Salary Report":
                        ShowForm(Kallans.REPORTS.HR.EmployeeWiseSalaryReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
//Stock
                    case "Stock Transfer Receipt Report":
                        ShowForm(Kallans.REPORTS.STOCK.StockTransferReceiptGridReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Stock Transfer Issue Report":
                        ShowForm(Kallans.REPORTS.STOCK.StockTransferIssueGridReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Old Gold Transaction":
                        ShowForm(Kallans.REPORTS.STOCK.OldGoldReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Department Opening Stock Report":
                        ShowForm(Kallans.REPORTS.STOCK.DepartmentOpeningStockReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Adjustment Report":
                        ShowForm(Kallans.REPORTS.STOCK.AdjustmentReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Ageing Report":
                        ShowForm(Kallans.REPORTS.STOCK.AgeingReportfrm.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Product Code History Report":
                        ShowForm(Kallans.REPORTS.STOCK.ProdCodeHistoryReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Daily Summary Report":
                        ShowForm(Kallans.REPORTS.STOCK.frmDailySummary.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Complement Stock Report":
                        ShowForm(Kallans.REPORTS.STOCK.ComplementStock.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Testing Pending Details":
                        ShowForm(Kallans.REPORTS.STOCK.TestingPendingList.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case"Item Below Reorder Level":
                        ShowForm(Kallans.REPORTS.STOCK.ItemsBelowReorderLevel.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Item Conversion Reports":
                        ShowForm(Kallans.REPORTS.STOCK.ItemConversionReports.Instance,treeview.SelectedNode.Text,frm);
                        break;

                    case "BarCode Print":
                        ShowForm(Kallans.REPORTS.STOCK.BarCodePrint.Instance, treeview.SelectedNode.Text, frm);
                        break;



//Sale
                    case "Discount Allowed Report":
                        ShowForm(Kallans.REPORTS.SALE.DiscountAllowedReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Sales Details Report":
                        ShowForm(Kallans.REPORTS.SALE.SalesDetailsGridReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Sales Master Report":
                        ShowForm(Kallans.REPORTS.SALE.SalesMasterReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Gold Booking Cancellation Report":
                        ShowForm(Kallans.REPORTS.SALE.GoldBookingCancelationEntryReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Supplier ItemWise Sales Report":
                        ShowForm(Kallans.REPORTS.SALE.SupplierItemWiseSales.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Gold Booking Report":
                        ShowForm(Kallans.REPORTS.SALE.GoldBookingMasterReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Sales Customer Wise Report":
                        ShowForm(Kallans.REPORTS.SALE.SalescutomerWiseRpt.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Sales Cash or Credit Report":
                        ShowForm(Kallans.REPORTS.SALE.SalesMasterCashorCreditReport.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Sales Chart Report":
                        //ShowForm(Kallans.REPORTS.SALE.SalesChart.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Complement Report":
                        ShowForm(Kallans.REPORTS.SALE.ComplementGridReport.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Sales Summary":
                        ShowForm(Kallans.REPORTS.SALE.SalesSummary.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Sales Date Wise Summary ":
                        ShowForm(Kallans.REPORTS.SALE.SalesDatewiseSummaryReport.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Sales Item Wise Summary ":
                        ShowForm(Kallans.REPORTS.SALE.SalesItemwiseSummaryReport.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Salesman Wise Summary":
                        ShowForm(Kallans.REPORTS.SALE.SalesmanwiseSummary.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Old Gold Receipt Report":
                        ShowForm(Kallans.Forms.SALE.OldGoldReceiptGridReport.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case "Old Gold Item Wise":
                        ShowForm(Kallans.REPORTS.SALE.OldGoldItemWiseReport.Instance,treeview.SelectedNode.Text,frm);
                        break;

                    case "Repair Receipt Report":
                          ShowForm(Kallans.REPORTS.STOCK.RepairReceiptGridReport.Instance, treeview.SelectedNode.Text, frm);
                          break;

                    case "Repair Issue Report":
                          ShowForm(Kallans.REPORTS.STOCK.RepairIssueDetailsGridReport.Instance, treeview.SelectedNode.Text, frm);
                          break; 

                    case"Supplier Wise Sales Report":
                          ShowForm(Kallans.REPORTS.SALE.SupplierWiseSalesReport.Instance,treeview.SelectedNode.Text,frm);
                          break;

                    case "Sales Balance Pending Report":
                          ShowForm(Kallans.REPORTS.SALE.SalesBalancePendingReport.Instance,treeview.SelectedNode.Text,frm);
                          break;

                    case "Average VA Report":
                          ShowForm(Kallans.REPORTS.SALE.AverageVAReport.Instance, treeview.SelectedNode.Text, frm);
                          break;


                    case "Sales Order Report":
                          ShowForm(Kallans.REPORTS.SALE.SalesOrderContinous.Instance, treeview.SelectedNode.Text, frm);
                          break;
                    case "Customer Search":
                          ShowForm(Kallans.REPORTS.SALE.CustomerSearch1.Instance, treeview.SelectedNode.Text, frm);
                          break;


             

 //Accounts     
                   case "Chart Of Accounts":
                        ShowForm(FA.frmChartOfAccounts.Instance,treeview.SelectedNode.Text,frm);
                        break;
                    case "Ledger Book":
                        ShowForm(FA.LedgerBook.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Day Book":
                        ShowForm(FA.DayBook.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Cash Book":
                        ShowForm(FA.CashBook.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Group Summary":
                        ShowForm(FA.GroupSummary.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Trial Balance":
                         ShowForm(FA.TrialBalance.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    case "Profit And Loss Accounts":
                        ShowForm(FA.ProfitAndLoss.Instance,  treeview.SelectedNode.Text,frm);
                        break;
                    //case "Cheque Transactions":
                    //    //ShowForm(FA.FORMS.ChequeIssueOrReciept., treeview.SelectedNode.Text, frm);
                    //    break;


           
                 
//HR

                    case "Attendence List":
                        ShowForm(Kallans.REPORTS.HR.AbsentpresentReportFrm.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Attendence Report":
                        ShowForm(Kallans.REPORTS.HR.MustRollRptFrm.Instance, treeview.SelectedNode.Text, frm);
                        break;

                      

//Continuous Print

                    case "Continuous Bill Print":
                        ShowForm(Kallans.Forms.PUR.ContinuousBillPrint.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Salary Payslip Continuous Print":
                        ShowForm(Kallans.REPORTS.HR.SalarySlipContinousPrint.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "All Transactions Report":
                        ShowForm(Kallans.REPORTS.STOCK.AllTransactionsReport.Instance, treeview.SelectedNode.Text, frm);
                        break;

//e files
                    case "Purchase":
                        ShowForm(Kallans.REPORTS.PUR.e_File_Purchase.Instance, treeview.SelectedNode.Text, frm);
                        break;
                    case "Sales":
                        ShowForm(Kallans.REPORTS.SALE.e_File_Sales.Instance, treeview.SelectedNode.Text, frm);
                        break;

                    case"Customers Address Print":
                        ShowForm(Kallans.REPORTS.SALE.CustomersAddressPrint.Instance,treeview.SelectedNode.Text,frm);
                        break;
              






                    default:
                        break;
                }
            }
        }
        public static void ShowForm(DockContent Form, string menuname,Form MdiParent)
        {
            Form.MdiParent = MdiParent ;

            ((frmMain)MdiParent).ShowChild(Form);
            Form.Focus();
            Form.Tag = menuname;
        }
        public static void ShowForm(Form Form, string menuname, Form MdiParent)
        {
            Form.StartPosition = FormStartPosition.CenterScreen;
            Form.ShowDialog();     
            Form.Focus();
            Form.Tag = menuname;
        }


         
    }
}

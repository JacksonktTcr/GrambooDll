using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGAL.Classes
{
    public enum VoucherTypes
    {
        Contra = 1,
        Payment = 2,
        Receipt = 3,
        Journal = 4,
        PurchaseMaster = 5,
        StockTransferMasterReceipt = 6, 
        PurchaseReturnMaster = 7,
        OldGoldReceipt = 8,
        SalesEntry = 9,
        GoldBooking = 10,
        MetalMixMaster = 11,
        SupplierSettlement = 12,
        AcidingMaster = 13,
        TestingIssueEntry = 14,
        StockTransferMasterIssue = 15,
        SchemeCancellationEntry = 16,
        SavingSchemeJoinEntry = 17,
        SavingSchemePaymentEntry = 18,
        ComplementsPurchaseMaster = 19,
        DepartmentOpeningStockMaster=22,
        AdjustmentsMaster =23,
        RepairIssue=20,
        RepairReceipt=21,
        GoldBookingCancelingMaster=24,
        ComplementIssueMaster=25,
        TestingReturnEntry=26,
        CashTransactionEntry=27,
        AdvancePayment=37,
        SalaryDetails=36,
        SalesBalanceReceiptEntry=38,
        SalesOrder=32
    }
}

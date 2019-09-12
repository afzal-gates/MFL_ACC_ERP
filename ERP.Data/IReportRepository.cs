using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
   public interface IReportRepository
    {
       System.Data.DataTable GetVoucherReport(int id, string compCode);

        DataTable GetLedgerReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string copmCode);
       DataTable GetControlLedgerReport(string mainHead, DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetTrialBalanceReport(DateTime fromdate, DateTime todate, string userId, string compCode);
       DataTable GetVoucherSummaryReportData(DateTime fromdate, DateTime todate, string userId, string compCode);
       DataTable GetVoucherStatementReportData(DateTime fromdate, DateTime todate, string userId, string compCode);
       DataTable GetChartOfAccount(string userId, string compCode);
       DataTable GetCashBookReport
           (string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode);

       DataTable GetBankBookReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode);
       DataTable GetDraftVoucherReport(int id, string compCode);
       DataTable GetDayBookReport(DateTime fromdate, DateTime todate, string userId, string compCode);
       DataTable GetReceivePaymentData(DateTime fromdate, DateTime todate, string userId, string compCode);
       DataTable GetBalanceSheetReport(DateTime fromdate, DateTime todate, string userId, string compCode);
       DataTable GetIncomeStatementReport(DateTime fromdate, DateTime todate, string userId, string compCode,int reportId);
       DataTable GetLedgerCostCenterReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode);
       DataTable GetCostCenterLedgerReport
           (int costCenterId, DateTime fromdate, DateTime todate, string userId, string compCode);

       DataTable GetBankBookWithCurrecny
           (string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode, int currencyId);

       DataTable GetChequeReport(int id, string compCode);
        DataTable GetCostCenterGroupLedgerReport(int groupId, DateTime fromdate, DateTime todate, string userId, object compCode);
        DataTable GetPaidBillsReport(string comp_code, string ac_code, string main_code, string sub_code);
        DataTable GetDuplicateBillPaymentReport(string comp_code);
    }
}

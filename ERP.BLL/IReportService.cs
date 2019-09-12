using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
    public interface IReportService
    {
        System.Data.DataTable GetVoucherReport(int id, string compCode);

        System.Data.DataTable GetLedgerReport(string accountHead, DateTime fromdate, DateTime todate,string userId,string comp_code);
        DataTable GetControlLedgerReport(string mainHead, DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetTrialBalanceReport(DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetVoucherSummaryReportData(DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetVoucherStatementReportData(DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetChartOfAccount(string userId, string compCode);
        DataTable GetCashBookReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetBankBookReport
            (string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode);

        DataTable GetDraftVoucherReport(int id, string p);
        DataTable GetDayBookReport(DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetReceivePaymentData(DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetBalanceSheetReport(DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable GetIncomeStatementReport(DateTime fromdate, DateTime todate, string userId, string compCode, int reportId);
        DataTable GetLedgerCostCenterReport
            (string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode);

        DataTable GetCostCenterLedgerReport
            (int costCenterId, DateTime fromdate, DateTime todate, string userId, string compCode);
        DataTable BankBookWithCurrecny(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode, int currencyId);
        DataTable GetChequeReport(int id, string compCode);
        DataTable GetBankReconsileVoucherReport(string userId, string comp_code, DateTime? fromDate, DateTime? toDate, string accountHead,int status);
        DataTable GetPandingBills(string comp_code, string ac_code, string main_code, string sub_code);
        DataTable GetCostCenterGroupLedgerReport(int groupId, DateTime fromdate, DateTime todate, string userId, string comp_code);
        DataTable PaidBillsReport(string comp_code, string ac_code, string main_code, string sub_code);
        DataTable GetDuplicateBillPaymentReport(string comp_code);
    }
}

using ERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
    public class ReportService : IReportService
    {
        private readonly IVoucherMasterRepository _voucherMasterRepository;
        public readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository, IVoucherMasterRepository voucherMasterRepository)
        {
            this._reportRepository = reportRepository;
            _voucherMasterRepository = voucherMasterRepository;
        }
        public System.Data.DataTable GetVoucherReport(int id, string compCode)
        {
            return _reportRepository.GetVoucherReport(id, compCode);
        }


        public System.Data.DataTable GetLedgerReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string copm_code)
        {
            return _reportRepository.GetLedgerReport(accountHead, fromdate, todate, userId, copm_code);
        }

        public DataTable GetControlLedgerReport(string mainHead, DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetControlLedgerReport(mainHead, fromdate, todate, userId, compCode);
        }

        public DataTable GetTrialBalanceReport(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetTrialBalanceReport(fromdate, todate, userId, compCode);
        }

        public DataTable GetVoucherSummaryReportData(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetVoucherSummaryReportData(fromdate, todate, userId, compCode);
        }

        public DataTable GetVoucherStatementReportData(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetVoucherStatementReportData(fromdate, todate, userId, compCode);
        }

        public DataTable GetChartOfAccount(string userId, string compCode)
        {
            return _reportRepository.GetChartOfAccount(userId, compCode);
        }

        public DataTable GetCashBookReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetCashBookReport(accountHead, fromdate, todate, userId, compCode);
        }

        public DataTable GetBankBookReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetBankBookReport(accountHead, fromdate, todate, userId, compCode);
        }


        public DataTable GetDraftVoucherReport(int id, string compCode)
        {
            return _reportRepository.GetDraftVoucherReport(id, compCode);
        }

        public DataTable GetDayBookReport(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetDayBookReport(fromdate, todate, userId, compCode);
        }

        public DataTable GetReceivePaymentData(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetReceivePaymentData(fromdate, todate, userId, compCode);
        }

        public DataTable GetBalanceSheetReport(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetBalanceSheetReport(fromdate, todate, userId, compCode);
        }

        public DataTable GetIncomeStatementReport(DateTime fromdate, DateTime todate, string userId, string compCode, int reportId)
        {
            return _reportRepository.GetIncomeStatementReport(fromdate, todate, userId, compCode, reportId);
        }

        public DataTable GetLedgerCostCenterReport(string accountHead, DateTime fromdate, DateTime todate, string userId,
            string compCode)
        {
            return _reportRepository.GetLedgerCostCenterReport(accountHead, fromdate, todate, userId, compCode);
        }

        public DataTable GetCostCenterLedgerReport(int costCenterId, DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            return _reportRepository.GetCostCenterLedgerReport(costCenterId, fromdate, todate, userId, compCode);
        }

        public DataTable BankBookWithCurrecny(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode,
            int currencyId)
        {
            return _reportRepository.GetBankBookWithCurrecny(accountHead, fromdate, todate, userId, compCode, currencyId);
        }

        public DataTable GetChequeReport(int id, string compCode)
        {
            return _reportRepository.GetChequeReport(id, compCode);
        }

        public DataTable GetBankReconsileVoucherReport(string userId, string comp_code, DateTime? fromDate, DateTime? toDate, string accountHead, int status)
        {
            DataTable dataTable = _voucherMasterRepository.GetBankReconsileVouchers(userId, comp_code, fromDate, toDate, accountHead, status);
            return dataTable;
        }

        public DataTable GetPandingBills(string comp_code, string ac_code, string main_code, string sub_code)
        {
            return _voucherMasterRepository.GetPandingBills(comp_code, ac_code, main_code, sub_code,null,null);
        }

        public DataTable GetCostCenterGroupLedgerReport(int groupId, DateTime fromdate, DateTime todate, string userId, string comp_code)
        {
            return _reportRepository.GetCostCenterGroupLedgerReport(groupId, fromdate, todate, userId, comp_code);
        }

        public DataTable PaidBillsReport(string comp_code, string ac_code, string main_code, string sub_code)
        {
            return _reportRepository.GetPaidBillsReport(comp_code, ac_code, main_code, sub_code);
        }

        public DataTable GetDuplicateBillPaymentReport(string comp_code)
        {
            return _reportRepository.GetDuplicateBillPaymentReport(comp_code);
        }
    }
}

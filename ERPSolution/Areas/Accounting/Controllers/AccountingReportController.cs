using ERP.BLL;
using ERP.Shared;
using ERPSolution.Controllers;
using ERPSolution.Extenssion;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Accounting.Controllers
{
    public class AccountingReportController : BaseController
    {
        private readonly IReportService _resportService;
        public AccountingReportController(IReportService resportService)
        {
            this._resportService = resportService;
        }

        public ViewResult Index()
        {
            return View();
        }
        public PartialViewResult _Leger()
        {
            return PartialView();
        }
        public ActionResult LedgerReport(string accountHead, DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetLedgerReport(accountHead, fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "LedgerReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("LedgerReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .1, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }

        public ActionResult VoucherReport(int id)
        {
            DataTable voucherDataTable = _resportService.GetVoucherReport(id, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "VoucherReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("VoucherReportDataSet", voucherDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 5.84, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = 1 };
            return ReportExtenssionMethod.ToFile(ReportType.PDF, path, reportDataSources, deviceInformation);
        }

        public ActionResult Cheque(int id)
        {
            DataTable voucherDataTable = _resportService.GetChequeReport(id, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "Cheque.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("ChequeDataSource", voucherDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 7.5, PageHeight = 3.5, MarginTop = 0, MarginLeft = 0, MarginRight = 0, MarginBottom = 0 };
            return ReportExtenssionMethod.ToFile(ReportType.PDF, path, reportDataSources, deviceInformation);
        }
        public ActionResult VoucherPrintReport(int id)
        {
            DataTable voucherDataTable = _resportService.GetDraftVoucherReport(id, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "VoucherReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("VoucherReportDataSet", voucherDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 5.84, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = 1 };
            return ReportExtenssionMethod.ToFile(ReportType.PDF, path, reportDataSources, deviceInformation);
        }
        public PartialViewResult _ControlLedger()
        {

            return PartialView();
        }

        public ActionResult ControlLedgerReport(string mainHead, DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetControlLedgerReport(mainHead, fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "ControlLedgerReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("ControlLedgerReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.26772, PageHeight = 5.5118, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }
        public PartialViewResult _VoucherSummary()
        {

            return PartialView();
        }

        public ActionResult VoucherSummaryReport(DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetVoucherSummaryReportData(fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "VoucherSummaryReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("VoucherSummaryReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }
        public ActionResult ChartOfAccountReport()
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetChartOfAccount(userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "ChartOfAccountReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("ChartOfAccountDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(ReportType.Excel, path, reportDataSources, deviceInformation);
        }
        public ActionResult VoucherStatementReport(DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetVoucherStatementReportData(fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "VoucherStatementReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("VoucherStatementReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 11, PageHeight = 8.5, MarginTop = .2, MarginLeft = .1, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }
        public PartialViewResult _VoucherStatement()
        {

            return PartialView();
        }
        public PartialViewResult _BalanceSheet()
        {
            return PartialView();
        }
        public ActionResult BalanceSheetReport(DateTime fromdate, DateTime todate, ReportType reportType)
        {

            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable balanceSheetDataTable = _resportService.GetBalanceSheetReport(fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "BalanceSheetReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("BalanceSheetReportDataSet", balanceSheetDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);

        }

        public PartialViewResult _IncomeStatement()
        {

            return PartialView();
        }

        public ActionResult IncomeStatementReport(DateTime fromdate, DateTime todate, int reportId, ReportType reportType)
        {

            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable dataTable = _resportService.GetIncomeStatementReport(fromdate, todate, userId, CompanyCode.comp_code, reportId);
            string dataSet = "";
            String reportName = "";
            switch (reportId)
            {
                case 1:
                    dataSet = "InocomeStatementDataSet";
                    reportName = "IncomeStatementSummaryReport.rdlc";
                    break;
                case 2:
                    dataSet = "IncomeStatementDetailDataSet";
                    reportName = "IncomeStatementDetailReport.rdlc";
                    break;
                default:
                    dataSet = "IncomeStatementDetailDataSet";
                    reportName = "IncomeStatementDetailReport.rdlc";
                    break;
            }
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), reportName);
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource(dataSet, dataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);

        }

        public PartialViewResult _ReceivePayment()
        {
            return PartialView();
        }
        public ActionResult ReceivePaymentReport(DateTime fromdate, DateTime todate, ReportType reportType)
        {

            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable dataTable = _resportService.GetReceivePaymentData(fromdate, todate, userId, CompanyCode.comp_code);
            DataTable rcvData = dataTable.AsEnumerable()
                                .Where(r => r.Field<string>("GR_CODE") == "R")
                                .CopyToDataTable() ?? new DataTable();

            DataTable paymentData = dataTable.AsEnumerable()
                             .Where(r => r.Field<string>("GR_CODE") == "P")
                             .CopyToDataTable() ?? new DataTable();

            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "ReceivePaymentReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("ReceiveDataSet", rcvData), new ReportDataSource("PaymentDataSet", paymentData) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 9, PageHeight = 11.69, MarginTop = .2, MarginLeft = .1, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);

        }
        public PartialViewResult _TrialBalance()
        {
            return PartialView();
        }
        public ActionResult TrialBalanceReport(DateTime fromdate, DateTime todate, ReportType reportType)
        {

            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetTrialBalanceReport(fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "TrialBalanceReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("TrialBalanceReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);

        }
        public ActionResult ShortTrialBalanceReport(DateTime fromdate, DateTime todate, ReportType reportType)
        {

            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetTrialBalanceReport(fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "ShortTrialBalanceReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("TrialBalanceReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);

        }

        public PartialViewResult _CashBook()
        {
            return PartialView();
        }

        public ActionResult CashBook(string accountHead, DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetCashBookReport(accountHead, fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "CashBookReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("CashBookReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.40, PageHeight = 11.69, MarginTop = .2, MarginLeft = .1, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }

        public PartialViewResult _BankBook()
        {
            return PartialView();
        }

        public ActionResult BankBook(string accountHead, DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetBankBookReport(accountHead, fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "BankBookReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("BankBookReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .1, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }
        public ActionResult BankBookWithCurrecny(string accountHead, DateTime fromdate, DateTime todate, ReportType reportType, int currencyId = 2)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.BankBookWithCurrecny(accountHead, fromdate, todate, userId, CompanyCode.comp_code, currencyId);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "BankBookCurrencyReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("BankBookReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .1, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }

        public PartialViewResult _DayBook()
        {
            return PartialView();
        }
        public ActionResult DayBookReport(DateTime fromdate, DateTime todate, ReportType reportType)
        {

            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetDayBookReport(fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "DayBookReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("DayBookReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);

        }
        public ActionResult DaybookReportDetail(DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetVoucherStatementReportData(fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "DayBookReportDetail.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("VoucherStatementReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .2, MarginLeft = .3, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }
        public ActionResult DaybookReportDetailWithNarration(DateTime fromdate, DateTime todate, ReportType reportType)
        {

            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetVoucherStatementReportData(fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "DayBookReportDetailWithNarration.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("VoucherStatementReportDataSet", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 11, PageHeight = 8.5, MarginTop = .2, MarginLeft = .1, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }



        public PartialViewResult _LegerCostCenter()
        {
            return PartialView();
        }

        public ActionResult LedgerCostCenterReport(string accountHead, DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetLedgerCostCenterReport(accountHead, fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "LedgerWiseCostCenterSummaryReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("DataSet1", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .1, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(reportType, path, reportDataSources, deviceInformation);
        }



        public PartialViewResult _CostCenterLeger()
        {
            return PartialView();
        }

        public ActionResult CostCenterLedgerReport(int costCenterId, DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetCostCenterLedgerReport(costCenterId, fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "CostCenterWiseLadgerSummaryReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("DataSet1", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .1, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(ReportType.PDF, path, reportDataSources, deviceInformation);
        }
        public ActionResult CostCenterGroupLedgerReport(int groupId, DateTime fromdate, DateTime todate, ReportType reportType)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable ledgerDataTable = _resportService.GetCostCenterGroupLedgerReport(groupId, fromdate, todate, userId, CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "CostCenterGroupLadgerSummaryReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("DataSet1", ledgerDataTable) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .1, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(ReportType.PDF, path, reportDataSources, deviceInformation);
        }
        public ActionResult BankReconciliationReport(DateTime? fromDate, DateTime? toDate, string accountHead, ReportType reportType,int status)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable data= _resportService.GetBankReconsileVoucherReport(userId, CompanyCode.comp_code, fromDate, toDate, accountHead, status);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "BankReconciliationStatus.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("BANKRECDSET", data) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .1, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(ReportType.Excel, path, reportDataSources, deviceInformation);
        }

        public ActionResult PandingBillsReport(string ac_code, string main_code, string sub_code)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable data = _resportService.GetPandingBills(CompanyCode.comp_code, ac_code, main_code, sub_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "PendingBillReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("PENDINGBILLDSET", data) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 8.27, PageHeight = 11.69, MarginTop = .1, MarginLeft = .4, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(ReportType.Excel, path, reportDataSources, deviceInformation);
        }

        public ActionResult PaidBillsReport(string ac_code, string main_code, string sub_code)
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable data = _resportService.PaidBillsReport(CompanyCode.comp_code, ac_code, main_code, sub_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "paidBillReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("PaidBillDataSet", data) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 11.69, PageHeight = 8.27, MarginTop = .1, MarginLeft = .2, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(ReportType.Excel, path, reportDataSources, deviceInformation);
        }

        public ActionResult DuplicateBillPaymentReport()
        {
            string userId = Convert.ToString(Session["multiScUserId"]);
            DataTable data = _resportService.GetDuplicateBillPaymentReport(CompanyCode.comp_code);
            string path = Path.Combine(Server.MapPath("~/Areas/Accounting/Reports"), "DuplicateBillPaymentReport.rdlc");
            if (!System.IO.File.Exists(path))
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            var reportDataSources = new List<ReportDataSource>() { new ReportDataSource("DBILLPAYDSET", data) };
            var deviceInformation = new DeviceInformation() { OutputFormat = 2, PageWidth = 11.69, PageHeight = 8.27, MarginTop = .1, MarginLeft = .2, MarginRight = .1, MarginBottom = .2 };
            return ReportExtenssionMethod.ToFile(ReportType.Excel, path, reportDataSources, deviceInformation);
        }
    }
}
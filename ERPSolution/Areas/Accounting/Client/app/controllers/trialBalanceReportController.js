
(function () {
    'use strict';
    var controllerId = 'trialBalanceReportController';
    angular.module('multitex.accounting').controller(controllerId, trialBalanceReportController);
    trialBalanceReportController.$inject = ['notificationService'];

    function trialBalanceReportController( notificationService) {
        var vm = this;
        vm.form = {};
        vm.showTrialBalanceReport = showTrialBalanceReport;
        vm.ShortTrialBalanceReport = ShortTrialBalanceReport;
        vm.showBalanceSheetReport = showBalanceSheetReport;
        vm.showIncomeStatementReport = showIncomeStatementReport;
        vm.fromdate = '';
        vm.todate = '';
        function showTrialBalanceReport(reportType) {
            window.open("/Accounting/AccountingReport/TrialBalanceReport?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }

        function ShortTrialBalanceReport(reportType) {
            window.open("/Accounting/AccountingReport/ShortTrialBalanceReport?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }

        function showBalanceSheetReport(reportType) {
            window.open("/Accounting/AccountingReport/BalanceSheetReport?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");

        }

        function showIncomeStatementReport(type,reportType) {
          
            window.open("/Accounting/AccountingReport/IncomeStatementReport?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportId=" + type + "&reportType=" + reportType, "_blank");
        }
    }
})();



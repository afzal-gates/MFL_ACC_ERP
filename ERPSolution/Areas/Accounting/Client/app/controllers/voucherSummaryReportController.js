
(function () {
    'use strict';
    var controllerId = 'voucherSummaryReportController';
    angular.module('multitex.accounting').controller(controllerId, voucherSummaryReportController);
    voucherSummaryReportController.$inject = ['notificationService', 'config'];

    function voucherSummaryReportController(notificationService, config) {
        var vm = this;
        vm.form = {};
        vm.showReport = showReport;
        vm.showVoucherStatementReport = showVoucherStatementReport;
        vm.fromdate = '';
        vm.todate = '';
        function showReport(reportType) {
            window.open("/Accounting/AccountingReport/VoucherSummaryReport?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }

        function showVoucherStatementReport(reportType) {
            window.open("/Accounting/AccountingReport/VoucherStatementReport?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }
        vm.QC_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.QC_DT_LNopened = true;
        }

        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
    }
})();




(function () {
    'use strict';
    var controllerId = 'dayBookReportController';
    angular.module('multitex.accounting').controller(controllerId, dayBookReportController);
    //dayBookReportController.$inject = ['config', '$state', '$stateParams','notificationService'];
    function dayBookReportController() {
        var vm = this;
        vm.showReport = showReport;
        vm.showReportDetail = showReportDetail;
        vm.showReportDetailWithNarration = showReportDetailWithNarration;
        vm.fromdate = '';
        vm.todate = '';
        function showReport(reportType) {
            window.open("/Accounting/AccountingReport/DayBookReport?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }
        function showReportDetail(reportType) {
            window.open("/Accounting/AccountingReport/DayBookReportDetail?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }
        function showReportDetailWithNarration(reportType) {
            window.open("/Accounting/AccountingReport/DaybookReportDetailWithNarration?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }

    }
})();



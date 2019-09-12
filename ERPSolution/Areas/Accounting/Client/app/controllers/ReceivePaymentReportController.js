
(function () {
    'use strict';
    var controllerId = 'receivePaymentReportController';
    angular.module('multitex.accounting').controller(controllerId, receivePaymentReportController);
   
    function receivePaymentReportController() {
        var vm = this;
        vm.showReport = showReport;
        vm.fromdate = '';
        vm.todate = '';
        function showReport(reportType) {
            window.open("/Accounting/AccountingReport/ReceivePaymentReport?fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }
    }
})();



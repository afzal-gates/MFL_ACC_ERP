
(function () {
    'use strict';
    var controllerId = 'bankBookReportController';
    angular.module('multitex.accounting').controller(controllerId, bankBookReportController);
    bankBookReportController.$inject = ['config', '$state', '$stateParams', 'unilayerChartOfAccountService', 'notificationService'];
    function bankBookReportController(config, $state, $stateParams, chartOfAccountService, notificationService) {
        var vm = this;
        vm.accountHead ='';
        vm.showReport = showReport;
        vm.showCurrencyReport = showCurrencyReport;
        vm.fromdate = '';
        vm.todate = '';
        vm.cashAccountHeads = [];
        init();
        function init() {
            chartOfAccountService.getBankAccounntHeads().then(function (res) {
                  vm.cashAccountHeads = res.Result;         
            }, function (err) {
              
            });
        }
        function showCurrencyReport(reportType) {
            window.open("/Accounting/AccountingReport/BankBookWithCurrecny?accountHead=" + vm.accountHead + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }

        function showReport(reportType) {
            window.open("/Accounting/AccountingReport/BankBook?accountHead=" + vm.accountHead + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }
    }
})();



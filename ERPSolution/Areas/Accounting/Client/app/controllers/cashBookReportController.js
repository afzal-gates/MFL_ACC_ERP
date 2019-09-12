
(function () {
    'use strict';
    var controllerId = 'cashBookReportController';
    angular.module('multitex.accounting').controller(controllerId, cashBookReportController);
    cashBookReportController.$inject = ['config', '$state', '$stateParams', 'unilayerChartOfAccountService', 'notificationService'];
    function cashBookReportController(config, $state, $stateParams, chartOfAccountService, notificationService) {
        var vm = this;
        vm.accountHead ='';
        vm.showReport = showReport;
        vm.fromdate = '';
        vm.todate = '';
        vm.cashAccountHeads = [];
        init();
        function init() {
            chartOfAccountService.getCashAccounntHeads().then(function (res) {
                  vm.cashAccountHeads = res.Result;         
            }, function (err) {
              
            });
        }
        function showReport(reportType) {
            window.open("/Accounting/AccountingReport/CashBook?accountHead=" + vm.accountHead + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }
    }
})();



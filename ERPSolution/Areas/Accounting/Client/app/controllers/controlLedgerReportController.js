
(function () {
    'use strict';
    var controllerId = 'controlLedgerReportController';
    angular.module('multitex.accounting').controller(controllerId, controlLedgerReportController);
    controlLedgerReportController.$inject = ['config', '$state', '$stateParams', 'unilayerChartOfAccountService', 'notificationService'];

    function controlLedgerReportController(config, $state, $stateParams, chartOfAccountService, notificationService) {
        var vm = this;
        vm.form = {};
        vm.mainHead ='';
        vm.showReport = showReport;
        vm.fromdate = '';
        vm.todate = '';
        vm.selectMainClass = selectMainClass;
        vm.treeData = new kendo.data.HierarchicalDataSource({
            transport: {
                read: function (e) {
                    return chartOfAccountService.getControlChartOfAccounts().then(function (res) {
                        e.success(res.Result);
                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "code",
                    children: "items"
                }
            },

        });
        function selectMainClass(dataItem) {
            vm.mainHead = dataItem.code;
         

        };
        function showReport(reportType) {
            window.open("/Accounting/AccountingReport/ControlLedgerReport?mainHead=" + vm.mainHead + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }
    }
})();



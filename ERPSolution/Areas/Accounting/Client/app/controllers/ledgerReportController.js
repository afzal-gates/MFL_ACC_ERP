
(function () {
    'use strict';
    var controllerId = 'ledgerReportController';
    angular.module('multitex.accounting').controller(controllerId, ledgerReportController);
    ledgerReportController.$inject = ['config', '$state', '$stateParams', 'chartOfAccountService', 'notificationService'];
    function ledgerReportController(config, $state, $stateParams, chartOfAccountService, notificationService) {
        var vm = this;
        vm.accountHead = {};
        vm.showReport = showReport;
        vm.showLedgerCostCenterReport = showLedgerCostCenterReport;
        vm.fromdate = '';
        vm.todate = '';
        vm.options = {
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
               
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            var searchKey = params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            var searchKey = "";
                        }
                        return chartOfAccountService.GetAccountHeards(searchKey).then(function (res) {
                            e.success(res.Result);
                        });
                    }
                },

                serverFiltering: true,
            },
            select: function (e) {
                var item = this.dataItem(e.item.index());
                vm.accountHead = item;
            },
            minLength: 3,
            dataTextField:"SUB_NAME",
            headerTemplate: '<div class="dropdown-header k-widget k-header" >' + '<span>AC Head</span>' + '<span>Group</span>' + '</div>',
            template: '<span class="k-state-default" style="width:250px;">#:SUB_NAME#</span>' +
            '<span class="k-state-default"><p>#:MAIN_NAME#</p></span>',
        };

        vm.QC_DT_LNopen = function ($event) {
            vm.preventDefault();
            vm.stopPropagation();
            vm.QC_DT_LNopened = true;
        };
        function showReport(reportType) {
            window.open("/Accounting/AccountingReport/LedgerReport?accountHead=" + vm.accountHead.AC_CODE + vm.accountHead.MAIN_CODE + vm.accountHead.SUB_CODE + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }

        function showLedgerCostCenterReport (reportType) {
            window.open("/Accounting/AccountingReport/LedgerCostCenterReport?accountHead=" + vm.accountHead.AC_CODE + vm.accountHead.MAIN_CODE + vm.accountHead.SUB_CODE + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }
    }
})();




(function () {
    'use strict';
    var controllerId = 'CostCenterLedgerReportController';
    angular.module('multitex.accounting').controller(controllerId, CostCenterLedgerReportController);
    CostCenterLedgerReportController.$inject = ['costCenterService','costCenterGroupService', 'notificationService'];
    function CostCenterLedgerReportController(costCenterService, costCenterGroupService, notificationService) {
        var vm = this;
        vm.costCenterId = 0;
        vm.costCenters = [];
        vm.costCenterGroups = [];
        vm.showCostCenterLedgerReport = showCostCenterLedgerReport;
        vm.showCostCenterGroupLedgerReport = showCostCenterGroupLedgerReport;
        vm.fromdate = '';
        vm.todate = '';
        vm.GROUP_ID = 0;

        init();
        function init() {
            costCenterService.getCostCenterSelectModels().then(function (res) {
                vm.costCenters = res.Result;
              
            }, function (err) {
                notificationService.displayError(err.Message);
                });


            costCenterGroupService.getCostCenterGroups().then(function (res) {
                vm.costCenterGroups = res.Result;

            }, function (err) {
                notificationService.displayError(err.Message);
            });
        }

        vm.COST_CENTER_NAME = "";
        vm.costCenterOptions = {
            placeholder: "Select Costcented...",
            dataTextField: "Text",
            dataValueField: "Value",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.costCenters);
                    }
                },
            },
            select: function (e) {
                var item = this.dataItem(e.item.index());
                vm.costCenterId = item.Value;
            },
        };

        function showCostCenterLedgerReport(reportType) {
            window.open("/Accounting/AccountingReport/CostCenterLedgerReport?costCenterId=" + vm.costCenterId + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }

        function showCostCenterGroupLedgerReport(reportType) {
            window.open("/Accounting/AccountingReport/CostCenterGroupLedgerReport?GroupId=" + vm.GROUP_ID + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType, "_blank");
        }

    }
})();



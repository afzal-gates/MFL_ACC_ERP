(function () {
    'use strict';
    var controllerId = 'chartOfAccountController';
    angular.module('multitex.accounting').controller(controllerId, chartOfAccountController);
    chartOfAccountController.$inject = ['config', '$state', '$stateParams', 'chartOfAccountService', 'notificationService'];
    function chartOfAccountController(config, $state, $stateParams, chartOfAccountService, notificationService) {
        var vm = this;
        vm.tree = {};
        vm.update = update;
        vm.delete = deleteF;
        vm.save = save;
        vm.click = click;
        vm.chartOfAccount = {};
        vm.chartOfAccountForm = {};
        vm.filterText = '';
        vm.treeData = new kendo.data.HierarchicalDataSource({
            transport: {
                read: function (e) {
                    return chartOfAccountService.getChartOfAccounts().then(function (res) {
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


        vm.searchKeyUp = function (keyEvent) {
            if (keyEvent.target.value == '') {
                vm.treeData.filter({});
            } else {
                vm.treeData.filter({
                    logic: "or",
                    filters: [
                        {
                            field: "text",
                            operator: "contains",
                            value: keyEvent.target.value
                        }
                    ]

                });
            }
        }
    
        function save() {
            chartOfAccountService.saveChartOfAccounts(vm.chartOfAccount).then(function (res) {
               
                notificationService.displaySuccess('Save Sucessfully');
                vm.treeData.read();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
        }
        function update() {
            chartOfAccountService.updateChartOfAccounts(vm.chartOfAccount.code, vm.chartOfAccount.controlCode, vm.chartOfAccount).then(function (res) {
               
                notificationService.displaySuccess('Update Sucessfully');
                vm.treeData.read();
            }, function (errorMessage) {

                notificationService.displayError(errorMessage.Message);
            });
        }

        function click(dataItem) {
            vm.chartOfAccount.code = dataItem.code;
            vm.chartOfAccount.parentCode = dataItem.parentCode;
            vm.chartOfAccount.controlCode = dataItem.controlCode;
            vm.chartOfAccount.text = dataItem.text;
            vm.chartOfAccount.recordId = dataItem.recordId;
        };
        function deleteF() {
            alert('h');
        }


    }

})();



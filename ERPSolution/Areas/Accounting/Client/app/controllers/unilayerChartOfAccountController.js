(function () {
    'use strict';
    var controllerId = 'unilayerChartOfAccountController';
    angular.module('multitex.accounting').controller(controllerId, unilayerChartOfAccountController);
    unilayerChartOfAccountController.$inject = ['config', '$state', '$stateParams', 'unilayerChartOfAccountService', 'notificationService'];
    function unilayerChartOfAccountController(config, $state, $stateParams, chartOfAccountService, notificationService) {
        var vm = this;
         vm.dataItem = {};
         vm.tree = {};
        vm.isGlTransactionExist = true;
        vm.update = update;
        vm.delete = deleteF;
        vm.save = save;
        vm.click = click;
        vm.chartOfAccount = {};
        vm.chartOfAccountForm = {};
        vm.filterText = '';
        vm.showChartOfAccount = showChartOfAccount;
        vm.treeData = new kendo.data.HierarchicalDataSource({

            transport: {
                read: function(e) {
                    return chartOfAccountService.getChartOfAccounts().then(function(res) {
                        e.success(res.Result);
                    }, function(err) {
                        console.log(err);
                    });
                },

            },
          
            template: '<span style:"color:red">#:item.text#</span>',
            schema: {
                model: {
                    id: "code",
                    children: "items"
                },

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

                //var array = vm.dataItem.parent();
                //var index = array.indexOf(vm.dataItem);
                //array.splice(index + 1, 0, res.Result);
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
            vm.chartOfAccount.mapCode = dataItem.mapCode;
            vm.chartOfAccount.text = dataItem.text;
            vm.chartOfAccount.recordId = dataItem.recordId;
            checkedIsGlTransactionExist(vm.chartOfAccount);
        };
        function deleteF() {
            vm.chartOfAccount = {};
        }
        vm.removeGlAccount = function removeGlAccount() {
            if (vm.chartOfAccount.controlCode === 'G') {
                chartOfAccountService.deleteGlAccount(vm.chartOfAccount.code, vm.chartOfAccount.controlCode, vm.chartOfAccount.recordId).then(function (res) {
                    vm.isGlTransactionExist = res.Result;
                    vm.treeData.read();
                    deleteF();
                 
                    notificationService.displaySuccess("Successfully deleted");
                }, function (errorMessage) {
                    notificationService.displayError(errorMessage.Message);
                });
            }
        }
        function checkedIsGlTransactionExist(chartOfAccount) {
            if (chartOfAccount.controlCode === 'G') {
                chartOfAccountService.checkedIsGlTransactionExist(chartOfAccount.code, chartOfAccount.controlCode).then(function (res) {
                    vm.isGlTransactionExist = res.Result;
                
                }, function (errorMessage) {
                    notificationService.displayError(errorMessage.Message);
                });
            }
         
        }
        function showChartOfAccount() {
            window.open("/Accounting/AccountingReport/ChartOfAccountReport", "_blank");
        }
    }

})();



(function () {
    'use strict';
    var controllerId = 'costCenterGroupsController';
    angular.module('multitex.accounting').controller(controllerId, costCenterGroupsController);
    costCenterGroupsController.$inject = ['$state', 'costCenterGroupService', 'notificationService'];
    function costCenterGroupsController($state, costCenterGroupService, notificationService) {
        var vm = this;
        vm.NAME = '';
        vm.GROUP_ID = 0;
        vm.costCenterForm = {};
        vm.editCostCenterGroup = editCostCenterGroup;
        vm.deleteCostCenterGroup = deleteCostCenterGroup;
        vm.costCenterGridOption = {
            sortable: true,
            scrollable: {
                virtual: true,
                scrollable: true
            },
            pageable: false,
            editable: false,
            selectable: "cell",
            navigatable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            columns: [
                { field: "GROUP_ID", title: "ID", type: "string", width: "50px" },
                { field: "NAME", title: "Name", type: "string", width: "80px" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "150px", filterable: false },
                {
                    title: "Action",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editCostCenterGroup(dataItem)' ><i class='fa fa-edit'> Edit</i></button> "
                            + "<button class='btn btn-xs blue' ng-click='vm.clickMe(dataItem)' ><i class='fa fa-edit'> Delete</i></button>";
                    },
                    width: "40px"
                }
            ]
        };

        vm.costCenterGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return costCenterGroupService.getCostCenterGroups().then(function (res) {
                        e.success(res.Result);
                    }, function (errorMessage) {
                        notificationService.displayError(errorMessage.Message);
                    });
                }
            },
        });

        function editCostCenterGroup(dataItem) {
            $state.go('cost-center-group-add', { id: dataItem.GROUP_ID });
        }

        function deleteCostCenterGroup(id) {
            costCenterGroupService.deleteCostCenterGroup(id).then(function (data) {
                vm.closeModal();
                vm.costCenterGridDataSource.read();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
        }
        vm.closeModal = function () {
            vm.modal.center().close();
        };

        vm.clickMe = function (dataItem) {
            vm.NAME = dataItem.NAME;
            vm.GROUP_ID = dataItem.GROUP_ID;
            vm.modal.center().open();
        };
    }



})();



(function () {
    'use strict';
   var controllerId = 'costCenterController';
   angular.module('multitex.accounting').controller(controllerId, costCenterController);
   costCenterController.$inject = ['config', '$state', '$stateParams', 'costCenterService','notificationService'];
   function costCenterController(config, $state, $stateParams, costCenterService, notificationService) {
       var vm = this;
       vm.deleteCostCenter = deleteCostCenter;
       vm.searchText = '';
       vm.COST_CENTER_NAME = '';
       vm.COST_CENTER_ID = 0;
       vm.costCenterForm = {};
        vm.editCostCenter=editCostCenter;
        vm.costCenterGridOption = {
            
            refresh: true,
            pageSizes: true,
            pageable: true,
            editable: false,
            height:400,
            columns: [
                { field: "COST_CENTER_CODE", title: "CODE", type: "string", width: "50px" },
                { field: "GR_NAME", title: "GROUP", type: "string", width: "60px" },
                { field: "COST_CENTER_NAME", title: "Name", type: "string", width: "80px" },
               
                { field: "REMARKS", title: "Remarks", type: "string", width: "80px", filterable: false },
                {
                    title: "Action",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editCostCenter(dataItem)' ><i class='fa fa-edit'> Edit</i></button> "
                            + "<button class='btn btn-xs blue' ng-click='vm.clickMe(dataItem)' ><i class='fa fa-edit'> Delete</i></button>";
                    },
                    width: "40px"
                }
            ]
        };


       
       vm.costCenterGridDataSource = new kendo.data.DataSource({
           serverPaging: true,
           serverSorting: true,
           serverFiltering: true,
           pageSize: 10,
           transport: {
               read: function (e) {
                   var webapi = new kendo.data.transports.webapi({});
                   var params = webapi.parameterMap(e.data);
                   var pn = params.page;
                   var ps = params.pageSize;
                   return costCenterService.getCostCenters(vm.searchText, pn, ps).then(function (res) {
                       e.success(res);
                   }, function (errorMessage) {
                       notificationService.displayError(errorMessage.Message);
                   });
               }
           },
           schema: {
               data: "Result",
               total: "Total",
           }
       });
    function editCostCenter(dataItem) {
            $state.go('cost-center-add', { id: dataItem.COST_CENTER_ID });
        }

    function deleteCostCenter(id) {
        costCenterService.deleteCostCenter(id).then(function (data) {
            vm.closeModal();
            vm.costCenterGridDataSource.read();
        }, function (errorMessage) {
            notificationService.displayError(errorMessage.Message);
        });
    }
    vm.closeModal = function () {
        vm.modal.center().close();
    }

    vm.clickMe = function (dataItem) {
        vm.COST_CENTER_NAME = dataItem.COST_CENTER_NAME;
        vm.COST_CENTER_ID = dataItem.COST_CENTER_ID;
        vm.modal.center().open();
    };
}


 
})();



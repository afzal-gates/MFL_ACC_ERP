
(function () {
    'use strict';
    var controllerId = 'costCenterGroupAddController';
    angular.module('multitex.accounting').controller(controllerId, costCenterGroupAddController);
    costCenterGroupAddController.$inject = [ '$state', '$stateParams', 'costCenterGroupService','notificationService'];
   
    function costCenterGroupAddController( $state, $stateParams, costCenterGroupService,notificationService) {
        var vm = this;
        vm.id=0;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.costCenterGroup = {};
        vm.costCenterGroupForm = {};
        vm.save = save;
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.id = $stateParams.id;
        }
        init();
        function init() {
            costCenterGroupService.getCostCenterGroup(vm.id).then(function (res) {
                vm.costCenterGroup = res.Result;
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
            });
        }

    function save(){
          if (vm.id !== 0 && vm.id !== '') {
                updateCostCenterGroup();
            } else {
              insertCostCenterGroup();
            }
       }

        function insertCostCenterGroup() {
            costCenterGroupService.saveCostCenterGroup(vm.costCenterGroup).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
          
                });
        }
  
        function updateCostCenterGroup(){
          costCenterGroupService.updateCostCenterGroup(vm.id, vm.costCenterGroup).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
          
                });
         }
        function close() {
            $state.go('cost-center-groups');
        }
      
    }
})();




(function () {
    'use strict';
    var controllerId = 'costCenterAddController';
    angular.module('multitex.accounting').controller(controllerId, costCenterAddController);
    costCenterAddController.$inject = ['config', '$state', '$stateParams', 'costCenterService','notificationService'];
   
   function costCenterAddController( config,$state, $stateParams, costCenterService,notificationService) {
        var vm = this;
        vm.id=0;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.costCenter = {};
        vm.costCenterForm = {};
        vm.save = save;
        vm.close = close;
       vm.groups = [];
        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.id = $stateParams.id;
        }
        init();
        function init() {
            costCenterService.getCostCenter(vm.id).then(function (res) {
                vm.costCenter = res.Result.CostCenter;
                vm.groups = res.Result.Groups;
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
            });
        }

    function save(){
          if (vm.id !== 0 && vm.id !== '') {
                updateCostCenter();
            } else {
                insertCostCenter();
            }
       }
        function insertCostCenter() {
            costCenterService.saveCostCenter(vm.costCenter).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
          
                });
        }
  
      function updateCostCenter(){
            costCenterService.updateCostCenter(vm.id, vm.costCenter).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
          
                });
         }
        function close() {
            $state.go('cost-centers');
        }
      
    }
})();



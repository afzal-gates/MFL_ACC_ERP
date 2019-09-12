
(function () {
    'use strict';
    var controllerId = 'paymentModeAddController';
    angular.module('multitex.accounting').controller(controllerId, paymentModeAddController);
    paymentModeAddController.$inject = ['config', '$state', '$stateParams', 'paymentModeService','notificationService'];

    function paymentModeAddController(config, $state, $stateParams, paymentModeService,notificationService) {
        var vm = this;
        vm.id=0;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.paymentMode = {};
        vm.form = {};
        vm.save = save;
        vm.close = close;
        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.id = $stateParams.id;
        }
        init();
        function init() {

            paymentModeService.getPaymentMode(vm.id).then(function (res) {
                vm.paymentMode = res.Result;
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
            });
        }

    function save(){
          if (vm.id >0) {
                updatePaymentMode();
            } else {
              insertPaymentMode();
            }
       }

        function insertPaymentMode() {
            paymentModeService.savePaymentMode(vm.paymentMode).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
                });
        }
  
        function updatePaymentMode(){
            paymentModeService.updatePaymentMode(vm.id, vm.paymentMode).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
         }
        function close() {
            $state.go('paymentmodes');
        }
      
    }
})();



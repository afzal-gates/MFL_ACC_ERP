
(function () {
    'use strict';
    var controllerId = 'currencyAddController';
    angular.module('multitex.accounting').controller(controllerId, currencyAddController);
    currencyAddController.$inject = ['config', '$state', '$stateParams', 'currencyService','notificationService'];
   
    function currencyAddController(config, $state, $stateParams, currencyService,notificationService) {
        var vm = this;
        vm.id=0;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.currency = {};
        vm.currencyForm = {};
        vm.save = save;
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.id = $stateParams.id;
        }
        init();
        function init() {
            currencyService.getCurrency(vm.id).then(function (res) {
                vm.currency = res.Result;
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
            });
        }

    function save(){
          if (vm.id>0) {
                updateCurrency();
          } else {
                insertCurrency();
            }
       }

        function insertCurrency() {
            currencyService.saveCurrency(vm.currency).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
          
                });
        }
  
        function updateCurrency() {
            currencyService.updateCurrency(vm.id, vm.currency).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
                });
         }
        function close() {
            $state.go('currencies');
        }
      
    }
})();



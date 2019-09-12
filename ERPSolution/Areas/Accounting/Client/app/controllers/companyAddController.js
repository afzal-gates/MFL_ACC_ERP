
(function () {
    'use strict';
    var controllerId = 'companyAddController';
    angular.module('multitex.accounting').controller(controllerId, companyAddController);
    companyAddController.$inject = ['config', '$state', '$stateParams', 'companyService','notificationService'];

    function companyAddController(config, $state, $stateParams, companyService,notificationService) {
        var vm = this;
        vm.id=0;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.company = {};
        vm.companyForm = {};
        vm.save = save;
        vm.close = close;
        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.id = $stateParams.id;
        }
        init();
        function init() {

            companyService.getCompany(vm.id).then(function (res) {
                vm.company = res.Result;
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
            });
        }

    function save(){
          if (vm.id !== 0 && vm.id !== '') {
                updateCompany();
            } else {
                insertCompany();
            }
       }

    function insertCompany() {
        companyService.saveCompany(vm.company).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
                });
        }
  
    function updateCompany(){
        companyService.updateCompany(vm.id, vm.company).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
         }
        function close() {
            $state.go('companies');
        }
      
    }
})();



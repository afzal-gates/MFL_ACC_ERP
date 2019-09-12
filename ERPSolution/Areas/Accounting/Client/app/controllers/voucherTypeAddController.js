////////// Start CoaController Controller
(function () {
    'use strict';
    var controllerId = 'VchTypeController';
    angular.module('multitex.accounting').controller(controllerId, VchTypeController);
    VchTypeController.$inject = ['config', '$state', '$stateParams', 'voucherTypeService','notificationService'];

    function VchTypeController( config,$state, $stateParams, voucherTypeService,notificationService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.save = save;
        vm.close = close;
        vm.form = {};
        init();
        function init() {
            voucherTypeService.getVoucherType($stateParams.voucherTypeId).then(function (res) {
                vm.form = res.Result;
                vm.showSplash = false;
               
            }, function (err) {
                console.log(err);
            });
        }


        function save() {
            voucherTypeService.saveVoucherType(vm.form).then(function (res) {
                close();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
          
                });
        }
        function close() {
            $state.go('VchTypeList');
        }
    }
})();



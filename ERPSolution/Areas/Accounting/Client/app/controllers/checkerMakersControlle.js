(function () {
    'use strict';
    var controllerId = 'checkerMakersController';
    angular.module('multitex.accounting').controller(controllerId, checkerMakersController);
    checkerMakersController.$inject = ['config', '$state', '$stateParams', 'checkerMakerService', 'notificationService'];
    function checkerMakersController(config, $state, $stateParams, checkerMakerService, notificationService) {
        var vm = this;
        vm.updateVoucherMasterPostId = updateVoucherMasterPostId;
        vm.updateBillReconciliation = updateBillReconciliation;
        function updateVoucherMasterPostId() {
            checkerMakerService.updateVoucherMasterPostId().then(function (res) {
                notificationService.displayInfo("Sucessfully updated!");
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
        }
        function updateBillReconciliation() {
            checkerMakerService.updateBillReconciliation().then(function (res) {
                notificationService.displayInfo("Sucessfully updated!");
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
        }
        
    }

}) ();



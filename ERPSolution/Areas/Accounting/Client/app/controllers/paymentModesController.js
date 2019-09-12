(function () {
    'use strict';
   var controllerId = 'paymentModesController';
    angular.module('multitex.accounting').controller(controllerId, paymentModesController);
    paymentModesController.$inject = ['config', '$state', '$stateParams', 'paymentModeService','notificationService'];
    function paymentModesController(config, $state, $stateParams, paymentModeService, notificationService) {
        var vm = this;
        vm.deletePaymentMode = deletePaymentMode;
        vm.PAYMENT_MODE_ID = 0;
        vm.form = {};
        vm.searchKey = '';
        vm.editPaymentMode = editPaymentMode;
        vm.gridOption = {
   
            pageable: false,
            editable: false,
       
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
                { field: "REF_CODE", title: "CODE", type: "string", width: "10px" },
                { field: "PM_NAME", title: "Name", type: "string", width: "80px" },
                { field: "SHORT_NAME", title: "Short", type: "string", width: "20px" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "50px" },
                {
                    title: "Action",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editPaymentMode(dataItem)' ><i class='fa fa-edit'> Edit</i></button> "
                         + "<button class='btn btn-xs blue' ng-click='vm.clickMe(dataItem)' ><i class='fa fa-edit'> Delete</i></button>";
                    },
                    width: "40px"
                }
            ]
        };

        vm.gridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return paymentModeService.getPaymentModes(vm.searchKey).then(function (res) {
                        e.success(res.Result);
                    }, function (errorMessage) {
                        notificationService.displayError(errorMessage.Message);
                    });
                }
            },
        });

        function editPaymentMode(dataItem) {
            $state.go('paymentmode-add', { id: dataItem.PAYMENT_MODE_ID });
        }

        function deletePaymentMode(id) {
            paymentModeService.deletePaymentMode(id).then(function (data) {
            vm.closeModal();
            vm.gridDataSource.read();
        }, function (errorMessage) {
            notificationService.displayError(errorMessage.Message);
        });
    }
    vm.closeModal = function () {
        vm.modal.center().close();
    }
        vm.search = function () {
            vm.gridDataSource.read();
        }
    vm.clickMe = function (dataItem) {
        vm.NAME = dataItem.NAME;
        vm.PAYMENT_MODE_ID = dataItem.PAYMENT_MODE_ID;
        vm.modal.center().open();
    };
}


 
})();



(function () {
    'use strict';
   var controllerId = 'currenciesController';
    angular.module('multitex.accounting').controller(controllerId, currenciesController);
    currenciesController.$inject = ['config', '$state', '$stateParams', 'currencyService','notificationService'];
    function currenciesController(config, $state, $stateParams, currencyService, notificationService) {
       var vm = this;
        vm.deleteCurrency = deleteCurrency;
       vm.FORMAL_NAME = '';
       vm.CURRENCY_ID = 0;
       vm.currencyForm = {};
        vm.editCurrency = editCurrency;
        vm.currencyGridOption = {
            sortable: true,
            scrollable: {
             virtual: true,
             scrollable:true
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
                { field: "FORMAL_NAME", title: "Formal Name", type: "string", width: "80px" },
                { field: "SYMBOL", title: "Symbole", type: "string", width: "50px" },
                { field: "RATE", title: "Rate", type: "string", width: "50px" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "150px", filterable: false },
                {
                    title: "Action",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editCurrency(dataItem)' ><i class='fa fa-edit'> Edit</i></button> " 
                            + "<button class='btn btn-xs blue' ng-click='vm.clickMe(dataItem)' ><i class='fa fa-edit'> Delete</i></button>";
                    },
                    width: "60px"
                }
            ]
        };

        vm.currencyGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return currencyService.getCurrencies().then(function (res) {
                        e.success(res.Result);
                    }, function (errorMessage) {
                        notificationService.displayError(errorMessage.Message);
                    });
                }
            },
        });

        function editCurrency(dataItem) {
            $state.go('currency-add', { id: dataItem.CURRENCY_ID });
        }

    function deleteCurrency(id) {
        currencyService.deleteCurrency(id).then(function (data) {
            vm.closeModal();
            vm.currencyGridDataSource.read();
        }, function (errorMessage) {
            notificationService.displayError(errorMessage.Message);
        });
    }
    vm.closeModal = function () {
        vm.modal.center().close();
    }

        vm.clickMe = function (dataItem) {
            vm.FORMAL_NAME = dataItem.FORMAL_NAME;
            vm.CURRENCY_ID = dataItem.CURRENCY_ID;
            vm.modal.center().open();
    };
}


 
})();



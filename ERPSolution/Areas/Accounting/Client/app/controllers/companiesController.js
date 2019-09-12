(function () {
    'use strict';
   var controllerId = 'companiesController';
   angular.module('multitex.accounting').controller(controllerId, companiesController);
   companiesController.$inject = ['config', '$state', '$stateParams', 'companyService','notificationService'];
   function companiesController(config, $state, $stateParams, companyService, notificationService) {
       var vm = this;
       vm.deleteCompany = deleteCompany;
       vm.COMP_NAME = '';
       vm.COMPANY_ID = 0;
       vm.companyForm = {};
       vm.editCompany = editCompany;
       vm.companyGridOption = {
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
                { field: "COMP_CODE", title: "CODE", type: "string", width: "40px" },
                { field: "COMP_NAME", title: "Name", type: "string", width: "80px" },
                { field: "PREFIX", title: "PREFIX", type: "string", width: "40px" },
                { field: "DESCRIPTION", title: "DESCRIPTION", type: "string", width: "150px", filterable: false },
                {
                    title: "Action",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editCompany(dataItem)' ><i class='fa fa-edit'> Edit</i></button> "
                         + "<button class='btn btn-xs blue' ng-click='vm.clickMe(dataItem)' ><i class='fa fa-edit'> Delete</i></button>";
                    },
                    width: "80px"
                }
            ]
        };

        vm.companyGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return companyService.getCompanies().then(function (res) {
                        e.success(res.Result);
                    }, function (errorMessage) {
                        notificationService.displayError(errorMessage.Message);
                    });
                }
            },
        });

    function editCompany(dataItem) {
            $state.go('company-add', { id: dataItem.COMPANY_ID });
        }

    function deleteCompany(id) {
        companyService.deleteCompany(id).then(function (data) {
            vm.closeModal();
            vm.companyGridDataSource.read();
        }, function (errorMessage) {
            notificationService.displayError(errorMessage.Message);
        });
    }
    vm.closeModal = function () {
        vm.modal.center().close();
    }

    vm.clickMe = function (dataItem) {
        vm.COMP_NAME = dataItem.COMP_NAME;
        vm.COMPANY_ID = dataItem.COMPANY_ID;
        vm.modal.center().open();
    };
}


 
})();



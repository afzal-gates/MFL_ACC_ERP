(function () {
    'use strict';
   var controllerId = 'voucherTypeListController';
    angular.module('multitex.accounting').controller(controllerId, voucherTypeListController);
    voucherTypeListController.$inject = [ 'config', '$state', '$stateParams', 'voucherTypeService'];
    function voucherTypeListController(config, $state, $stateParams, voucherTypeService) {
        var vm = this;
        vm.frmVchType = {};
        vm.title="Voucher Type Etry Section "
        vm.voucherTypeName = '';
        vm.vOUCHERTYPEID = 0;
        vm.editData = editData;
        vm.deleteData = deleteData;
        vm.vouchertypeGridOption = {
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
                { field: "VOUCHERTYPEID", title: "ID", type: "number", width: "50px" },
                { field: "TYPENAME", title: "Name", type: "string", width: "80px" },
               
                { field: "REMARKS", title: "Code", type: "string", width: "150px", filterable: false },
                {
                    title: "Action",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editData(dataItem)' ><i class='fa fa-edit'> Edit</i></button>"
                            //+ "<button class='btn btn-xs blue' ng-click='vm.clickMe(dataItem)' ><i class='fa fa-edit'> Delete</i></button>"
                      
                    },
                    width: "40px"
                }
            ]
        };
        vm.clickMe = function (dataItem) {
            vm.voucherTypeName = dataItem.TYPENAME;
            vm.vOUCHERTYPEID = dataItem.VOUCHERTYPEID;
            vm.modal.center().open();
        };
        vm.vouchertypeGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return voucherTypeService.getVoucherTypes().then(function (res) {
                        e.success(res.Result);
               
                    }, function (err) {
                        console.log(err);
                    });
                }
            },
        });

        vm.closeModal = function () {
            vm.modal.center().close();
        }
        function deleteData(id) {
            voucherTypeService.deleteVoucherType(id).then(function (res) {
                vm.closeModal();
                vm.vouchertypeGridDataSource.read();
            }, function (err) {
                console.log(err);
            });
         
        }
        function editData(dataItem) {
            $state.go('VchType', { voucherTypeId: dataItem.VOUCHERTYPEID });
        }
    }
 
})();



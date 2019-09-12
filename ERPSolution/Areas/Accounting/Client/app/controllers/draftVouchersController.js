(function () {
    'use strict';
    var controllerId = 'draftVouchersController';
    angular.module('multitex.accounting').controller(controllerId, draftVouchersController);
    draftVouchersController.$inject = ['config', '$state', '$stateParams', 'draftVoucherService', 'unilayerChartOfAccountService', 'notificationService'];
    function draftVouchersController(config, $state, $stateParams, draftVoucherService, chartOfAccountService, notificationService) {
        var vm = this;
        vm.BDT_VALUE = 0;
        vm.PostTypes = [];
        vm.post_id = '';
        vm.searchRefNo = "";
        vm.searchVhcNo = "";
        vm.searchDate = "";
        vm.keyText = '';
        vm.isChecqVisisbile = false;
        vm.isCashVisisbile = false;
        vm.VOUCHER_MASTER_ID = 0;
        vm.btnText = 'Save';
        vm.costCenters = [];
        vm.currencies = [];
        vm.voucherTypes = [];
        vm.paymentModes = [];
        vm.ACC_VOUCHER_DETAIL = {};
        vm.saveDrTemVoucherDetail = saveDrTemVoucherDetail;
        vm.saveCrTemVoucherDetail = saveCrTemVoucherDetail;
        vm.editVoucher = editVoucher;
        vm.deleteVoucher = deleteVoucher;
        vm.createNew = createNew;
        vm.showReport = showReport;
        vm.showSplash = true;
        vm.errors = null;
        vm.dr_cr_amt = 0;
        vm.formal_name = "";
        vm.levelText ='CHEQUE';
        vm.Title = $state.current.Title || '';
        vm.voucher = {};
        vm.voucherForm = {};
        vm.save = save;
        vm.close = close;
        vm.accountHead = {};
        vm.moveToRate = moveToRate;
        vm.moveAndConvert = moveAndConvert;
        vm.VisisbileChecq = VisisbileChecq;
        vm.ChangeLabelName = ChangeLabelName;
        vm.deleteTemVoucherDetail = deleteTemVoucherDetail;
        vm.editVchrDtl = editVchrDtl;
        vm.IsEqualAmt = true;
        vm.amnt = 0;
        vm.symbol = "";
        function VisisbileChecq(voucherType) {
            if (voucherType == 3 || voucherType == 4) {
                vm.isChecqVisisbile = true;
                vm.isCashVisisbile = true;
                vm.voucher.PAY_TYPE = "A";
            } else {
                vm.isChecqVisisbile = false;
                vm.isCashVisisbile = false;
            }
            if (voucherType == 3 || voucherType == 4 || voucherType == 1 || voucherType == 2) {
                if (voucherType == 1 || voucherType == 2) {
                    vm.voucher.PAY_TYPE = "C";
                }
                vm.isCashVisisbile = true;
            } else {
                vm.isCashVisisbile = false;
            }
        }
       
        init();
        function init() {
            draftVoucherService.getVoucher('').then(function (res) {
               
                vm.voucherTypes = res.Result.VoucherTypes;
                vm.costCenters = res.Result.CostCenters;
                vm.paymentModes = res.Result.PaymentModes;
                vm.ACC_VOUCHER_DETAIL = res.Result.ACC_VOUCHER_DETAIL;
                vm.currencies = res.Result.Currencies;
                vm.voucher.POST_ID = res.Result.ACC_VOUCHER_MASTER.POST_ID;
                vm.voucher.VOUCHER_NO = res.Result.ACC_VOUCHER_MASTER.VOUCHER_NO;
                vm.voucher.VOUCHER_MASTER_ID = res.Result.ACC_VOUCHER_MASTER.VOUCHER_MASTER_ID;
                vm.voucher.IS_POSTED = res.Result.ACC_VOUCHER_MASTER.IS_POSTED;
                if (vm.voucher.VOUCHER_MASTER_ID<= 0) {
                    vm.ACC_VOUCHER_DETAIL.COST_CENTER_ID = 1;
                }
                readOnlyRate(vm.ACC_VOUCHER_DETAIL.CURRENCY_ID);
                vm.voucher.POST_DATE = new Date(vm.voucher.POST_DATE);
                vm.voucher.CHQ_DATE = new Date(vm.voucher.CHQ_DATE);
                vm.showSplash = false;
                
            }, function (err) {
            });
        }
        vm.selectedCostCenter = {};
        vm.COST_CENTER_NAME = "";
        vm.costCenterOptions = {
            placeholder: "Select Costcented...",
            dataTextField: "Text",
            dataValueField: "Value",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.costCenters);
                    }
                },
            },
            select: function (e) {
                var item = this.dataItem(e.item.index());
                vm.selectedCostCenter = item;
            },
        };
        function save(isPosted) {
            vm.voucher.IS_POSTED = isPosted;
            if (vm.voucher.VOUCHER_MASTER_ID !== 0 && vm.voucher.VOUCHER_MASTER_ID !== '') {
                updateVoucher();
            } else {
                insertVoucher();

            }
        }

        function insertVoucher() {
            draftVoucherService.saveVoucher(vm.voucher).then(function (res) {
                init();
                vm.voucherTemDetailGridDataSource.read();
                vm.voucherGridDataSource.read();
            }, function (errorMessage) {
                vm.voucher.IS_POSTED ='';
                notificationService.displayError(errorMessage.Message);
            });
        }

        function updateVoucher() {
            draftVoucherService.updateVoucher(vm.voucher.VOUCHER_MASTER_ID, vm.voucher).then(function (res) {
                init();
           
                vm.voucherTemDetailGridDataSource.read();
                vm.voucherGridDataSource.read();
                if (vm.voucher.VOUCHER_MASTER_ID > 0) {
                    vm.btnText = 'Save';
                }
            }, function (errorMessage) {
                vm.voucher.IS_POSTED ='';
                notificationService.displayError(errorMessage.Message);
            });
        }

        function editVoucher(dataItem) {

            draftVoucherService.getVoucher(dataItem.POST_ID).then(function (res) {
                vm.voucherTypes = res.Result.VoucherTypes;
                vm.costCenters = res.Result.CostCenters;
                vm.ACC_VOUCHER_DETAIL = res.Result.ACC_VOUCHER_DETAIL;
                vm.voucher = res.Result.ACC_VOUCHER_MASTER;
                vm.voucher.POST_DATE = new Date(vm.voucher.POST_DATE);
                vm.voucher.CHQ_DATE = new Date(vm.voucher.CHQ_DATE);
                vm.showSplash = false;
                ChangeLabelName(vm.voucher.PAYMENT_MODE_ID);
                VisisbileChecq(vm.voucher.VOUCHER_TYPE_ID);
                vm.voucherTemDetailGridDataSource.read();
                if (vm.voucher.VOUCHER_MASTER_ID > 0) {
                    vm.btnText = 'Update';
                }
            }, function (err) {

            });
        }
        function deleteVoucher() {
            draftVoucherService.deleteVoucher(vm.VOUCHER_MASTER_ID).then(function (res) {
                vm.closeModal();
                vm.voucherGridDataSource.read();
                vm.VOUCHER_MASTER_ID = 0;
                init();
                vm.voucherTemDetailGridDataSource.read();
            }, function (err) {
            });
        }
        function editVchrDtl(dataItem) {
           
            vm.ACC_VOUCHER_DETAIL.DESCRIPTION = dataItem.DESCRIPTION;
            vm.ACC_VOUCHER_DETAIL.COST_CENTER_ID = dataItem.COST_CENTER_ID;
            vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE = dataItem.EXCHANGE_RATE;
            vm.ACC_VOUCHER_DETAIL.CURRENCY_ID = dataItem.CURRENCY_ID;
            vm.COST_CENTER_NAME = dataItem.COST_CENTER_NAME;
            vm.accountHead.AC_CODE = dataItem.AC_CODE;
            vm.accountHead.MAIN_CODE = dataItem.MAIN_CODE;
            vm.accountHead.SUB_CODE = dataItem.SUB_CODE;
            vm.accountHead.SUB_NAME = dataItem.SUB_NAME;
            vm.selectedCostCenter.Value = dataItem.COST_CENTER_ID;
            vm.selectedCostCenter.Text =dataItem.COST_CENTER_NAME;
            vm.keyText = dataItem.SUB_NAME;
            vm.ACC_VOUCHER_DETAIL.DR_AMT = '';
            vm.ACC_VOUCHER_DETAIL.CR_AMT = '';
            if (dataItem.DR_AMT > 0) {
                vm.moveTo('DR_AMT');
                vm.ACC_VOUCHER_DETAIL.DR_AMT = dataItem.DR_AMT / dataItem.EXCHANGE_RATE;
            } else {
                vm.moveTo('CR_AMT');
                vm.ACC_VOUCHER_DETAIL.CR_AMT = dataItem.CR_AMT / dataItem.EXCHANGE_RATE;
            }
            readOnlyRate(vm.ACC_VOUCHER_DETAIL.CURRENCY_ID);
        }
        vm.clickMe = function (dataItem) {
            vm.VOUCHER_NO = dataItem.VOUCHER_NO;
            vm.VOUCHER_MASTER_ID = dataItem.VOUCHER_MASTER_ID;
            if (dataItem.IS_POSTED == 'P') {
                notificationService.displayWarning("Voucher already posted !");
            } else {
                vm.modal.center().open();
            }
      
        };
        vm.closeModal = function () {
            vm.modal.center().close();
        }
      
        function close() {
            $state.go('voucher-masters');
        }

        vm.options = {
            //optionLabel: "--  Prev. Style--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            var searchKey = params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            var searchKey = "";
                        }
                        return chartOfAccountService.GetAccountHeards(searchKey).then(function (res) {
                            e.success(res.Result);
                        
                        });
                    }
                },

                serverFiltering: true,
            },
            select: function (e) {
                var item = this.dataItem(e.item.index());
                vm.accountHead = item;
                var acCode = vm.accountHead.AC_CODE + vm.accountHead.MAIN_CODE + vm.accountHead.SUB_CODE;
                draftVoucherService.getLastNarrationByAccountHead(acCode).then(function(res) {
                    vm.ACC_VOUCHER_DETAIL.DESCRIPTION = res.Result;
                });
            },
           
            width: 800,
            minLength: 3,
            dataTextField:"SUB_NAME",
            headerTemplate: '<div class="dropdown-header k-widget k-header" >'+ '<span style="width:605px;">AC Head</span>' + '<span style="width:405px;">Group</span>' + '</div>',
            template: '<span class="k-state-default" style="width:595px;">#:AC_CODE##:MAIN_CODE##:SUB_CODE#--#:SUB_NAME#</span>' +
            '<span class="k-state-default">#:MAIN_NAME#</span>',


        };

       
        vm.voucherGridOption = {
            refresh: true,
            pageSizes: true ,
            pageable: true,
            editable: false,
            height: 200,

            // selectable: "cell",

            //change: function() {
            //    var cell = this.select();
            //    var cellIndex = cell[0].cellIndex;
            //    var column = this.columns[cellIndex];
            //    var dataItem = this.dataItem(cell.closest("tr"));
            //    alert("Selected value " + dataItem[column.field]);
            //},
            columns: [
                { field: "POST_ID", title: "REF No", type: "string" },
                { field: "VOUCHER_NO", title: "VOUCHER NO", type: "string"},
                {
                    field: "POST_DATE", title: "DATE", "type": "date", template: "#= kendo.toString(kendo.parseDate(POST_DATE, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
                },
                 { field: "TOTAL_AMT", title: "AMOUNT", type: "string" },
                   { field: "IS_POSTED", title: "STATUS", type: "string" },
                {
                    title: "ACTION",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editVoucher(dataItem)' ><i class='fa fa-edit'></i></button> "
                            + "<button class='btn btn-xs blue' ng-click='vm.showReport(dataItem)' ><i class='fa fa-file-pdf-o'></i></button> "
                            + "<button class='btn btn-xs blue' ng-click='vm.clickMe(dataItem)' ><i class='fa fa-remove'></i></button>";

                    },

                   
                }
            ]
        };
  
     
        vm.voucherGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize:5,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var pn = params.page;
                    var ps = params.pageSize;
                    return draftVoucherService.getVoucherList(vm.searchRefNo,vm.searchVhcNo,vm.searchDate,pn,ps).then(function (res) {
                        e.success(res);
                    }, function (errorMessage) {
                        notificationService.displayError(errorMessage.Message);
                    });
                }
            },
            schema: {
                data: "Result",
                total: "Total",
            }
        });
 
        vm.search=function() {
            vm.voucherGridDataSource.read();
        }

        function saveDrTemVoucherDetail() {

            vm.ACC_VOUCHER_DETAIL.AC_CODE = vm.accountHead.AC_CODE;
            vm.ACC_VOUCHER_DETAIL.MAIN_CODE = vm.accountHead.MAIN_CODE;
            vm.ACC_VOUCHER_DETAIL.SUB_CODE = vm.accountHead.SUB_CODE;
            vm.ACC_VOUCHER_DETAIL.SUB_NAME = vm.accountHead.SUB_NAME;
            //vm.ACC_VOUCHER_DETAIL.VOUCHER_TYPE_ID = vm.voucher.VOUCHER_TYPE_ID;
            if (vm.ACC_VOUCHER_DETAIL.DR_AMT > 0) {
                vm.ACC_VOUCHER_DETAIL.CR_AMT = 0;
                vm.amnt = vm.ACC_VOUCHER_DETAIL.DR_AMT;
                if (vm.BDT_VALUE > 0) {
                    vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE = vm.BDT_VALUE / vm.amnt;
                }
                vm.ACC_VOUCHER_DETAIL.DR_AMT = vm.ACC_VOUCHER_DETAIL.DR_AMT * vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
                saveTempVDetail();
              
                vm.moveTo('acc_head');
            }else {
              
                vm.moveTo('CR_AMT');
            }

        }

        function saveCrTemVoucherDetail() {

            vm.ACC_VOUCHER_DETAIL.AC_CODE = vm.accountHead.AC_CODE;
            vm.ACC_VOUCHER_DETAIL.MAIN_CODE = vm.accountHead.MAIN_CODE;
            vm.ACC_VOUCHER_DETAIL.SUB_CODE = vm.accountHead.SUB_CODE;
            vm.ACC_VOUCHER_DETAIL.SUB_NAME = vm.accountHead.SUB_NAME;
            vm.ACC_VOUCHER_DETAIL.VOUCHER_TYPE_ID = vm.voucher.VOUCHER_TYPE_ID;

            if (vm.ACC_VOUCHER_DETAIL.CR_AMT > 0) {
                vm.ACC_VOUCHER_DETAIL.DR_AMT = 0;
                vm.amnt = vm.ACC_VOUCHER_DETAIL.CR_AMT;
                if (vm.BDT_VALUE > 0) {
                    vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE = vm.BDT_VALUE / vm.amnt;
                }
                vm.ACC_VOUCHER_DETAIL.CR_AMT = vm.ACC_VOUCHER_DETAIL.CR_AMT * vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
               
                saveTempVDetail();
                vm.moveTo('acc_head');
            } else {
                vm.moveTo('CR_AMT');
            }
         
        }

        function saveTempVDetail() {
            if (vm.ACC_VOUCHER_DETAIL.CURRENCY_ID !== 1) {
                vm.ACC_VOUCHER_DETAIL.DESCRIPTION = vm.ACC_VOUCHER_DETAIL.DESCRIPTION + " " + vm.formal_name + "@:" + vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE + " Amount :" + vm.amnt;
            };
            vm.ACC_VOUCHER_DETAIL.COST_CENTER_ID = vm.selectedCostCenter.Value;
            draftVoucherService.saveTemVoucherDetail(vm.ACC_VOUCHER_DETAIL).then(function (res) {
                vm.ACC_VOUCHER_DETAIL.DESCRIPTION = '';
                vm.ACC_VOUCHER_DETAIL.DR_AMT = '';
                vm.ACC_VOUCHER_DETAIL.CR_AMT = '';
                vm.COST_CENTER_NAME = '';
                $("#acc_head").data("kendoAutoComplete").value("");
                vm.voucherTemDetailGridDataSource.read();

            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);

            });
        }
        vm.moveTo=function(id) {
            $('#' + id).focus();
            $('#' + id).css('border-color', 'black');
        }

        vm.voucherTemDetailGridOption = {
            editable: false,
            selectable: "cell",
            columns: [
                { field: "COST_CENTER_NAME", title: "COST CENTER", type: "string", width: "40px" },
                { field: "SUB_NAME", title: "ACCOUNT HEAD", type: "string", width: "80px" },
                { field: "DESCRIPTION", title: "NARRATION", type: "string", width: "50px" },
                { field: "DR_AMT", headerTemplate: '<div style="text-align:right;">DR</div>', type: "string", width: "15px", footerTemplate: '<div style="text-align:right;">#= kendo.toString(sum, "n3") #</div>', template: '<div style="text-align:right;">#= kendo.toString(DR_AMT, "n3") #</div>'},
                { field: "CR_AMT", headerTemplate: '<div style="text-align:right;">CR</div>', type: "string", width: "15px", footerTemplate: '<div style="text-align:right;">#= kendo.toString(sum, "n3") #</div>' , template: '<div style="text-align:right;">#= kendo.toString(CR_AMT, "n3") #</div>'},
                {
                    title: "ACTION", width: "25px",
                    template: function () {
                        return "<button  ng-click='vm.deleteTemVoucherDetail(dataItem)' ><i class='fa fa-remove'></i></button>"
                            + "<button  ng-click='vm.editVchrDtl(dataItem)' ><i class='fa fa-edit'></i></button>";
                    },

                }
            ]
        };

        vm.voucherTemDetailGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return draftVoucherService.getVoucherTemDetails().then(function (res) {
                        vm.IsEqualAmt = res.Result.IsEqualAmt;
                        if (res.Result.NdrAmt > 0) {
                            vm.ACC_VOUCHER_DETAIL.DR_AMT = res.Result.NdrAmt / vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
                            vm.dr_cr_amt = vm.ACC_VOUCHER_DETAIL.DR_AMT;
                        } else {
                            vm.ACC_VOUCHER_DETAIL.DR_AMT = '';
                        }
                        if (res.Result.NcrAmt > 0) {
                            vm.ACC_VOUCHER_DETAIL.CR_AMT = res.Result.NcrAmt / vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
                            vm.dr_cr_amt = vm.ACC_VOUCHER_DETAIL.CR_AMT;
                        } else {
                            vm.ACC_VOUCHER_DETAIL.CR_AMT = '';
                        }
                        e.success(res.Result.Vtemtdetails);
                    }, function (errorMessage) {
                        notificationService.displayError(errorMessage.Message);
                    });
                }
            },
            aggregate: [
                { field: "DR_AMT", aggregate: "sum" },
                { field: "CR_AMT", aggregate: "sum" }
            ]
        });

        function deleteTemVoucherDetail(dataItem) {
            draftVoucherService.deleteTemVoucherDetail(dataItem.TEMP_ID).then(function (res) {
                vm.voucherTemDetailGridDataSource.read();
                vm.btnText = 'Save';
                
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
        }
        function showReport(dataItem) {
            window.open("/AccountingReport/VoucherPrintReport?id=" + dataItem.VOUCHER_MASTER_ID, "_blank");
        }
        function createNew() {
            draftVoucherService.deleteVoucher(0).then(function (res) {
                vm.btnText = "Save";
                vm.ACC_VOUCHER_DETAIL.DESCRIPTION = ''
                init();
                //editVoucher({ POST_ID: '' });
                vm.voucherTemDetailGridDataSource.read();
            }, function (err) {
            });
        }
        function readOnlyRate(CURRENCY_ID) {
            if (CURRENCY_ID === 1) {
                $('#EXCHANGE_RATE').prop("readonly", true);
                $('#BDT_VALUE').prop("readonly", true);
                vm.BDT_VALUE = 0;
            } else {
                $('#EXCHANGE_RATE').prop("readonly", false);
                $('#BDT_VALUE').prop("readonly", false);
            }
        }
        function moveToRate(id) {
           
            var currency = vm.currencies.find(x => x.CURRENCY_ID === vm.ACC_VOUCHER_DETAIL.CURRENCY_ID);
            readOnlyRate(currency.CURRENCY_ID);
          
            vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE = currency.RATE;
            vm.symbol = currency.SYMBOL;
            vm.formal_name = currency.FORMAL_NAME;
            vm.moveTo(id);
        }

        function moveAndConvert(id) {
            vm.moveTo(id);
            if (vm.BDT_VALUE === 0) {
                if (vm.ACC_VOUCHER_DETAIL.DR_AMT>0) {
                    vm.ACC_VOUCHER_DETAIL.DR_AMT = vm.dr_cr_amt  / vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
                }
                if (vm.ACC_VOUCHER_DETAIL.CR_AMT > 0) {
                    vm.ACC_VOUCHER_DETAIL.CR_AMT = vm.dr_cr_amt  / vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
                }
            }
         
        }
        function ChangeLabelName(payment_mode_id) {
            if (payment_mode_id > 0) {
                var pm = vm.paymentModes.find(x=>x.PAYMENT_MODE_ID === payment_mode_id);
                vm.levelText = pm.PM_NAME;
            } else {
                vm.levelText = 'CHEQUE';
            }
           
        }
    }



})();



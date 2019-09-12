
(function () {
    'use strict';
    var controllerId = 'voucherMastersController';
    angular.module('multitex.accounting').controller(controllerId, voucherMastersController);
    voucherMastersController.$inject = ['config', '$state', '$stateParams', 'voucherMasterService', 'unilayerChartOfAccountService', 'notificationService'];
    function voucherMastersController(config, $state, $stateParams, voucherMasterService, chartOfAccountService, notificationService) {
        var vm = this;

        vm.CP = 1;//Cash pament voucher
        vm.CR = 2;//Cash receive voucher
        vm.BP = 3;//Bank pament voucher
        vm.BR = 4;//Cash Receive voucher
        vm.CV = 6;//Contra voucher
        vm.BV = 10;//Bill voucher
        vm.LCP = 9; //LC Payment Voucher
        vm.TK_CURRENCY = 1;

        vm.BILL_AMNT = 0;
        vm.BDT_VALUE = 0;
        vm.post_id = '';
        vm.searchRefNo = "";
        vm.searchVhcNo = "";
        vm.searchDate = "";
        vm.keyText = '';
        vm.isChecqVisisbile = false;
        vm.isCashVisisbile = false;
        vm.isBillVisisbile = false;
        vm.isBillVisisbile = true;
        vm.VOUCHER_MASTER_ID = 0;
        vm.btnText = 'Save';
        vm.costCenters = [];
        vm.currencies = [];
        vm.voucherTypes = [];
        vm.paymentModes = [];
        vm.ACC_VOUCHER_DETAIL = {};
        vm.showPandingBillReports = showPandingBillReports;
        vm.showPaidBillReports = showPaidBillReports;
        vm.GetDuplicateBillPaymentReport = GetDuplicateBillPaymentReport;
        vm.saveDrTemVoucherDetail = saveDrTemVoucherDetail;
        vm.saveCrTemVoucherDetail = saveCrTemVoucherDetail;
        vm.showPandingBills = showPandingBills;
        vm.editVoucher = editVoucher;
        vm.deleteVoucher = deleteVoucher;
        vm.createNew = createNew;
        vm.showReport = showReport;
        vm.showChequeReport = showChequeReport;
        vm.shortCutKeyPress = shortCutKeyPress;
        vm.showSplash = true;
        vm.errors = null;
        vm.dr_cr_amt = 0;
        vm.formal_name = "";
        vm.levelText = 'CHEQUE';
        vm.Title = $state.current.Title || '';
        vm.voucher = {};
        vm.voucherForm = {};
        vm.save = save;
        vm.close = close;
        vm.accountHead = { AC_CODE: '', MAIN_CODE: '', SUB_CODE: '', MAP_CODE: '' };
        vm.moveToRate = moveToRate;
        vm.moveAndConvert = moveAndConvert;
        vm.VisisbileChecq = VisisbileChecq;
        vm.ChangeLabelName = ChangeLabelName;
        vm.deleteTemVoucherDetail = deleteTemVoucherDetail;
        vm.editVchrDtl = editVchrDtl;
        vm.IsEqualAmt = true;
        vm.amnt = 0;
        vm.symbol = "";
        vm.fromdate = '';
        vm.todate = '';
        function VisisbileChecq(voucherType) {
            if (voucherType == vm.BP || voucherType == vm.BR || voucherType == vm.CV) {
                vm.isChecqVisisbile = true;
                vm.isCashVisisbile = true;
                vm.voucher.PAY_TYPE = "A";
            } else {
                vm.isChecqVisisbile = false;
                vm.isCashVisisbile = false;
            }

            if (voucherType == vm.BP || voucherType == vm.BR || voucherType == vm.CP || voucherType == vm.CR || voucherType == vm.LCP) {
                if (voucherType == vm.CP || voucherType == vm.CR) {
                    vm.voucher.PAY_TYPE = "C";
                }
                //  vm.isCashVisisbile = true;
                vm.isBillVisisbile = true;
            } else {
                vm.isCashVisisbile = false;
                vm.isBillVisisbile = false;

            }


            vm.ACC_VOUCHER_DETAIL.BILL_REF_ID = '';
            vm.BILL_AMNT = 0;
            vm.ACC_VOUCHER_DETAIL.DR_AMT = vm.BILL_AMNT;
            vm.ACC_VOUCHER_DETAIL.CR_AMT = vm.BILL_AMNT;
            if (voucherType == vm.BV) {
                vm.ACC_VOUCHER_DETAIL.BILL_REF_ID = vm.voucher.POST_ID;
                vm.isBillNoVisisbile = true;
                vm.levelText = "Bill";
            } else {
                vm.isBillNoVisisbile = false;
                vm.levelText = 'CHEQUE';
            }

            loadPaymentMode(voucherType);

        }
        function loadPaymentMode(voucherType) {
            voucherMasterService.getPaymentModes(voucherType).then(function (res) {
                vm.paymentModes = res.Result;
            }, function (err) {
                notificationService.displayError(err.Message);
            });
        }

        init();
        function init() {
            voucherMasterService.getVoucher('').then(function (res) {
                vm.voucherTypes = res.Result.VoucherTypes;
                vm.costCenters = res.Result.CostCenters;
               // vm.voucher.POST_DATE = res.Result.ACC_VOUCHER_MASTER.POST_DATE;
                vm.voucher.CHQ_DATE = res.Result.ACC_VOUCHER_MASTER.CHQ_DATE;
                vm.ACC_VOUCHER_DETAIL = res.Result.ACC_VOUCHER_DETAIL;
                vm.currencies = res.Result.Currencies;
                vm.voucher.POST_ID = res.Result.ACC_VOUCHER_MASTER.POST_ID;
                vm.voucher.VOUCHER_NO = res.Result.ACC_VOUCHER_MASTER.VOUCHER_NO;
                vm.voucher.VOUCHER_MASTER_ID = res.Result.ACC_VOUCHER_MASTER.VOUCHER_MASTER_ID;

                if (vm.voucher.VOUCHER_MASTER_ID <= 0) {
                    vm.ACC_VOUCHER_DETAIL.COST_CENTER_ID = 1;
                }
                readOnlyRate(vm.ACC_VOUCHER_DETAIL.CURRENCY_ID);
                vm.voucher.POST_DATE = new Date(vm.voucher.POST_DATE);
                vm.voucher.CHQ_DATE = new Date(vm.voucher.CHQ_DATE);
                vm.showSplash = false;

            }, function (err) {
                notificationService.displayError(err.Message);
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



        function save() {

            if (vm.voucher.VOUCHER_MASTER_ID !== 0 && vm.voucher.VOUCHER_MASTER_ID !== '') {
                updateVoucher();
            } else {
                insertVoucher();

            }
        }

        function insertVoucher() {
            voucherMasterService.saveVoucher(vm.voucher).then(function (res) {
                init();
                //  loadPaymentMode(vm.voucher.VOUCHER_TYPE_ID);
                vm.voucherTemDetailGridDataSource.read();
                vm.voucherGridDataSource.read();
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
        }

        function updateVoucher() {
            voucherMasterService.updateVoucher(vm.voucher.VOUCHER_MASTER_ID, vm.voucher).then(function (res) {
                init();
                // loadPaymentMode(vm.voucher.VOUCHER_TYPE_ID);
                vm.voucherTemDetailGridDataSource.read();
                vm.voucherGridDataSource.read();
                if (vm.voucher.VOUCHER_MASTER_ID > 0) {
                    vm.btnText = 'Save';
                }
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
        }

        function editVoucher(dataItem) {

            voucherMasterService.getVoucher(dataItem.POST_ID).then(function (res) {
                vm.voucherTypes = res.Result.VoucherTypes;
                vm.costCenters = res.Result.CostCenters;
                vm.ACC_VOUCHER_DETAIL = res.Result.ACC_VOUCHER_DETAIL;
                vm.voucher = res.Result.ACC_VOUCHER_MASTER;
               
                vm.voucher.POST_DATE = new Date(vm.voucher.POST_DATE);
                vm.voucher.CHQ_DATE = new Date(vm.voucher.CHQ_DATE);
                vm.showSplash = false;

                VisisbileChecq(vm.voucher.VOUCHER_TYPE_ID);
                // ChangeLabelName(vm.voucher.PAYMENT_MODE_ID);
                vm.voucherTemDetailGridDataSource.read();
                if (vm.voucher.VOUCHER_MASTER_ID > 0) {
                    vm.btnText = 'Update';
                }
            }, function (err) {

            });
        }
        function deleteVoucher() {
            voucherMasterService.deleteVoucher(vm.VOUCHER_MASTER_ID).then(function (res) {
                vm.closeModal();
                vm.voucherGridDataSource.read();
                vm.VOUCHER_MASTER_ID = 0;
                init();

                vm.voucherTemDetailGridDataSource.read();
            }, function (err) {
            });
        }
        function editVchrDtl(dataItem) {
            vm.ACC_VOUCHER_DETAIL.TEMP_ID = dataItem.TEMP_ID;

            vm.ACC_VOUCHER_DETAIL.DESCRIPTION = dataItem.DESCRIPTION;
            vm.ACC_VOUCHER_DETAIL.COST_CENTER_ID = dataItem.COST_CENTER_ID;
            vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE = dataItem.EXCHANGE_RATE;
            vm.ACC_VOUCHER_DETAIL.CURRENCY_ID = dataItem.CURRENCY_ID;
            vm.ACC_VOUCHER_DETAIL.BILL_NO = dataItem.BILL_NO;
            vm.ACC_VOUCHER_DETAIL.BILL_REF_ID = dataItem.BILL_REF_ID;
            vm.ACC_VOUCHER_DETAIL.BILL_DETAIL_ID = dataItem.BILL_DETAIL_ID;

            vm.COST_CENTER_NAME = dataItem.COST_CENTER_NAME;
            vm.accountHead.AC_CODE = dataItem.AC_CODE;
            vm.accountHead.MAIN_CODE = dataItem.MAIN_CODE;
            vm.accountHead.SUB_CODE = dataItem.SUB_CODE;
            vm.accountHead.SUB_NAME = dataItem.SUB_NAME;
            vm.selectedCostCenter.Value = dataItem.COST_CENTER_ID;
            vm.selectedCostCenter.Text = dataItem.COST_CENTER_NAME;
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
            vm.modal.center().open();
        };
        vm.closeModal = function () {
            vm.modal.center().close();
        };

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
                //voucherMasterService.getLastNarrationByAccountHead(acCode).then(function (res) {
                //    vm.ACC_VOUCHER_DETAIL.DESCRIPTION = res.Result;
                //});
            },

            width: 800,
            minLength: 3,
            dataTextField: "SUB_NAME",
            headerTemplate: '<div class="dropdown-header k-widget k-header" >' + '<span style="width:605px;">AC Head</span>' + '<span style="width:405px;">Group</span>' + '</div>',
            template: '<span class="k-state-default" style="width:595px;">#:AC_CODE##:MAIN_CODE##:SUB_CODE#--#:SUB_NAME#</span>' +
                '<span class="k-state-default">#:MAIN_NAME#</span>',


        };


        vm.voucherGridOption = {
            refresh: true,
            pageSizes: true,
            pageable: true,
            editable: false,
            height: 150,
            columns: [
                { field: "POST_ID", title: "REF No", type: "string" },
                { field: "VOUCHER_NO", title: "VOUCHER NO", type: "string" },
                { field: "TYPENAME", title: "VOUCHER TYPE", type: "string" },
                {
                    field: "POST_DATE", title: "DATE", "type": "date", template: "#= kendo.toString(kendo.parseDate(POST_DATE, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
                },
                { field: "TOTAL_AMT", title: "AMOUNT", type: "string" },
                {
                    title: "ACTION",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editVoucher(dataItem)' ><i class='fa fa-edit'></i></button> "
                            + "<button  class='btn btn-xs ' ng-click='vm.showReport(dataItem)' title='Other Voucher'><i class='fa fa-file-pdf-o'></i></button> "
                            + "<button ng-show=dataItem.VOUCHER_TYPE_ID==vm.BP  title='Bank payment Voucher' class='btn btn-xs red' ng-click='vm.showChequeReport(dataItem)' ><i class='fa fa-file-pdf-o'></i></button> "
                            + "<button class='btn btn-xs blue' ng-click='vm.clickMe(dataItem)' ><i class='fa fa-remove'></i></button>";

                    },

                }
            ]
        };


        vm.voucherGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var pn = params.page;
                    var ps = params.pageSize;
                    return voucherMasterService.getVoucherList(vm.searchRefNo, vm.searchVhcNo, vm.searchDate, pn, ps).then(function (res) {
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

        vm.search = function () {
            vm.voucherGridDataSource.read();
        }

        function saveDrTemVoucherDetail() {

            vm.ACC_VOUCHER_DETAIL.AC_CODE = vm.accountHead.AC_CODE;
            vm.ACC_VOUCHER_DETAIL.MAIN_CODE = vm.accountHead.MAIN_CODE;
            vm.ACC_VOUCHER_DETAIL.SUB_CODE = vm.accountHead.SUB_CODE;
            vm.ACC_VOUCHER_DETAIL.SUB_NAME = vm.accountHead.SUB_NAME;
            vm.ACC_VOUCHER_DETAIL.VOUCHER_TYPE_ID = vm.voucher.VOUCHER_TYPE_ID;
            if (vm.ACC_VOUCHER_DETAIL.DR_AMT > 0) {
                vm.ACC_VOUCHER_DETAIL.CR_AMT = 0;
                vm.amnt = vm.ACC_VOUCHER_DETAIL.DR_AMT;
                if (vm.BDT_VALUE > 0) {
                    vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE = vm.BDT_VALUE / vm.amnt;
                }
                vm.ACC_VOUCHER_DETAIL.DR_AMT = vm.ACC_VOUCHER_DETAIL.DR_AMT * vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
                if (vm.ACC_VOUCHER_DETAIL.VOUCHER_TYPE_ID > 0) {
                    saveTempVDetail();
                    vm.moveTo('acc_head');
                } else {
                    notificationService.displayError("Select Voucher Type !!");
                }
          

               
            } else {

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

                if (vm.ACC_VOUCHER_DETAIL.VOUCHER_TYPE_ID > 0) {
                    saveTempVDetail();
                    vm.moveTo('acc_head');
                } else {
                    notificationService.displayError("Select Voucher Type !!");
                }
            } else {
                vm.moveTo('CR_AMT');
            }

        }

        function saveTempVDetail() {
            if (vm.ACC_VOUCHER_DETAIL.CURRENCY_ID !== vm.TK_CURRENCY) {
                vm.ACC_VOUCHER_DETAIL.DESCRIPTION = vm.ACC_VOUCHER_DETAIL.DESCRIPTION + " " + vm.formal_name + "@:" + vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE + " Amount :" + round(vm.amnt,2);
            };
            if (vm.voucher.VOUCHER_TYPE_ID == vm.BV) {
                vm.ACC_VOUCHER_DETAIL.BILL_REF_ID = vm.voucher.POST_ID;
            }
            vm.ACC_VOUCHER_DETAIL.COST_CENTER_ID = vm.selectedCostCenter.Value;
            voucherMasterService.saveTemVoucherDetail(vm.ACC_VOUCHER_DETAIL).then(function (res) {
                vm.ACC_VOUCHER_DETAIL.DESCRIPTION = '';
                vm.ACC_VOUCHER_DETAIL.DR_AMT = '';
                vm.ACC_VOUCHER_DETAIL.CR_AMT = '';
                vm.ACC_VOUCHER_DETAIL.BILL_REF_ID = '';
                vm.ACC_VOUCHER_DETAIL.BILL_NO = '';
                vm.BILL_AMNT = 0;

                vm.COST_CENTER_NAME = '';

                $("#acc_head").data("kendoAutoComplete").value("");
                vm.voucherTemDetailGridDataSource.read();

            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);

            });
        }
        vm.moveTo = function (id) {
            $('#' + id).focus();
            $('#' + id).css('border-color', 'black');
        };

        vm.voucherTemDetailGridOption = {
            editable: false,
            selectable: true,
            columns: [
                { field: "COST_CENTER_NAME", title: "COST CENTER", type: "string", width: "40px" },
                { field: "SUB_NAME", title: "ACCOUNT HEAD", type: "string", width: "60px" },
                { field: "BILL_NO", title: "BILL NO", type: "string", width: "20px" },
                { field: "DESCRIPTION", title: "NOTE", type: "string", width: "50px" },
                { field: "DR_AMT", headerTemplate: '<div style="text-align:right;">DR</div>', type: "string", width: "15px", footerTemplate: '<div style="text-align:right;">#= kendo.toString(sum, "n3") #</div>', template: '<div style="text-align:right;">#= kendo.toString(DR_AMT, "n3") #</div>' },
                { field: "CR_AMT", headerTemplate: '<div style="text-align:right;">CR</div>', type: "string", width: "15px", footerTemplate: '<div style="text-align:right;">#= kendo.toString(sum, "n3") #</div>', template: '<div style="text-align:right;">#= kendo.toString(CR_AMT, "n3") #</div>' },
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
                    return voucherMasterService.getVoucherTemDetails().then(function (res) {
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
            voucherMasterService.deleteTemVoucherDetail(dataItem.TEMP_ID).then(function (res) {
                vm.voucherTemDetailGridDataSource.read();
                vm.btnText = 'Save';

            }, function (errorMessage) {
                notificationService.displayError(errorMessage.Message);
            });
        }
        function showReport(dataItem) {
            window.open("/AccountingReport/VoucherReport?id=" + dataItem.VOUCHER_MASTER_ID, "_blank");
        }

        function showChequeReport(dataItem) {
            window.open("/AccountingReport/Cheque?id=" + dataItem.VOUCHER_MASTER_ID, "_blank");
        }
        function createNew() {
            voucherMasterService.deleteVoucher(0).then(function (res) {
                vm.btnText = "Save";
                vm.ACC_VOUCHER_DETAIL.DESCRIPTION = '';
                init();
                //editVoucher({ POST_ID: '' });
                loadPaymentMode(vm.voucher.VOUCHER_TYPE_ID);
                vm.voucherTemDetailGridDataSource.read();
            }, function (err) {
            });
        }
        function readOnlyRate(CURRENCY_ID) {
            if (CURRENCY_ID === vm.TK_CURRENCY) {
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
                if (vm.ACC_VOUCHER_DETAIL.DR_AMT > 0) {
                    vm.ACC_VOUCHER_DETAIL.DR_AMT = vm.dr_cr_amt / vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
                }
                if (vm.ACC_VOUCHER_DETAIL.CR_AMT > 0) {
                    vm.ACC_VOUCHER_DETAIL.CR_AMT = vm.dr_cr_amt / vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE;
                }
            }
        }
        function ChangeLabelName(payment_mode_id) {
            if (payment_mode_id > 0) {
                var pm = vm.paymentModes.find(x => x.PAYMENT_MODE_ID === payment_mode_id);
                vm.levelText = pm.PM_NAME;
            } else {
                vm.levelText = 'CHEQUE';
            }

        }
        vm.billModal = {};
        function showPandingBills() {
            vm.billModal.center().open();
            vm.billVoucherGridDataSource.read();
        }
        vm.billVoucherGridOption = {
            refresh: true,

            editable: false,
            height: 400,
            width: 800,
            selectable: true,
            filterable: true,
            columns: [
                { field: "POST_ID", title: "REF No", type: "string", width: "15px" },
                { field: "VOUCHER_NO", title: "VOUCHER NO", type: "string", width: "15px" },
                { field: "CHQ_NO", title: "Bill NO", type: "string", width: "25px" },
                { field: "POST_DATE", title: "DATE", "type": "date", width: "20px", template: "#= kendo.toString(kendo.parseDate(POST_DATE, 'yyyy-MM-dd'), 'dd/MM/yyyy') #" },
                { field: "DESCRIPTION", title: "NOTE", type: "string", width: "50px" },
                { field: "BILL_AMT", title: "BILL AMT", type: "string", width: "15px" },
                { field: "PAY_AMT", title: "PAY AMT", type: "string", width: "15px" },
                { field: "FINAL_AMT", title: "FINAL AMT", type: "string", width: "15px" },
                { field: "CURRENCY", title: "CURRENCY", type: "string", width: "10px" },

            ]
        };

        vm.billVoucherGridDataSource = new kendo.data.DataSource({
            filterable: true,
            transport: {
                read: function (e) {
                    if (vm.accountHead.AC_CODE !== '' && vm.accountHead.MAIN_CODE !== '' && vm.accountHead.SUB_CODE !== '') {
                        return voucherMasterService.getPendingBills(vm.accountHead.AC_CODE, vm.accountHead.MAIN_CODE, vm.accountHead.SUB_CODE, vm.fromdate, vm.todate).then(function (res) {
                            e.success(res);
                        }, function (errorMessage) {
                            notificationService.displayError(errorMessage.Message);
                        });
                    } else {
                        e.success({ Result: [], Total: 0 });
                    }

                }
            },

            schema: {
                data: "Result",
                total: "Total",
            }
        });
      
        vm.filterPendingBills = function () {
            vm.billVoucherGridDataSource.read();
        };

        vm.onSelection = function (kendoEvent) {
            var grid = kendoEvent.sender;
            var selectedData = grid.dataItem(grid.select());
            vm.ACC_VOUCHER_DETAIL.BILL_REF_ID = selectedData.POST_ID;
            vm.ACC_VOUCHER_DETAIL.BILL_NO = selectedData.CHQ_NO;
            vm.ACC_VOUCHER_DETAIL.BILL_DETAIL_ID = selectedData.BILL_DETAIL_ID;
            vm.ACC_VOUCHER_DETAIL.CURRENCY_ID = selectedData.CURRENCY_ID;
            vm.ACC_VOUCHER_DETAIL.EXCHANGE_RATE = selectedData.EXCHANGE_RATE;
            
            vm.BILL_AMNT = selectedData.FINAL_AMT;

            if (vm.voucher.VOUCHER_TYPE_ID == vm.CP || vm.voucher.VOUCHER_TYPE_ID == vm.BP) {
                vm.ACC_VOUCHER_DETAIL.DR_AMT = vm.BILL_AMNT;

            }
            else if (vm.voucher.VOUCHER_TYPE_ID == vm.CR || vm.voucher.VOUCHER_TYPE_ID == vm.BR) {
                vm.ACC_VOUCHER_DETAIL.CR_AMT = vm.BILL_AMNT;

            }
            vm.billModal.close();
            vm.moveTo('Narration');
        };


        function shortCutKeyPress(event) {
            //return keyEventDesc + " (keyCode: " + (window.event ? keyEvent.keyCode : keyEvent.which) + ")";
            if (event.ctrlKey) {
                // logic here
             
            }
        }

        function showPandingBillReports(reportType) {
            if (vm.accountHead.AC_CODE !== '' && vm.accountHead.MAIN_CODE !== '' && vm.accountHead.SUB_CODE !== '') {
                window.open("/Accounting/AccountingReport/PandingBillsReport?ac_code=" + vm.accountHead.AC_CODE + "&main_code=" + vm.accountHead.MAIN_CODE + "&sub_code=" + vm.accountHead.SUB_CODE + "&reportType=" + reportType, "_blank");
            }
        }

        function showPaidBillReports(reportType) {
            if (vm.accountHead.AC_CODE !== '' && vm.accountHead.MAIN_CODE !== '' && vm.accountHead.SUB_CODE !== '') {
                window.open("/Accounting/AccountingReport/PaidBillsReport?ac_code=" + vm.accountHead.AC_CODE + "&main_code=" + vm.accountHead.MAIN_CODE + "&sub_code=" + vm.accountHead.SUB_CODE + "&reportType=" + reportType, "_blank");
            }
        }
        function GetDuplicateBillPaymentReport() {
            window.open("/Accounting/AccountingReport/DuplicateBillPaymentReport");
        }
        
        vm.getLastNarration = function () {
            voucherMasterService.getLastNarration().then(function (res) {
                vm.voucher.DESCRIPTION = res.Result;
            });
        };
        vm.onSelectionForNarration = function (kendoEvent) {
            var grid = kendoEvent.sender;
            var selectedData = grid.dataItem(grid.select());
            var acCode = selectedData.AC_CODE + selectedData.MAIN_CODE + selectedData.SUB_CODE;
            voucherMasterService.getLastNarrationByAccountHead(acCode).then(function (res) {
                vm.voucher.DESCRIPTION = res.Result;
            });
        };
    }

    function round(value, decimals) {
        return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
    }


})();



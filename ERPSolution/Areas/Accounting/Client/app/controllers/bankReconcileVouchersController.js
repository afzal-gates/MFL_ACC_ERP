
(function () {
    'use strict';
    var controllerId = 'bankReconcileVouchers';
    angular.module('multitex.accounting').controller(controllerId, bankReconcileVouchers);
    bankReconcileVouchers.$inject = ['voucherMasterService','unilayerChartOfAccountService','notificationService', 'config'];

    function bankReconcileVouchers(voucherMasterService, chartOfAccountService,notificationService, config) {
        var vm = this;
        vm.form = {};
        vm.vouchers = [];
        vm.bankAccountHeads = [];
        vm.status = 1;
        vm.fromdate='';
        vm.todate = '';
        vm.accountHead = '';
        vm.showReconcileVoucherReport = showReconcileVoucherReport;
        vm.showReconcileVouchers = showReconcileVouchers;
        vm.updateBankDate = updateBankDate;
        function showReconcileVouchers() {
            voucherMasterService.getBankReconsileVouchers(vm.fromdate, vm.todate, vm.accountHead, vm.status).then(function (res) {
                vm.vouchers = res.Result.VOUCHERAS;
                vm.BANK_BALANCE = res.Result.BANK_BALANCE;
                vm.BOOK_BALANCE = res.Result.BOOK_BALANCE;
                vm.BLANCE_DIFF = res.Result.BLANCE_DIFF;
                vm.showSplash = false;
                vm.bulkUpdateBankDate = bulkUpdateBankDate;
                //var list = vm.vouchers.filter(p => {
                //    p.BANK_DATE = new Date(p.BANK_DATE);
                //    return p;
                //});
                //vm.vouchers = list;
            }, function (err) {
                notificationService.displayError(err.Message);
            });
        }
        vm.actions = [
            { text: 'Skip this version' },
            { text: 'Remind me later' },
            { text: 'Install update', primary: true }
        ];

        init();
       function init(){
            chartOfAccountService.getBankAccounntHeads().then(function (res) {
                vm.bankAccountHeads = res.Result;
            }, function (err) {
                notificationService.displayError(err.Message);
            });
        }

        function updateBankDate(voucher) {
           
     
            voucherMasterService.updateBankDate(voucher.VOUCHER_MASTER_ID, voucher.BANK_DATE).then(function (resp) {
                    // vm.showReconcileVouchers();
                    remove(voucher);
                    notificationService.displaySuccess('Successfully Updated !');

                }, function (err) {
                    notificationService.displayError(err.Message);
                });

           
        }

        function showReconcileVoucherReport(reportType) {
            window.open("/Accounting/AccountingReport/BankReconciliationReport?accountHead=" + vm.accountHead + "&fromdate=" + vm.fromdate + "&todate=" + vm.todate + "&reportType=" + reportType + "&status=" + vm.status, "_blank");
        }

        function remove(item) {
            var index = vm.vouchers.indexOf(item);
            vm.vouchers.splice(index, 1); 
        }

        function bulkUpdateBankDate() {
            var selectedVDates = vm.vouchers.filter(v => v.BANK_DATE != null);

            angular.forEach(selectedVDates, function (value, key) {
               
                updateBankDate(value);
            });
        }

    }

    angular.module('multitex.accounting').directive('maskedDatePicker', [function () {
        return {
            link: function (scope, elem, attrs) {
                $(elem).kendoMaskedTextBox({
                    mask: "00/00/0000"
                });
                $(elem).removeClass("k-textbox");
            }
        };
    }]);
})();



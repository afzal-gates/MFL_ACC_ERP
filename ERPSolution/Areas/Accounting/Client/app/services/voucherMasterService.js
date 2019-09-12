(function () {
    'use strict';
    angular.module('multitex.accounting').service('voucherMasterService', ['apiHttpService', 'dataConstants', voucherMasterService]);

    function voucherMasterService(apiHttpService,dataConstants) {
        var service = {
            getVoucherList: getVoucherList,
            getVoucher: getVoucher,
            saveVoucher: saveVoucher,
            updateVoucher: updateVoucher,
            deleteVoucher: deleteVoucher,
            saveTemVoucherDetail: saveTemVoucherDetail,
            getVoucherTemDetails: getVoucherTemDetails,
            deleteTemVoucherDetail: deleteTemVoucherDetail,
            getLastNarrationByAccountHead: getLastNarrationByAccountHead,
            getBankReconsileVouchers: getBankReconsileVouchers,
            updateBankDate: updateBankDate,
            getPendingBills: getPendingBills,
            getPaymentModes: getPaymentModes,
            getLastNarration: getLastNarration
        
        };

        return service;
        function getLastNarrationByAccountHead(accountCode) {
            var url = dataConstants.VOUCHER_MASTER + 'get-last-narration?accountCode=' + accountCode;
            return apiHttpService.GET(url);
        }
        function getVoucherList(searchRefNo, searchVhcNo, searchDate, pn, ps) {
            var url = dataConstants.VOUCHER_MASTER + 'get-voucher-masters?searchRefNo=' + searchRefNo + "&searchVhcNo=" + searchVhcNo + "&searchDate=" + searchDate + "&pn=" + pn + "&ps=" + ps;
            return apiHttpService.GET(url);
        }

        function getVoucher(id) {
            var url = dataConstants.VOUCHER_MASTER + 'get-voucher-master?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveVoucher(data) {
            var url = dataConstants.VOUCHER_MASTER + 'save-voucher-master';
            return apiHttpService.POST(url, data);
        }

        function deleteVoucher(id) {
            var url = dataConstants.VOUCHER_MASTER + 'delete-voucher-master?id=' + id;
            return apiHttpService.DELETE(url);
        }
        function updateVoucher(id, data) {
            var url = dataConstants.VOUCHER_MASTER + 'update-voucher-master?id=' + id;
            return apiHttpService.PUT(url, data);
        }
        function saveTemVoucherDetail(data) {
            var url = dataConstants.VOUCHER_MASTER + 'save-tem-voucher-detail';
            return apiHttpService.POST(url,data);
        }

        function getVoucherTemDetails() {
            var url = dataConstants.VOUCHER_MASTER + 'get-voucher-temp-details';
            return apiHttpService.GET(url);
        }
        function deleteTemVoucherDetail(tempId) {
            var url = dataConstants.VOUCHER_MASTER + 'delete-tem-voucher-detail?id=' + tempId;
            return apiHttpService.DELETE(url);
        }

        function getBankReconsileVouchers(fromDate, toDate, accountHead,status) {
            var url = dataConstants.VOUCHER_MASTER + 'get-bank-reconcile-vouchers?fromDate=' + fromDate + "&toDate=" + toDate + "&accountHead=" + accountHead + "&status=" + status;
            return apiHttpService.GET(url);
        }
        function updateBankDate(id, bank_date) {
           
            var url = dataConstants.VOUCHER_MASTER + 'update-bank-date?id=' + id + "&bankDate=" + bank_date;
            return apiHttpService.PUT(url, {});
        }
        function getPendingBills(ac_code, main_code, sub_code,fromDate,toDate) {

            var url = dataConstants.VOUCHER_MASTER + 'get-pending-bills?ac_code=' + ac_code + "&main_code=" + main_code + "&sub_code=" + sub_code + "&fromDate=" + fromDate + "&toDate=" + toDate;
            return apiHttpService.GET(url);
        }
        function getPaymentModes(voucherTypeId) {

            var url = dataConstants.VOUCHER_MASTER + 'get-payment-modes?voucherTypeId=' + voucherTypeId;
            return apiHttpService.GET(url);
        }
        function getLastNarration() {

            var url = dataConstants.VOUCHER_MASTER + 'get-lat-narration';
            return apiHttpService.GET(url);
        }
    }
})();
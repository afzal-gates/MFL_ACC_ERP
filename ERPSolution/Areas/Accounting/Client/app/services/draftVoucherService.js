(function () {
    'use strict';
    angular.module('multitex.accounting').service('draftVoucherService', ['apiHttpService', 'dataConstants', draftVoucherService]);

    function draftVoucherService(apiHttpService, dataConstants) {
        var service = {
            getVoucherList: getVoucherList,
            getVoucher: getVoucher,
            saveVoucher: saveVoucher,
            updateVoucher: updateVoucher,
            deleteVoucher: deleteVoucher,
            saveTemVoucherDetail: saveTemVoucherDetail,
            getVoucherTemDetails: getVoucherTemDetails,
            deleteTemVoucherDetail: deleteTemVoucherDetail,
            getLastNarrationByAccountHead: getLastNarrationByAccountHead
        };

        return service;
        function getLastNarrationByAccountHead(accountCode) {
            var url = dataConstants.DRAFT_VOUCHER + 'get-last-narration?accountCode=' + accountCode;
            return apiHttpService.GET(url);
        }
        function getVoucherList(searchRefNo, searchVhcNo, searchDate, pn, ps) {

            var url = dataConstants.DRAFT_VOUCHER + 'get-draft-vouchers?searchRefNo=' + searchRefNo + "&searchVhcNo=" + searchVhcNo + "&searchDate=" + searchDate + "&pn=" + pn + "&ps=" + ps;
            return apiHttpService.GET(url);
        }

        function getVoucher(id) {
            var url = dataConstants.DRAFT_VOUCHER + 'get-draft-voucher?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveVoucher(data) {
            var url = dataConstants.DRAFT_VOUCHER + 'save-draft-voucher';
            return apiHttpService.POST(url, data);
        }

        function deleteVoucher(id) {
            var url = dataConstants.DRAFT_VOUCHER + 'delete-draft-voucher?id=' + id;
            return apiHttpService.DELETE(url);
        }
        function updateVoucher(id, data) {
            var url = dataConstants.DRAFT_VOUCHER + 'update-draft-voucher?id=' + id;
            return apiHttpService.PUT(url, data);
        }
        function saveTemVoucherDetail(data) {
            var url = dataConstants.DRAFT_VOUCHER + 'save-tem-draft-voucher';
            return apiHttpService.POST(url,data);
        }

        function getVoucherTemDetails() {
            var url = dataConstants.DRAFT_VOUCHER + 'get-temp-draft-vouchers';
            return apiHttpService.GET(url);
        }
        function deleteTemVoucherDetail(tempId) {
            var url = dataConstants.DRAFT_VOUCHER + 'delete-temp-draft-voucher?id=' + tempId;
            return apiHttpService.DELETE(url);
        }

   
        
    }
})();
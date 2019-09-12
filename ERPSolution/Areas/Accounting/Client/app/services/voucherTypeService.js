(function () {
    'use strict';
    angular.module('multitex.accounting').service('voucherTypeService', ['apiHttpService','dataConstants', voucherTypeService]);

    function voucherTypeService(apiHttpService,dataConstants) {
        var service = {
            getVoucherTypes: getVoucherTypes,
            getVoucherType: getVoucherType,
            saveVoucherType: saveVoucherType,
            deleteVoucherType: deleteVoucherType,
            updateVoucherType: updateVoucherType,
         
        };

        return service;
        function getVoucherTypes() {
            var url = dataConstants.VOUCHER_TYPE_URL + 'get-voucher-types';
            return apiHttpService.GET(url);
        }

        function getVoucherType(voucherTypeId) {
            var url = dataConstants.VOUCHER_TYPE_URL + 'get-voucher-type?voucherTypeId=' + voucherTypeId;
            return apiHttpService.GET(url);
        }

        function saveVoucherType(data) {
            var url = dataConstants.VOUCHER_TYPE_URL + 'save-voucher-type';
            return apiHttpService.POST(url, data);
        }

        function deleteVoucherType(id) {
            var url = dataConstants.VOUCHER_TYPE_URL + 'delete-voucher-type?id=' + id;
            return apiHttpService.DELETE(url);
        }
        function updateVoucherType(voucherTypeId, data) {
            var url = dataConstants.VOUCHER_TYPE_URL + 'update-board/' + boardId;
            return apiHttpService.PUT(url, data);
        }

      

    }
})();
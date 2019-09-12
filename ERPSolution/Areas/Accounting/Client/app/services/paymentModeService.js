(function () {
    'use strict';
    angular.module('multitex.accounting').service('paymentModeService', ['apiHttpService', 'dataConstants', paymentModeService]);

    function paymentModeService(apiHttpService,dataConstants) {
        var service = {
            getPaymentModes: getPaymentModes,
            getPaymentMode: getPaymentMode,
            savePaymentMode: savePaymentMode,
            updatePaymentMode: updatePaymentMode,
            deletePaymentMode: deletePaymentMode
        };

        return service;
        function getPaymentModes(searchKey) {
            var url = dataConstants.PAYMENT_MODE_URL + 'get-payment-modes?searchKey=' + searchKey;
            return apiHttpService.GET(url);
        }

        function getPaymentMode(id) {
            var url = dataConstants.PAYMENT_MODE_URL + 'get-payment-mode?id=' + id;
            return apiHttpService.GET(url);
        }

        function savePaymentMode(data) {
            var url = dataConstants.PAYMENT_MODE_URL + 'save-payment-mode';
            return apiHttpService.POST(url, data);
        }

        function deletePaymentMode(id) {
            var url = dataConstants.PAYMENT_MODE_URL + 'delete-payment-mode?id=' + id;
            return apiHttpService.DELETE(url);
        }
        function updatePaymentMode(id, data) {
            var url = dataConstants.PAYMENT_MODE_URL + 'update-payment-mode?id=' + id;
            return apiHttpService.PUT(url, data);
        }

      

    }
})();
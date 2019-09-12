(function () {
    'use strict';
    angular.module('multitex.accounting').service('currencyService', ['apiHttpService', 'dataConstants', currencyService]);

    function currencyService(apiHttpService,dataConstants) {
        var service = {
            getCurrencies: getCurrencies,
            getCurrency: getCurrency,
            saveCurrency: saveCurrency,
            updateCurrency: updateCurrency,
            deleteCurrency: deleteCurrency
        };

        return service;
        function getCurrencies() {
            var url = dataConstants.CURRENCY + 'get-currencies';
            return apiHttpService.GET(url);
        }

        function getCurrency(id) {
            var url = dataConstants.CURRENCY + 'get-currency?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveCurrency(data) {
            var url = dataConstants.CURRENCY + 'save-currency';
            return apiHttpService.POST(url, data);
        }

        function deleteCurrency(id) {
            var url = dataConstants.CURRENCY + 'delete-currency?id=' + id;
            return apiHttpService.DELETE(url);
        }
        function updateCurrency(id, data) {
            var url = dataConstants.CURRENCY + 'update-currency?id=' + id;
            return apiHttpService.PUT(url, data);
        }
    }
})();
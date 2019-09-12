(function () {
    'use strict';
    angular.module('multitex.accounting').service('stockClosingService', ['apiHttpService', 'dataConstants', stockClosingService]);

    function stockClosingService(apiHttpService,dataConstants) {
        var service = {
            stockClosings: stockClosings,
            saveStockClosings: saveStockClosings,
            updateStockHistory: updateStockHistory
        };

        return service;
        function stockClosings(year_code,month_code) {
            var url = dataConstants.STOCK_CLOSING + 'get-stock-closings?month_code=' + month_code + "&year_code=" + year_code ;
            return apiHttpService.GET(url);
        }

        function saveStockClosings(data) {
            var url = dataConstants.STOCK_CLOSING + 'save-stock-closing';
            return apiHttpService.POST(url, data);
        }
        function updateStockHistory(year_code,month_code) {
            var url = dataConstants.STOCK_CLOSING + 'save-monthly-stock-ledger?year_code=' + year_code + "&month_code=" + month_code;
            return apiHttpService.POST(url, {});
        }
    }
})();
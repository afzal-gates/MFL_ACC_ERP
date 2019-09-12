(function () {
    'use strict';
    angular.module('multitex.accounting').service('chartOfAccountService', ['apiHttpService', 'dataConstants', chartOfAccountService]);

    function chartOfAccountService(apiHttpService, dataConstants) {
        var service = {
            getChartOfAccounts: getChartOfAccounts,
            saveChartOfAccounts: saveChartOfAccounts,
            updateChartOfAccounts: updateChartOfAccounts,
            deleteChartOfAccounts: deleteChartOfAccounts,
            GetAccountHeards: GetAccountHeards
        };
        return service;
        function getChartOfAccounts() {
            var url = dataConstants.CHART_OF_ACCOUNT_URL + 'get-chart-of-accounts';
            return apiHttpService.GET(url);
        }
        function saveChartOfAccounts(data) {
            var url = dataConstants.CHART_OF_ACCOUNT_URL + 'save-chart-of-accounts';
            return apiHttpService.POST(url,data);
        }
       function updateChartOfAccounts(code,controlCode,data) {

            var url = dataConstants.CHART_OF_ACCOUNT_URL + 'update-chart-of-accounts?code='+code+'&controlCode='+controlCode;
            return apiHttpService.PUT(url,data);
        }
       function deleteChartOfAccounts(code,controlCode) {
            var url = dataConstants.CHART_OF_ACCOUNT_URL + 'delete-chart-of-accounts?id='+code+'&controlCode='+controlCode;
            return apiHttpService.DELETE(url);
       }

       function GetAccountHeards(searchKey) {
           var url = dataConstants.CHART_OF_ACCOUNT_URL + 'get-account-heads?searchKey='+searchKey;
           return apiHttpService.GET(url);
       }
    }
})();
(function () {
    'use strict';
    angular.module('multitex.accounting').service('unilayerChartOfAccountService', ['apiHttpService', 'dataConstants', unilayerChartOfAccountService]);

    function unilayerChartOfAccountService(apiHttpService, dataConstants) {
        var service = {
            getChartOfAccounts: getChartOfAccounts,
            saveChartOfAccounts: saveChartOfAccounts,
            updateChartOfAccounts: updateChartOfAccounts,
            deleteChartOfAccounts: deleteChartOfAccounts,
            GetAccountHeards: getAccountHeards,
            getControlChartOfAccounts: getControlChartOfAccounts,
            getCashAccounntHeads: getCashAccounntHeads,
            getBankAccounntHeads: getBankAccounntHeads,
            checkedIsGlTransactionExist: checkedIsGlTransactionExist,
            deleteGlAccount: deleteGlAccount
        };
        return service;
        function getChartOfAccounts() {
            var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'get-chart-of-accounts';
            return apiHttpService.GET(url);
        }
        function getControlChartOfAccounts() {
            var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'get-control-chart-of-accounts';
            return apiHttpService.GET(url);
        }
        function saveChartOfAccounts(data) {
            var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'save-chart-of-accounts';
            return apiHttpService.POST(url,data);
        }
       function updateChartOfAccounts(code,controlCode,data) {

           var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'update-chart-of-accounts?code=' + code + '&controlCode=' + controlCode;
            return apiHttpService.PUT(url,data);
        }
       function deleteChartOfAccounts(code,controlCode) {
           var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'delete-chart-of-accounts?id=' + code + '&controlCode=' + controlCode;
            return apiHttpService.DELETE(url);
       }

       function getAccountHeards(searchKey) {
           var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'get-account-heads?searchKey=' + searchKey;
       
           return apiHttpService.GET(url);
       }

       function getCashAccounntHeads() {
           var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'get-cash-account-heads';
           return apiHttpService.GET(url);
       }
       function getBankAccounntHeads() {
           var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'get-bank-account-heads';
           return apiHttpService.GET(url);
       }
       function checkedIsGlTransactionExist(gl_code, control_code) {
           var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'check-gl-transaction?gl_code=' + gl_code + '&control_code=' + control_code;
           return apiHttpService.GET(url);
       }

       function deleteGlAccount(gl_code, control_code,id) {
           var url = dataConstants.UNILAYER_CHART_OF_ACCOUNT_URL + 'delete-gl-account?gl_code=' + gl_code + '&control_code=' + control_code+"&id="+id;
           return apiHttpService.DELETE(url);
       }
       
    }
})();
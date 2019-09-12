(function () {
    'use strict';
    angular.module('multitex.accounting').service('companyService', ['apiHttpService', 'dataConstants', companyService]);

    function companyService(apiHttpService,dataConstants) {
        var service = {
            getCompanies: getCompanies,
            getCompany: getCompany,
            saveCompany: saveCompany,
            updateCompany: updateCompany,
            deleteCompany: deleteCompany
        };

        return service;
        function getCompanies() {
            var url = dataConstants.COMPANY_URL + 'get-companies';
            return apiHttpService.GET(url);
        }

        function getCompany(id) {
            var url = dataConstants.COMPANY_URL + 'get-company?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveCompany(data) {
            var url = dataConstants.COMPANY_URL + 'save-company';
            return apiHttpService.POST(url, data);
        }

        function deleteCompany(id) {
            var url = dataConstants.COMPANY_URL + 'delete-company?id=' + id;
            return apiHttpService.DELETE(url);
        }
        function updateCompany(id, data) {
            var url = dataConstants.COMPANY_URL + 'update-company?id=' + id;
            return apiHttpService.PUT(url, data);
        }

      

    }
})();
(function () {
    'use strict';
    angular.module('multitex.accounting').service('costCenterService', ['apiHttpService', 'dataConstants', costCenterService]);

    function costCenterService(apiHttpService,dataConstants) {
        var service = {
            getCostCenters: getCostCenters,
            getCostCenter: getCostCenter,
            saveCostCenter: saveCostCenter,
            updateCostCenter: updateCostCenter,
            deleteCostCenter: deleteCostCenter,
            getCostCenterSelectModels: getCostCenterSelectModels,
         
        };

        return service;
        function getCostCenters(searchText, pn, ps) {
            var url = dataConstants.COST_CENTER + 'get-cost-centers?searchText=' + searchText + "&pn=" + pn + "&ps=" + ps;
            return apiHttpService.GET(url);
        }
        function getCostCenterSelectModels() {
            var url = dataConstants.COST_CENTER + 'get-cost-center-select-models';
            return apiHttpService.GET(url);
        }

        function getCostCenter(id) {
            var url = dataConstants.COST_CENTER + 'get-cost-center?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveCostCenter(data) {
            var url = dataConstants.COST_CENTER + 'save-cost-center';
            return apiHttpService.POST(url, data);
        }

        function deleteCostCenter(id) {
            var url = dataConstants.COST_CENTER + 'delete-cost-center?id=' + id;
            return apiHttpService.DELETE(url);
        }
        function updateCostCenter(id, data) {
            var url = dataConstants.COST_CENTER + 'update-cost-center?id=' + id;
            return apiHttpService.PUT(url, data);
        }

      

    }
})();
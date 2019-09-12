(function () {
    'use strict';
    angular.module('multitex.accounting').service('costCenterGroupService', ['apiHttpService', 'dataConstants', costCenterGroupService]);

    function costCenterGroupService(apiHttpService,dataConstants) {
        var service = {
            getCostCenterGroups: getCostCenterGroups,
            getCostCenterGroup: getCostCenterGroup,
            saveCostCenterGroup: saveCostCenterGroup,
            updateCostCenterGroup: updateCostCenterGroup,
            deleteCostCenterGroup: deleteCostCenterGroup
           
         
        };

        return service;
        function getCostCenterGroups() {
            var url = dataConstants.COST_CENTER_GROUP + 'get-cost-center-groups';
            return apiHttpService.GET(url);
        }

        function getCostCenterGroup(id) {
            var url = dataConstants.COST_CENTER_GROUP + 'get-cost-center-group?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveCostCenterGroup(data) {
            var url = dataConstants.COST_CENTER_GROUP + 'save-cost-center-group';
            return apiHttpService.POST(url, data);
        }

        function deleteCostCenterGroup(id) {
            var url = dataConstants.COST_CENTER_GROUP + 'delete-cost-center-group?id=' + id;
            return apiHttpService.DELETE(url);
        }
        function updateCostCenterGroup(id, data) {
            var url = dataConstants.COST_CENTER_GROUP + 'update-cost-center-group?id=' + id;
            return apiHttpService.PUT(url, data);
        }

      

    }
})();
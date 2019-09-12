(function () {
    'use strict';
    angular.module('multitex.accounting').service('checkerMakerService', ['apiHttpService', 'dataConstants', checkerMakerService]);

    function checkerMakerService(apiHttpService,dataConstants) {
        var service = {
            updateVoucherMasterPostId: updateVoucherMasterPostId,
            updateBillReconciliation: updateBillReconciliation
        };
        return service;
        function updateVoucherMasterPostId() {
            var url = dataConstants.CHECKER_MAKER_URL + 'update-vocher-master-post-id';
            return apiHttpService.POST(url, {});
        }
        function updateBillReconciliation() {
            var url = dataConstants.CHECKER_MAKER_URL + 'update-bill-reconciliation';
            return apiHttpService.POST(url, {});
        }

        

    }
})();
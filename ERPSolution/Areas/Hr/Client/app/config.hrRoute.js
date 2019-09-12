(function () {
    'use strict';

    angular
        .module('multitex.hr')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {

        routerHelper.configureStates(getStates(), '/');


    }

    function getStates() {
        return [

            
            {
                state: 'IncrimentMemo',
                config: {
                    url: '/incrmemo',
                    views: {
                        'IncrimentMemo': {
                            controller: 'HrIncrMemoController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/Hr/_IncrMemoCreation"                            
                        }
                    },
                    Title: 'Incriment Memo'
                }
            },
            {
                state: 'IncrimentProposalH',
                config: {
                    url: '/incrProposalH/:pHR_YR_INCR_H_ID/:pHR_INCR_MEMO_ID/:pEMPLOYEE_TYPE_ID/:pHR_DEPARTMENT_ID/:pLK_FLOOR_ID/:pPROPOSE_BY/:pIS_FROM_BATCH_LIST',
                    //controller: 'HrIncrProposalHController',
                    //controllerAs: 'vm',
                    //templateUrl: "/Hr/Hr/_IncrProposalH",
                    views: {
                        'IncrimentProposalH': {
                            controller: 'HrIncrProposalHController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/Hr/_IncrProposalH"                          
                        }
                    },                    
                    Title: 'Yearly Incriment Proposal',
                    resolve: {
                        incrHdrData: function (HrService, $stateParams) {
                            if (angular.isDefined($stateParams.pHR_YR_INCR_H_ID) && $stateParams.pHR_YR_INCR_H_ID > 0) {

                                return HrService.getDataByFullUrl('/api/hr/HrIncriment/GetIncrHdr?pHR_YR_INCR_H_ID=' + $stateParams.pHR_YR_INCR_H_ID + '&pHR_INCR_MEMO_ID=' + $stateParams.pHR_INCR_MEMO_ID
                                            + '&pHR_DEPARTMENT_ID=' + $stateParams.pHR_DEPARTMENT_ID + '&pLK_FLOOR_ID=' + $stateParams.pLK_FLOOR_ID + '&pPROPOSE_BY=' + $stateParams.pPROPOSE_BY + '&pIS_FROM_BATCH_LIST=' + $stateParams.pIS_FROM_BATCH_LIST).then(function (res) {
                                                return res;
                                            });

                            }
                            else {
                                return {};
                            }
                            
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'IncrimentProposalH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "IncrimentProposalH@Dtl": {
                            controller: 'HrIncrProposalDController',
                            controllerAs: 'vm',
                            templateUrl: '/Hr/Hr/_IncrProposalD'                           
                        }
                    },                    
                    Title: 'Yearly Incriment Proposal',                    
                    reloadOnSearch: false
                }
            },
            {
                state: 'IncrProposalBatchList',
                config: {
                    url: '/incrPropBatchList/:pHR_INCR_MEMO_ID',
                    views: {
                        'IncrProposalBatchList': {
                            controller: 'HrIncrProposalBatchListController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/Hr/_IncrProposalBatchList"
                        }
                    },
                    Title: 'Yearly Incriment Proposal'
                }
            }
            



        ];
    }
})();

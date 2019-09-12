//(function() {
//    'use strict';

//    angular
//        .module('multitex.hr')
//        .run(appRun);

//    // appRun.$inject = ['routehelper'];

//    /* @ngInject */
//    function appRun(routehelper) {
//        routehelper.configureRoutes(getRoutes());
//    }

//    function getRoutes() {
//        return [
//            {
//                url: '/',
//                config: {
//                    templateUrl: '/hr/HrOffice/OfficeEntry',
//                    controller: 'HrOfficeController',
//                    controllerAs: 'vm'
//                    //foodata: 'OfficeEntry'
//                }
//            },
//            {
//                url: '/OfficeEntry',
//                config: {
//                    templateUrl: '/hr/HrOffice/OfficeEntry',
//                    controller: 'HrOfficeController',
//                    controllerAs: 'vm'
//                    //foodata: 'OfficeEntry'
//                }
//            },
//             {
//                 url: '/OfficeList',
//                 config: {
//                     templateUrl: '/hr/HrOffice/OfficeList',
//                     controller: 'HrOfficeController',
//                     controllerAs: 'vm',
//                     foodata: 'OfficeList'
//                 }
//             }
//        ];
//    }
//})();



(function () {
    'use strict';

    angular
        .module('multitex.security')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        
        routerHelper.configureStates(getStates(),'/');

      
    }

    function getStates() {
        return [

            {
                state: 'ChangePassword',
                config: {
                    url: '/changepassword',
                    controller: "ScUserController",
                    params: {
                        data: null
                    },
                    controllerAs: "vm",
                    templateUrl: '/Security/ScUser/_ChangePassword',
                    title: 'Change Password'
                }
            },

            {
                state: 'UserEntry',
                config: {
                    url: '/create',
                    controller: "ScUserController",
                    params:{
                        data:null
                    },
                    controllerAs:"vm",
                    templateUrl: '/Security/ScUser/_CreateUser',
                    title: 'Create User Profile'
                }
            },
            {
                state: 'UserList',
                config: {
                    url: '/List',
                    controller: "ScUserListController",
                    controllerAs: "vm",
                    templateUrl: '/Security/ScUser/_UserList',
                    title: 'User List'
                }
            }



        ];
    }
})();

(function () {
    'use strict';

    angular
        .module('multitex.core')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {

        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [
            {
                state: 'UserDashBoard',
                config: {
                    url: '/',
                    //controller: 'UserDashBoardController',
                    controllerAs:'vm',
                    templateUrl: '/Home/UserDashBoard'

                }
            },
            {
                state: 'SignInCaptcha',
                config: {
                    cache:false,
                    url: '/LoginCaptcha',
                    templateUrl: '/Security/ScUser/_CaptchaImage'

                }
            }



        ];
    }
})();

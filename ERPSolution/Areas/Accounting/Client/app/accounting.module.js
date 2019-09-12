(function () {
    'use strict';

    angular.module('multitex.accounting', []);

    angular.module('multitex.accounting').factory('sessionInjector', ['access_token', function (access_token) {
        var sessionInjector = {
            request: function (config) {
                config.headers.Authorization = 'Bearer ' + access_token;
                return config;
            }
        };
        return sessionInjector;
    }]);
    angular.module('multitex.accounting').config(['$httpProvider', function ($httpProvider) {

        $httpProvider.interceptors.push('sessionInjector');
    }]);


})();

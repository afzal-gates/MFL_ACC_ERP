(function () {
    'use strict';

    angular
        .module('blocks.router')
        .provider('routerHelper', routerHelperProvider);

    routerHelperProvider.$inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider'];
    /* @ngInject */
    function routerHelperProvider($locationProvider, $stateProvider, $urlRouterProvider) {
        /* jshint validthis:true */
        var config = {
            docTitle: undefined,
            resolveAlways: {}
        };

        //$locationProvider.html5Mode(true);

        this.configure = function (cfg) {
            angular.extend(config, cfg);
        };

        this.$get = RouterHelper;
        RouterHelper.$inject = ['$location', '$rootScope', '$state', 'logger'];
        /* @ngInject */
        function RouterHelper($location, $rootScope, $state, logger) {
            var handlingStateChangeError = false;
            var hasOtherwise = false;
            var stateCounts = {
                errors: 0,
                changes: 0
            };

            var service = {
                configureStates: configureStates,
                getStates: getStates,
                stateCounts: stateCounts
            };

            init();

            return service;

            ///////////////

            function configureStates(states, otherwisePath) {
                //console.log(states);
                states.forEach(function (state) {
                    state.config.resolve =
                        //angular.extend(state.config.resolve || {}, config.resolveAlways);
                        //console.log(state.config);
                    $stateProvider.state(state.state, state.config);
                });
                if (otherwisePath && !hasOtherwise) {
                    hasOtherwise = true;
                    $urlRouterProvider.otherwise(otherwisePath);
                }
            }

            function handleRoutingErrors() {
                // Route cancellation:
                // On routing error, go to the dashboard.
                // Provide an exit clause if it tries to do it twice.
                $rootScope.$on('$stateChangeError',
                    function (event, toState, toParams, fromState, fromParams, error) {
                        if (handlingStateChangeError) {
                            return;
                        }
                        stateCounts.errors++;
                        handlingStateChangeError = true;
                        var destination = (toState &&
                            (toState.title || toState.name || toState.loadedTemplateUrl)) ||
                            'unknown target';
                        var msg = 'Error routing to ' + destination + '. ' +
                            (error.data || '') + '. <br/>' + (error.statusText || '') +
                            ': ' + (error.status || '');
                        logger.warning(msg, [toState]);
                        $location.path('/');
                    }
                );
            }

            function init() {
                handleRoutingErrors();
                updateDocTitle();
            }

            function getStates() { return $state.get(); }

            function updateDocTitle() {
                $rootScope.$on('$stateChangeSuccess',
                    function (event, toState, toParams, fromState, fromParams) {
                        stateCounts.changes++;
                        handlingStateChangeError = false;
                        var title = config.docTitle + ' ' + (toState.title || '');
                        $rootScope.title = title; // data bind to <title>
                    }
                );
            }
        }
    }
})();



//(function() {
//    'use strict';

//    angular
//        .module('blocks.router')
//        .provider('routehelperConfig', routehelperConfig)
//        .factory('routehelper', routehelper);

//    routehelper.$inject = ['$location', '$rootScope', '$route', 'logger', 'routehelperConfig'];

//    // Must configure via the routehelperConfigProvider
//    function routehelperConfig() {
//        /* jshint validthis:true */
//        this.config = {
//            // These are the properties we need to set
//            // $routeProvider: undefined
//            // docTitle: ''
//            // resolveAlways: {ready: function(){ } }
//        };

//        this.$get = function() {
//            return {
//                config: this.config
//            };
//        };
//    }

//    function routehelper($location, $rootScope, $route, logger, routehelperConfig) {
//        var handlingRouteChangeError = false;
//        var routeCounts = {
//            errors: 0,
//            changes: 0
//        };
//        var routes = [];
//        var $routeProvider = routehelperConfig.config.$routeProvider;

//        var service = {
//            configureRoutes: configureRoutes,
//            getRoutes: getRoutes,
//            routeCounts: routeCounts
//        };

//        init();

//        return service;
//        ///////////////

//        function configureRoutes(routes) {
//            routes.forEach(function(route) {
//                route.config.resolve =
//                    angular.extend(route.config.resolve || {});
//                $routeProvider.when(route.url, route.config);
//            });
//            $routeProvider.otherwise({redirectTo: '/'});
//        }

//        function handleRoutingErrors() {
//            // Route cancellation:
//            // On routing error, go to the dashboard.
//            // Provide an exit clause if it tries to do it twice.
//            $rootScope.$on('$routeChangeError',
//                function(event, current, previous, rejection) {
//                    if (handlingRouteChangeError) {
//                        return;
//                    }
//                    routeCounts.errors++;
//                    handlingRouteChangeError = true;
//                    var destination = (current && (current.title || current.name || current.loadedTemplateUrl)) ||
//                        'unknown target';
//                    var msg = 'Error routing to ' + destination + '. ' + (rejection.msg || '');
//                    logger.warning(msg, [current]);
//                    $location.path('/');
//                }
//            );
//        }

//        function init() {
//            handleRoutingErrors();
//            updateDocTitle();
//        }

//        function getRoutes() {
//            for (var prop in $route.routes) {
//                if ($route.routes.hasOwnProperty(prop)) {
//                    var route = $route.routes[prop];
//                    var isRoute = !!route.title;
//                    if (isRoute) {
//                        routes.push(route);
//                    }
//                }
//            }
//            return routes;
//        }

//        function updateDocTitle() {
//            $rootScope.$on('$routeChangeSuccess',
//                function(event, current, previous) {
//                    routeCounts.changes++;
//                    handlingRouteChangeError = false;
//                    var title = routehelperConfig.config.docTitle + ' ' + (current.title || '');
//                    $rootScope.title = title; // data bind to <title>
//                }
//            );
//        }
//    }
//})();

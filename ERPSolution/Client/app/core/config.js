(function () {
    'use strict';

    var core = angular.module('multitex.core');

    core.config(toastrConfig);

    /* @ngInject */
    function toastrConfig(toastr) {
        toastr.options.timeOut = 4000;
        toastr.options.positionClass = 'toast-bottom-right';
    }
    
    var config = {
        appErrorPrefix: '[MultiTex Error] ', //Configure the exceptionHandler decorator
        appTitle: 'MultiTex',
        //imageBasePath: '/images/photos/',
        //unknownPersonImageSource: 'unknown_person.jpg',

        appDateFormat: 'dd-MMM-yyyy',
        appDateTimeFormat: 'dd-MMM-yyyy HH:mm:ss',
        appTimeFormat: 'HH:mm',

        appToastMsg: function (vMsg) {
                       
            //console.log(vMsg);
            if (vMsg != '') {

                var vMsgOrg = vMsg;
                var vMsgPrefix = vMsg.substr(0, 9);
                var vMsg = vMsg.substr(9);

                if (vMsgPrefix == 'MULTI-001') {
                    toastr.success(vMsg, "MultiTEX");
                    //vMsgSuccess = true;
                }
                else if (vMsgPrefix == 'MULTI-002') {
                    toastr.info(vMsg, "MultiTEX");
                }
                else if (vMsgPrefix == 'MULTI-003') {
                    toastr.warning(vMsg, "MultiTEX");
                }
                else if (vMsgPrefix == 'MULTI-005') {
                    toastr.error(vMsg, "MultiTEX");
                }
                else
                {
                    toastr.info(vMsgOrg, "MultiTEX");
                }

            }
        }

        
    };


    


    core.value('config', config);

    core.config(configure);

    configure.$inject = ['$compileProvider', '$logProvider',
          'exceptionHandlerProvider', 'routerHelperProvider'];
    /* @ngInject */
    function configure($compileProvider, $logProvider,
          exceptionHandlerProvider, routerHelperProvider) {

      

        $compileProvider.debugInfoEnabled(false);

        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
        exceptionHandlerProvider.configure(config.appErrorPrefix);
        configureStateHelper();

        ////////////////

        function configureStateHelper() {
            var resolveAlways = { /* @ngInject */
                //ready: function (dataservice) {
                //    return dataservice.ready();
                //}
              
            };

            routerHelperProvider.configure({
                docTitle: 'Gulp: ',
                resolveAlways: resolveAlways
            });
        }
    }
})();


//(function() {
//    'use strict';

//    var core = angular.module('multitex.core');

//    core.config(toastrConfig);

//    /* @ngInject */
//    function toastrConfig(toastr) {
//        toastr.options.timeOut = 4000;
//        toastr.options.positionClass = 'toast-bottom-right';
//    }

//    var config = {
//        appErrorPrefix: '[multitex ERP] ', //Configure the exceptionHandler decorator
//        appTitle: 'MultiTex ERP',
//        version: '1.0.0',

//        appDateFormat: 'dd-MMM-yyyy'
//    };
    

//    core.value('config', config);

//    core.config(configure);

//    /* @ngInject */
       
//    function configure ($logProvider, $routeProvider, routehelperConfigProvider, exceptionHandlerProvider) {
//        // turn debugging off/on (no info or warn)
//        if ($logProvider.debugEnabled) {
//            $logProvider.debugEnabled(true);
//        }

//        // Configure the common route provider
//        routehelperConfigProvider.config.$routeProvider = $routeProvider;
//        routehelperConfigProvider.config.docTitle = 'MultiTex ERP: ';
//        //var resolveAlways = { /* @ngInject */
//        //    ready: function(dataservice) {
//        //        return dataservice.ready();
//        //    }
//            // ready: ['dataservice', function (dataservice) {
//            //    return dataservice.ready();
//            // }]
//        //};
//        //routehelperConfigProvider.config.resolveAlways = resolveAlways;

//        // Configure the common exception handler
//        exceptionHandlerProvider.configure(config.appErrorPrefix);
//    }
//})();

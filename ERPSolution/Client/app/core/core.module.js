 (function() {
    'use strict';

    angular.module('multitex.core', [
        /*
         * Angular modules
         */
         'ngSanitize', 'ui.router', 'ngAnimate', 'ngMessages', 'ui.utils', 'kendo.directives',
        /*
         * Our reusable cross app code modules
         */
        'blocks.exception', 'blocks.logger', 'blocks.router',
        //'ui.bootstrap.datetimepicker',
        /*
         * 3rd Party modules
         */
         'ngplus', 'ui.utils.masks', 'ngStorage', 'oi.multiselect', 'ui.slimscroll', 'dualmultiselect', 'gm.datepickerMultiSelect',
         'SignalR', 'ngNotify'
    ]);
})();
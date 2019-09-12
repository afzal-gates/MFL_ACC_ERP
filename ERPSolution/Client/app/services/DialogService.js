(function () {
    'use strict';

    angular.module('multitex.core').factory('Dialog', ['$modal', Dialog]);
    function Dialog($modal) {

        var self = this;
        self.confirm=function(message, title, buttons) {
            var modalInstance = $modal.open({
                template: "<div class='modal-header' style='padding-bottom: 0px;padding-top: 0px;'><h3 class='title blue'>{{vm.properties.title}}</h3></div><div class='modal-body'>{{vm.properties.message}}</div><div class='modal-footer'><button class='btn btn-primary btn-sm' ng-click='vm.ok()'>{{vm.properties.buttons[0]}}</button><button class='btn btn-default btn-sm' ng-click='vm.cancel()'>{{vm.properties.buttons[1]}}</button></div>",
                controller: 'ConfirmModalController',
                windowClass: 'large-Modal',
                controllerAs: 'vm',
                resolve: {
                    data: function () {
                        return {
                            message: message,
                            title: title,
                            buttons: buttons
                        };
                    }
                },
                size: 'sm'
            });

            return modalInstance.result;
        }

        return self;
    }
})();
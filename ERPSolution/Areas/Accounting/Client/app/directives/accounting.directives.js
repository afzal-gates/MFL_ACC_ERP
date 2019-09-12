
(function () {
    "use strict";

    var app = angular.module('multitex.accounting');
    app.directive('yesNo', function () {
        return {
            restrict: 'E',
            scope: {},
            templateUrl: "/Areas/Accounting/Client/app/templates/labelYesNo.html",
            link: function ($scope, $element, $attrs) {
                $scope.getYesNoClass = function () {
                    if ($attrs.isTrue === 'true') {
                        return 'label label-success'
                    }
                    else {
                        return 'label label-default'
                    }
                };
                $scope.getYesNoValue = function () {
                    if ($attrs.isTrue === 'true') {
                        return 'Yes'
                    }
                    else {
                        return 'No'
                    }
                };
            }
        }
    });
    app.directive('ngConfirmationClick', ['$uibModal', function ($uibModal) {
        var ModalInstanceCtrl = function ($scope, $uibModalInstance) {
            $scope.ok = function () {
                $uibModalInstance.close();
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        };

        return {
            restrict: 'A',
            scope: {
                ngConfirmationClick: "&",
                ngConfirmationCancelClick: "&",
            },
            link: function (scope, element, attrs) {
                element.bind('click', function () {
                    var message = attrs.ngConfirmationMessage || "Are you sure ?";
                    var yesButtonText = attrs.ngConfirmationYesText || "Yes";
                    var cancelButtonText = attrs.ngConfirmationCancelText || "Cancel";

                    var modalHtml =
                        (attrs.ngConfirmationTitle ? ('<div class="modal-header"><h3>' + attrs.ngConfirmationTitle + '</h3></div>') : '')
                        + '<div class="modal-body"><h4>' + message + '</h4></div>';
                    modalHtml += '<div class="modal-footer"><button class="btn btn-primary" ng-click="cancel()"> ' + cancelButtonText + '</button><button class="btn btn-primary" ng-click="ok()"> ' + yesButtonText + '</button></div>';

                    var modalInstance = $uibModal.open({
                        animation: true,
                        size: 'md',
                        overflow: scroll,
                        template: modalHtml,
                        controller: ModalInstanceCtrl,

                    });

                    modalInstance.result.then(function () {
                        scope.ngConfirmationClick({});
                    }, function () {
                        scope.ngConfirmationCancelClick && scope.ngConfirmationCancelClick();
                    });
                });

            }
        };
    }]);

})();

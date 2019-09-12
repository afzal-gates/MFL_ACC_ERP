(function () {
    'use strict';

    angular.module('multitex').controller('ActiveLinkController', ['logger', 'config', '$scope', '$q', '$sessionStorage', ActiveLinkController]);

    function ActiveLinkController(logger, config, $scope, $q, $sessionStorage) {
   
        var vm = this;
        activate();

        function activate() {
            var promise = [getCurrentLevels()];
            return $q.all(promise).then(function () {
            });
        }
        //vm.datas = $localStorage.obj;

        function getCurrentLevels() {
            $scope.levels = $sessionStorage.levels;           
        }

        //MultitexSocket.on('error', function (ev, data) {
        //    console.log(data);
        //});    

        //MultitexSocket.on('socket:someEvent', function (data) {
        //    console.log(data);
        //    console.log($scope);
        //});
       
       

        //MenuDataService.getMenuData().then(function (res) {
        //    vm.datas = res;
        //    $localStorage.obj = res;
        //    return vm.datas;
        //});
        vm.clicked = function (level1, level2, level3) {
   

            var levels = [level1, level2, level3];
            console.log(levels);
            delete $sessionStorage.levels;
            $sessionStorage.levels = levels;
            //$scope.$apply();
        }

     
    }

    

})();
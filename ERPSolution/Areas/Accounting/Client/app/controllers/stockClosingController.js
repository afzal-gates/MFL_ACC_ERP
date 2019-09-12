
(function () {
    'use strict';
    var controllerId = 'stockClosingController';
    angular.module('multitex.accounting').controller(controllerId, stockClosingController);
    stockClosingController.$inject = ['config', '$state', '$stateParams', 'stockClosingService','notificationService'];
   
    function stockClosingController(config, $state, $stateParams, stockClosingService, notificationService) {
        var vm = this;
        vm.year_code=0;
        vm.month_code=0;
        vm.showSplash=true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.stocks = [];
        vm.years =[];
        vm.months = [];
        vm.stockClosingForm = {};
        vm.ShowStock = ShowStock;
        vm.save = save;
        vm.postToAccount = postToAccount;

        init();
        function init() {
       
            stockClosingService.stockClosings(vm.year_code, vm.month_code).then(function (res) {
                vm.years = res.Result.Years;
                vm.months = res.Result.Months;
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
            });
        }
        function ShowStock() {

            stockClosingService.stockClosings(vm.year_code, vm.month_code).then(function (res) {
                vm.stocks = res.Result.Stocks;
            }, function (err) {
                console.log(err);
            });
          
        }

        function save(stock) {

            stockClosingService.saveStockClosings(stock).then(function (res) {
                vm.moveTo(stock.STOCK_CLOSING_ID + 1);
                notificationService.displaySuccess("Save Amount Successully !!");
            }, function (err) {
                notificationService.displayError(err.Message);
            });

        }
        function postToAccount() {

            stockClosingService.updateStockHistory(vm.year_code, vm.month_code).then(function (res) {
                notificationService.displaySuccess('Monthly Stock closing saved sucessfully !!');
            }, function (err) {
                notificationService.displayError(err.Message);

            });

        }
        vm.moveTo = function (id) {
            $('#' + id).focus();
            $('#' + id).css('border-color', 'black');
        };
    }
})();



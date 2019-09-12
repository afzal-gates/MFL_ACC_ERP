(function () {
    'use strict';
    angular.module('multitex.core').factory('DashBoardService', ['$http', 'exception', '$q', '$filter', DashBoardService]);

    function DashBoardService($http, exception, $q, $filter) {
        var self = this;


        self.SalaryAdvReqesterNotif = function () {
            return $http.get('/Home/SalaryAdvReqesterNotif')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.SalaryAdvApproverNotif = function () {
            return $http.get('/Home/SalaryAdvApproverNotif')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }

        self.OnlineLeaveReqesterNotif = function () {
            return $http.get('/Home/OnlineLeaveReqesterNotif')
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.OnlineLeaveApproverNotif = function () {
            return $http.get('/Home/OnlineLeaveApproverNotif')
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getNotifications = function (HR_LEAVE_TRANS_ID) {
            var data = [];
            return $http({
                method: 'get',
                url: '/hr/hrleavetrans/Notifications',
                params: { HR_LEAVE_TRANS_ID: HR_LEAVE_TRANS_ID}
            }).then(function (res) {
                angular.forEach(res.data, function (val, key) {
                    val['ACTION_DATE'] = $filter('date')(moment(val.ACTION_DATE)._d, 'medium');
                    val['CREATION_DATE'] = $filter('date')(moment(val.CREATION_DATE)._d, 'medium');
                    
                    data.push(val);
                });

                console.log(data);
                return data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.getSalAdvNotif = function (HR_SAL_ADVANCE_ID) {
            var data = [];
            return $http({
                method: 'get',
                url: '/hr/HrSalAdvance/SalAdvNotif',
                params: { HR_SAL_ADVANCE_ID: HR_SAL_ADVANCE_ID }
            }).then(function (res) {
                angular.forEach(res.data, function (val, key) {
                    val['ACTION_DATE'] = $filter('date')(moment(val.ACTION_DATE)._d, 'medium');
                    val['CREATION_DATE'] = $filter('date')(moment(val.CREATION_DATE)._d, 'medium');

                    data.push(val);
                });
                return data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }




        return self;
    }


})();
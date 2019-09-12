(function () {
    'use strict';
    angular.module('multitex.security').factory('SecurityService', ['$http', 'exception', SecurityService]);

    function SecurityService($http, exception) {
        var self = this;

        self.getReportListData = function (pSC_MENU_ID) {            
            return $http({
                method: 'post',
                url: '/Security/ScRoleMenu/ReportListData',
                data: { pSC_MENU_ID: pSC_MENU_ID }
            }).then(function (res) {
                return res;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getName = function () {
            return $http.get('/Security/Users/SignUp')
                  .then(geComplete).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                  });

                function geComplete(data, status, headers, config) {
                    return data.data;
                }
        }

        //self.getEmployeeData = function () {
        //    return $http.get('/Hr/HrEmployee/employeelistdata')
        //           .then(function (result) {
        //               return result.data;
        //           }).catch(function (message) {
        //               exception.catcher('XHR loading Failded')(message);
        //           });
        //}


        self.getEmployeeList = function (deptID, jobStatus, empCode, empOldCode) {
            return $http({
                method: 'post',
                url: '/Hr/HrEmployee/EmployeeListData',
                data: { pHR_DEPARTMENT_ID: deptID, pLK_JOB_STATUS_ID: jobStatus, pEMPLOYEE_CODE: empCode, pOLD_EMP_CODE: empOldCode }

            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }




        self.getUserDataList = function () {
            return $http.get('/Security/ScUser/UserDataList')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }


        self.getRolesByUserId = function (SC_USER_ID) {
            return $http({
                method: 'get',
                url: '/Security/ScUser/RolesByUserId',
                params: { SC_USER_ID: SC_USER_ID }
            }).then(function (result) {
                     return result.data;
                 }).catch(function (message) {
                     exception.catcher('XHR loading Failded')(message);
                 });
        }

       
        self.saveUser = function (data,p,token) {
            return $http({
                method: 'post',
                url: '/Security/ScUser/SaveUser',
                data: { ob1: data, p: p },
                headers: { "RequestVerificationToken": token }
            }).then(function (res) {
                return res;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.updateUser = function (data, p, token) {            
            return $http({
                method: 'post',
                url: '/Security/ScUser/UpdateUser',
                data: { ob2: data, p: p },
                headers: { "RequestVerificationToken": token }
            }).then(function (res) {
                return res;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.saveRoleData = function (data) {
            return $http({
                method: 'post',
                url: '/Security/ScUser/saveRoleData',
                data: { SC_USER_ID: data.SC_USER_ID, SC_ROLE_ID: data.SC_ROLE_ID }
            }).then(function (res) {
                return res;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.updateRoleData = function (data) {
            return $http({
                method: 'post',
                url: '/Security/ScUser/updateRoleData',
                data: { SC_USER_ID: data.SC_USER_ID, SC_ROLE_ID: data.SC_ROLE_ID, SC_USER_ROLE_ID: data.SC_USER_ROLE_ID }
            }).then(function (res) {
                return res;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }





        return self;
    }


})();
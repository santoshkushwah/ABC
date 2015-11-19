    var app = angular.module("ApplicationModule", ["ngRoute"]);

    app.factory('authInterceptor', function ($rootScope, $q, $window) {
        return {
            request: function (config) {
                config.headers = config.headers || {};
                if ($window.sessionStorage.token) {
                    config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
                }
                return config;
            },
            response: function (response) {
                if (response.status === 401) {
                    // handle the case where the user is not authenticated
                }
                return response || $q.when(response);
            }
        };
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

    app.run(function ($rootScope) {
        $rootScope.showMe = false;
    });

    //The Factory used to define the value to
    //Communicate and pass data across controllers

    app.factory("ShareData", function () {
        return { value: 0 }
    });

    //Defining Routing
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider.when('/login',
                           {
                               templateUrl: 'EmployeeInfo/login.html',
                               controller: 'LoginController'
                           });
        $routeProvider.when('/showemployees',
                            {
                                templateUrl: 'EmployeeInfo/ShowEmployees.html',
                                controller: 'ShowEmployeesController'
                            });
        $routeProvider.when('/addemployee',
                            {
                                templateUrl: 'EmployeeInfo/AddEmployee.html',
                                controller: 'AddEmployeeController'
                            });
        $routeProvider.when("/editemployee",
                            {
                                templateUrl: 'EmployeeInfo/EditEmployee.html',
                                controller: 'EditEmployeeController'
                            });
        $routeProvider.when('/deleteemployee',
                            {
                                templateUrl: 'EmployeeInfo/DeleteEmployee.html',
                                controller: 'DeleteEmployeeController'
                            });
        $routeProvider.otherwise(
                            {
                                redirectTo: '/login'
                            });
        $locationProvider.html5Mode(true);

    }]);

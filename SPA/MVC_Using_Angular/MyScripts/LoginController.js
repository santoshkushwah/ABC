app.controller('LoginController', function ($scope, $http, $window, $location, $rootScope) {
    $scope.user = { username: 'admin', password: '123' };
    $scope.message = '';
    $scope.submit = function () {
        $http
          .post('api/authenticate', $scope.user)
          .success(function (data, status, headers, config) {
              $window.sessionStorage.token = data;
              $scope.message = 'Welcome';
              $rootScope.showMe = true;
              $location.path('/showemployees');
          })
          .error(function (data, status, headers, config) {

              // Erase the token if the user fails to log in
              delete $window.sessionStorage.token;

              // Handle login errors here
              $scope.message = 'Error: Invalid user or password';

              $rootScope.showMe = false;
          });
    };
});
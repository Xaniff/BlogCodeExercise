angular.module('blog').controller('LoginController', function ($scope, $http, $rootScope, $cookieStore, $state) {
	$scope.container = {};
	$scope.isProcessingLogin = false;
	$scope.loginError = '';

	if ($rootScope.authenticated == true)
		$state.go('root.home');

	$scope.loginClick = function(login) {
		if ($scope.container.loginForm.$valid) {
			$scope.isProcessingLogin = true;
			$http.post('/api/auth/login', login)
				.success(function(data, status, headers, config) {
					$cookieStore.put('blog_session_token', data.token);
					$rootScope.userName = data.userName;
					$scope.authenticated = true;
					$scope.loginError = '';
					$rootScope.isProcessingLogin = false;
					$state.go('root.home');
				})
				.error(function(data, status, headers, config) {
					$cookieStore.remove('blog_session_token');
					$scope.loginError = data.error;
					$scope.isProcessingLogin = false;
				});
		}
	}
});
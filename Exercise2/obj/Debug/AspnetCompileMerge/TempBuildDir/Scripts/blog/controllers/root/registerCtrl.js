angular.module('blog').controller('RegisterController', function($scope, $http, $rootScope, $cookieStore, $state) {
	$scope.container = {};
	$scope.isProcessingRegistration = false;
	$scope.registrationError = '';

	if ($rootScope.authenticated == true)
		$state.go('root.home');

	$scope.registerClick = function(register) {
		if ($scope.container.registrationForm.$valid) {
			$scope.isProcessingRegistration = true;
			$http.post('/api/auth/register', register)
				.success(function(data, status, headers, config) {
					$cookieStore.put('blog_session_token', data.token);
					$rootScope.userName = data.userName;
					$scope.registrationError = '';
					$scope.isProcessingRegistration = false;
					$state.go('root.home');
				})
				.error(function(data, status, headers, config) {
					$cookieStore.remove('blog_session_token');
					$scope.registrationError = data.error;
					$scope.isProcessingRegistration = false;
				});
		}
	}
});
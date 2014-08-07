angular.module('blog').controller('LogoutController', function($cookieStore, $rootScope, $location, $state) {
	$cookieStore.remove('blog_session_token');
	$rootScope.authenticated = false;
	$state.go('root.home');
});
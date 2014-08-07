angular.module('blog').controller('ApplicationController', function($rootScope, $cookieStore) {
	$rootScope.authenticated = ($cookieStore.get('blog_session_token') != null);
});
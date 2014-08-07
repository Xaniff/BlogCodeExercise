angular.module('blog').controller('HomeController', function($scope, $location, $http, $rootScope, $cookieStore, $state, update, postRepositoryService, commentRepositoryService, authorId) {
	$scope.userName = {};
	$scope.container = {};
	$scope.isProcessingAuthentication = false;
	$scope.isProcessingLogin = false;
	$scope.isProcessingRegistration = false;

	$scope.loadedRecentComments = false;
	$scope.loadedRecentPosts = false;
	$scope.loadedPosts = false;
	$scope.loadedAuthors = false;

	$scope.registrationError = '';
	$scope.loginError = '';

	$scope.posts = {};

	update.client.updatePosts = function onNewMessage(message) {
		// We just got a new update message from the server; populate the main post bit
		//Check if we're filtering by the author id
		if (authorId != null) {
			postRepositoryService.getPostsByAuthor(authorId).then(function (promise) {
				$scope.posts = promise;
				$scope.loadedPosts = true;
			});
		} else {
			postRepositoryService.getPostsByDate().then(function (promise) {
				$scope.posts = promise;
				$scope.loadedPosts = true;
			});
		}
		//$scope.$apply();
	};

	//Retrieve the recent posts for the current page
	//postRepositoryService.getRecentPosts().then(function(promise) {
	//	$scope.recentPosts = promise;
	//	$scope.loadedRecentPosts = true;
	//});

	//Retrieve all the current authors
	postRepositoryService.getAllAuthors().then(function(promise) {
		$scope.authors = promise;
		$scope.loadedAuthors = true;
	});

	//Retrieve the recent comments for the current page
	//commentRepositoryService.getRecentComments().then(function(promise) {
	//	$scope.recentComments = promise;
	//	$scope.loadedRecentComments = true;
	//});

	//Populate the main post bit
	//Check if we're filtering by the author id
	if (authorId != null) {
		postRepositoryService.getPostsByAuthor(authorId).then(function (promise) {
			$scope.posts = promise;
			$scope.loadedPosts = true;
		});
	} else {
		postRepositoryService.getPostsByDate().then(function (promise) {
			$scope.posts = promise;
			$scope.loadedPosts = true;
		});
	}
	
	$scope.loginClick = function (login) {
		if ($scope.container.loginForm.$valid) {
			var username = login.username;
			$scope.isProcessingAuthentication = true;
			$scope.isProcessingLogin = true;
			$http.post('/api/auth/login', login)
				.success(function (data, status, headers, config) {
					$cookieStore.put('blog_session_token', data.token);
					$rootScope.userName = data.username;
					$rootScope.authenticated = true;
					$scope.loginError = '';
					$scope.isProcessingAuthentication = false;
					$scope.isProcessingLogin = false;
				})
				.error(function (data, status, headers, config) {
					$cookieStore.remove('blog_session_token');
					$scope.loginError = data.error;
					$scope.isProcessingAuthentication = false;
					$scope.isProcessingLogin = false;
				});
		}
	};

	$scope.registerClick = function(register) {
		if ($scope.container.registrationForm.$valid) {
			$scope.isProcessingAuthentication = true;
			$scope.isProcessingRegistration = true;
			$http.post('/api/auth/register', register)
				.success(function(data, status, headers, config) {
					$cookieStore.put('blog_session_token', data.token);
					$rootScope.userName = data.username;
					$rootScope.authenticated = true;
					$scope.registrationError = '';
					$scope.isProcessingAuthentication = false;
					$scope.isProcessingRegistration = false;
				})
				.error(function(data, status, headers, config) {
					$cookieStore.remove('blog_session_token');
					$scope.registrationError = data.error;
					$scope.isProcessingAuthentication = false;
					$scope.isProcessingRegistration = false;
				});
		}
	};
});
angular.module('blog').controller('HomeController', function ($scope, $location, $http, $rootScope, $cookieStore, $state, signalRFactory, postRepositoryService, commentRepositoryService, authorId) {
	//Related to signalR
	$scope.updateHub = signalRFactory.connection.update;

	//Set up the signalR functions (must have at least one)
	$scope.updateHub.client.ReceiveNotifyUpdatePosts = function(postIdentification) {
		console.log('Received notification to update posts');

		//Update the posts for this page again
		if (authorId != null) {
			postRepositoryService.getPostsByAuthor(authorId).then(function(promise) {
				$scope.posts = promise;
				$scope.loadedPosts = true;
			});
		} else {
			postRepositoryService.getPostsByDate().then(function(promise) {
				$scope.posts = promise;
				$scope.loadedPosts = true;
			});
		}
		$scope.$apply();
	};

	$scope.updateHub.client.ReceiveNotifyCreateOrDeletePost = function() {
		console.log('Received notification that a post was created or deleted');
		//Update the posts for this page again
		if (authorId != null) {
			postRepositoryService.getPostsByAuthor(authorId).then(function(promise) {
				$scope.posts = promise;
				$scope.loadedPosts = true;
			});
		} else {
			postRepositoryService.getPostsByDate().then(function(promise) {
				$scope.posts = promise;
				$scope.loadedPosts = true;
			});
		}

		//Additionally, update the author list since the post counts must have changed for the various users
		postRepositoryService.getAllAuthors().then(function(promise) {
			$scope.authors = promise;
			$scope.loadedAuthors = true;
		});
		$scope.$apply();
	};

	$scope.updateHub.client.ReceiveNotifyAddComments = function(postIdentification) {
		console.log('Received notification of added comments');

		//Retrieve the recent comments for the current page
		commentRepositoryService.getRecentComments().then(function(promise) {
			$scope.recentComments = promise;
			$scope.loadedRecentComments = true;
		});
		$scope.$apply();
	};

	$scope.updateHub.client.ReceiveNotifyAddAuthor = function() {
		console.log('Received notification of an added author');

		//Retrieve relevant author data again
		postRepositoryService.getAllAuthors().then(function(promise) {
			$scope.authors = promise;
			$scope.loadedAuthors = true;
		});
		$scope.$apply();
	};
	
	//Now, start the service
	signalRFactory.start();

	//Set up the rest of the objects
	$scope.usernametest = $rootScope.userName;
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

	//Retrieve all the current authors
	postRepositoryService.getAllAuthors().then(function (promise) {
		$scope.authors = promise;
		$scope.loadedAuthors = true;
	});

	//Retrieve the recent comments for the current page
	commentRepositoryService.getRecentComments().then(function (promise) {
		$scope.recentComments = promise;
		$scope.loadedRecentComments = true;
	});

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

					//Remove the username and password from the model
					login.username = '';
					login.password = '';

					$scope.isProcessingAuthentication = false;
					$scope.isProcessingLogin = false;
				})
				.error(function (data, status, headers, config) {
					$cookieStore.remove('blog_session_token');
					$scope.loginError = data.error;

					//Remove the password from the model
					login.password = '';

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

					//Remove the username and password from the model
					register.username = '';
					register.password = '';

					$scope.isProcessingAuthentication = false;
					$scope.isProcessingRegistration = false;
				})
				.error(function(data, status, headers, config) {
					$cookieStore.remove('blog_session_token');
					$scope.registrationError = data.error;

					//Remove the password from the model
					register.password = '';

					$scope.isProcessingAuthentication = false;
					$scope.isProcessingRegistration = false;
				});
		}
	};
});
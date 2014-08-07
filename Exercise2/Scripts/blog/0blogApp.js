angular.module('blog', ['ui.router', 'ngCookies', 'ngSanitize', 'textAngular'])
	.value('update', $.connection.update)
	.config(function($stateProvider, $urlRouterProvider, $locationProvider) {
		$stateProvider
			.state('root', {
				url: '',
				abstract: true,
				templateUrl: '/Scripts/blog/templates/root.html',
				data: {
					requiresAuthentication: false,
					title: 'My Blog Platform'
				}
			})
			.state('root.home', {
				url: '/?id',
				templateUrl: '/Scripts/blog/templates/home.html',
				controller: 'HomeController',
				data: {
					requiresAuthentication: false,
					title: 'Home'
				},
				resolve: {
					authorId: function ($stateParams) {
						return $stateParams.id; //The id of a specific object to filter by - assumed to be author.id for now
					}
				}
			})
			.state('root.login', {
				url: '/login',
				templateUrl: '/Scripts/blog/templates/login.html',
				controller: 'LoginController',
				data: {
					requiresAuthentication: false,
					title: 'Login'
				}
			})
			.state('root.register', {
				url: '/register',
				templateUrl: '/Scripts/blog/templates/register.html',
				controller: 'RegisterController',
				data: {
					requiresAuthentication: false,
					title: 'Register'
				}
			})
			.state('root.logout', {
				url: '/logout',
				controller: 'LogoutController',
				data: {
					requiresAuthentication: false,
					title: 'Register'
				}
			})
			.state('root.viewPost', {
				url: '/view?id',
				controller: 'ViewPostController',
				templateUrl: '/Scripts/blog/templates/viewPost.html',
				data: {
					requiresAuthentication: false,
					title: 'View Post'
				},
				resolve: {
					postId: function($stateParams) {
						return $stateParams.id;
					},
					post: function($q, $stateParams, postRepositoryService) {
						var deferred = $q.defer();
						deferred.resolve(postRepositoryService.getPostById($stateParams.id));
						return deferred.promise;
					}
				}
			})
			.state('root.newPost', {
				url: '/new',
				controller: 'NewPostController',
				templateUrl: '/Scripts/blog/templates/newPost.html',
				data: {
					requiresAuthentication: true,
					title: 'New Post'
				}
			})
			.state('root.editPost', {
				url: '/edit?id',
				controller: 'EditPostController',
				templateUrl: '/Scripts/blog/templates/editPost.html',
				data: {
					requiresAuthentication: false,
					title: 'Edit Post'
				},
				resolve: {
					postId: function($stateParams) {
						return $stateParams.id;
					},
					post: function($q, $stateParams, postRepositoryService) {
						var deferred = $q.defer();
						deferred.resolve(postRepositoryService.getPostById($stateParams.id));
						return deferred.promise;
					}
				}
			});

		$urlRouterProvider.when("", "/");

		//For any unmatched url, send to /
		$urlRouterProvider.otherwise("/");

		//Fix for trailing slashes. Found at https://github.com/angular-ui/ui-router/issues/50
		$urlRouterProvider.rule(function($injector, $location) {
			if ($location.protocol() === "file")
				return;
			var path = $location.path();
			//Note: misnoder, this requires a query object, not a search string.
			search = $location.search(),
				params;

			//Check to see if the path already ends in '/'
			if (path[path.length - 1] === '/') {
				return;
			}

			//If there was no search string/query params, return with a '/'
			if (Object.keys(search).length === 0) {
				return path + '/';
			}

			//Otherwise build the search string and return a '/?' prefix
			params = [];
			angular.forEach(search, function(v, k) {
				params.push(k + '=' + v);
			});
			return path + '/?' + params.join('&');
		});
		$locationProvider.html5Mode(true);
	})
	.run([
		'$rootScope', '$state', function($rootScope, $state) {
			$rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState) {
				//Does the state require authentication to view it?
				var reqAuth = toState.data.requiresAuthentication;
				if (reqAuth && !$rootScope.authenticated) {
					event.preventDefault();
					$state.go('root.home');
				}
			});

			$(function () {
				$.connection.hub.logging = true;
			$.connection.hub.start();
			});

			$.connection.hub.error(function(err) {
				console.log('An error occurred: ' + err);
			});

			$rootScope.$on('$stateChangeSuccess', function(event, next) {
				$rootScope.title = next.data.title;
			});
		}
	]);
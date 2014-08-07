angular.module('blog').factory('postRepositoryService', function($q, $rootScope, $http, $cookieStore) {
	return {
		getRecentPosts: function() {
			var deferred = $q.defer();
			$http({ method: 'GET', url: '/api/posts/GetRecentPosts' })
				.success(function(data, status, headers, config) {
					deferred.resolve(data);
				})
				.error(function(data, status, headers, config) {
					deferred.reject(status);
				});

			return deferred.promise;
		},
		getPostsByDate: function() {
			var deferred = $q.defer();
			$http({ method: 'GET', url: '/api/posts/GetPostsByDate' })
				.success(function(data, status, headers, config) {
					deferred.resolve(data);
				})
				.error(function(data, status, headers, config) {
					deferred.reject(status);
				});
			return deferred.promise;
		},
		getPostsByAuthor: function(authorId) {
			var deferred = $q.defer();
			var configuration = { params: { authorId: authorId } };
			$http.get('/api/posts/GetPostsByAuthor', configuration)
				.success(function(data, status, headers, config) {
					deferred.resolve(data);
				})
				.error(function(data, status, headers, config) {
					deferred.reject(status);
				});
			return deferred.promise;
		},
		getAllAuthors: function() {
			var deferred = $q.defer();
			$http.get('/api/posts/GetAllAuthors')
				.success(function(data, status, headers, config) {
					deferred.resolve(data);
				})
				.error(function(data, status, headers, config) {
				deferred.reject(status);
				});
			return deferred.promise;
		},
		getPostById: function(postId) {
			var deferred = $q.defer();
			var configuration = { params: { postId: postId } };
			var token = $cookieStore.get('blog_session_token');
			$http.defaults.headers.common.Token = token;
			$http.get('/api/posts/GetPostById', configuration)
				.success(function(data, status, headers, config) {
					deferred.resolve(data);
				})
				.error(function(data, status, headers, config) {
					deferred.reject(status);
				});
			return deferred.promise;
		},
		deletePost: function(postId) {
			var token = $cookieStore.get('blog_session_token');
			$http.defaults.headers.common.Token = token;
			var configuration = { params: { postId: Number(postId) } };
			$http.post('/api/posts/DeletePost', configuration)
				.success(function(data, status, headers, config) {
					return true;
				})
				.error(function(data, status, headers, config) {
					return false;
				});
		}
	}
});
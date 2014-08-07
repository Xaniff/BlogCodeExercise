angular.module('blog').factory('commentRepositoryService', function($q, $rootScope, $http) {
	return {
		getRecentComments: function() {
			var deferred = $q.defer();
			$http({ method: 'GET', url: '/api/posts/GetRecentComments' })
				.success(function(data, status, headers, config) {
					deferred.resolve(data);
				})
				.error(function(data, status, headers, config) {
					deferred.reject(status);
				});
			return deferred.promise;
		},
		getCommentsForPost: function(blogPostId) {
			var deferred = $q.defer();
			$http({ method: 'GET', url: '/api/posts/GetCommentsForPost?blogPostId=' + Number(blogPostId) })
				.success(function(data, status, headers, config) {
					deferred.resolve(data);
				})
				.error(function(data, status, headers, config) {
					deferred.reject(status);
				});
			return deferred.promise;
		}
	}
});
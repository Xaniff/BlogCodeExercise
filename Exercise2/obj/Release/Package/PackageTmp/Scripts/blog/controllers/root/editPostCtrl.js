angular.module('blog').controller('EditPostController', function ($scope, postId, post, $http, $state, $cookieStore) {
	$scope.container = {};
	$scope.post = post;
	$scope.isProcessing = false;
	$scope.postError = '';

	$scope.savePost = function() {
		if ($scope.container.postForm.$valid) {
			$scope.isProcessing = true;
			var token = $cookieStore.get('blog_session_token');
			$http.defaults.headers.common.Token = token;
			$http.post('/api/posts/editpost', post)
				.success(function(data, status, headers, config) {
					$scope.postError = '';
					$scope.isProcessing = false;
					$state.go('root.viewPost', { id: data.postId });
				})
				.error(function(data, status, headers, config) {
					$scope.postError = data.error;
					$scope.isProcessing = false;
				});
		}
	};
});
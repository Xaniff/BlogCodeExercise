angular.module('blog').controller('NewPostController', function($scope, $http, $state, $rootScope, $cookieStore) {
	$scope.isProcessing = false;
	$scope.postError = '';

	$scope.createPost = function(post) {
		if ($scope.container.postForm.$valid) {
			$scope.isProcessing = true;
			var token = $cookieStore.get('blog_session_token');
			$http.defaults.headers.common.Token = token;
			$http.post('/api/posts/createpost', post)
				.success(function(data, status, headers, config) {
					$scope.postError = '';
					$scope.isProcessing = false;
					//$state.go('root.home');
					$state.go('root.viewPost', { id: data.postId });
				})
				.error(function(data, status, headers, config) {
					$scope.postError = data.error;
					$scope.isProcessing = false;
				});
		}
	}
});
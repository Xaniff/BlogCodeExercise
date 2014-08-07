angular.module('blog').controller('ViewPostController', function ($scope, $rootScope, postId, post, $http, $state, postRepositoryService, randomFactory) {
	$scope.username = $rootScope.userName; //Will be null if the user isn't currently logged in
	$scope.postId = postId;
	$scope.post = post;
	$scope.boxColor = { 'background-color': randomFactory.randomPostColor() };
	
	//Retrieve other posts written by the same author for the right sidebar
	postRepositoryService.getPostsByAuthor(post.authorId).then(function(promise) {
		$scope.otherPosts = promise;
	});

	$scope.editPost = function () {
		$state.go('root.editPost', {id: postId});
	};

	$scope.deletePost = function () {
		$http({ method: 'GET', url: '/api/posts/BlogItemDeletion?postId=' + postId})
			.success(function(data, status, headers, config) {
				$state.go('root.home');
			});
	};
});
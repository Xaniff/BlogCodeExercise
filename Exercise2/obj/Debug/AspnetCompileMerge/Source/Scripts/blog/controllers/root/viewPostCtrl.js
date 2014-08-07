angular.module('blog').controller('ViewPostController', function ($scope, $rootScope, postId, $cookieStore, post, $http, $state, commentRepositoryService, postRepositoryService, randomFactory, signalRFactory) {
	//Related to SignalR
	$scope.updateHub = signalRFactory.connection.update;

	//Set up the signalR functions (must have at least one)
	$scope.updateHub.client.ReceiveNotifyCreateOrDeletePost = function () {
		console.log('Received notification that a post was created or deleted');
		//Update the "other posts by author" portion
		postRepositoryService.getPostsByAuthor(post.authorId).then(function (promise) {
			$scope.otherPosts = promise;
		});
		$scope.$apply();
	};

	$scope.updateHub.client.ReceiveNotifyUpdatePosts = function(postIdentification) {
		console.log('Received notification that a comment was created for postId: ' + postIdentification);
		//Retrieve the post information from the server again if the postIdentification matches the current postId
		if (Number(postId) == Number(postIdentification)) {
			postRepositoryService.getPostById(postId).then(function(promise) {	
				$scope.post = promise;
			});
			$scope.$apply();
		}
	};

	$scope.updateHub.client.ReceiveNotifyAddComments = function(postIdentification) {
		console.log('Received notification that a comment was created for postId: ' + postIdentification);

		//Check if the comment was added for this post
		if (Number(postIdentification) == Number(postId)) {
			//Re-retrieve the comments for this post
			commentRepositoryService.getCommentsForPost(post.id).then(function(promise) {
				$scope.comments = promise;
			});

			//Only need to retrieve the comments if there are actually any to be found
			if (post.comments > 0) {
				commentRepositoryService.getCommentsForPost(post.id).then(function (promise) {
					$scope.comments = promise;
				});
			}
			$scope.$apply();
		}
	};

	//Now, start the server
	signalRFactory.start();

	//Set up the rest of the objects
	$scope.username = $rootScope.userName; //Will be null if the user isn't currently logged in
	$scope.postId = postId;
	$scope.post = post;
	$scope.boxColor = { 'background-color': randomFactory.randomPostColor() };
	$scope.comments = {};
	$scope.isProcessing = false;

	//Retrieve other posts written by the same author for the right sidebar
	postRepositoryService.getPostsByAuthor(post.authorId).then(function(promise) {
		$scope.otherPosts = promise;
	});

	//Only need to retrieve the comments if there are actually any to be found
	if (post.comments > 0) {
		commentRepositoryService.getCommentsForPost(post.id).then(function(promise) {
			$scope.comments = promise;
		});
	}

	$scope.editPost = function () {
		$state.go('root.editPost', {id: postId});
	};

	$scope.deletePost = function () {
		var token = $cookieStore.get('blog_session_token');
		$http.defaults.headers.common.Token = token;
		$http.get('/api/posts/BlogItemDeletion?postId=' + postId)
			.success(function(data, status, headers, config) {
				$state.go('root.home');
			});
	};

	$scope.createComment = function(comment) {
		if ($scope.container.commentForm.$valid) {
			$scope.isProcessing = true;
			var token = $cookieStore.get('blog_session_token');
			$http.defaults.headers.common.Token = token;
			$http.get('/api/posts/createcomment?blogPostId=' + postId + "&comment=" + comment.body)
				.success(function(data, status, headers, config) {
					//Clear the currently entered comment
					$scope.comment.body = '';

					//Refresh the loaded comments
					commentRepositoryService.getCommentsForPost(post.id).then(function(promise) {
						$scope.comments = promise;
					});
					$scope.isProcessing = false;
				})
				.error(function(data, status, headers, config) {
					$scope.isProcessing = false;
				});
		}
	};
});
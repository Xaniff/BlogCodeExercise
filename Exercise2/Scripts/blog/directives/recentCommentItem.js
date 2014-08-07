angular.module('blog').directive('recentCommentItem', function() {
	return {
		link: function($scope, element, attr, model) {
			$scope.author = attr.author;
			$scope.body = attr.body;
		},
		restrict: 'E',
		replace: true,
		scope: {},
		templateUrl: '/Scripts/blog/templates/directives/recentCommentItem.html'
	}
});
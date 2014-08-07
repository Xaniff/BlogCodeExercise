angular.module('blog').directive('recentBlogItem', function() {
	return {
		link: function($scope, element, attr, model) {
			$scope.date = attr.date;
			$scope.title = attr.title;
		},
		restrict: 'E',
		replace: true,
		scope: {},
		templateUrl: '/Scripts/blog/templates/directives/recentBlogItem.html'
	}
});
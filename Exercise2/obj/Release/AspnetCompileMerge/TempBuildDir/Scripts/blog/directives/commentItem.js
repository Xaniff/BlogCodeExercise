angular.module('blog').directive('commentItem', function() {
	return {
		link: function($scope, element, attr, model) {
			$scope.author = attr.author;
			$scope.body = attr.body;
			$scope.date = attr.date;
		},
		restrict: 'E',
		replace: true,
		scope: {},
		templateUrl: '/Scripts/blog/templates/directives/commentItem.html'
	}
});
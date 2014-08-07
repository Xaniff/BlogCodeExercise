angular.module('blog').directive('xsmallBlogItem', function (randomFactory) {
	return {
		link: function ($scope, element, attr, model) {
			$scope.title = attr.title;
			$scope.body = attr.body;
			$scope.date = attr.date;
			$scope.author = attr.author;
			$scope.id = attr.id;
			//Select a color for the post background
			$scope.style = { 'background-color': randomFactory.randomPostColor() };
		},
		restrict: 'E',
		replace: true,
		scope: {},
		templateUrl: '/Scripts/blog/templates/directives/xSmallBlogItem.html'
	}
});
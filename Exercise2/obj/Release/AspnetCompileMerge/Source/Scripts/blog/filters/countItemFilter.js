angular.module('blog').filter('pluralize', function() {
	return function (input, count) {
		input = input || '';
		if (count == 1)
			return count + ' ' + input;
		else
			return count + ' ' + input + 's';
	}
});
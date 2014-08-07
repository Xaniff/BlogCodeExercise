angular.module('blog').factory('randomFactory', [
function() {
	return {
		randomIntegerWithinRange: function(min, max) {
			return Math.floor((Math.random() * max) + min);
		},
		randomPostColor: function() {
			var colors = ['#5690B2', '#6B66BF', '#5AC67F', '#003E26', '#2B5300', '#5B1C00', '#AF3C08', '#05764C', '#FFC174', '#485800', '#174F00', '#122D76', '#EC476A', '#056B64', '#AF5008', '#FF784D', '#AF5708', '#7D0667', '#1F9207', '#A90817', '#0B4A6F', '#470D76', '#5B4600', '#C93D92', '#38BA68', '#E64573', '#004414', '#5B3200', '#080540', '#02253A', '#9D0732', '#630873'];
			//Get a random color from this list
			var randomNumber = this.randomIntegerWithinRange(1, colors.length) - 1;
			return colors[randomNumber];
		}
	}
}])
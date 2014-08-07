angular.module('blog').factory('signalRFactory', function ($rootScope, $timeout) {
	var factory = {};
	factory.connection = $.connection;

	factory.start = function () {
		//We want to log everything dealing with signalR
		factory.connection.hub.logging = true; //In production I'd set this to false

		//Stop the connection if we already have one
		factory.connection.hub.stop();

		//What to do if there's an error
		factory.connection.hub.error(function (err) {
			console.log('A signalR error occurred: ' + err);
		});

		//Start the connection
		factory.connection.hub.start();
	};

	return factory;
});
// assets/js/websockettester.js

// initialize global variables
var Handler;
var WebSocketClient;

var KEY_SERVER_URI = "websockettest.serveruri";

/**
 * set up the actions and global variables on the page
 */
$( document ).ready(function() {
	// initialize the console and the Handler
	Console = new Console($("#console"));
	Handler = new WebSocketTestHandler(Console);
	
	// load the saved serverURI into the serveruri input
	var server = localStorage.getItem(KEY_SERVER_URI);
	$("#serveruri").val(server);
	
	// assign event handler to the connect button
	$("#connect").click(function(e) {
		var server = $("#serveruri").val();
		
		// save the current serveruri so we don't have to type it all the time
		localStorage.setItem(KEY_SERVER_URI, server);
		window.WebSocketClient = new WebSocketClient(server, window.Handler);
		e.preventDefault();
	});
	
	// assign event handler to the disconnect button
	$("#disconnect").click(function(e) {
		window.WebSocketClient.disconnect();
		e.preventDefault();
	});
	
	// assign event handler to the send button
	$("#send").click(function(e) {
		var message = $("#prompt").val();
		window.WebSocketClient.send(message);
		$("#prompt").val('');
		e.preventDefault();
	});
});



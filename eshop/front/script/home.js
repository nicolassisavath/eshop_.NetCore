window.onload = function() {

	/******************INSTANCIATION */
	include('test.js');
	include('helpers/helper.js');
	include('modules/signUp.js');
	include('modules/signIn.js');


	function include(src) {
		var head = document.getElementsByTagName('head')[0];
		var script = document.createElement('script');

		script.type = 'text/javascript';
		script.src = './script/' + src;
		head.appendChild(script);
	}

	


}

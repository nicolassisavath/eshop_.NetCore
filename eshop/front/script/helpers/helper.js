/********** Tools  */
	var baseRoute = "https://localhost:44337/api/";
	displayNotifications()

function request(method, url, callback = null, data = null, json = true) {
	var xhr = new XMLHttpRequest();

	xhr.onload = function(){
		if (callback != null)
			callback(this);
	}

	xhr.open(method, url);
	if (json == true) {
		xhr.setRequestHeader("Content-Type", "application/json");
		if (data)
			data = JSON.stringify(data);
	}

    if (localStorage.getItem('jwt') != null)
        xhr.setRequestHeader("Authorization", "Bearer " + localStorage.getItem('jwt'));
	xhr.send(data);
}

	function displayNotifications() {
	    var notifications = document.querySelector("#userNotif");
	    var signInForm = document.querySelector("#signInFormContainer");
	    var signUpForm = document.querySelector("#signUpFormContainer");
	    var logoutBtn = document.querySelector('#logout');

	    if (localStorage.getItem('jwt') != null) {
	        signInForm.style.display = "none";
	        signUpForm.style.display = "none";
	        logoutBtn.style.display = "block";
	        notifications.innerHTML = "Vous êtes connecté <br />" + notifications.innerHTML;
	    }
	    else {
	        signInForm.style.display = "block";
	        signUpForm.style.display = "block";
	        logoutBtn.style.display = "none";
	        notifications.innerHTML = "Vous n'êtes pas connecté <br />";
	    }
	}



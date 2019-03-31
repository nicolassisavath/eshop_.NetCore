//**************** INITIALIZATION 
var signInBtn = document.querySelector("#signInBtn");
var logoutBtn = document.querySelector('#logout');

//**************** EVENTS 
signInBtn.onclick = function(e) {
	e.preventDefault();
	var login = document.querySelectorAll('#signInForm>input[name="uLogin"]')[0].value;
	var password = document.querySelectorAll('#signInForm>input[name="uPassword"]')[0].value;

	if (login == '' || password == '') 
		alert('Tous les champs ne sont pas remplis')
	else {
		var data = {
			ULogin : login,
			uPassword : password
		}
		request("POST", baseRoute + 'users/login', signInBtnCbk, data);
	}
}
logoutBtn.onclick = function() {
	localStorage.removeItem('jwt');
    displayNotifications();
}
//**************** CALLBACKS 

function signInBtnCbk(xhr) {
	if (xhr.status == 200) {
		var response = JSON.parse(xhr.responseText);

		if (response["user"] != 'undefined') {
			login = response["user"]["uLogin"];
			avatar = response["user"]["uAvatar"];

			var userNotif = document.querySelector('#userNotif');
			userNotif.innerHTML = "Bonjour " + login + "<br />";

			var avatarImage = document.createElement('img');
			avatarImage.alt = "Avatar du user";
			avatarImage.style.height = 200 + "px";
			avatarImage.src = "data:image/jpg;base64," + avatar.replace(/"/g, "");
			userNotif.appendChild(avatarImage);
		}
		if (response["token"] != 'undefined') {
			token = response["token"];
            localStorage.setItem("jwt", token);
            displayNotifications();
		}
	}
	else
		alert(xhr.responseText);
}

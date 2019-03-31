//**************** INITIALIZATION 
var signUpBtn = document.querySelector('#signUpBtn');


//**************** EVENTS 
signUpBtn.onclick = function(e) {
	e.preventDefault();

	var login = document.querySelectorAll('#signUpForm>input[name="uLogin"]')[0].value;
	var password = document.querySelectorAll('#signUpForm>input[name="uPassword"]')[0].value;
	var passwordC = document.querySelectorAll('#signUpForm>input[name="uPasswordC"]')[0].value;

	if (password != '' && password != passwordC) {
		alert('Vos mots de passe sont différents');
	}
	else if (login != '' && password != '' && password == passwordC)	{
		var form = document.querySelector('#signUpForm');
		var formData = new FormData(form);
		request("POST", baseRoute + 'users/create', signUpBtnCbk, formData, false);
	}
	else {
		alert('Des données sont manquantes');
	}
}


//**************** CALLBACKS 
function signUpBtnCbk(xhr) {
	if (xhr.status == 200) 
		alert('Compte crée avec succès');
	else
		alert(xhr.responseText);
}
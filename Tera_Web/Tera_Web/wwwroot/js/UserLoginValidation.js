function validateEmail() {
    var emailInput = document.getElementById('email');
    var emailError = document.getElementById('emailError');

    var email = emailInput.value.trim();
    if (email === '') {
        emailError.innerText = 'Por favor, ingrese su correo';
    } else if (!validateEmailFormat(email)) {
        emailError.innerText = 'No cumple con el formato de correo electrónico';
    } else {
        emailError.innerText = '';
    }
}

function validateEmailFormat(email) {
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
}

function validatePassword() {
    var passwordInput = document.getElementById('password');
    var passwordError = document.getElementById('passwordError');

    var password = passwordInput.value.trim();
    if (password === '') {
        passwordError.innerText = 'Por favor, ingrese su contraseña';
    } else {
        passwordError.innerText = '';
    }
}

document.getElementById('loginForm').addEventListener('submit', function (event) {
    validateEmail();
    validatePassword();

    var emailError = document.getElementById('emailError').innerText;
    var passwordError = document.getElementById('passwordError').innerText;

    if (emailError !== '' || passwordError !== '') {
        event.preventDefault();
    }
});
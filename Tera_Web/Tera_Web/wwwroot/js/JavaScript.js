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

document.getElementById('user-form').addEventListener('submit', function (event) {
    validateEmail();

    var emailError = document.getElementById('emailError').innerText;

    if (emailError !== '') {
        event.preventDefault();
    }
});
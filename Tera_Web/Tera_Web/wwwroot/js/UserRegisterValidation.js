function validateEmail() {
    var emailInput = document.getElementById('email');
    var emailError = document.getElementById('userEmail-error');

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

function validateSiteId() {
    var siteIdDropdown = document.getElementById('userSiteId');
    var siteIdError = document.getElementById('userSiteId-error');

    if (siteIdDropdown.selectedIndex === 0) {
        siteIdError.innerText = 'Por favor, selecciona una sede';
    } else {
        siteIdError.innerText = '';
    }
}

function clearError(elementId) {
    var errorElement = document.getElementById(elementId + '-error');
    errorElement.innerText = '';
}

document.getElementById('user-form').addEventListener('submit', function (event) {
    validateEmail();
    validateSiteId();

    var emailError = document.getElementById('userEmail-error').innerText;
    var siteIdError = document.getElementById('userSiteId-error').innerText;

    if (emailError !== '' || siteIdError !== '') {
        event.preventDefault();
    }
});

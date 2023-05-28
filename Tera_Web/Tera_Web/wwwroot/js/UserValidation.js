function validateUserGovId() {
    var userGovIdInput = document.getElementById('userGovId');
    var userGovIdError = document.getElementById('userGovIdError');
    var userGovIdValue = userGovIdInput.value.trim();

    if (userGovIdValue === '') {
        userGovIdError.textContent = 'Por favor, ingrese su cédula';
    } else if (userGovIdValue.length < 9) {
        userGovIdError.textContent = 'Cédula incompleta';
    } else if (!/^\d+$/.test(userGovIdValue)) {
        userGovIdError.textContent = 'Ingrese solo números';
    } else {
        userGovIdError.textContent = '';
    }
}

function toggleMode() {
    var modeButton = document.getElementById('modeButton');
    var userNameInput = document.getElementById('userName');
    var userFirstSurnameInput = document.getElementById('userFirstSurname');
    var userSecondSurnameInput = document.getElementById('userSecondSurname');

    if (modeButton.textContent === 'Manual') {
        modeButton.textContent = 'Automático';
        userNameInput.removeAttribute('readonly');
        userNameInput.classList.remove('disabled');
        userFirstSurnameInput.removeAttribute('readonly');
        userFirstSurnameInput.classList.remove('disabled');
        userSecondSurnameInput.removeAttribute('readonly');
        userSecondSurnameInput.classList.remove('disabled');
    } else {
        modeButton.textContent = 'Manual';
        userNameInput.setAttribute('readonly', 'readonly');
        userNameInput.classList.add('disabled');
        userFirstSurnameInput.setAttribute('readonly', 'readonly');
        userFirstSurnameInput.classList.add('disabled');
        userSecondSurnameInput.setAttribute('readonly', 'readonly');
        userSecondSurnameInput.classList.add('disabled');
    }

    clearErrorMessages();
}

function validateUserName() {
    var modeButton = document.getElementById('modeButton');
    var userNameInput = document.getElementById('userName');
    var userNameError = document.getElementById('userNameError');

    if (userNameInput.value.trim() === '') {
        if (modeButton.textContent === 'Manual') {
            userNameError.textContent = 'Complete el campo de cédula';
        } else {
            userNameError.textContent = 'Por favor, ingrese el/su nombre';
        }
    } else {
        userNameError.textContent = '';
    }
}

function validateUserFirstSurname() {
    var modeButton = document.getElementById('modeButton');
    var userFirstSurnameInput = document.getElementById('userFirstSurname');
    var userFirstSurnameError = document.getElementById('userFirstSurnameError');

    if (userFirstSurnameInput.value.trim() === '') {
        if (modeButton.textContent === 'Manual') {
            userFirstSurnameError.textContent = 'Complete el campo de cédula';
        } else {
            userFirstSurnameError.textContent = 'Por favor, ingrese el/su primer apellido';
        }
    } else {
        userFirstSurnameError.textContent = '';
    }
}

function validateUserSecondSurname() {
    var modeButton = document.getElementById('modeButton');
    var userSecondSurnameInput = document.getElementById('userSecondSurname');
    var userSecondSurnameError = document.getElementById('userSecondSurnameError');

    if (userSecondSurnameInput.value.trim() === '') {
        if (modeButton.textContent === 'Manual') {
            userSecondSurnameError.textContent = 'Complete el campo de cédula';
        } else {
            userSecondSurnameError.textContent = 'Por favor, ingrese el/su segundo apellido';
        }
    } else {
        userSecondSurnameError.textContent = '';
    }
}

function validateUserEmail() {
    var userEmailInput = document.getElementById('userEmail');
    var userEmailError = document.getElementById('userEmailError');
    var userEmailValue = userEmailInput.value.trim();

    if (userEmailValue === '') {
        userEmailError.textContent = 'Por favor, ingrese el/su correo';
    } else if (!/\S+@\S+\.\S+/.test(userEmailValue)) {
        userEmailError.textContent = 'No cumple con el formato de correo electrónico';
    } else {
        userEmailError.textContent = '';
    }
}

function clearErrorMessages() {
    var userNameError = document.getElementById('userNameError');
    var userFirstSurnameError = document.getElementById('userFirstSurnameError');
    var userSecondSurnameError = document.getElementById('userSecondSurnameError');

    userNameError.textContent = '';
    userFirstSurnameError.textContent = '';
    userSecondSurnameError.textContent = '';
}
function validateUserSiteId() {
    var userSiteIdSelect = document.getElementById('userSiteId');
    var userSiteIdError = document.getElementById('userSiteIdError');
    var userSiteIdValue = userSiteIdSelect.value;

    if (userSiteIdValue === '0') {
        userSiteIdError.textContent = 'Por favor, selecciona una sede';
    } else {
        userSiteIdError.textContent = '';
    }
}
function validateUserRole() {
    var userRoleSelect = document.getElementById('userRoleId');
    var userRoleError = document.getElementById('userRoleError');
    var userRoleValue = userRoleSelect.value;

    if (userRoleValue === '0') {
        userRoleError.textContent = 'Por favor, selecciona un rol';
    } else {
        userRoleError.textContent = '';
    }
}
function validateForm() {
    // Realiza todas las validaciones necesarias y verifica si hay errores
    var userGovIdError = document.getElementById('userGovIdError');
    var userNameError = document.getElementById('userNameError');
    var userFirstSurnameError = document.getElementById('userFirstSurnameError');
    var userSecondSurnameError = document.getElementById('userSecondSurnameError');
    var userEmailError = document.getElementById('userEmailError');
    var userSiteIdError = document.getElementById('userSiteIdError');
    var userRoleError = document.getElementById('userRoleError');

    // Verifica si hay errores en algún campo
    if (userGovIdError.textContent !== '' ||
        userNameError.textContent !== '' ||
        userFirstSurnameError.textContent !== '' ||
        userSecondSurnameError.textContent !== '' ||
        userEmailError.textContent !== '' ||
        userSiteIdError.textContent !== '' ||
        userRoleError.textContent !== '') {
        // Si hay errores, no se envía el formulario
        return false;
    }

    // Si no hay errores, se envía el formulario
    return true;
}
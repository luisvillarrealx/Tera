function validateEmail() {
    var emailInput = document.getElementById('email');
    var emailError = document.getElementById('userEmail-error');
    var email = emailInput.value.trim();

    if (email === '') {
        showError(emailError, 'Por favor, ingrese su correo');
    } else if (!validateEmailFormat(email)) {
        showError(emailError, 'No cumple con el formato de correo electrónico');
    } else {
        clearError(emailError);
    }
}

function validateEmailFormat(email) {
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
}

function validateEmailExist() {
    var userEmail = $("#email").val();

    // Realizar una solicitud AJAX para verificar si el correo electrónico existe
    $.ajax({
        url: "/User/EmailExists",
        type: "POST",
        data: { userEmail: userEmail },
        success: function (result) {
            if (result === true) {
                // El correo electrónico existe, muestra el mensaje de error
                $("#userEmail-error").text("El correo electrónico ya está registrado.");
            }
        },
        error: function () {
            // Manejar el error de la solicitud AJAX si es necesario
            console.log("Error al verificar el correo electrónico.");
        }
    });
}


function validateSiteId() {
    var siteIdDropdown = document.getElementById('userSiteId');
    var siteIdError = document.getElementById('userSiteId-error');

    if (siteIdDropdown.selectedIndex === 0) {
        showError(siteIdError, 'Por favor, selecciona una sede');
    } else {
        clearError(siteIdError);
    }
}

function validateRoleId() {
    var roleIdDropdown = document.getElementById('userRoleId');
    var roleIdError = document.getElementById('userRoleId-error');

    if (roleIdDropdown.selectedIndex === 0) {
        showError(roleIdError, 'Por favor, selecciona un Rol');
    } else {
        clearError(roleIdError);
    }
}
function validateRoleId() {
    var roleIdDropdown = document.getElementById('userRoleId');
    var roleIdError = document.getElementById('userRoleId-error');

    if (roleIdDropdown.selectedIndex === 0) {
        showError(roleIdError, 'Por favor, selecciona un Rol');
    } else {
        clearError(roleIdError);
    }
}

function showError(errorElement, errorMessage) {
    errorElement.innerText = errorMessage;
}

function clearError(errorElement) {
    errorElement.innerText = '';
}

function validateForm() {
    // Realizar todas las validaciones necesarias
    validateEmail();
    validateSiteId();
    validateRoleId();

    // Verificar si hay errores
    var hasErrors = document.querySelectorAll('.text-danger').length > 0;

    // Si hay errores, no enviar el formulario
    if (hasErrors) {
        event.preventDefault(); // Evitar que el formulario se envíe
    }
}

// Agregar el evento onsubmit al formulario
var userForm = document.getElementById('user-form');
if (userForm) {
    userForm.addEventListener('submit', validateForm);
}


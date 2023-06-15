function validateEmail() {
    var emailInput = document.getElementById('email');
    var emailError = document.getElementById('userEmail-error');
    var email = emailInput.value.trim();

    if (email === '') {
        showError(emailError, 'Por favor, ingrese su correo');
        return false;
    } else if (!validateEmailFormat(email)) {
        showError(emailError, 'No cumple con el formato de correo electrónico');
        return false;
    } else {
        clearError(emailError);
        return true;
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
        return false;
    } else {
        clearError(siteIdError);
        return true;
    }
}

function validateRoleId() {
    var roleIdDropdown = document.getElementById('userRoleId');
    var roleIdError = document.getElementById('userRoleId-error');

    if (roleIdDropdown.selectedIndex === 0) {
        showError(roleIdError, 'Por favor, selecciona un Rol');
        return false;
    } else {
        clearError(roleIdError);
        return true;
    }
}

function showError(errorElement, errorMessage) {
    errorElement.innerText = errorMessage;
}

function clearError(errorElement) {
    errorElement.innerText = null;
}

function validateForm(event) {
    // Realizar todas las validaciones necesarias
    var hasErrors = false;

    if (!validateEmail()) {
        hasErrors = true;
    }

    if (!validateSiteId()) {
        hasErrors = true;
    }

    if (!validateRoleId()) {
        hasErrors = true;
    }

    if (hasErrors) {
        event.preventDefault(); // Evitar que el formulario se envíe
    } else {
        // Verificar si el correo electrónico existe
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
                    preventDefault();
                }
            },
            error: function () {
                // Manejar el error de la solicitud AJAX si es necesario
                console.log("Error al verificar el correo electrónico.");
            }
        });
    }
}
function preventDefault() {
    event.preventDefault(); // Evitar que el formulario se envíe inmediatamente
}



// Agregar el evento onsubmit al formulario
var userForm = document.getElementById('user-form');
if (userForm) {
    userForm.addEventListener('submit', validateForm);
}

// Validación para roleName
var roleNameInput = document.getElementById('roleNameInput');
roleNameInput.addEventListener('blur', function () {
    var value = this.value.trim();

    if (value === '') {
        displayErrorMessage(this, 'Por favor, ingrese el nombre del Rol');
    } else {
        clearErrorMessage(this);
    }
});

// Validación para roleDescription
var roleDescriptionInput = document.getElementById('roleDescriptionInput');
roleDescriptionInput.addEventListener('blur', function () {
    var value = this.value.trim();

    if (value === '') {
        displayErrorMessage(this, 'Por favor, ingrese la descripción del rol');
    } else {
        clearErrorMessage(this);
    }
});

// Función para mostrar un mensaje de error
function displayErrorMessage(element, message) {
    var parentElement = element.parentElement;
    var errorSpan = parentElement.querySelector('.text-danger');

    if (!errorSpan) {
        errorSpan = document.createElement('span');
        errorSpan.className = 'text-danger';
        parentElement.appendChild(errorSpan);
    }

    errorSpan.textContent = message;
}

// Función para borrar un mensaje de error
function clearErrorMessage(element) {
    var parentElement = element.parentElement;
    var errorSpan = parentElement.querySelector('.text-danger');

    if (errorSpan) {
        parentElement.removeChild(errorSpan);
    }
}

// Validación del formulario antes de enviarlo
var form = document.querySelector('form');
form.addEventListener('submit', function (event) {
    var isValid = true;

    // Validar roleName
    var roleName = roleNameInput.value.trim();
    if (roleName === '') {
        displayErrorMessage(roleNameInput, 'Por favor, ingrese el nombre del Rol');
        isValid = false;
    } else {
        clearErrorMessage(roleNameInput);
    }

    // Validar roleDescription
    var roleDescription = roleDescriptionInput.value.trim();
    if (roleDescription === '') {
        displayErrorMessage(roleDescriptionInput, 'Por favor, ingrese la descripción del rol');
        isValid = false;
    } else {
        clearErrorMessage(roleDescriptionInput);
    }

    if (!isValid) {
        event.preventDefault(); // Evitar que el formulario se envíe si hay errores
    }
});
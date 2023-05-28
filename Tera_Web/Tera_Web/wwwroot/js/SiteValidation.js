// Validación para siteName
var siteNameInput = document.getElementById('siteNameInput');
siteNameInput.addEventListener('blur', function () {
    var value = this.value.trim();

    if (value === '') {
        displayErrorMessage(this, 'Por favor, inserte el nombre de la sede');
    } else {
        clearErrorMessage(this);
    }
});

// Validación para siteUbication
var siteUbicationInput = document.getElementById('siteUbicationInput');
siteUbicationInput.addEventListener('blur', function () {
    var value = this.value.trim();

    if (value === '') {
        displayErrorMessage(this, 'Por favor, ingrese la ubicación de la sede');
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

    // Validar siteName
    var siteName = siteNameInput.value.trim();
    if (siteName === '') {
        displayErrorMessage(siteNameInput, 'Por favor, inserte el nombre de la sede');
        isValid = false;
    } else {
        clearErrorMessage(siteNameInput);
    }

    // Validar siteUbication
    var siteUbication = siteUbicationInput.value.trim();
    if (siteUbication === '') {
        displayErrorMessage(siteUbicationInput, 'Por favor, ingrese la ubicación de la sede');
        isValid = false;
    } else {
        clearErrorMessage(siteUbicationInput);
    }

    if (!isValid) {
        event.preventDefault(); // Evitar que el formulario se envíe si hay errores
    }
});
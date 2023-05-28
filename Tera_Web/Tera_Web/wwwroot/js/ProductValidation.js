// Validación para productName
var productNameInput = document.getElementById('productNameInput');
productNameInput.addEventListener('blur', function () {
    var value = this.value.trim();

    if (value === '') {
        displayErrorMessage(this, 'Por favor, ingrese un nombre para el producto');
    } else {
        clearErrorMessage(this);
    }
});

// Validación para productCost
var productCostInput = document.getElementById('productCostInput');
productCostInput.addEventListener('blur', function () {
    var value = this.value.trim();

    if (value === '') {
        displayErrorMessage(this, 'Por favor, ingrese un costo');
    } else {
        var regex = /^[0-9]+$/;

        if (!regex.test(value) || parseInt(value) < 0) {
            displayErrorMessage(this, 'Por favor, ingrese un costo válido');
        } else {
            clearErrorMessage(this);
        }
    }
});

// Validación para productMeasurementUnit
var unitDropdown = document.getElementById('productMeasurementUnit');
unitDropdown.addEventListener('change', function () {
    var selectedIndex = this.selectedIndex;

    if (selectedIndex === 0) {
        displayErrorMessage(this, 'Por favor, seleccione la unidad');
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

    // Validar productName
    var productName = productNameInput.value.trim();
    if (productName === '') {
        displayErrorMessage(productNameInput, 'Por favor, ingrese un nombre para el producto');
        isValid = false;
    } else {
        clearErrorMessage(productNameInput);
    }

    // Validar productCost
    var productCost = productCostInput.value.trim();
    if (productCost === '') {
        displayErrorMessage(productCostInput, 'Por favor, ingrese un costo');
        isValid = false;
    } else {
        var regex = /^[0-9]+$/;
        if (!regex.test(productCost) || parseInt(productCost) < 0) {
            displayErrorMessage(productCostInput, 'Por favor, ingrese un costo válido');
            isValid = false;
        } else {
            clearErrorMessage(productCostInput);
        }
    }

    // Validar productMeasurementUnit
    var selectedIndex = unitDropdown.selectedIndex;
    if (selectedIndex === 0) {
        displayErrorMessage(unitDropdown, 'Por favor, seleccione la unidad');
        isValid = false;
    } else {
        clearErrorMessage(unitDropdown);
    }

    if (!isValid) {
        event.preventDefault(); // Evitar que el formulario se envíe si hay errores
    }
});
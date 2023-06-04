function validatePassword() {
    var passwordInput = document.getElementById("userPassword");
    var confirmPasswordInput = document.getElementById("confirmPassword");
    var passwordError = document.getElementById("password-error");
    var confirmPasswordError = document.getElementById("confirmPassword-error");

    if (passwordInput.value === "") {
        passwordError.textContent = "La contraseña no puede estar vacía";
    } else {
        passwordError.textContent = "";
        confirmPasswordInput.removeAttribute("disabled");
    }

    if (confirmPasswordInput.value !== "") {
        validateConfirmPassword();
    }
}

function validateConfirmPassword() {
    var passwordInput = document.getElementById("userPassword");
    var confirmPasswordInput = document.getElementById("confirmPassword");
    var confirmPasswordError = document.getElementById("confirmPassword-error");

    if (confirmPasswordInput.value === "") {
        confirmPasswordError.textContent = "La confirmación de contraseña no puede estar vacía";
    } else if (confirmPasswordInput.value !== passwordInput.value) {
        confirmPasswordError.textContent = "Las contraseñas no coinciden";
    } else {
        confirmPasswordError.textContent = "";
    }
}

function validateForm(event) {
    var passwordInput = document.getElementById("userPassword");
    var confirmPasswordInput = document.getElementById("confirmPassword");
    var passwordError = document.getElementById("password-error");
    var confirmPasswordError = document.getElementById("confirmPassword-error");

    if (passwordInput.value === "") {
        passwordError.textContent = "La contraseña no puede estar vacía";
        event.preventDefault(); // Evita que se envíe el formulario
    }

    if (confirmPasswordInput.value === "") {
        confirmPasswordError.textContent = "La confirmación de contraseña no puede estar vacía";
        event.preventDefault(); // Evita que se envíe el formulario
    } else if (confirmPasswordInput.value !== passwordInput.value) {
        confirmPasswordError.textContent = "Las contraseñas no coinciden";
        event.preventDefault(); // Evita que se envíe el formulario
    }
}

// Asignar los eventos a los elementos correspondientes
var passwordInput = document.getElementById("userPassword");
var confirmPasswordInput = document.getElementById("confirmPassword");

passwordInput.addEventListener("input", validatePassword);
confirmPasswordInput.addEventListener("input", validateConfirmPassword);

var form = document.getElementById("user-form");
form.addEventListener("submit", validateForm);

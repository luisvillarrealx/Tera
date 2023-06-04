function validateEmail() {
    var emailInput = document.getElementById("email");
    var email = emailInput.value;
    var errorElement = document.getElementById("userEmail-error");

    if (email.trim().length === 0) {
        errorElement.textContent = "Por favor, ingrese el/su correo";
    } else if (!isValidEmail(email)) {
        errorElement.textContent = "No cumple con el formato de correo";
    } else {
        errorElement.textContent = "";
    }
}

function isValidEmail(email) {
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

//function validatePassword() {
//    var passwordInput = document.getElementById("password");
//    var password = passwordInput.value;
//    var errorElement = document.getElementById("userPassword-error");

//    if (password.trim().length === 0) {
//        errorElement.textContent = "Por favor, ingrese una contraseña";
//    } else {
//        errorElement.textContent = "";
//    }
//}

//function validateConfirmPassword() {
//    var passwordInput = document.getElementById("password");
//    var confirmPasswordInput = document.getElementById("confirmPassword");
//    var password = passwordInput.value;
//    var confirmPassword = confirmPasswordInput.value;
//    var errorElement = document.getElementById("confirmPassword-error");

//    if (password === confirmPassword) {
//        errorElement.textContent = "";
//    } else {
//        errorElement.textContent = "Las contraseñas no coinciden.";
//    }
//}

function validateSiteId() {
    var siteIdSelect = document.getElementById("userSiteId");
    var selectedValue = siteIdSelect.value;
    var errorElement = document.getElementById("userSiteId-error");

    if (selectedValue === "0") {
        errorElement.textContent = "Por favor, selecciona una sede";
    } else {
        errorElement.textContent = "";
    }
}

function validateRoleId() {
    var roleIdSelect = document.getElementById("userRoleId");
    var selectedValue = roleIdSelect.value;
    var errorElement = document.getElementById("userRoleId-error");

    if (selectedValue === "0") {
        errorElement.textContent = "Por favor, selecciona un rol";
    } else {
        errorElement.textContent = "";
    }
}

document.getElementById("user-form").addEventListener("submit", function (event) {
    clearError(event);
});

function clearError(event) {
    var errorElements = document.getElementsByClassName("text-danger");

    for (var i = 0; i < errorElements.length; i++) {
        var errorElement = errorElements[i];
        errorElement.textContent = "";
    }

    var isFormValid = validateForm();

    if (!isFormValid) {
        event.preventDefault();
    }
}

function validateForm() {
    var emailInput = document.getElementById("email");
    //var passwordInput = document.getElementById("password");
    //var confirmPasswordInput = document.getElementById("confirmPassword");
    var siteIdSelect = document.getElementById("userSiteId");
    var roleIdSelect = document.getElementById("userRoleId");

    var isFormValid = true;

    if (emailInput.value.trim() === "") {
        document.getElementById("userEmail-error").textContent = "Por favor, ingrese el/su correo";
        isFormValid = false;
    } else if (!isValidEmail(emailInput.value)) {
        document.getElementById("userEmail-error").textContent = "No cumple con el formato de correo";
        isFormValid = false;
    } else {
        document.getElementById("userEmail-error").textContent = "";
    }

    //if (passwordInput.value.trim() === "") {
    //    document.getElementById("userPassword-error").textContent = "Por favor, ingrese una contraseña";
    //    isFormValid = false;
    //} else {
    //    document.getElementById("userPassword-error").textContent = "";
    //}

    //if (confirmPasswordInput.value.trim() === "") {
    //    document.getElementById("confirmPassword-error").textContent = "Por favor, confirme la contraseña";
    //    isFormValid = false;
    //} else {
    //    document.getElementById("confirmPassword-error").textContent = "";
    //}

    //if (passwordInput.value !== confirmPasswordInput.value) {
    //    document.getElementById("confirmPassword-error").textContent = "Las contraseñas no coinciden";
    //    isFormValid = false;
    //}

    if (siteIdSelect.value === "0") {
        document.getElementById("userSiteId-error").textContent = "Por favor, selecciona una sede";
        isFormValid = false;
    } else {
        document.getElementById("userSiteId-error").textContent = "";
    }

    if (roleIdSelect.value === "0") {
        document.getElementById("userRoleId-error").textContent = "Por favor, selecciona un rol";
        isFormValid = false;
    } else {
        document.getElementById("userRoleId-error").textContent = "";
    }

    return isFormValid;
}
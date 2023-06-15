function validateCategoryExist() {
    var CategoryName = $("#CategoryName").val();

    // Realizar una solicitud AJAX para verificar si la categoría existe
    $.ajax({
        url: "/Category/CategoryExist",
        type: "POST",
        data: { CategoryName: CategoryName },
        success: function (result) {
            if (result === true) {
                // La categoría existe, muestra el mensaje de error
                $("#CategoryName-error").text("La categoría ya está registrada.");
                $("#submit-btn").prop("disabled", true);
            } else {
                $("#CategoryName-error").text("");
                $("#submit-btn").prop("disabled", false);
            }
        },
        error: function () {
            // Manejar el error de la solicitud AJAX si es necesario
            console.log("Error al verificar la categoría.");
        }
    });
}

function validateForm() {
    var CategoryName = $("#CategoryName").val();

    if (CategoryName.trim() === "") {
        // Campo vacío, muestra el mensaje de error
        $("#CategoryName-error").text("Por favor, ingrese un nombre.");
        $("#empty-error").text("");
        $("#submit-btn").prop("disabled", true);
    } else {
        // Campo no vacío, valida si la categoría ya existe
        $("#CategoryName-error").text("");
        $("#empty-error").text("");
        $("#submit-btn").prop("disabled", false);
        validateCategoryExist();
    }
}

$(document).ready(function () {
    $("#category-form").submit(function (e) {
        if ($("#CategoryName").val().trim() === "") {
            e.preventDefault();
            $("#empty-error").text("Por favor, ingrese un nombre.");
            $("#submit-btn").prop("disabled", true);
        }
    });
});
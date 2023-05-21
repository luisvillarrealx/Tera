function EmailExists() {

    $("#form-submit").prop("disabled", true);
    let validateEmailExists = $("#userEmail").val();

    $.ajax({
        type: "POST",
        url: "/User/EmailExists",
        dataType: "json",
        data: {
            "validateEmailExists": validateEmailExists
        },
        success: function (res) {

            if (res != "ERROR") {
                if (res == "") {
                    $("#form-submit").prop("disabled", false);
                }
                else {
                    alert(res);
                }
            }
        }
    });
}

function ChangeUserActive() {

    let id = $("#idModalHidden").val();

    $.ajax({
        type: "POST",
        url: "/User/ChangeUserActive",
        dataType: "json",
        data: {
            "id": id
        },
        success: function (res) {
            window.location.href = "/User/List";
        }
    });
}
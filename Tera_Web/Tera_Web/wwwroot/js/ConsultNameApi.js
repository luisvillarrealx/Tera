    function ConsultNameApi() {

        let userGovId = $("#userGovId").val();

        if (userGovId.trim().length >= 8) {
            $.ajax({
                type: "GET",
                url: "https://apis.gometa.org/cedulas/" + userGovId,
                dataType: "json",
                success: function (res) {
                    $("#userName").val(res.results[0].firstname);
                    $("#userFirstSurname").val(res.results[0].lastname1);
                    $("#userSecondSurname").val(res.results[0].lastname2);
                }
            });
        }
        else {
            $("#userName").val("");
            $("#userFirstSurname").val("");
            $("#userSecondSurname").val("");
        }
    }
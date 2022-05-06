

let Upload = () => {
    $('#loading').removeClass('hidden');
    var files = $("#files").get(0).files;
    var param = new FormData();
    if (files.length == 0) {
        $('#loading').addClass('hidden');
        toastr.error("Please upload at least one file", "Alert");
    }
    else if (!isEmail($('#ToEmail').val())) {
        $('#loading').addClass('hidden');
        toastr.error("Invalid Email", "Alert");
    }
    else {
        for (var i = 0; i < files.length; i++) {
            param.append("files", files[i]);
        }
        param.append("ToEmail", $('#ToEmail').val());
        $.ajax({
            type: "POST",
            url: "/FileUpload/UploadFile",
            dataType: "json",
            contentType: false,
            processData: false,
            data: param,
            success: function (result) {
                $('#loading').addClass('hidden');
                if (result.code == "Success") {
                    toastr.success(result.message, result.code);
                }
                else {
                    toastr.error(result.message, result.code);
                }
                $('#files').val('');
                $('#ToEmail').val('');
            },
            error: function (xhr, status, error) {
                toastr.error(status, "error");
            }
        });
    }
};

function isEmail(email) {
    var EmailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return EmailRegex.test(email);
}
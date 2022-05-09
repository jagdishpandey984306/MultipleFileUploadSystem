
$(function () {
    $('#UploadFileForm').validate({
        rules: {
            files: {
                required: true,
                extension: "jpeg|jpg|png|pdf"
            },
            ToEmail: {
                required: true,
                email: true
            }
        },
        messages: {
            files: {
                required: "*Please  upload valid file formats",
                extension: "*Please  upload valid file formats"
            },
            ToEmail: {
                required: "*Please enter a valid email address",
                email: "*Please enter a valid email address"
            }
        }
    });

    $('#btnSave').on("click", function () {
        if (!$("#UploadFileForm").valid()) {
            $("#UploadFileForm").valid();
        }
        else {
            Upload();
        }
    });
});

let Upload = () => {
    $('#loading').removeClass('hidden');
    var files = $("#files").get(0).files;
    var param = new FormData();
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
                List();
                $('#files').val('');
                $('#ToEmail').val('');
            }
            else {
                toastr.error(result.message, result.code);
            }
        },
        error: function (xhr, status, error) {
            toastr.error(status, "error");
        }
    });
};

let List = () => {
    $('#dataBind').html('');
    $.ajax({
        type: "Get",
        url: "/FileUpload/List",
        success: function (result) {
            $('#dataBind').html(result);
        },
        error: function (xhr, status, error) {
            toastr.error(status, "error");
        }
    });
};

let Delete = (id) => {
    $.ajax({
        type: "Get",
        url: "/FileUpload/Delete?id=" + id,
        success: function (result) {
            if (result.code == "Success") {
                toastr.success(result.message, result.code);
                List();
            }
            else {
                toastr.error(result.message, result.code);
            }
        },
        error: function (xhr, status, error) {
            toastr.error(status, "error");
        }
    });
};

function isEmail(email) {
    var EmailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return EmailRegex.test(email);
}
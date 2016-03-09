$('#uploadFile').on('change', function (e) {
    var files = e.target.files;
    if (files.length > 0) {
        var data = new FormData();
        for (var x = 0; x < files.length; x++) {
            data.append("file" + x, files[x]);
        }
        $.ajax({
            type: "POST",
            url: 'Home/UploadFile',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                $("div.result").empty();
                for (var i = 0; i < result.length; i++) {
                    var item = dllLoader.getTypeInfoHtml(result[i]);
                    $(".result").append(item);
                }
                dllLoader.rebindShowEvents();
            },
            error: function (ex) {
                dllLoader.writeErrorMessage("Something is wrong. " + ex.responseText);
            }
        });
    }
});
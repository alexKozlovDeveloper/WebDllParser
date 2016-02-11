var rebindShowEvents = function (item) {
    $('.image-container').unbind();
    $('.image-container').click(showTypeFunc);
}

var getTypeInfoHtml = function (item, imageClass) {
    var res = "<div class='member-container'>";
    if (item.IsHasChild === true) {
        res += "<div class='image-plus image-container'></div>";
    }
    res += "<div class='image-" + imageClass + " image-container'></div><div class='name-container'>" + item.Name + "</div><div class='name-container'>: " + item.TypeName + "</div>";
    res += "<div class='name-container'>" + item.ParametrsInfo + "</div>";
    res += "</div>";
    return res;
}

var getTypesInfoHtml = function (arr, imageClass) {
    var res = "";
    arr.forEach(function (item) {
        res += getTypeInfoHtml(item, imageClass);
    });
    return res;
}

var showTypeFunc = function (e) {
    var parrentNode = e.target.parentNode;
    var childDiv = parrentNode.childNodes[5];
    var nameDiv = parrentNode.childNodes[2];
    var imageDiv = parrentNode.childNodes[0];

    if (parrentNode.childNodes.length === 5) {

        $.ajax({
            type: "POST",
            url: 'Home/GetTypeInfo?name=' + nameDiv.innerText,
            contentType: false,
            processData: false,
            success: function (result) {
                var str = "<div class='childs-container'>";

                str += getTypesInfoHtml(result.Constructors, "constructors");
                str += getTypesInfoHtml(result.Events, "events");
                str += getTypesInfoHtml(result.FieldsChilds, "fields");
                str += getTypesInfoHtml(result.Methods, "methods");
                str += getTypesInfoHtml(result.PropertiesChilds, "properties");

                str += "</div><div class='image-plus'></div>";

                $(parrentNode).append(str);
                $(imageDiv).removeClass("image-plus").addClass("image-minus");

                rebindShowEvents();

                console.log(result);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });

    }
    else {

        if ($(imageDiv).hasClass("image-plus") === true) {

            $(childDiv).removeClass("childs-container-hide").addClass("childs-container-show");
            $(imageDiv).removeClass("image-plus").addClass("image-minus");
        }
        else {

            $(childDiv).removeClass("childs-container-show").addClass("childs-container-hide");
            $(imageDiv).removeClass("image-minus").addClass("image-plus");
        }
    }
}


$('#uploadFile').on('change', function (e) {
    var files = e.target.files;

    if (files.length > 0) {

        if (window.FormData !== undefined) {
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
                        var item = getTypeInfoHtml(result[i], "class");
                        $(".result").append(item);
                    }

                    $('.image-container').click(showTypeFunc);
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    if (xhr.responseText && xhr.responseText[0] == "{")
                        err = JSON.parse(xhr.responseText).Message;
                }
            });
        } else {
            alert("This browser doesn't support HTML5 file uploads!");
        }
    }
});
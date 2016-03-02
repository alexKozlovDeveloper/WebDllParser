var showTypesFromNamespaceUrl = "Home/GetTypeFromNamespace?namespaceName=";
var showTypeInfoUrl = "Home/GetTypeInfo?name=";

var writeErrorMessage = function (message) {
    $("div.result").empty();
    var message = "<div class='error-container'>" + message + "</div>";
    $(".result").append(message);
}

var rebindShowEvents = function (item) {
    $('.image-container.type').unbind();
    $('.image-container.type').click(showTypeFunc);
    $('.image-container.namespace').unbind();
    $('.image-container.namespace').click(showTypesFromNamespaceFunc);
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

var getTypeHtml = function (item) {
    var res = "<div class='member-container'>";
    res += "<div class='image-plus image-container type'></div>";
    res += "<div class='image-class image-container'></div><div class='name-container'>" + item + "</div>";
    res += "</div>";
    return res;
}

var getNamespaceHtml = function (item, imageClass) {
    var res = "<div class='member-container'>";
    res += "<div class='image-plus image-container namespace'></div>";
    res += "<div class='image-namespace image-container'></div><div class='name-container'>" + item + "</div>";
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

    var image = $(parrentNode).children(".image-container")[0];
    var childs = $(parrentNode).children(".childs-container")[0];
    var name = $(parrentNode).children(".name-container")[0];

    if ($(e.target).hasClass("image-plus") === true) {
        if ($(parrentNode).hasClass("container-loaded") === true) {
            $(childs).removeClass("childs-container-hide").addClass("childs-container-show");
            $(image).removeClass("image-plus").addClass("image-minus");
        }
        else {
            $.ajax({
                type: "POST",
                url: showTypeInfoUrl + name.innerText,
                contentType: false,
                processData: false,
                success: function (result) {
                    var str = "<div class='childs-container'>";

                    str += getTypesInfoHtml(result.Constructors, "constructors");
                    str += getTypesInfoHtml(result.Events, "events");
                    str += getTypesInfoHtml(result.Fields, "fields");
                    str += getTypesInfoHtml(result.Methods, "methods");
                    str += getTypesInfoHtml(result.Properties, "properties");

                    str += "</div><div class='image-plus'></div>";

                    $(parrentNode).append(str);
                    $(image).removeClass("image-plus").addClass("image-minus");

                    $(parrentNode).addClass("container-loaded");

                    rebindShowEvents();
                },
                error: function (ex) {
                    writeErrorMessage("Something is wrong. " + ex.responseText);
                }
            });
        }
    }
    else {
        $(childs).removeClass("childs-container-show").addClass("childs-container-hide");
        $(image).removeClass("image-minus").addClass("image-plus");
    }
}

var showTypesFromNamespaceFunc = function (e) {
    var parrentNode = e.target.parentNode;

    var image = $(parrentNode).children(".image-container")[0];
    var childs = $(parrentNode).children(".childs-container")[0];
    var name = $(parrentNode).children(".name-container")[0];

    if ($(e.target).hasClass("image-plus") === true) {
        if ($(parrentNode).hasClass("container-loaded") === true) {
            $(childs).removeClass("childs-container-hide").addClass("childs-container-show");
            $(image).removeClass("image-plus").addClass("image-minus");
        }
        else {
            $.ajax({
                type: "POST",
                url: showTypesFromNamespaceUrl + name.innerText,
                contentType: false,
                processData: false,
                success: function (result) {
                    var str = "<div class='childs-container'>";
                    result.forEach(function (item) {
                        str += getTypeHtml(item);
                    });
                    str += "</div>";

                    $(parrentNode).append(str);
                    $(parrentNode).addClass("container-loaded");

                    $(image).removeClass("image-plus").addClass("image-minus");

                    rebindShowEvents();
                },
                error: function (ex) {
                    writeErrorMessage("Something is wrong. " + ex.responseText);
                }
            });
        }
    }
    else {
        $(childs).removeClass("childs-container-show").addClass("childs-container-hide");
        $(image).removeClass("image-minus").addClass("image-plus");
    }
}

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
                    var item = getNamespaceHtml(result[i]);
                    $(".result").append(item);
                }
                rebindShowEvents();
            },
            error: function (ex) {
                debugger;
                writeErrorMessage("Something is wrong. " + ex.responseText);
            }
        });
    }
});
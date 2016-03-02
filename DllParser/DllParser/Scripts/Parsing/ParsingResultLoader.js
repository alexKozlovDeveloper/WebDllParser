var dllLoader = {
    showTypesFromNamespaceUrl: "Home/GetTypeFromNamespace?namespaceName=",
    showTypeInfoUrl: "Home/GetTypeInfo?name=",
    writeErrorMessage: function (message) {
        $("div.result").empty();
        var message = "<div class='error-container'>" + message + "</div>";
        $(".result").append(message);
    },
    rebindShowEvents: function (item) {
        var me = this;

        $('.image-container.type').unbind();
        $('.image-container.type').click(me.showTypeFunc);
        $('.image-container.namespace').unbind();
        $('.image-container.namespace').click(me.showTypesFromNamespaceFunc);
    },
    getTypeInfoHtml: function (item, imageClass) {
        var res = "<div class='member-container'>";
        if (item.IsHasChild === true) {
            res += "<div class='image-plus image-container'></div>";
        }
        res += "<div class='image-" + imageClass + " image-container'></div><div class='name-container'>" + item.Name + "</div><div class='name-container'>: " + item.TypeName + "</div>";
        res += "<div class='name-container'>" + item.ParametrsInfo + "</div>";
        res += "</div>";
        return res;
    },
    getTypeHtml: function (item) {
        var res = "<div class='member-container'>";
        res += "<div class='image-plus image-container type'></div>";
        res += "<div class='image-class image-container'></div><div class='name-container'>" + item + "</div>";
        res += "</div>";
        return res;
    },
    getNamespaceHtml: function (item, imageClass) {
        var res = "<div class='member-container'>";
        res += "<div class='image-plus image-container namespace'></div>";
        res += "<div class='image-namespace image-container'></div><div class='name-container'>" + item + "</div>";
        res += "</div>";
        return res;
    },
    getTypesInfoHtml: function (arr, imageClass) {
        var me = this;

        var res = "";
        arr.forEach(function (item) {
            res += me.getTypeInfoHtml(item, imageClass);
        });
        return res;
    },
    showTypeFunc: function (e) {
        var me = this;

        var parrentNode = e.target.parentNode;

        var image = $(parrentNode).children(".image-container").get(0);
        var childs = $(parrentNode).children(".childs-container").get(0);
        var name = $(parrentNode).children(".name-container").get(0);

        if ($(e.target).hasClass("image-plus") === true) {
            if ($(parrentNode).hasClass("container-loaded") === true) {
                $(childs).toggle();
                $(image).removeClass("image-plus").addClass("image-minus");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: dllLoader.showTypeInfoUrl + name.innerText,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        var str = "<div class='childs-container'>";

                        str += dllLoader.getTypesInfoHtml(result.Constructors, "constructors");
                        str += dllLoader.getTypesInfoHtml(result.Events, "events");
                        str += dllLoader.getTypesInfoHtml(result.Fields, "fields");
                        str += dllLoader.getTypesInfoHtml(result.Methods, "methods");
                        str += dllLoader.getTypesInfoHtml(result.Properties, "properties");

                        str += "</div><div class='image-plus'></div>";

                        $(parrentNode).append(str);
                        $(image).removeClass("image-plus").addClass("image-minus");

                        $(parrentNode).addClass("container-loaded");

                        dllLoader.rebindShowEvents();
                    },
                    error: function (ex) {
                        dllLoader.writeErrorMessage("Something is wrong. " + ex.responseText);
                    }
                });
            }
        }
        else {
            $(childs).toggle();
            $(image).removeClass("image-minus").addClass("image-plus");
        }
    },
    showTypesFromNamespaceFunc: function (e) {
        var me = this;

        var parrentNode = e.target.parentNode;

        var image = $(parrentNode).children(".image-container").get(0);
        var childs = $(parrentNode).children(".childs-container").get(0);
        var name = $(parrentNode).children(".name-container").get(0);
        
        if ($(e.target).hasClass("image-plus") === true) {
            if ($(parrentNode).hasClass("container-loaded") === true) {
                $(childs).toggle();
                $(image).removeClass("image-plus").addClass("image-minus");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: dllLoader.showTypesFromNamespaceUrl + name.innerText,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        var str = "<div class='childs-container'>";
                        result.forEach(function (item) {
                            str += dllLoader.getTypeHtml(item);
                        });
                        str += "</div>";

                        $(parrentNode).append(str);
                        $(parrentNode).addClass("container-loaded");

                        $(image).removeClass("image-plus").addClass("image-minus");

                        dllLoader.rebindShowEvents();                        
                    },
                    error: function (ex) {
                        dllLoader.writeErrorMessage("Something is wrong. " + ex.responseText);
                    }
                });
            }
        }
        else {
            $(childs).toggle();
            $(image).removeClass("image-minus").addClass("image-plus");
        }
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
                    var item = dllLoader.getNamespaceHtml(result[i]);
                    $(".result").append(item);
                }
                dllLoader.rebindShowEvents();
            },
            error: function (ex) {
                writeErrorMessage("Something is wrong. " + ex.responseText);
            }
        });
    }
});
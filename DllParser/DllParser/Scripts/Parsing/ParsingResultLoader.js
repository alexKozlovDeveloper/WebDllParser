window.dllLoader = {
    showTypesFromNamespaceUrl: "Home/GetTypeFromNamespace?namespaceName=",
    showTypeInfoUrl: "Home/GetTypeInfo?name=",
    errorContainerTemplate: "<div class='error-container'>{0}</div>",
    imageContainerSelector: ".image-container",
    resultSelector: ".result",
    typeInfoTemplate: "<div class='member-container'>{0}<div class='image-{1} image-container'></div><div class='name-container'>{2}</div></div>",
    imagePlusTemplate: "<div class='image-plus image-container {0}'></div>",
    nameSpaceCLassName: "Namespace",
    childsContainerTemplate: "<div class='childs-container'>{0}</div>",
    childsContainerTemplateWithPlus: "<div class='childs-container'>{0}</div><div class='image-plus'></div>",
    childsContainerSelector: ".childs-container",
    nameContainerSelector: ".name-container",
    imagePlusClassName: "image-plus",
    containerLoadedClassName: "container-loaded",
    imageMinusClassName: "image-minus",
    wrongMesage: "Something is wrong. {0}",
    writeErrorMessage: function (message) {
        var me = this;

        $(me.resultSelector).empty();
        var message = me.errorContainerTemplate.format([message]);
        $(me.resultSelector).append(message);
    },
    rebindShowEvents: function (item) {
        var me = this;

        $(me.imageContainerSelector).unbind();
        $(me.imageContainerSelector).click(me.showItemFunc);
    },
    getTypeInfoHtml: function (item) {
        var me = this;

        var imagePlus = "";

        if (item.IsHasChild === true) {
            imagePlus = me.imagePlusTemplate.format([item.Type]);
        }

        return me.typeInfoTemplate.format([imagePlus, item.Type, item.Description]);
    },
    getTypesInfoHtml: function (arr) {
        var me = this;
        var res = "";
        arr.forEach(function (item) {
            res += me.getTypeInfoHtml(item);
        });
        return res;
    },
    GetShowTypeUrl: function (e, name) {
        var me = this;

        if ($(e).hasClass(me.nameSpaceCLassName) === true) {
            return window.dllLoader.showTypesFromNamespaceUrl + name;
        }
        else {
            return window.dllLoader.showTypeInfoUrl + name;
        }
    },
    showTypesFromNamespaceSuccessFunc: function (result) {
        var me = this;

        var str = "";
        result.forEach(function (item) {
            str += window.dllLoader.getTypeInfoHtml(item);
        });

        return me.childsContainerTemplate.format([str]);
    },
    showTypeSuccessFunc: function (result) {
        var me = this;

        var str = "";

        str += window.dllLoader.getTypesInfoHtml(result.Events);
        str += window.dllLoader.getTypesInfoHtml(result.Fields);
        str += window.dllLoader.getTypesInfoHtml(result.Methods);
        str += window.dllLoader.getTypesInfoHtml(result.Properties);

        return me.childsContainerTemplateWithPlus.format([str]);
    },
    showItemFunc: function (e) {
        var me = window.dllLoader;

        var parrentNode = e.target.parentNode;

        var image = $(parrentNode).children(me.imageContainerSelector).get(0);
        var childs = $(parrentNode).children(me.childsContainerSelector).get(0);
        var name = $(parrentNode).children(me.nameContainerSelector).get(0);
        debugger;
        if ($(e.target).hasClass(me.imagePlusClassName) === true) {
            if ($(parrentNode).hasClass(me.containerLoadedClassName) === true) {
                $(childs).toggle();
                $(image).removeClass(me.imagePlusClassName).addClass(me.imageMinusClassName);
            }
            else {
                $.ajax({
                    type: "POST",
                    url: window.dllLoader.GetShowTypeUrl(e.target, name.innerText),
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        var str = "";

                        if ($(e.target).hasClass(me.nameSpaceCLassName) === true) {
                            str = window.dllLoader.showTypesFromNamespaceSuccessFunc(result, parrentNode);
                        }
                        else {
                            str = window.dllLoader.showTypeSuccessFunc(result, parrentNode);
                        }

                        $(parrentNode).append(str);
                        $(image).removeClass(me.imagePlusClassName).addClass(me.imageMinusClassName);

                        $(parrentNode).addClass(me.containerLoadedClassName);

                        window.dllLoader.rebindShowEvents();

                    },
                    error: function (ex) {
                        window.dllLoader.writeErrorMessage(me.wrongMesage.format([ex.responseText]));
                    }
                });
            }
        }
        else {
            $(childs).toggle();
            $(image).removeClass(me.imageMinusClassName).addClass(me.imagePlusClassName);
        }
    }
}


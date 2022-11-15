$validator.addMethod("onlyimage", function (value, element, parameters) {
    let fileType = $(element)[0].files[0].type;
    let fileTypes = ["image/jpeg", "image/jpg", "image/png", "image/webp", "image/raw", "image/svg"];
    let validExtension = $.inArray(fileType, fileTypes) !== -1;
    return validExtension;
});
$validator.unobtrusive.adapters.add("onlyimage", [], function (options) {
    let params = {
        fileExtensions = $(options.element).data("val-onlyimage").split(',')
    };

    options.rules["onlyimage"] = params;
    options.messages["onlyimage"] = $(options.element).data("val-onlyimage");
});
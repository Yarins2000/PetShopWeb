﻿$(function () {
    $("#catSelect").change(function () {
        let selected = $('#catSelect').val();
        window.location.href = "/Catalog/ShowCatalog/" + selected
    });
});

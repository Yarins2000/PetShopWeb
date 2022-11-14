$(function () {
    $("#catSelect").change(function () {
        let selected = $('#catSelect').val();
        window.location.href = "/Administrator/ManageAnimals/" + selected
    });
});

//This function occures every choosing a category from the select.
//Get the category id and passes it to the same action for filtering the animals by the chosen category.
//Admin-controller
$(function () {
    $("#catSelect").change(function () {
        let selected = $('#catSelect').val();
        window.location.href = "/Administrator/ManageAnimals/" + selected
    });
});

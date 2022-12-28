function confirmDelete(uniqueId, isDeleteClicked) {
    let deleteSpan = 'deleteSpan_' + uniqueId;
    let confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;


    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}
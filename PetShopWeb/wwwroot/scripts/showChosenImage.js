//Presents the chosen image from the file-input.
let fileInput = document.querySelector("#imageFile");
fileInput.addEventListener("change", function () {
    let img = document.querySelector("#chosenImg");
    img.src = window.URL.createObjectURL(this.files[0]);
});
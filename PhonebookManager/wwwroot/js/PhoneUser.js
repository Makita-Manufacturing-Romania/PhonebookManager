window.onload = function () {
    const makeTransparent = document.getElementById('transparentDiv');
    makeTransparent.style.opacity = '0.3';

    const tableBody = document.getElementById('tBody'); 
    tableBody.style.display = 'none';

    const input = document.querySelector('#searchInput'); 
    input.disabled = true;

    const btnAddUser = document.querySelector("#addUserBtn"); 
    btnAddUser.disabled = true;

    const btnOwnership = document.querySelector("#ownershipBtn"); 
    btnOwnership.disabled = true;

    
    //btnOwnership.disabled = false;

};

//OR
//$(document).ready(function () {
//    var makeTransparent = document.getElementById('transparentDiv');
//    makeTransparent.style.opacity = '0.5';
//});



function changeConfirm() {
    var modalTitleOne = document.getElementById("changeModalLabelOne");
    var modalTitleTwo = document.getElementById("changeModalLabelTwo");

    var modalRequestText = document.getElementById("modalRequestBox");
    var modalConfirmationBox = document.getElementById("modalConfirmationBox");

    modalTitleOne.className = "d-none";
    modalTitleTwo.className = "d-block";

    modalRequestText.className = "d-none";

    modalConfirmationBox.className = "d-flex";

}

function openModalChange(clicked_id) {


    $('#changeModal').modal('show');
}
// Close the import modal
//function closeChangeModal() {
//    Toastify({
//        text: "Not implemented",
//        duration: 3000,
//        newWindow: true,
//        close: true,
//        gravity: "bottom", // `top` or `bottom`
//        position: "left", // `left`, `center` or `right`
//        stopOnFocus: true, // Prevents dismissing of toast on hover
//        style: {
//            background: "linear-gradient(to right, red, #55B1BB)",
//        },
//    }).showToast();

//     $('#importModal').modal('hide');
//}

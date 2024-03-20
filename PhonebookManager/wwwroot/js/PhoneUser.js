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

function ReverseOpacity() {
    const makeTransparent = document.getElementById('transparentDiv');
    makeTransparent.style.opacity = '1';

    const tableBody = document.getElementById('tBody');
    tableBody.style.display = 'block';

    const input = document.querySelector('#searchInput');
    input.disabled = false;

    const btnAddUser = document.querySelector("#addUserBtn");
    btnAddUser.disabled = false;

    const btnOwnership = document.querySelector("#ownershipBtn");
    btnOwnership.disabled = false;
}

//OR
//$(document).ready(function () {
//    var makeTransparent = document.getElementById('transparentDiv');
//    makeTransparent.style.opacity = '0.5';
//});


function OpenModalChange(clicked_id) {


    $('#changeModal').modal('show');
}

function ChangeConfirm() {
    var modalTitleOne = document.getElementById("changeModalLabelOne");
    var modalTitleTwo = document.getElementById("changeModalLabelTwo");

    var modalRequestText = document.getElementById("modalRequestBox");
    var modalConfirmationBox = document.getElementById("modalConfirmationBox");

    modalTitleOne.className = "d-none";
    modalTitleTwo.className = "d-block";

    modalRequestText.className = "d-none";

    modalConfirmationBox.className = "d-flex";

}


function AllocateNumber() {
    var phonenumber = document.getElementById("phoneNo").innerHTML;


    var selectAppUser = document.getElementById("selectAppUser");
    var userValue = selectAppUser.options[selectAppUser.selectedIndex].value;

    var selectDepartments = document.getElementById("selectDepartments");
    var depValue = selectDepartments.options[selectDepartments.selectedIndex].value;

    $.ajax({
        type: "POST",
        url: "/PhoneUser/Allocate",
        data: {
            'userId': userValue,
            'depId': depValue,
            'lineNo': phonenumber
        },
        /*dataType: 'json',*/
        success: function (result) {
            window.location.href = location.origin + "/Dashboard";
        },
        error: function (error) {
            console.log(error);
        }
    });


}
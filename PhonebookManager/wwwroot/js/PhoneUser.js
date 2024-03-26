window.onload = function () {
    const makeTransparent = document.getElementById('transparentDiv');
    makeTransparent.style.opacity = '0.3';

    const input = document.querySelector('#searchUserInput');
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

    const input = document.querySelector('#searchUserInput');
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

    //var selectOwner = document.getElementById("selectOwner");
    //var userValue = selectOwner.options[selectOwner.selectedIndex].value;
    var selectLineOwner = document.getElementById("searchLineOwnerInput");
    var userValue = selectLineOwner.value;
    userValue = userValue.split(" ").shift();
    var selectDepartments = document.getElementById("selectDepartments");
    var depValue = selectDepartments.options[selectDepartments.selectedIndex].value;

    if (userValue === "" || depValue === "") {
        Toastify({
            text: "All fields are required",
            duration: 3000,
            newWindow: true,
            close: true,
            gravity: "bottom", // `top` or `bottom`
            position: "left", // `left`, `center` or `right`
            stopOnFocus: true, // Prevents dismissing of toast on hover
            style: {
                background: "linear-gradient(to right, #008A99, #55B1BB)",

            },
        }).showToast();
    }
    else {

        $.ajax({
            type: "POST",
            url: "/PhoneUser/Allocate",
            data: {
                'empId': userValue,
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

}



$(document).ready(function () {
    $(function () {
        // Autocomplete for add users
        $("#searchUserInput").autocomplete({
            source: function (request, response) { // response is the server response, request is the search term
                document.getElementById("spinner").style.display = "block"; // start the spinner here
                $.ajax({
                    url: '/PhoneUser/AutocompleteSearchUsers/',
                    data: { "searchText": request.term },
                    type: "POST",
                    success: function (data) {
                        document.getElementById("spinner").style.display = "none";
                        response($.map(data, function (item) {
                            return item;
                        }));

                        $(window).resize(function () {
                            $(".ui-autocomplete").css('display', 'none');
                        });

                    },
                    error: function (response) {
                        $("#searchUserInput").val("Error: " + response);
                    },
                    //failure: function (response) { // use error or failure
                    //    $("#searchInput").val("Failure: " + response);
                    //}
                });
            },
            select: function (e, i) {
                $("#userId").val(i.item.val).trigger('change');
                //$(this).autocomplete("close");
            },

        });
    });
    $('#userId').change(function () {
        var userId = $("#userId");
        document.getElementById("spinner").style.display = "block";

        $.ajax({
            url: '/PhoneUser/FindUser/',
            data: { "searchText": userId.val() },
            type: "GET",
            success: function (data) {
                document.getElementById("spinner").style.display = "none";
                var result = $.parseJSON(data);
                var name = document.getElementById('Name');
                name.value = result.FullName;
                var empId = document.getElementById('EmployeeID');
                empId.value = result.EmployeeID;
                var adUsername = document.getElementById('AdUsername');
                adUsername.value = result.Username;
                var depCode = document.getElementById('DepartmentCode');
                depCode.value = result.Department;
                var email = document.getElementById('Email');
                email.value = result.Email;

                var formDiv = document.getElementById('formDiv');
                formDiv.style.display = 'inline';
            },
            error: function (response) {
            },
            failure: function (response) {
            }
        });
    });

    $(function () {
        // Autocomplete for employee responsible
        $("#searchLineOwnerInput").autocomplete({
            source: function (request, response) { // response is the server response, request is the search term
                document.getElementById("spinner").style.display = "block"; // start the spinner here
                $.ajax({
                    url: '/PhoneUser/AutocompleteSearchUsers/',
                    data: { "searchText": request.term },
                    type: "POST",
                    success: function (data) {
                        document.getElementById("spinner").style.display = "none";
                        response($.map(data, function (item) {
                            return item;
                        }));

                        $(window).resize(function () {
                            $(".ui-autocomplete").css('display', 'none');
                        });

                    },
                    error: function (response) {
                        $("#searchLineOwnerInput").val("Error: " + response);
                    },
                    //failure: function (response) { // use error or failure
                    //    $("#searchInput").val("Failure: " + response);
                    //}
                });
            },
            select: function (e, i) {
                $("#lineOwnerId").val(i.item.val).trigger('change');
                //$(this).autocomplete("close");
            },

        });
    });
    $('#lineOwnerId').change(function () {
        var userId = $("#lineOwnerId");
        document.getElementById("spinner").style.display = "block";

        $.ajax({
            url: '/PhoneUser/FindUser/',
            data: { "searchText": userId.val() },
            type: "GET",
            success: function (data) {
                document.getElementById("spinner").style.display = "none";
                var result = $.parseJSON(data);

                var ownerExists = document.getElementById('ownerExists');
                if (ownerExists) {
                    var table = document.getElementById("myTable");

                    // Select the row to replace
                    var row = table.rows[1];

                    // Create new row content
                    var newRowContent = `<td>${result.EmployeeID}</td> <td>${result.FullName}</td> <td>${result.Email}</td> <td>Main user</td> <td> <span id="ownerExists" class="action-btn-disabled"> <i class="bi bi-trash-fill"></i> </span> </td>`;

                    // Replace the row
                    row.innerHTML = newRowContent;
                }
                else
                {
                    const tbody = document.querySelector('.table tbody');
                    // Create a new row and columns
                    let row = document.createElement('tr');
                    row.classList.add('border-0');

                    let idCell = document.createElement('td');
                    idCell.textContent = result.EmployeeID;
                    let nameCell = document.createElement('td');
                    nameCell.textContent = result.FullName;
                    let emailCell = document.createElement('td');
                    emailCell.textContent = result.Email;
                    let typeCell = document.createElement('td');
                    typeCell.textContent = "Main user";
                    let actionCell = document.createElement('td');
                    actionCell.innerHTML = `<span id="ownerExists" class="action-btn-disabled"> <i class="bi bi-trash-fill"></i> </span>`;


                    // Append the columns to the row
                    row.appendChild(idCell);
                    row.appendChild(nameCell);
                    row.appendChild(emailCell);
                    row.appendChild(typeCell);
                    row.appendChild(actionCell);

                    // Append the row to the table body
                    tbody.appendChild(row);
                }


                //var empId = document.getElementById('lineOwnerBadge');
                //empId.innerText = result.EmployeeID;
                //var name = document.getElementById('lineOwnerName');
                //name.innerText = result.FullName;
                //var email = document.getElementById('lineOwnerEmail');
                //email.innerText = result.Email;


            },
            error: function (response) {
            },
            failure: function (response) {
            }
        });
    });
});
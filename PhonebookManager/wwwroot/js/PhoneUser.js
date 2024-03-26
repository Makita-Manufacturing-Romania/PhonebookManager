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

    var table = document.getElementById("myTable");
    var rows = table.getElementsByTagName("tr");
    var rowIds = [];
    for (var i = 0; i < rows.length; i++) {
        // Get the ID of the row
        var rowId = rows[i].id;

        // Add the row ID to the array
        rowIds.push(rowId);
    }


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
                'lineOwnerId': userValue,
                'depId': depValue,
                'lineNo': phonenumber,
                'userIds': rowIds
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
    // Autocomplete for employee responsible
    $(function () {
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
    // for employee responsible
    $("#resetLineOwnerInput").on("click", function () {
        document.getElementById("searchLineOwnerInput").value = "";
        document.getElementById("resetLineOwnerInput").style.display = "none";

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
                document.getElementById("resetLineOwnerInput").style.display = "block";

                var result = $.parseJSON(data);

                var ownerExists = document.getElementById('ownerExists');
                if (ownerExists) {
                    var table = document.getElementById("myTable");

                    // Select the row to replace
                    var row = table.rows[1];

                    // Create new row content
                    var newRowContent = `<td>${result.EmployeeID}</td> <td>${result.FullName}</td> <td>${result.Email}</td> <td>Main user</td> <td> <span id="ownerExists"  class="action-btn-disabled"> <i class="bi bi-trash-fill"></i> </span> </td>`;

                    // Replace the row
                    row.innerHTML = newRowContent;
                }
                else {
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

                ReverseOpacity();
            },
            error: function (response) {
            },
            failure: function (response) {
            }
        });
    });

    // Autocomplete for add users
    $(function () {
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

    $("#resetUserInput").on("click", function () {
        document.getElementById("searchUserInput").value = "";
        document.getElementById("resetUserInput").style.display = "none";

    });
    // for add users
    $('#userId').change(function () {
        var userId = $("#userId");
        document.getElementById("spinner").style.display = "block";

        $.ajax({
            url: '/PhoneUser/FindUser/',
            data: { "searchText": userId.val() },
            type: "GET",
            success: function (data) {
                document.getElementById("spinner").style.display = "none";
                document.getElementById("resetUserInput").style.display = "block";
                var result = $.parseJSON(data);

                var userExists = document.getElementById(result.EmployeeID);
                if (userExists) {

                    // Get the cell
                    let cell = document.getElementById(result.EmployeeID);

                    // Get the row
                    let oldRow = cell.parentNode;

                    // Create a new row
                    //let newRow = document.createElement("tr");
                    // Add new cells to the new row as needed
                    //for (let i = 0; i < oldRow.cells.length; i++) {
                    //    let newCell = document.createElement("td");
                    //    newCell.textContent = "New text " + (i + 1);
                    //    newRow.appendChild(newCell);
                    //}
                    // oldRow.parentNode.replaceChild(newRow, oldRow);

                    // Create new row content
                    var newRowContent = `<td>${result.EmployeeID}</td> <td>${result.FullName}</td> <td>${result.Email}</td> <td>Secondary user</td> <td> <span id="removeBtn" role="button" user-id="${result.EmployeeID}" class="action-btn-danger" onclick="RemoveUser()"> <i class="bi bi-trash-fill"></i> </span> </td>`;

                    // Replace the old row with the new one
                    oldRow.innerHTML = newRowContent;

                }
                else {
                    const tbody = document.querySelector('.table tbody');
                    // Create a new row and columns
                    let row = document.createElement('tr');
                    row.id = result.EmployeeID; // add id to row

                    row.classList.add('border-0');

                    let idCell = document.createElement('td');
                    idCell.textContent = result.EmployeeID;
                    let nameCell = document.createElement('td');
                    nameCell.textContent = result.FullName;
                    let emailCell = document.createElement('td');
                    emailCell.textContent = result.Email;
                    let typeCell = document.createElement('td');
                    typeCell.textContent = "Secondary user";
                    let actionCell = document.createElement('td');
                    actionCell.innerHTML = `<span id="removeBtn" user-id="${result.EmployeeID}" role="button" class="action-btn-danger" onclick="RemoveUser()"> <i class="bi bi-trash-fill"></i> </span>`;


                    // Append the columns to the row
                    row.appendChild(idCell);
                    row.appendChild(nameCell);
                    row.appendChild(emailCell);
                    row.appendChild(typeCell);
                    row.appendChild(actionCell);

                    // Append the row to the table body
                    tbody.appendChild(row);
                }

            },
            error: function (response) {
            },
            failure: function (response) {
            }
        });
    });
});

function RemoveUser() {
    let findId = document.querySelector('[user-id]');
    let userId = findId.getAttribute('user-id');
    var userTd = document.getElementById(userId);
    let row = userTd.parentNode;
    row.remove();
}



$(document).ready(function () {
    $("#addUserBtn").on("click", function () {
        var table = document.getElementById("myTable");
        var rows = table.getElementsByTagName("tr");
        var rowIds = [];
        for (var i = 0; i < rows.length; i++) {
            // Get the ID of the row
            var rowId = rows[i].id;

            // Add the row ID to the array
            rowIds.push(rowId);
        }
        console.log(rowIds);
    });
});
//document.getElementById('yourInputId').addEventListener('input', function (e) {
//    // Handle input changes here
//    console.log("Character typed:", e.target.value);
//    // You can perform further actions based on the input value
//});

// On initialize
$(document).ready(function () {
    let viewBag = document.querySelector('[viewbag-name]');
    let vbName = viewBag.getAttribute('viewbag-name');
    var selectedDepartment = document.getElementById("selectDepartment");
    for (let i = 0; i < selectedDepartment.options.length; i++) {
        if (selectedDepartment.options[i].text === vbName) {
            selectedDepartment.selectedIndex = i;
            break;
        }
    }
});
$(document).ready(function () {
    $("#selectDepartment").on("change", function () {
        var selectedDepartment = document.getElementById("selectDepartment");
        var name = selectedDepartment.value;
        $.ajax({
            url: '/AppUsers/Index/',
            data: { "depName": name },
            type: "POST",
            success: function (data) {
                // window.location.href = location.origin + "/Dashboard";
                window.location.href = location.origin + "/AppUsers?depName=" + selectedDepartment.value;

            },
            error: function (response) {
                console.log("error");
            }

        });
    });
});

// Check if user exists
$(document).ready(function () {
    const badgeInputElement = document.querySelector('[badge-input="badgeNoInput"]');
    $(badgeInputElement).on('input', function () {
        const inputValue = $(this).val();
        $.ajax({
            url: 'AppUsers/CheckExistence', // Your server endpoint
            method: 'POST', // Adjust as needed
            data: { badgeNo: inputValue },
            success: function (response) {
                if (response == "Exists") {
                    Toastify({
                        text: "User exists",
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
                } else {
                    console.log("Value does not exist.");
                    // Handle non-existence case
                }
            },
            error: function () {
                console.error("Error checking existence.");
            }
        });
    });
});


// EDIT
function AppUserEditFunction(clicked_id) {
    const button = clicked_id;
    var userId = button.getAttribute("user-Id");
    var userName = button.getAttribute("user-Name");
    var userBadge = button.getAttribute("user-BadgeNo");
    var userDepartmentName = button.getAttribute("user-DepartmentName");
    var userDepartmentCode = button.getAttribute("user-DepartmentCode");
    var userRoleRole = button.getAttribute("user-Role-Role");

    var modalUserId = document.getElementById("editModalUserId");
    modalUserId.value = userId;
    var modalUserName = document.getElementById("editModalName");
    modalUserName.value = userName;
    var modalBadgeNo = document.getElementById("editModalBadgeNo");
    modalBadgeNo.value = userBadge;
    var modalDepartmentName = document.getElementById("editModalDepartmentName");
    modalDepartmentName.value = userDepartmentName;
    var modalDepartmentCode = document.getElementById("editModalDepartmentCode");
    modalDepartmentCode.value = userDepartmentCode;
    var modalSelectBoxRole = document.getElementById("editModalSelectBoxRole");

    for (let i = 0; i < modalSelectBoxRole.options.length; i++) {
        if (modalSelectBoxRole.options[i].text === userRoleRole) {
            modalSelectBoxRole.selectedIndex = i;
            break;
        }
    }

    $('#editModal').modal('show');
}

$("body").on("click", "#editBtn", function () {
    var modalUserId = $("#editModalUserId");
    //var modalUserAdIdentity = $("#modalAdIdentity");
    //var modalUserEmail = $("#modalEmail");
    var modalUserName = $("#editModalName");
    var modalUserBadge = $("#editModalBadgeNo");
    var modalUserDepName = $("#editModalDepartmentName");
    var modalUserDepCode = $("#editModalDepartmentCode");
    var modalUserRole = $("#editModalSelectBoxRole");
    
    $.ajax({
        type: "POST",
        url: "/AppUsers/Edit",
        data: {
            'id': modalUserId.val(),
            //'adIdentity': modalUserAdIdentity.val(),
            //'email': modalUserEmail.val(),
            'name': modalUserName.val(),
            'badgeNo': modalUserBadge.val(),
            'depName': modalUserDepName.val(),
            'depCode': modalUserDepCode.val(),
            'role': modalUserRole.val(),
        }, 
        /*dataType: 'json',*/
        success: function (result) {
            if (result == "Not found") {
                Toastify({
                    text: "Not found",
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

                $('#editModal').modal('hide');
            }
            else {
               //$('#editModal').modal('hide');
                window.location.href = location.origin + "/AppUsers";
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
});


// DELETE
function AppUserDeleteFunction(clicked_id) {
    const button = clicked_id;
    var userId = button.getAttribute("user-Id");
    var userName = button.getAttribute("user-Name");
    var userBadge = button.getAttribute("user-BadgeNo");
    var userDepartmentName = button.getAttribute("user-DepartmentName");
    var userDepartmentCode = button.getAttribute("user-DepartmentCode");

    var modalUserId = document.getElementById("delModalUserId");
    modalUserId.value = userId;
    var modalUserName = document.getElementById("delModalName");
    modalUserName.value = userName;
    var modalBadgeNo = document.getElementById("delModalBadgeNo");
    modalBadgeNo.value = userBadge;
    var modalDepartmentName = document.getElementById("delModalDepartmentName");
    modalDepartmentName.value = userDepartmentName;
    var modalDepartmentCode = document.getElementById("delModalDepartmentCode");
    modalDepartmentCode.value = userDepartmentCode;


    $('#deleteModal').modal('show');
}

$("body").on("click", "#deleteBtn", function () {
    var modalUserId = $("#delModalUserId");

    $.ajax({
        type: "POST",
        url: "/AppUsers/DeleteConfirmed",
        data: {
            'id': modalUserId.val(),
        },
        /*dataType: 'json',*/
        success: function (result) {
            if (result == "Not found") {
                Toastify({
                    text: "Not found",
                    duration: 3000,
                    newWindow: true,
                    close: true,
                    gravity: "bottom", // `top` or `bottom`
                    position: "left", // `left`, `center` or `right`
                    stopOnFocus: true, // Prevents dismissing of toast on hover
                    style: {
                        background: "linear-gradient(to right, #008A99, #00ff80)",
                    },
                }).showToast();

                $('#deleteModal').modal('hide');
            }
            else {

                //$('#deleteModal').modal('hide');
                window.location.href = location.origin + "/AppUsers";

            }
        },
        error: function (error) {
            console.log(error);
        }
    });
});

// SEARCH
$(document).ready(function () {

    $("#searchInput").on("keydown", function (event) {
        if (event.key === "Enter") {
            PerformSearch();
        }
    });
});

$(document).ready(function () {
    $("#searchButton").on("click", function () {
        PerformSearch();
    });
});
function PerformSearch() {
    var searchInputText = document.getElementById("searchInput");
    $.ajax({
        url: '/AppUsers/SearchAppUsers/',
        data: { "searchText": searchInputText.value },
        type: "POST",
        success: function (data) {
            if (data === "Not found") {
                Toastify({
                    text: "Not found",
                    duration: 10000,
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
                searchInputText.value = searchInputText.value.replace(/\s+/g, '');
                window.location.href = location.origin + "/AppUsers?searchText=" + searchInputText.value;
            }
        },
        error: function (response) {
            $("#searchInput").val("Error: " + response);
        }

    });
}
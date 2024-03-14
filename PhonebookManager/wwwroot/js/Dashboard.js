// <a id="dummyBtn" hidden>Dummy a tag for click functionality</a>
//function quickAddPhoneLine() {
//    var searchInputText = document.getElementById("searchInput").value;
//    var url = "@Url.Action("AddQuickPhoneLine", "Dashboard")/?phoneLine=" + searchInputText;
//    console.log(url);
//    $("#dummyBtn").attr("href", url);
//    $("#dummyBtn")[0].click();
//}

function GotoPhoneUserPage(clicked_id) {
    const button = clicked_id;
    var pNumber = button.getAttribute("phone-number");

    if (pNumber != null && pNumber.length == 10) {
        $.ajax({
            url: '/Dashboard/CheckPhoneNumber/',
            data: { "phoneNumber": pNumber },
            type: "GET",
            success: function (data) {
                if (data == "Exists") {
                    Toastify({
                        text: "Phone number exists",
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
                    window.location.href = location.origin + "/PhoneUser?phoneNumber=" + pNumber;
                }

            },
            error: function (response) { // use error or failure
                console.log("Error " + response)
            }
        });

       // window.location.href = location.origin + "/PhoneUser?phoneNumber=" + pNumber;
    }
    else {
        Toastify({
            text: "Phone number not checked",
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
}

function AddQuickPhoneLine() {
    var searchInputText = document.getElementById("searchInput");
    $.ajax({
        url: '/Dashboard/AddQuickPhoneLine/',
        data: { "phoneLine": searchInputText.value },
        type: "POST",
        success: function (data) {
            if (data === "Is null") {
                Toastify({
                    text: "Invalid phone number",
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
            else if (data === "Exists") {
                Toastify({
                    text: "Phone number exists",
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
                location.reload(true);
                console.log("Success!")
            }
            
        },
        error: function (response) { // use error or failure
            console.log("Error " + response)
        }
    });
}

// SEARCH
//$("#searchInput").focus(function () {
//    if ($(this).val() === "Not found") { // clear the input field
//        $(this).val("");
//    }
//});

// On click and keydown
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
        url: '/Dashboard/SearchPhoneLine/',
        data: { "searchText": searchInputText.value },
        type: "POST",
        success: function (data) {
            if (data === "Not found") {
                //$("#searchInput").val("Not found"); // add a message in the input field
                //$("#searchInput").blur(); // clear the focus
                Toastify({
                    text: "Phone line does not exist",
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
                const createAndAllocateBtn = document.getElementById("createAndAllocate"); // get the create and allocate page button
                createAndAllocateBtn.setAttribute('phone-number', searchInputText.value); // add the search text number into ViewBag.PhoneNumber
            }
            else {
                window.location.href = location.origin + "/Dashboard?searchText=" + searchInputText.value;
            }
        },
        error: function (response) {
            $("#searchInput").val("Error: " + response);
        }

    });
}

// Autocomplete
$(function () {
    $("#searchInput").autocomplete({

        source: function (request, response) { // response is the server response, request is the search term
            $.ajax({
                url: '/Dashboard/SearchPhoneLine/',
                data: { "searchText": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        
                        return item;
                    }));
                    $(window).resize(function () {
                        $(".ui-autocomplete").css('display', 'none');
                    });

                },
                error: function (response) {
                    $("#searchInput").val("Error: " + response);
                },
                //failure: function (response) { // use error or failure
                //    $("#searchInput").val("Failure: " + response);
                //}
            });
        },

        select: function (e, i) {
            $("#phoneLine").val(i.item.val);
            window.location.href = location.origin + "/Dashboard?searchText=" + i.item.label;
            //$(this).autocomplete("close");
        },
        minLength: 1
    });
});


// VIEW
function viewFunction(clicked_id) {
    const button = clicked_id;
    var empName = button.getAttribute("emp-name");
    var empId = button.getAttribute("emp-id");

    var modalEmpName = document.getElementById("mViewEmpName");
    modalEmpName.value = empName; // if it's an input use .value
    //modalEmpName.innerHTML = empName; // if it's a label use .innerHTML

    var modalEmpId = document.getElementById("mViewEmpId");
    modalEmpId.value = empId;
    //modalEmpId.innerHTML = empId;

    $('#viewModal').modal('show');
}

// EDIT
function editFunction(clicked_id) {
    const button = clicked_id;
    var empName = button.getAttribute("emp-name");
    var empId = button.getAttribute("emp-id");

    var modalEmpName = document.getElementById("mEditEmpName");
    modalEmpName.value = empName; // if it's an input use .value
    //modalEmpName.innerHTML = empName; // if it's a label use .innerHTML

    var modalEmpId = document.getElementById("mEditEmpId");
    modalEmpId.value = empId;
    //modalEmpId.innerHTML = empId;

    $('#editModal').modal('show');
}

// Close the edit modal
function closeEditModal() {
    Toastify({
        text: "Not implemented",
        duration: 3000,
        newWindow: true,
        close: true,
        gravity: "bottom", // `top` or `bottom`
        position: "left", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        style: {
            background: "linear-gradient(to right, red, #55B1BB)",
        },
    }).showToast();

    $('#editModal').modal('hide');
}


// DELETE
function deleteFunction(clicked_id) {
    const button = clicked_id;
    var empName = button.getAttribute("emp-name");
    var empId = button.getAttribute("emp-id");

    var modalEmpName = document.getElementById("mDelEmpName");
    modalEmpName.value = empName; // if it's an input use .value
    //modalEmpName.innerHTML = empName; // if it's a label use .innerHTML

    var modalEmpId = document.getElementById("mDelEmpId");
    modalEmpId.value = empId;
    //modalEmpId.innerHTML = empId;

    $('#deleteModal').modal('show');
}
// ajax for delete function
$("body").on("click", "#deleteBtn", function () {
    var modalEmpId = $("#mDelEmpId");

    //var modalEmpName = $("#mEmpName");
    //alert(modalEmpId.val())
    //alert(modalEmpName.val())

    $.ajax({
        type: "POST",
        url: "/Dashboard/ModalDelete",
        data: { 'id': modalEmpId.val() }, // send only id /*, 'name': modalEmpName.val()*/
        dataType: 'json',
        success: function (result) {
            if (result == "invalidData") {

                Toastify({
                    text: "Eroare",
                    duration: 3000,
                    newWindow: true,
                    close: true,
                    gravity: "bottom", // `top` or `bottom`
                    position: "left", // `left`, `center` or `right`
                    stopOnFocus: true, // Prevents dismissing of toast on hover
                    style: {
                        background: "linear-gradient(to right, red, #55B1BB)",
                    },
                }).showToast();

                $('#deleteModal').modal('hide');
            }
            else {
                Toastify({
                    text: " " + modalEmpId.val() + " deleted? Nope.",
                    duration: 3000,
                    newWindow: true,
                    close: true,
                    gravity: "bottom", // `top` or `bottom`
                    position: "left", // `left`, `center` or `right`
                    stopOnFocus: true, // Prevents dismissing of toast on hover
                    style: {
                        background: "linear-gradient(to right, red, #55B1BB)",
                    },
                }).showToast();

                $('#deleteModal').modal('hide');
                //document.location.href = "/" + result;

            }
        },
        error: function (status, error) {
            $("#deleteModal").html("Result: " + status + " " + error)
        }
    });
});

// IMPORT DATA
function ImportFunction(clicked_id) {


    $('#importModal').modal('show');
}
// Close the import modal
//function closeImportModal() {
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

//    $('#importModal').modal('hide');
//}



//document.getElementById("deleteIcon").addEventListener("click", function () {
//    Toastify({
//        text: "Are you sure?",
//        duration: 3000,
//        newWindow: true,
//        close: true,
//        gravity: "bottom", // `top` or `bottom`
//        position: "left", // `left`, `center` or `right`
//        stopOnFocus: true, // Prevents dismissing of toast on hover
//        style: {
//            background: "linear-gradient(to right, #ff0000, #FDE063)",
//        },
//    }).showToast();
//});

// <a id="dummyBtn" hidden>Dummy a tag for click functionality</a>
//function quickAddPhoneLine() {
//    var searchInputText = document.getElementById("searchInput").value;
//    var url = "@Url.Action("AddQuickPhoneLine", "Dashboard")/?phoneLine=" + searchInputText;
//    console.log(url);
//    $("#dummyBtn").attr("href", url);
//    $("#dummyBtn")[0].click();
//}

// On initialize
$(document).ready(function () {
    let viewBag = document.querySelector('[viewbag-id]');
    let vbId = viewBag.getAttribute('viewbag-id');
    var selectedDepartment = document.getElementById("selectDepartment");
    for (let i = 0; i < selectedDepartment.options.length; i++) {
        if (selectedDepartment.options[i].value === vbId) {
            selectedDepartment.selectedIndex = i;
            break;
        }
    }
});
$(document).ready(function () {
    $("#selectDepartment").on("change", function () {
        var selectedDepartment = document.getElementById("selectDepartment");
        var id = selectedDepartment.value;
        $.ajax({
            url: '/Dashboard/Index/',
            data: { "depId": id },
            type: "POST",
            success: function (data) {
               // window.location.href = location.origin + "/Dashboard";
                window.location.href = location.origin + "/Dashboard?depId=" + selectedDepartment.value;

            },
            error: function (response) {
                console.log("error");
            }

        });
    });
});

// Search text length
$(document).ready(function () {
    $("#searchInput").autocomplete({
        source: function (request, response) { // response is the server response, request is the search term
            var count = document.getElementById("counter");
            count.innerHTML = request.term.length;
        },
    });
});
// Create and allocate button
$(document).ready(function () {
    $("#createAndAllocate").on("click", function () {
        var searchInputText = document.getElementById("searchInput");
        $.ajax({
            url: '/Dashboard/CheckNumberToAllocate/',
            data: { "searchText": searchInputText.value },
            type: "POST",
            success: function (data) {
                if (data === "Not found") {
                    window.location.href = location.origin + "/PhoneUser?phoneNumber=" + searchInputText.value;
                    //console.log("success");

                    //const createAndAllocateBtn = document.getElementById("createAndAllocate"); // get the create and allocate page button
                    //createAndAllocateBtn.setAttribute('phone-number', searchInputText.value); // add the search text number into ViewBag.PhoneNumber
                }
                else if (data === "NaN") {
                    Toastify({
                        text: "Invalid number",
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
                    Toastify({
                        text: "Invalid or exists",
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
            },
            error: function (response) {
                console.log("error");
            }

        });
    });
});

/*GotoPhoneUserPage*/
//function GotoPhoneUserPage(clicked_id) {
//    const button = clicked_id;
//    var pNumber = button.getAttribute("phone-number");

//    if (pNumber != null) { //&& pNumber.length == 10
//        $.ajax({
//            url: '/Dashboard/CheckPhoneNumber/',
//            data: { "phoneNumber": pNumber },
//            type: "POST",
//            success: function (data) {
//                if (data == "Exists") {
//                    Toastify({
//                        text: "Phone number exists",
//                        duration: 3000,
//                        newWindow: true,
//                        close: true,
//                        gravity: "bottom", // `top` or `bottom`
//                        position: "left", // `left`, `center` or `right`
//                        stopOnFocus: true, // Prevents dismissing of toast on hover
//                        style: {
//                            background: "linear-gradient(to right, #008A99, #55B1BB)",

//                        },
//                    }).showToast();
//                }
//                else {
//                    window.location.href = location.origin + "/PhoneUser?phoneNumber=" + pNumber;
//                }

//            },
//            error: function (response) { // use error or failure
//                console.log("Error " + response)
//            }
//        });

//       // window.location.href = location.origin + "/PhoneUser?phoneNumber=" + pNumber;
//    }
//    else {
//        Toastify({
//            text: "Phone number not checked",
//            duration: 3000,
//            newWindow: true,
//            close: true,
//            gravity: "bottom", // `top` or `bottom`
//            position: "left", // `left`, `center` or `right`
//            stopOnFocus: true, // Prevents dismissing of toast on hover
//            style: {
//                background: "linear-gradient(to right, #008A99, #55B1BB)",

//            },
//        }).showToast();
//    }
//}

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
                //console.log("Success!")
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
                const createAndAllocateBtn = document.getElementById("createAndAllocate"); // get the create and allocate page button
                createAndAllocateBtn.setAttribute('phone-number', searchInputText.value); // add the search text number into ViewBag.PhoneNumber
            }
            else {
                searchInputText.value = searchInputText.value.replace(/\s+/g, '');
                window.location.href = location.origin + "/Dashboard?searchText=" + searchInputText.value;
            }
        },
        error: function (response) {
            $("#searchInput").val("Error: " + response);
        }

    });
}


// Autocomplete - not used

//$(function () {
//    $("#searchInput").autocomplete({
//        source: function (request, response) { // response is the server response, request is the search term
//            $.ajax({
//                url: '/Dashboard/AutocompleteSearchPhoneLine/',
//                data: { "searchText": request.term },
//                type: "POST",
//                success: function (data) {
//                    //var cont = document.getElementById("counter");
//                    //cont.innerHTML = request.term.length;
//                    response($.map(data, function (item) {
//                        return item;
//                    }));
//                    $(window).resize(function () {
//                        $(".ui-autocomplete").css('display', 'none');
//                    });

//                },
//                error: function (response) {
//                    $("#searchInput").val("Error: " + response);
//                },
//                //failure: function (response) { // use error or failure
//                //    $("#searchInput").val("Failure: " + response);
//                //}
//            });
//        },

//        select: function (e, i) {
//            $("#phoneLine").val(i.item.val);
//            window.location.href = location.origin + "/Dashboard?searchText=" + i.item.label;
//            //$(this).autocomplete("close");
//        },
//        minLength: 1
//    });
//});


// IMPORT DATA


// EDIT
function DashboardEditFunction(clicked_id) {
    const button = clicked_id;
    var lineId = button.getAttribute("line-id");
    var lineOwnerName = button.getAttribute("line-owner-name");
    var lineOwnerBadge = button.getAttribute("line-owner-badge");
    var lineNumber = button.getAttribute("line-number");
    var lineDepartmentName = button.getAttribute("line-department-name");
    var lineDepartmentCode = button.getAttribute("line-department-code");

    var editModalPhoneId = document.getElementById("editModalPhoneId");
    editModalPhoneId.value = lineId;
    var editModalLineOwnerName = document.getElementById("editModalLineOwnerName");
    editModalLineOwnerName.value = lineOwnerName;
    var editModalLineOwnerbadge = document.getElementById("editModalLineOwnerBadge");
    editModalLineOwnerbadge.value = lineOwnerBadge;
    var editModalPhoneLine = document.getElementById("editModalPhoneLine");
    editModalPhoneLine.value = lineNumber;
    var modalDepartmentName = document.getElementById("editModalDepartmentName");
    modalDepartmentName.value = lineDepartmentName;
    var modalDepartmentCode = document.getElementById("editModalDepartmentCode");
    modalDepartmentCode.value = lineDepartmentCode;


    $('#editModal').modal('show');
}

$("body").on("click", "#editBtn", function () {
    var modalUserId = $("#editModalPhoneId");
    var modalLineOwnerName = $("#editModalLineOwnerName");
    var modalLineOwnerBadge = $("#editModalLineOwnerBadge");
    var modalPhoneLine = $("#editModalPhoneLine");
    var modalLineDepName = $("#editModalDepartmentName");
    var modalLineDepCode = $("#editModalDepartmentCode");

    $.ajax({
        type: "POST",
        url: "/Dashboard/Edit",
        data: {
            'id': modalUserId.val(),
            'name': modalLineOwnerName.val(),
            'badgeNo': modalLineOwnerBadge.val(),
            'phonelineNo': modalPhoneLine.val(),
            'depName': modalLineDepName.val(),
            'depCode': modalLineDepCode.val(),
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
                window.location.href = location.origin + "/Dashboard";
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
});


// DELETE
function DashboardDeleteFunction(clicked_id) {
    const button = clicked_id;
    var lineId = button.getAttribute("line-Id");
    var lineNumber = button.getAttribute("line-number");

    var delModalPhoneId = document.getElementById("delModalPhoneId");
    delModalPhoneId.value = lineId;
    var delPhoneNumber = document.getElementById("delPhoneNumber");
    delPhoneNumber.value = lineNumber;

    $('#deleteModal').modal('show');
}

$("body").on("click", "#deleteBtn", function () {
    var modalPhoneLineId = $("#delModalPhoneId");

    $.ajax({
        type: "POST",
        url: "/Dashboard/DeleteConfirmed",
        data: {
            'id': modalPhoneLineId.val(),
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
                window.location.href = location.origin + "/Dashboard";

            }
        },
        error: function (error) {
            console.log(error);
        }
    });
});
function UploadFiles(inputId) {
    var formData = new FormData();
    formData.append('file', $('#myfile')[0].files[0]); // myFile is the input type="file" contr


    $.ajax({
        url: "Dashboard/UploadCsv",
        type: 'POST',
        data: formData,
        datatype: 'json',
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
            //download result
            const fileObject = JSON.parse(result);
            console.log(result);

            const blob = base64toBlob(fileObject.ByteContent, fileObject.ContentType);
            const link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = fileObject.FileName;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

        },
        error: function () {
        },
        complete: function (status) {
          //  console.log(status);

        }
    });
}
function base64toBlob(base64, contentType) {
    const byteCharacters = atob(base64);
    const byteArray = new Uint8Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteArray[i] = byteCharacters.charCodeAt(i);
    }
    return new Blob([byteArray], { type: contentType });
}
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

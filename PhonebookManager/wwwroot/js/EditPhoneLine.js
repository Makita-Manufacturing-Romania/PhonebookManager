// On initialize
$(document).ready(function () {
    
});


// Create request of ownership
function OpenModalChange(clicked_id) {
    document.getElementById("ownershipBtn").innerHTML = "Change of ownership";

    $('#changeModal').modal('show');
}

function ChangeConfirm() {
    var modalInfoText = document.getElementById("infoText");
    var modalTitleOne = document.getElementById("changeModalLabelOne");
    var modalTitleTwo = document.getElementById("changeModalLabelTwo");

    var modalRequestText = document.getElementById("modalRequestBox");
    var modalConfirmationBox = document.getElementById("modalConfirmationBox");

    modalInfoText.className = "d-none";
    modalTitleOne.className = "d-none";
    modalTitleTwo.className = "d-block";

    modalRequestText.className = "d-none";

    modalConfirmationBox.className = "d-flex";
}

let newUserId;
$(document).ready(function () {
    // Autocomplete for new owner
    $(function () {
        $("#searchNewLineOwnerInput").autocomplete({
            source: function (request, response) { // response is the server response, request is the search term
                document.getElementById("spinnerTwo").style.display = "block"; // start the spinner here
                $.ajax({
                    url: '/Dashboard/AutocompleteSearchUsers/',
                    data: { "searchText": request.term },
                    type: "POST",
                    success: function (data) {
                        document.getElementById("spinnerTwo").style.display = "none";
                        response($.map(data, function (item) {
                            return item;
                        }));

                        $(window).resize(function () {
                            $(".ui-autocomplete").css('display', 'none');
                        });

                    },
                    error: function (response) {
                        $("#searchNewLineOwnerInput").val("Error: " + response);
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

    $("#resetNewLineOwnerInput").on("click", function () {
        document.getElementById("searchNewLineOwnerInput").value = "";
        document.getElementById("resetNewLineOwnerInput").style.display = "none";
        newUserId = undefined;
    });

    $('#lineOwnerId').change(function () {
        var userId = $("#lineOwnerId");
        document.getElementById("spinnerTwo").style.display = "block";

        $.ajax({
            url: '/Dashboard/FindUser/',
            data: { "searchText": userId.val() },
            type: "GET",
            success: function (data) {
                document.getElementById("spinnerTwo").style.display = "none";
                document.getElementById("resetNewLineOwnerInput").style.display = "block";

                var result = JSON.parse(data);
                newUserId = result.EmployeeID;
                //console.log(newUserId);
            },
            error: function (response) {
            },
            failure: function (response) {
            }
        });
    });
});

function Cancel() {
    document.getElementById("searchNewLineOwnerInput").value = "";
    document.getElementById("resetNewLineOwnerInput").style.display = "none";
    newUserId = undefined;
    location.reload();
    //$('#changeModal').modal('hide');
}

function ConfirmRequest() {
    let findViewBag = document.querySelector('[phone-number]');
    let vbNumber = findViewBag.getAttribute('phone-number');

    if (newUserId == undefined) {
        Toastify({
            text: "Select a new owner",
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
            url: '/Dashboard/ChangeOwnerRequest/',
            data: {
                "phoneNumber": vbNumber,
                "userId": newUserId
            },
            type: "GET",
            success: function (data) {
                ChangeConfirm();
            },
            error: function (response) {
            },
            failure: function (response) {
            }
        });
    }
}


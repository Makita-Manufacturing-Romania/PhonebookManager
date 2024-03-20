
// INDEX
function MoveLine() {
    const firstList = document.getElementById('firstListSelect');
    const secondList = document.getElementById('secondListSelect');

    // Move all selected options from firstList to secondList
    const selectedOptions = Array.from(firstList.selectedOptions);
    selectedOptions.forEach((option) => {
        secondList.appendChild(option);
    });
}

function RemoveLine() {
    const firstList = document.getElementById('firstListSelect');
    const secondList = document.getElementById('secondListSelect');

    // Move all selected options from secondList back to firstList
    const selectedOptions = Array.from(secondList.selectedOptions);
    selectedOptions.forEach((option) => {
        firstList.appendChild(option);
    });
}

function AddDepartment() {
    var departmentCode = $("#depCode");
    var departmentName = $("#depName");
    var selectManager = $("#selectManager");
    var selectResponsible = $("#selectResponsible");

    const secondList = document.getElementById('secondListSelect');
    const selectedValues = Array.from(secondList.selectedOptions).map(option => option.value);

    $.ajax({
        type: 'POST',
        url: '/Department/Create',
        data: {
            selectedOptions: selectedValues,
            depCode: departmentCode.val(),
            depName: departmentName.val(),
            depManager: selectManager.val(),
            depResponsible: selectResponsible.val()
        },
        success: function (response) {
            window.location.href = location.origin + "/Department";
            //console.log('Data sent successfully:', response);
        },
        error: function (error) {
            console.error('Error sending data:', error);
        }
    });
}


// DELETE
function DepartmentDeleteFunction(clicked_id) {
    const button = clicked_id;
    var depId = button.getAttribute("dep-id");
    var depName = button.getAttribute("dep-name");

    var delModalDepId = document.getElementById("delModalDepId");
    delModalDepId.value = depId;
    var delModalDepName = document.getElementById("delModalDepName");
    delModalDepName.value = depName;

    $('#deleteModal').modal('show');
}

$("body").on("click", "#deleteBtn", function () {
    var delModalDepId = $("#delModalDepId");

    $.ajax({
        type: "POST",
        url: "/Department/DeleteConfirmed",
        data: {
            'id': delModalDepId.val(),
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
                window.location.href = location.origin + "/Department";

            }
        },
        error: function (error) {
            console.log(error);
        }
    });
});

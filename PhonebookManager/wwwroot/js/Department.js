
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




// EDIT
$(document).ready(function () {

    let elementManager = document.querySelector('[dep-manager]');
    let depManager = elementManager.getAttribute('dep-manager');

    let elementResponsible = document.querySelector('[dep-responsible]');
    let depResponsible = elementResponsible.getAttribute('dep-responsible');


    var editModalSelectManager = document.getElementById("editModalSelectManager");
    for (let i = 0; i < editModalSelectManager.options.length; i++) {
        if (editModalSelectManager.options[i].text === depManager) {
            editModalSelectManager.selectedIndex = i;
            break;
        }
    }

    var editModalSelectResponsible = document.getElementById("editModalSelectResponsible");
    for (let i = 0; i < editModalSelectResponsible.options.length; i++) {
        if (editModalSelectResponsible.options[i].text === depResponsible) {
            editModalSelectResponsible.selectedIndex = i;
            break;
        }
    }
    $('#editFirstListSelect option').each(function () {
        var optionValue = $(this).val();
        if ($('#editSecondListSelect option[value="' + optionValue + '"]').length) {
            $(this).remove();
        }
    });


});
function EditMoveLine() {
    const firstList = document.getElementById('editFirstListSelect');
    const secondList = document.getElementById('editSecondListSelect');

    // Move all selected options from firstList to secondList
    const selectedOptions = Array.from(firstList.selectedOptions);
    selectedOptions.forEach((option) => {
        if (!secondList.contains(option)) {
            secondList.appendChild(option);
        }
    });
}

function EditRemoveLine() {
    const firstList = document.getElementById('editFirstListSelect');
    const secondList = document.getElementById('editSecondListSelect');

    // Move all selected options from secondList back to firstList
    const selectedOptions = Array.from(secondList.selectedOptions);
    selectedOptions.forEach((option) => {
        firstList.appendChild(option);
    });
}
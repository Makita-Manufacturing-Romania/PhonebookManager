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
    //const departmentCode = document.getElementById('depCode');
    //const departmentName = document.getElementById('depName');
    //const selectManager = document.getElementById('selectManager');
    //const selectResponsible = document.getElementById('selectResponsible');

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
            
            console.log('Data sent successfully:', response);
        },
        error: function (error) {
            console.error('Error sending data:', error);
        }
    });
}



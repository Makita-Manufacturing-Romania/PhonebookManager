//document.getElementById('yourInputId').addEventListener('input', function (e) {
//    // Handle input changes here
//    console.log("Character typed:", e.target.value);
//    // You can perform further actions based on the input value
//});

$(document).ready(function () {
    $('#badgeNoInput').on('input', function () {
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
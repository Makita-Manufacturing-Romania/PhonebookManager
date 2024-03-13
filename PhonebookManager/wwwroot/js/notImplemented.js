﻿const words = ['Nope', 'Not happening', 'No way', 'Stop it', 'Not implemented', 'Nah'];

function notImplemented() {
    const randomIndex = Math.floor(Math.random() * words.length);
    const randomWord = words[randomIndex];
    Toastify({
        text: randomWord,
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
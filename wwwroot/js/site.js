// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const logarBut = document.querySelector('.logarBut')
const cadasBut = document.querySelector('.cadasBut')
const formbx = document.querySelector('.formbx')

cadasBut.onclick = function () {
    formbx.classList.add('active')
    body.classList.add('active')
}
logarBut.onclick = function () {
    formbx.classList.remove('active')
}
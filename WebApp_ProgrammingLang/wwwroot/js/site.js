// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $.ajaxSetup({ cache: true });
    $('#modalLink').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#modalContent').html(data);
            $('#modalDialog').modal('show');
        })
    })
})
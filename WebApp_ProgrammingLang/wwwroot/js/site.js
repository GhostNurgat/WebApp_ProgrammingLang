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

$(function () {
    $.ajaxSetup({ cache: true });
    $('#infoLink').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#profile').html(data);
            $('#infoLink').addClass = ".active";
        })
    })
})

$(function () {
    $.ajaxSetup({ cache: true });
    $('#worksLink').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#profile').html(data);
        })
    })
})

$(function () {
    $.ajaxSetup({ cache: true });
    $('#editInfo').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#cardBody').html(data);
        })
    })
})

$(function () {
    $.ajaxSetup({ cache: true });
    $('#editImage').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#cardBody').html(data);
        })
    })
})

$(function () {
    $('#deleteLink').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#modContent').html(data);
            $('#modDeleteDialog').modal('show');
        })
    })
})

$(function () {
    $.ajaxSetup({ cache: true });
    $('#taskList').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#page').html(data);
        })
    })
})
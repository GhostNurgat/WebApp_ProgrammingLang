// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $.ajaxSetup({ cache: false })
    let placeholderElement = $('#placeholder');
    $('#modalLink').click(function (e) {
        e.preventDefault();
        $.get(this.href).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
            bindForm(this);
        });
    });
});

$(function bindForm() {
    let placeholderElement = $('#placeholder');
    placeholderElement.on('sumbit', '[data-save="modal"]', function (e) {
        e.preventDefault();

        var actionUrl = $(this.form).attr('action');
        var dataToSend = $(this.form).serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            let newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
            }
        });
    });
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
        });
    });
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
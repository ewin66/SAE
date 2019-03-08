/// <reference path="../lib/require/require.min.js" />
/// <reference path="../lib/sweetalert2/sweetalert2.all.js" />
define(["sweetalert", "common"], function (Swal, common) {

    const popup = {};

    const modals = function (type, content, title, cancelButton) {
        return Swal.fire({
            title: title,
            text: content,
            type: type,
            showCancelButton: cancelButton || false
            //allowOutsideClick: false
        });
    }
    //popup info message
    popup.info = modals.bind(undefined, "info");
    //popup warn message
    popup.warn = modals.bind(undefined, "warn");
    //popup error message
    popup.error = modals.bind(undefined, "error");
    //popup success message
    popup.success = modals.bind(undefined, "success");
    //popup question message
    popup.question = modals.bind(undefined, "question");
    //popup custoom message
    popup.custom = function () {
        throw "not imp";
    }

    return popup;
})
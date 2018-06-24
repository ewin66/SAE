/// <reference path="../../lib/requirejs/require.js" />
/// <reference path="method.js" />
/// <reference path="../../lib/knockout/build/knockout-raw.js" />
/// <reference path="../../lib/jquery/dist/jquery.min.js" />
define(["jquery", "jquery-validation"], function ($) {
    require(["jquery-validation-localization-zh", "jquery-validation-unobtrusive"]);
    //$(function () {
    //    $(".sae-form").each(function (index,item) {
    //        $(item).validate({
    //            submitHandler: function (form) {
    //                if ($(form).prop("ajaxRequest")) {
    //                    $(form).ajaxSubmit();
    //                } else {
    //                    $(form).Submit();
    //                }
    //            }
    //        });
    //    });
    //});
})
/// <reference path="../../lib/text/text.js" />
/// <reference path="../../lib/requirejs/require.js" />
/// <reference path="../../lib/jquery/dist/jquery.min.js" />
require.config({
    baseUrl: '/lib/',
    urlArgs: 'v=' + (new Date()).getTime(),
    paths: {
        //lib
        "text": "text/text",
        "css": "require-css/css.min",
        "jquery": "jquery/dist/jquery.min",
        "bootstrap": "bootstrap/dist/js/bootstrap.min",
        "adminlte": "admin-lte/dist/js/adminlte.min",
        "ko": "knockout/dist/knockout",
        "jquery-validation": "jquery-validation/dist/jquery.validate.min",
        "jquery-validation-unobtrusive":"jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min",
        "jquery-validation-localization-zh": "jquery-validation/src/localization/messages_zh",
        "jquery-unobtrusive-ajax": "jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.min",
        //local
        "base": "/content/js/base",
        "method": "/content/js/method",
        "template": "/content/js/template",
        "vm": "/content/js/vm",
        "bind": "/content/js/bind",
        "convert": "/content/js/convert",
        "route": "/content/js/route",
        "validate":"/content/js/validate",
        //模板
        "sae-table": "/content/template/sae-table.html",
        "sae-paging": "/content/template/sae-paging.html",
        "sae-form-text": "/content/template/sae-form-text.html"
    },
    shim: {
        "bootstrap": {
            deps: ["jquery",
                   "css!Ionicons/css/ionicons.min",
                   "css!font-awesome/web-fonts-with-css/css/fontawesome-all.min",
                   "css!https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic",
                   "css!/lib/bootstrap/dist/css/bootstrap.min",
            ]
        },
        "adminlte": {
            deps: ["jquery",
                   "css!admin-lte/dist/css/adminlte.min",
                   "css!admin-lte/dist/css/skins/skin-blue.min.css"
            ]
        },
        "vm": {
            deps:["template","convert","route"]
        },
        "validate": {
            deps: ["jquery-unobtrusive-ajax"]
        },
        "jquery-unobtrusive-ajax": {
            deps:["jquery"]
        }
    }
});
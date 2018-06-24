/// <reference path="common.js" />
define(["method", "jquery"], function (method, $) {

    if (!method.hasOwnProperty("convert")) {
        method.convert = {};
    }
    method.convert.status = function (val) {
        
        val = val == null ? -1 : parseInt(val);
        var font;
        switch (val) {
            case 1: font = "fa-check bg-green"; break;
            case 2: font = "fa-ban bg-yellow"; break;
            default: font = "fa-question bg-red"; break;
        }
        return '<i class="fa {0}"></i>'.format(font);
    }
    method.convert.date = function (val) {
        return new Date(val).format("yyyy-MM-dd hh:mm:ss");
    }
    method.convert.accountType = function (val) {
        val = val == null ? -1 : parseInt(val);
        var font;
        switch (val) {
            case 0: font = "本地账号"; break;
            case 1: font = "集中式认证"; break;
            default: font = "未知?"; break;
        }
        return font;
    }
})

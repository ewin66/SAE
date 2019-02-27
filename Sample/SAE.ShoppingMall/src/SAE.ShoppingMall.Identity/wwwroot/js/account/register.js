/// <reference path="../../lib/jquery/jquery.min.js" />
/// <reference path="../../lib/require/require.min.js" />
/// <reference path="../httpclient.js" />
define(["jquery", "validate", "httpClient", "popup"], function ($, validate, httpClient, popup) {
    window.requestSuccess = function (data) {
        httpClient.defaultSuccess(data, function (body) {
            popup.success("注册成功").then(function () {
                window.location = "/";
            });
        });
    }
});
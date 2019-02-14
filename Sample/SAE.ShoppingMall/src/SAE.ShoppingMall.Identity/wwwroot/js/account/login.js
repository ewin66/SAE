/// <reference path="../../lib/jquery/jquery.min.js" />
/// <reference path="../../lib/require/require.min.js" />
/// <reference path="../httpclient.js" />
define(["jquery", "validate", "httpClient", "popup","common"], function ($, validate, httpClient, popup,common) {
    window.requestSuccess = function (data) {
        httpClient.defaultSuccess(data, function (body) {
            debugger;
            var url = common.getQueryString("returnurl") || "/";
            location.href = url;
        });
    }
});
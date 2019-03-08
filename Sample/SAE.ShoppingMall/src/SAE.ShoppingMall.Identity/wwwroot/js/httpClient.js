/// <reference path="../lib/require/require.min.js" />
/// <reference path="../lib/jquery/jquery.min.js" />
define(["jquery", "route", "popup", "common"], function ($, route, popup, common) {

    //http client object
    const httpClient = function (method, url, data) {

        const self = this;
        //request options
        this.option = {
            type: method || "get",
            url: url,
            data: data,
            success: function (data) {
                httpClient.defaultSuccess.call(self, data, self.success);
            },
            error: function (e) {
                httpClient.defaultError.call(self, e, self.error);
            }
        };
        //start request
        this.request = function () {
            $.ajax(self.option);
        }
    };
    //default success callback
    httpClient.defaultSuccess = function (data, success) {
        if (data && data.statusCode) {
            if (data.statusCode == 0) {
                success(data.body);
            } else {
                httpClient.defaultError.call(this, data, this.error);
            }
        } else {
            success(data);
        }
    };
    //default error callback
    httpClient.defaultError = function (e, error) {

        if (error && common.isFunction(error)) {
            const bl = error(e);
            if (bl == null || bl) {
                return;
            }
        }
        debugger;
        if (e && !e.hasOwnProperty("status") && e.hasOwnProperty("statusCode")) {
            popup.error(e.statusMsg)
        } else if (e.hasOwnProperty(status)) {
            switch (e.status) {
                case 401:
                    window.location.href = route.login;
                    break;
                case 403:
                    popup.error("您没有权限执行该操作");
                    break;
                default:
                    popup.error("网络异常，请稍后访问");
                    break;
            }
        }
    }

    return httpClient;
});
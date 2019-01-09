/// <reference path="../lib/require/require.min.js" />
/// <reference path="../lib/jquery/jquery.min.js" />
define(["jquery","route"], function ($,route) {

    //http client object
    const httpClient = function (method, url, data) {

        const self = this;
        //request options
        this.option = {
            type: method || "get",
            url: url,
            data: data,
            success: function (data) {
                httpClient.defaultSuccess(data, self.success);
            },
            error: function (e) {
                httpClient.defaultError(e, self.error);
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
                httpClient.defaultError(data);
            }
        } else {
            success(data);
        }
    };
    //default error callback
    httpClient.defaultError = function (e, error) {

        if (error && error(e)) {
            return;
        }

        if (e && e.statusCode) {
            console.error(e.statusMessages)
        } else if (e.status) {
            switch (e.status) {
                case 401:
                    window.location.href = route.login;
                    break;
                case 403:
                    alert("您没有权限执行该操作");
                    break;
                default:
                    alert("网络异常，请稍后访问");
                    break;
            }
        }
    }

    return httpClient;
});
/// <reference path="../../lib/requirejs/require.js" />
/// <reference path="method.js" />
/// <reference path="../../lib/knockout/build/knockout-raw.js" />
/// <reference path="../../lib/jquery/dist/jquery.min.js" />
define(["jquery", "method"], function ($, method) {
    var root = "http://api.sae.com:11001";

    method.route = {};

    var setRoute = function (action,url) {
        var temp;
        if (url.startsWith("http")) {
            temp = url;
        } else {
            temp = root + url;
        }
        method.route[action] = temp;
    };
    //----------------app-------------------
    //paging
    setRoute("appPaging", "/api/app/paging");
    setRoute("appAdd", "/api/app");

    return setRoute;
});
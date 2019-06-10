/// <reference path="../lib/require/require.js" />
(function (window) {
    //const version = new Date().getTime().toString();
    const version = "1";
    const path = "/dist";
    require.config({
        urlArgs: "v=" + version,
        baseUrl: "/lib",
        waitSeconds: 0,
        paths: {
            "css": "require/plugins/css",
            "text": "require/plugins/text",
            "json": "require/plugins/json",
            "component": path + "/component"
        }
    });
    require([path + "/js/config.js"], function (config) {
        requirejs.config(config);
        const trimReg = /(\/*)$/;
        const url = window.location.pathname.toLocaleLowerCase().replace(trimReg, "");
        require([path + "/js" + url + ".js"]);
    });
})(window)
/// <reference path="../lib/require/require.min.js" />
(function () {
    require.config({
        urlArgs: "v=" + new Date().getDate(),
        baseUrl: "/lib",
        paths: {
            "jquery": "jquery/jquery",
            "react": "react/umd/react.production.min",
            "react-dom": "react/umd/react-dom.production.min",
            "route": "/js/route",
        },
        bundles:{}
    });
    
    require(["route"], function (route) {
        const url = route.search();
        require(["/js" + url + ".js"]);
    });
}())
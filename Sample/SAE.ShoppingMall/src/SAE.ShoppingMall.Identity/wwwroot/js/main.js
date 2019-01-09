/// <reference path="../lib/require/require.min.js" />
(function () {
    const version = new Date().getTime().toString();
    require.config({
        urlArgs: "v=" + version,
        baseUrl: "/lib",
        paths: {
            //require pulgin
            "css": "require/css",
            "text": "require/text",
            "json": "require/json",
            //jquery
            "jquery": "jquery/jquery",
            //react
            "react": "react/umd/react.development",
            "react-dom": "react/umd/react-dom.development",
            //adminlte
            "adminlte": "admin-lte/js/adminlte",
            "bootstrap": "bootstrap/js/bootstrap",
            //local
            "route": "/js/route",
            "template": "/js/template",
            "common": "/js/common",
            "httpClient": "/js/httpClient",
        },
        shim: {
            "adminlte": {
                deps: ["jquery",
                    "bootstrap",
                    "css!admin-lte/css/AdminLTE",
                    "css!admin-lte/css/skins/skin-blue",
                    "css!ionicons/css/ionicons",
                    "css!fontawesome/css/font-awesome"
                ]
            },
            "bootstrap": {
                deps: ["jquery","css!/lib/bootstrap/css/bootstrap"]
            }
        }
    });
    
    require(["route", "adminlte"], function (route) {
        const url = route.search();
        require(["/js" + url + ".js"]);
    });
}())
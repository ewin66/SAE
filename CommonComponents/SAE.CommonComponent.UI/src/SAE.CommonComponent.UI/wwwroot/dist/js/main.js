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
      "template": path + "/js/template",
      "lib": "/lib",
      "react": "react/umd/react.development",
      "react-dom": "react/umd/react-dom.development",
      "jquery": "jquery/jquery",
      "adminlte": "admin-lte/js/adminlte",
      "bootstrap": "bootstrap/js/bootstrap.bundle",
      "templateData": "/component/all",
      "layer": "/js/layerExtend"
    },
    shim: {
      "adminlte": {
        deps: ["jquery", "bootstrap", "css!admin-lte/css/AdminLTE", "css!admin-lte/css/skins/skin-blue"]
      },
      "bootstrap": {
        deps: ["jquery", "css!/lib/bootstrap/css/bootstrap"]
      }
    }
  });

  require(["adminlte"], function (route) {
    const trimReg = /(\/*)$/;
    const url = window.location.pathname.toLocaleLowerCase().replace(trimReg, "");

    require([path + "/js" + url + ".js"]);
  });
})(window);
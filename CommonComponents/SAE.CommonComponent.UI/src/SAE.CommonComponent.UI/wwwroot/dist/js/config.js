define({
  paths: {
    "react": "react/umd/react.development",
    "react-dom": "react/umd/react-dom.development",
    "adminlte": "admin-lte/js/adminlte.min",
    "bootstrap": "bootstrap/js/bootstrap.bundle.min",
    "ionicons": "ionicons/index",
    "jquery": "jquery/jquery.min",
    "layer": "layer/layer.adapter",
    "select2": "select2/js/select2.full.min"
  },
  shim: {
    layer: {
      deps: ["jquery"],
      load: function (a, b, c, d) {
        debugger;
      }
    },
    adminlte: ["css!admin-lte/css/AdminLTE.min", "css!admin-lte/css/skins/_all-skins.min", "bootstrap", "jquery", "ionicons"],
    bootstrap: ["css!/lib/bootstrap/css/bootstrap.min", "jquery", "ionicons"],
    select2: ["css!/lib/select2/css/select2.min", "jquery"],
    ionicons: ["css!/lib/ionicons/css/ionicons.min"]
  }
});
define(function () {
  const config = {
    paths: {
      "react": "react/umd/react.development",
      "react-dom": "react/umd/react-dom.development",
      "adminlte": "admin-lte/js/adminlte.min",
      "bootstrap": "bootstrap/js/bootstrap.bundle.min",
      "ionicons": "ionicons/css/ionicons.min",
      "font-awesome": "font-awesome/css/all.min",
      "jquery": "jquery/jquery.min",
      "layer": "layer/layer.adapter",
      "select2": "select2/js/select2.full.min",
      "bootstrap-switch": "bootstrap-switch/js/bootstrap-switch.min"
    },
    shim: {
      layer: ["jquery"],
      adminlte: ["css!admin-lte/css/AdminLTE.min", "css!admin-lte/css/skins/_all-skins.min", "bootstrap", "jquery", "css!font-awesome"],
      bootstrap: ["css!/lib/bootstrap/css/bootstrap.min", "jquery", "css!font-awesome"],
      select2: ["css!/lib/select2/css/select2.min", "jquery"],
      "bootstrap-switch": ["css!/lib/bootstrap-switch/css/bootstrap3/bootstrap-switch.min"]
    }
  };
  requirejs.config(config);
});
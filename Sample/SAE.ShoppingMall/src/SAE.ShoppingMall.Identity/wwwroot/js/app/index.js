/// <reference path="../../lib/jquery/jquery.min.js" />
/// <reference path="../../lib/require/require.min.js" />
/// <reference path="../../lib/react/umd/react.development.js" />
/// <reference path="../../lib/react/umd/react-dom.development.js" />
/// <reference path="../template.js" />
define(["react","react-dom", "template"], function (React,ReactDOM, parts) {
    ReactDOM.render(new parts.paging(),document.getElementById("page_div"));
});
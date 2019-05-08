/// <reference path="../lib/require/require.js" />
/// <reference path="../lib/layer/layer.js" />
define(["lib/layer/layer"], function (layer) {
    let url = require.toUrl("lib/layer/layer");

    url = url.substring(0, url.indexOf("?") - "layer".length)
    layer.config({
        path: url
    });
    return layer;
})
define(["package/jquery"], function () {
  const path = "lib/layer/layer";

  const layer = require(path);

  let url = require.toUrl(path);

  url = url.substring(0, url.indexOf("?") - "layer".length);
  layer.config({
    path: url
  });
  return layer;
});
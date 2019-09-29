/// <reference path="../../lib/jquery/jquery.js" />
define(["jquery", "react", "react-dom", "component/container/form", "component/listbox/simple", "layer", "adminlte"], function ($, React, ReactDOM, Form, Simple, layer) {
  $("#btn_dependencies").click(function () {
    const index = $(this).nextAll("input").length;
    let html = '<br/><input type="text" class="form-control required" name="dependencies[' + index + ']" maxlength="256" placeholder="请输入引用名称">';
    $(this).parent().append(html);
  });
});
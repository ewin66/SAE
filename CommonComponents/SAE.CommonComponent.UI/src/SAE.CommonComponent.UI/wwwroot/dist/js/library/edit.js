/// <reference path="../../lib/jquery/jquery.js" />
define(["jquery", "react", "react-dom", "component/container/form", "component/listbox/simple", "layer", "adminlte"], function ($, React, ReactDOM, Form, Simple, layer) {
  const dependencies = eval('(' + $("#Dependencies").val() + ')');
  $("#btn_dependencies").click(function () {
    const index = $(this).nextAll().length;
    let html = '<br/><input type="text" class="form-control required" name="dependencies[' + index + ']" maxlength="256" placeholder="请输入引用名称">';
    $(this).parent().append(html);
  });
  debugger;
  dependencies.forEach(val => {
    $("#btn_dependencies").click();
    $("#btn_dependencies").nextAll().last().val(val);
  });
});
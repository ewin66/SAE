define(["jquery"], function () {
  const parent = window.parent;

  const close = function () {
    parent.layer.closeAll();
  };

  $(".btn-success").click(function () {
    $(":checked").each(function () {
      let id = $(this).parent().siblings().last().text();
      parent.addComponent(id);
    });
    close();
  });
  $(".btn-warning").click(function () {
    close();
  });
});
/// <reference path="../../lib/requirejs/require.js" />
/// <reference path="../../lib/knockout/dist/knockout.js" />
/// <reference path="../../lib/jquery/dist/jquery.min.js" />
require(["jquery", "ko", "vm", "method", "require","base"], function ($, ko, vm, method, require) {
    vm.paging = method.createPaging(method.route.appPaging, true);
    vm.app = {
        keys: ["name", "appId", "appSecret", "signin", "signout", "createTime", "status"],
        converts: {
            "status": method.convert.status,
            "createTime": method.convert.date
        },
        heads: ["姓名", "应用标识", "应用秘钥", "登录地址", "退出地址", "创建时间", "状态"],
        event: {
            onEdit: function (item) {
                vm.paging.pullData(vm.paging);
            },
            onRemove: function (item) {
                vm.paging.pullData(vm.paging);
            },
            onSearch: function () {
                vm.paging.index(1);
                var searchValue = vm.app.searchValue.peek();
                method.setRequestQueryString("name", searchValue);
                vm.paging.pullData(vm.paging);
            },
            onAdd: function () {
                debugger;
                window.open("/app/add");
            }
        },
        searchValue: ko.observable(method.getRequestQueryString("name"))
    }
    require(["bind"]);
});
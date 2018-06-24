/// <reference path="../../lib/knockout/dist/knockout.js" />
define(["ko", "method"], function (ko, method) {
    ko.mapping = {
        formJs: function (o) {
            if (o == null) {
                return ko.observable();
            } else if (ko.isObservable(o)) {
                return o;
            } else if (common.isArray(o)) {
                return ko.observableArray(o);
            } else if (common.isObject(o)) {
                for (var key in o) {
                    o[key] = this.formJs(o[key]);
                }
                return o;
            } else {
                return ko.observable(o);
            }
        },
        toJs: function (o) {

        }
    };

    var viewModel = {};


    viewModel.method = method;
    //创建一个分页对象
    method.createPaging = function (url,init) {
        return {
            items: ko.observableArray([]),//分页数据
            index: ko.observable(0),//分页索引
            size: ko.observable(0),//分页大小
            count: ko.observable(0),//总页数
            url: url,
            init: init != null ? init : false
        }
    };
    //配置一个默认的分页请求
    method.config.paging.request = function (paging) {
        method.setRequestQueryString("index", paging.index.peek());
        var queryString = window.location.search;
        method.getRequest(method.joinUrl(paging.url, queryString), null, function (data) {
            paging.items(data.items);
            paging.index(data.index);
            paging.size(data.size);
            paging.count(data.count);
        });
    }

    return viewModel;
});
/// <reference path="../../lib/knockout/dist/knockout.js" />
define(["ko", "method", "template", "convert"], function (ko, method) {
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
    return viewModel;
});
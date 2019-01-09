/// <reference path="../lib/require/require.min.js" />
define(function () {
    let route = new function () {
        let self = this;
        this.store = {};
        this.add = function (key, val) {
            self.store[key] = val;
        };
        this.search = function (key) {
            key = key || window.location.pathname;
            if (self.store.hasOwnProperty(key)) {
                key = self.store[key];
            }
            return key;
        };
    };
    
    route.add("/", "/home/index");
    route.add("/app", "/app/index");

    return route;
});
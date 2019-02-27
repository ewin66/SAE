/// <reference path="../lib/require/require.min.js" />
define(function () {
    let route = new function () {
        let self = this;
        this.store = {};
        this.add = function (key, val) {
            self.store[key.toLocaleLowerCase()] = val || key;
        };
        this.search = function (key) {
            key = (key || window.location.pathname).toLocaleLowerCase();
            if (self.store.hasOwnProperty(key)) {
                key = self.store[key];
            }
            return key;
        };
    };
    
    route.add("/", "/home/index");
    route.add("/app", "/app/index");
    route.add("/user", "/user/index");
    route.appEdit = "/app/edit?id={0}";
    route.userEdit = "/user/edit?id={0}";
    return route;
});
/// <reference path="../../../lib/jquery/dist/jquery.min.js" />
/// <reference path="../../../lib/requirejs/require.js" />
/// <reference path="../../../lib/knockout/dist/knockout.js" />

require(["jquery", "ko", "vm","method","require", "base"], function ($, ko, vm, method,require) {
    
    var fromCtor = function (name, displayName, value, icon, attr) {
        return {
            name: name,
            displayName: displayName,
            value: value,
            icon: icon,
            attr: attr
        };
    };
    
    vm.app = {
        id: fromCtor("appId", null, method.getGuid(), 'fa-key', { required: true,readonly:"readonly"}),
        secret: fromCtor("appSecret", null, null, 'fa-user-secret',{ required: true }),
        name: fromCtor("name", null, null, null, { required: true }),
        signin: fromCtor("signin", null, null, 'fa-link', { required: true }),
        signout: fromCtor("signout", null, null, 'fa-link', { required: true }),
        url: method.route.appAdd
    };
    debugger;
    require(["bind","validate"]);
});
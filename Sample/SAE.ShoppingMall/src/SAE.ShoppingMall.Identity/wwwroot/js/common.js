/// <reference path="../lib/require/require.min.js" />
define(function () {
    const common = {};
    // format date example: date.format("yyyy-MM-dd");
    Date.prototype.format = function (fmt) { //author: meizz 
        var o = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "h+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }

    if (!String.prototype.format) {
        //format string
        String.prototype.format = function (args) {
            var result = this;
            if (arguments.length < 1) {
                return result;
            }

            var data = arguments;

            if (arguments.length == 1 && typeof (args) == "object") {
                data = args;
            }
            for (var key in data) {
                var value = data[key];
                if (undefined != value) {
                    result = result.replace("{" + key + "}", value);
                }
            }
            return result;
        }
    }
    //http https protocol regexp
    const protocolRegExp = /^https?:\/\//;

    //'obj' is string ?
    common.isString = function (obj) {
        return typeof (obj) == 'string';
    }
    //'obj' is Array ?
    common.isArray = function (obj) {
        return Array.isArray(obj);
    }
    //'obj' is function ?
    common.isFunction = function (obj) {
        return typeof (obj) == 'function';
    }
    //'obj' is object ?
    common.isObject = function (obj) {
        return typeof (obj) == "object";
    }

    //change browser address bar url
    common.changeUrl = function (url) {
        if (history.replaceState) {
            if (url) {
                if (protocolRegExp.test(url)) {
                    if (url.indexOf("/") != -1) {
                        url = url.substr(url.indexOf("/"));
                    } else {
                        console.error("Can only change 'pathname' and 'search'");
                    }
                }
                if (url.indexOf("?") != 0 && url.indexOf("/") != 0) {
                    if (window.location.search.indexOf("?") == 0) {
                        url = window.location.search + "&" + url;
                    } else {
                        url = "?" + url;
                    }
                }
                history.pushState(null, null, url);
            } else {
                console.warn("'url' is null");
            }
        } else {
            console.error("Browser does not suppot 'history.replaceState'");
        }
    }
    //get request parameter
    common.getQueryString = function (name) {
        let search = window.location.search;
        if (name && search && search.length > 0) {
            name = name.toLowerCase();
            let requestParameterRegExp = RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = search.substr(1).match(requestParameterRegExp);
            if (r != null) return unescape(r[2]);
        }
        return null;
    }
    //Change request parameter 
    common.setQueryString = function (name, value) {
        if (name) {
            let search = window.location.search.toLocaleLowerCase();
            name = name.toLocaleLowerCase();
            let val = common.getQueryString(name);
            const parameter = "{0}={1}".format(name, value || "");
            if (val) {
                search = search.replace("{0}={1}".format(name, val), parameter);
            } else {
                search = parameter;
            }
            common.changeUrl(search);
        } else {
            console.warn("name is null");
        }
    }
    // property accessor
    common.propertyAccessor = function (obj, property) {
        const func = new Function("o", "return o.{0};".format(property));
        return func(obj);
    }

    // get arguments parameter list return array
    common.getArgs = function (args) {
        const array = [];
        for (let i = 0; i < args.length; i++) {
            array.push(args[i]);
        }
        return array;
    };
    //empty object point
    common.empty = function () { return {}; };
    //is initial
    common.init = function (o) {
        const self = o || this;
        if (self.__init__) {
            return true;
        } else {
            self.__init__ = true;
            return false;
        }
    }
    //generate uuid
    common.uuid = function () {
        const s = [];
        const hexDigits = "0123456789abcdef";
        for (var i = 0; i < 36; i++) {
            s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
        }
        s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
        s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
        s[8] = s[13] = s[18] = s[23] = "-";

        const uuid = s.join("");
        return uuid;
    }
    return common;
});
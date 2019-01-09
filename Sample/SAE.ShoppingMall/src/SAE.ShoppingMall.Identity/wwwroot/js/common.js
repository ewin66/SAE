/// <reference path="../lib/require/require.min.js" />
define(function () {
    const common = {};
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
    
    //change browser address bar url
    common.changeUrl = function (url) {
        if (history.replaceState) {
            if (!url) {
                if (protocolRegExp.test(url)) {
                    if (url.indexOf("/")!=-1) {
                        url = url.substr(url.indexOf("/"));
                    } else {
                        console.error("Can only change 'pathname' and 'search'");
                    }
                }
                if (url.indexOf("?") != 0 || url.indexOf("/") != 0) {
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
        let search = window.location.search.toLowerCase();
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


    return common;
});
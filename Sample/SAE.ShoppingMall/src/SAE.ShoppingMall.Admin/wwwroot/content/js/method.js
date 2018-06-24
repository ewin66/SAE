define(["jquery"], function ($) {
    //格式化字符串
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
    ///格式化日期
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

    if (typeof String.prototype.startsWith != 'function') {
        String.prototype.startsWith = function (prefix) {
            return this.slice(0, prefix.length) === prefix;
        };
    }

    var method = {};
    //全局配置
    method.config = {
        //分页
        paging: {
            //因子
            minuend: 7,
        },
        //jquery ajax
        ajax: {
            //成功
            success: function (data) {
                method.log.info(data);
                debugger;
            },
            //错误
            error: function (e) {
                method.log.error(e);
                debugger;
            }
        }
    };
    /**
     * 通过"propertyStr"字符串检索"obj"对应的值
     * @param {any} obj
     * @param {any} propertyStr
     */
    method.getValue=function(obj, propertyStr) {
        if (obj == null) {
            method.log.error('"obj" is not null')
            return obj;
        }
        var result =method.isFunction(obj) ? obj() : obj;
        if (propertyStr == null || propertyStr.length <= 0) {
            method.log.error('"propertyStr" is not null')
        } else {
            var propertyArray = propertyStr.split(".");
            for (var i = 0; i < propertyArray.length; i++) {
                result = result[propertyArray[i]];
            }
        }
        return result;
    }
    /**
     * 检索"obj"的属性值并通过array的方式返回
     * @param {any} obj
     */
    method.objToString = function (obj) {
        var result = [];
        if (obj != null) {
            obj = method.isFunction(obj) ? obj() : obj;
            return method.proeprtyToString(obj);
        }
        return result
    }
    /**
     * 递归的对"obj"的属性进行字符数组转换
     * @param {any} obj 要转换的对象
     * @param {any} str 转换的根目录
     */
    method.proeprtyToString = function (obj, str) {
        var result = [];
        if (str == null) {
            str = "";
        }
        if (!method.isArray(obj) && method.isObject(obj)) {
            for (var key in obj) {
                var name = str + (str.length > 0 ? "." : "") + key;
                result = result.concat(this.proeprtyToString(obj[key], name));
            }
        } else {
            result = [str];
        }
        return result;
    }
    /**
     * 使用"convertFunc" 转换 "value"
     * @param {any} value 需要转换的值
     * @param {any} convertFunc 转换函数
     */
    method.convert = function (value, convertFunc) {
        return convertFunc == null ? value : convertFunc(method.isFunction(value) ? value() : value);
    }
    /**
     * "obj"不是一个函数
     * @param {any} obj
     */
    method.isFunction = function (obj) {
        if (obj == null) {
            method.log.error('"obj" is not null');
            return false;
        } else {
            return typeof obj === "function";
        }
    }
    /**
     * "obj"不是一个数组
     * @param {any} obj
     */
    method.isArray = function (obj) {
        if (obj == null) {
            method.log.error('"obj" is not null')
            return false;
        }
        return Object.prototype.toString.call(obj) === '[object Array]';
    }
    /**
     * "obj"不是一个对象
     * @param {any} obj
     */
    method.isObject = function (obj) {
        if (obj == null) {
            method.log.error('"obj" is not null')
            return false;
        }
        return typeof obj === "object";
    }
    /**
     * 获得ajax请求配置配置
     * @param {string} type 请求类型
     * @param {string} url  请求地址默认window.location.href
     * @param {JSON} data 请求数据
     * @param {function} success 请求回掉
     * @param {function} error 请求异常
     * @param {string} jsonp 跨域请求
     */
    method.getAjaxOption = function (type, url, data, success, error, jsonp) {
        var option = {
            type: method.getValueOrDefault(type, "get"),
            url: method.getValueOrDefault(url, window.location.href),
            success: method.getValueOrDefault(success, method.config.ajax.success),
            error: method.getValueOrDefault(error, method.config.ajax.error)
        }
        if (jsonp != null) {
            option.jsonp = jsonp;
        }
        if (data != null) {
            option.data = data;
        }
        return option;
    }
    /**
     * 获得值入宫"val"为null则获得"def"
     * @param {any} val 
     * @param {any} def "val"为null则获得该值
     */
    method.getValueOrDefault = function (val, def) {
        return val == null ? def : val;
    }

    /**
     * post请求
     * @param {string} url 地址
     * @param {JSON} data 数据
     * @param {function} success 回调
     * @param {function} error 异常
     */
    method.postRequest = function (url, data, success, error) {
        var option = method.getAjaxOption("post", url, data, success, error);
        $.ajax(option);
    }
    /**
     * get 请求
     * @param {string} url 地址
     * @param {JSON} data 数据
     * @param {function} success 回调
     * @param {function} error 异常
     * @param {string} jsonp 跨域支持
     */
    method.getRequest = function (url, data, success, error, jsonp) {
        var option = method.getAjaxOption("get", url, data, success, error, jsonp);
        $.ajax(option);
    }
    /**
     * 更改地址栏参数
     * @param {string} data 更改的参数
     */
    method.changeUrl = function changeUrl(data) {
        if (typeof (history).hasOwnProperty("replaceState")) {
            var href = location.href;
            var index = href.indexOf("?");
            var curUrl = href;
            if (index > 0) {
                curUrl = href.substr(0, index)
            }
            history.replaceState(null, null, curUrl + data);
        }
    }

    /**
     * 获得请求参数
     * @param {any} name 查询的key
     */
    method.getRequestQueryString = function (name) {
        var urlSearch = window.location.search;
        var regExp = new RegExp("{0}=([^&]+)".format(name));
        if (regExp.test(urlSearch)) {
            return urlSearch.match(regExp)[1];
        } else {
            return null;
        }
    }
    /**
     * 设置浏览器QueryString
     * @param {any} name 要设置的name
     * @param {any} value 设置的值
     */
    method.setRequestQueryString = function (name, value) {
        
        var urlSearch = window.location.search;
        var regExp = new RegExp("({0}=)([^&]*)".format(name));
        if (regExp.test(urlSearch)) {
            urlSearch = urlSearch.replace(regExp, "$1" + value);
        } else {
            if (urlSearch.length <= 0) {
                urlSearch += "?";   
            } else {
                urlSearch += "&";
            }
            urlSearch += "{0}={1}".format(name, value);
        }
        method.changeUrl(urlSearch);
    }
    /**
     * 连接两个地址
     * @param {any} left 带有域名的url
     * @param {any} right 纯search
     */
    method.joinUrl = function (left, right) {
        var url;
        if (left.indexOf("?")!=-1) {
            if (right.indexOf("?") != -1) {
                url = left + right.replace("?", "&");
            } else {
                url = left + right.startsWith("&")?"":"&" + right;
            }
        } else {
            if (right.indexOf("?") != -1) {
                url = left + right;
            } else {
                url = left + right.startsWith("&") ? "?" + right.substring(1, right.length) : "?" + right;
            }
        }
        return url;
    }

    /**
     * 日志记录器
     */
    method.log= {
        /**
         *记录警告日志
         */
        warn: function (message) {
            console.warn(message);
        },
        /**
         *信息警告日志
         */
        info: function (message) {
            console.info(message);
        },
        /**
         *异常警告日志
         */
        error: function (message) {
            console.error(message);
        },
        /**
         *跟踪警告日志
         */
        trace: function (message) {
            console.trace(message);
        },
    
    }
    /**
    *  生成guid
    */
    method.getGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        }).toUpperCase();
    };

    return method;
})
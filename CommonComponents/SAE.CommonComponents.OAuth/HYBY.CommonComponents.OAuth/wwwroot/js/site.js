//格式化时间
Date.prototype.Format = function (fmt) {
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
///视图模型
var VM = {
    "body": ko.observableArray([]),
    "converter": {
        date: function (cur, format) {
            var value = typeof (cur) == "function" ? cur() : cur;

            var reg = /^\/Date\((\d+)\)\/$/;
            if (reg.test(value)) {
                value = parseInt(reg.exec(value)[1]);
            }
            date = new Date(value);
            return date.Format(format == null ? "yyyy-MM-dd" : format);
        }
    },
    //状态
    "status": [{ text: "禁用", value: "0" }, { text: "启用", value: "1" }, { text: "移除", value: "2" }],
    "siteMap": ko.observableArray()
};

var regempty = /[\w_]+/;//字符为空
//使用post类型请求数据
function postData(
    ///请求url
    url,
    ///数据
    data,
    ///成功回调
    callback,
    ///错误回调
    error) {
    if (url == null || url == "") {
        url = window.location.href;
    }
    if (callback == null || typeof (callback) != "function") {
        callback = function (e) {
            debugger;
        };
    }
    if (error == null || typeof (error) != "function") {
        error = function (e) {
            debugger;
        };
    }
    $.ajax({
        type: "post",
        data: data,
        url: url,
        success: callback,
        error: error
    });
}

//分页计算
function pagingCompute(data,minuend) {
    if (minuend == null || minuend <= 0) {
        minuend = 10;
    }
    var cur = data.index;
    var max = data.max;
    var divisor = minuend / 2;

    var a = 1;
    var b = max;

    var result;

    if ((cur - divisor) > 0) {
        a = cur - divisor;
    }

    if ((cur + divisor) > max) {
        a -= ((cur + divisor) - max);
    }

    var result = [];

    for (var i = a; i <= a + minuend; i++) {
        if (i > 0 && i <= max) {
            result.push(i);
        }
    }
    return result;
}

//改变地址栏
function changeUrl(data) {
    if (typeof (history).hasOwnProperty("replaceState")) {
        var href = location.href;
        var index = href.indexOf("?");
        var curUrl = href;
        if (index > 0) {
            curUrl = href.substr(0, index) + "?";
        } else {
            curUrl += "?";
        }
        history.replaceState(null, null, curUrl + data);
    }
}


///跳转
function skip(
    ///当前索引
    index,
    ///父级
    parent,
    //事件对象
    event) {
    if (index <= 0 || index == parent.index || index > parent.max) {
        return;
    }
    var form = $(event.target).parents("form").first();
    form.find("input[name=index]").val(index);
    changeUrl(form.serialize());
    var json = arrayToJson(form.serializeArray());
    postData(form.attr("action"), json, parent.success);
   
}
//数据转json
function arrayToJson(array) {
    var json = {};
    for (var i = 0; i < array.length; i++) {
        var temp = array[i];
        json[temp["name"]] = temp["value"];
    }
    return json;
}
//默认的初始化函数
function defaultInit() {
    var success = function (data) {
        data.success = success;
        VM.body(data);
    }
    ///初始化Body
    postData(null, null, success);
}

$(function () {
    var commonConvert = function (json, val, propertyKey, propertyValue) {
        var value = val;
        if (propertyKey == undefined)
            propertyKey = "value";
        if (propertyValue == undefined)
            propertyValue = "text";
        $(json).each(function () {
            if (this[propertyKey] == val) {
                value = this[propertyValue];
                return false;
            }
        });
        return value;
    }
    //转换
    VM.convert = function (input, value,arg) {
        if (typeof (value) == "function") {
            value = value();
        }
        var result = value;
        if (regempty.test(input)) {
            input = input.trim().toLocaleLowerCase();
            if (VM.converter.hasOwnProperty(input)) {
                result = VM.converter[input](value, arg);
            } else {
                result = commonConvert(VM[input], value, arg)
            }
        }
        return result;
    }

    $.ajax({
        type: "get",
        url: "/js/siteMap.json",
        success: function (data) {
            VM.siteMap(data);
        }
    })

    ko.applyBindings(VM);
});

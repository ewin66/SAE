/// <reference path="../../lib/requirejs/require.js" />
/// <reference path="../../lib/knockout/dist/knockout.js" />
/// <reference path="../../lib/jquery/dist/jquery.min.js" />
define(["ko","method"], function (ko,method) {
    
    var converObervable = function (obj) {
        return ko.isObservable(obj) ? obj : ko.observable(obj);
    }
    //注册模板函数
    var registerTemplate = function (templateName, url, viewModel) {
        ko.components.register(templateName, {
            viewModel: viewModel,
            template: { require: url },
        });
    };
    //表格模板
    registerTemplate("sae-table", "text!sae-table", function (params) {
        var self = this;
        if (params.items == null) {
            console.error('Property "items" not null')
        }
        
        self.items = params.items;
        //定义keys
        if (params.keys == null) {
            
            if (ko.isObservable(this.items)) {
                self.keys = ko.pureComputed(function () {
                    if ((!this.hasOwnProperty("key") || self.keys.peek().length <= 0) && self.items().length > 0) {
                        return method.objToString(self.items.peek()[0]);
                    }
                    return [];
                }, self);
            } else {
                if (this.items.length <= 0) {
                    console.warn("The array items element is 0 ")
                } else {
                    self.keys = [];
                    for (var key in this.items[0]) {
                        self.keys.push(key);
                    }
                }
            }
        } else {
            self.keys = params.keys;
        }
        //定义转换
        if (params.converts != null) {
            this.converts = params.converts;
        } else {
            this.converts = [];
        }
        //定义头部
        if (params.heads == null) {
            self.heads = self.keys;
        } else {
            self.heads = params.heads;
        }
    
        self.events = method.getValueOrDefault(params.events, {});

        self.isEdit = self.events.hasOwnProperty("onEdit");
        self.isRemove = self.events.hasOwnProperty("onRemove");

    });
    //分页模板
    registerTemplate("sae-paging", "text!sae-paging", function (params) {
        var self = this;
        var href = window.location.href;
        var hrefIndex = href.indexOf("?");
        self.url = method.getValueOrDefault(params.paging.url, hrefIndex != -1 ? href.substr(0, hrefIndex) : href);
        //当前索引
        self.index = converObervable(params.paging.index);
        //总长度
        self.count = converObervable(params.paging.count);
        //每页大小
        self.size = converObervable(params.paging.size);
        //最大值
        self.max = ko.pureComputed(function () {
            var size = self.size.peek();
            var count = self.count.peek();
            size = size < 1 ? 10 : size;
            var max = 0;
            if (count > 0) {
                max = Math.ceil(count / size);
            }
            return max;
        });
        //分页数据
        self.items = params.paging.items;
        //所有展示页
        self.indexs = ko.pureComputed(function () {
            var cur = self.index();
            var max = self.max.peek();
            var minuend = method.config.paging.minuend;
            var divisor = parseInt(minuend / 2);
            var a = 1;
            var b = max;
            var result;
            var result = [];
            if ((cur - divisor) > 0) {
                a = cur - divisor;
            }
            if ((cur + divisor) > max) {
                a -= ((cur + divisor) - max);
            }
            for (var i = a; i < a + minuend; i++) {
                if (i > 0 && i <= max) {
                    result.push(i);
                }
            }
            return result;
        });
        //跳转页面
        self.skipIndex = function (cur) {
            var max = self.max.peek();
            var index = self.index.peek();

            if (cur < 1 || cur > max) {
                method.log.warn("页面超出索引{0}".format(cur));
            } else {
                self.index(cur);
                self.pullData();
            }
        }
        //显示
        self.display = ko.pureComputed(function () {
            return self.max() > 1;
        });
        //禁用上一页
        self.disabledPrevious = ko.pureComputed(function () {
            return self.index() <= 1;
        });
        //禁用下一页
        self.disabledNext = ko.pureComputed(function () {
            return self.index() >= self.max();
        });
        //上一页
        self.previous = function () {
            var index = self.index.peek();
            self.skipIndex(--index);
        }
        //下一页
        self.next = function () {
            var index = self.index.peek();
            self.skipIndex(++index);
        }
        //是否是当前页
        self.isIndex = function (cur) {
            var index = self.index();
            return cur == index;
        }
   
        if (params.paging.pullData == null) {
            params.paging.pullData = method.config.paging.request;
        }

        //拉取数据
        self.pullData = function () {
            params.paging.pullData(self);
        }
       
        if (params.paging.init) {
            self.pullData();
        }
    });

    //表单元素:text
    registerTemplate("sae-form-text", "text!sae-form-text", function (params) {
        if (params.hasOwnProperty("object")) {
            params=params.object;
        }
        var self = this;
        self.attr = method.getValueOrDefault(params.attr, {});
        if (!self.attr.hasOwnProperty("placeholder")) {
            self.attr.placeholder = method.getValueOrDefault(params.placeholder, ko.observable(""));
        }
        
        if (!self.attr.hasOwnProperty("readonly")) {
            self.attr.readonly = method.getValueOrDefault(params.readonly, null);
        }
        if (!self.attr.hasOwnProperty("name")) {
            self.attr.name = method.getValueOrDefault(params.name, new Date().getTime());
        }
        
        self.displayName = method.getValueOrDefault(params.displayName, ko.observable());
        self.icon = method.getValueOrDefault(params.icon, "fa-bars");
        self.value = method.getValueOrDefault(params.value, ko.observable());
        
    });

    return registerTemplate;
});
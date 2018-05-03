/// <reference path="../../lib/requirejs/require.js" />
/// <reference path="../../lib/knockout/dist/knockout.js" />
/// <reference path="../../lib/jquery/dist/jquery.min.js" />
require(["ko", "vm", "convert"], function (ko, vm) {
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
    var method = vm.method;
    //表格模板
    registerTemplate("sae-table", "text!sae-table", function (params) {

        if (params.items == null) {
            console.error('Property "items" not null')
        }
        this.items = params.items;
        //定义keys
        if (params.keys == null) {
            var keys = [];
            if (ko.isObservable(this.items)) {
                this.keys = ko.computed(function () {
                    if ((!this.hasOwnProperty("key") || this.keys.peek().length <= 0) && this.items.peek().length > 0) {
                        keys = objToString(this.items.peek()[0]);
                    }
                    return keys;
                }, this);
            } else {
                if (this.items.length <= 0) {
                    console.warn("The array items element is 0 ")
                } else {
                    for (var key in this.items[0]) {
                        keys.push(key);
                    }
                }
            }
            this.keys = keys;
        } else {
            this.keys = params.keys;
        }
        //定义转换
        if (params.converts != null) {
            this.converts = params.converts;
        } else {
            this.converts = [];
        }
        //定义头部
        if (params.heads == null) {
            this.heads = this.keys;
        } else {
            this.heads = params.heads;
        }

    });
    //分页模板
    registerTemplate("sae-paging", "text!sae-paging", function (params) {
        var self = this;
        //当前索引
        self.index = converObervable(params.index);
        //总长度
        self.count = converObervable(params.count);
        //每页大小
        self.size = converObervable(params.size);
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
                method.changeUrl("index=" + cur);
                self.index(cur);
                self.pullData();
            }
        }
        //显示"上一页"
        self.displayPrevious = function () {
            return self.max() > 1;
        }
        //显示"下一页"
        self.displayNext = function () {
            return self.max() > 1;
        }
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
        //拉取数据
        self.pullData = function () {
            var index = self.index.peek();
            if (params.pullData == null) {
                method.log.warn("没有为分页组件定义拉取函数");
            } else {
                params.pullData();
            }
        }
    });

});
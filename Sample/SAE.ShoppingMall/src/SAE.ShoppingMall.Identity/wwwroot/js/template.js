/// <reference path="../lib/react/umd/react-dom.development.js" />
/// <reference path="common.js" />
/// <reference path="../lib/require/require.min.js" />
/// <reference path="../lib/react/umd/react.development.js" />
/// <reference path="../lib/uppy/uppy.min.js" />
define(["react", "react-dom", "jquery", "common", "httpClient", "uppy", "dataTables"],
function (React, ReactDOM, $, common, httpClient, Uppy) {
    const parts = {};
    const template = {};
    //pageing class
    template.paging = function (props) {
        React.useState({
            pageIndex: props.pageIndex, pageCount: props.pageCount
        }, this);
        if (!common.init(this)) {
            props.init(this);
            this.skip = props.skip.bind(this);
            this.setPaging = function (pageData) {
                if (pageData.hasOwnProperty("pageIndex") && pageData.hasOwnProperty("pageCount")) {
                    this.state = pageData;
                } else {
                    console.error("not exist 'pageIndex' and 'pageCount'");
                }
            }.bind(this);
            //internal skip function
            this._skip = function (i, event) {
                i = parseInt(i);
                if (isNaN(i) || i < 1 || i == this.state.pageIndex || i > this.state.pageCount) {
                    console.warn("number of pages '{0}' invalid".format(i));
                } else {
                    common.setQueryString("pageindex", i);
                    this.skip(i, event);
                }
            }.bind(this);
            //skip previous
            this.previous = function (event) {
                this._skip(this.state.pageIndex - 1, event);
            }.bind(this);
            //skip next
            this.next = function (event) {
                this._skip(this.state.pageIndex + 1, event);
            }.bind(this);
            //skip first page 
            this.fistPage = function (event) {
                this.skip(1, event);
            }.bind(this);
            //skip last page
            this.lastPage = function (event) {
                this.skip(this.state.pageCount, event);
            }.bind(this);

            this.render = function () {
                let self = this;

                const left = Math.ceil(props.pageNumber / 2);

                const node = function (data) {
                    return React.createElement("li", { className: data.className },
                        React.createElement("a", { href: "#", onClick: data.skip || self._skip.bind(self, data.index) }, data.index));
                }

                let start = 0;

                const array = [];

                const className = "paginate_button {0}";
                const right = this.state.pageIndex + props.pageNumber - left;

                if (this.state.pageIndex - left >= start) {
                    start = this.state.pageIndex - left;
                    if (this.state.pageCount > props.pageNumber) {
                        array.push(node({
                            className: className,
                            index: "first page",
                            skip: self.fistPage
                        }));
                    }
                }

                if (right > this.state.pageCount) {
                    start -= right - this.state.pageCount;
                }

                array.push(node({
                    className: className.format("previous" + (this.state.pageIndex == 1 ? " disabled" : "")),
                    index: "Previous",
                    skip: self.previous
                }));

                for (let i = 0; i < props.pageNumber; i++) {
                    ++start;
                    if (start > 0 && start <= this.state.pageCount) {
                        array.push(node({
                            className: className.format(start == this.state.pageIndex ? "active" : ""),
                            index: start
                        }));
                    }
                }

                array.push(node({
                    className: className.format("next" + (this.state.pageIndex == this.state.pageCount ? " disabled" : "")),
                    index: "Next",
                    skip: self.next
                }));

                if (this.state.pageCount > props.pageNumber && start < this.state.pageCount) {
                    array.push(node({
                        className: className,
                        index: "last page",
                        skip: self.lastPage
                    }));
                }

                if (!this.state.pageCount || this.state.pageCount < 2) {
                    return React.createElement("ul");
                } else {
                    return React.createElement("ul", { className: "pagination" }, array);
                }
            }
            this.skip();
        }

        return this.render();
    }
    //data table class
    template.dataTable = function (props) {
        const self = this;
        //setting state
        React.useState({ data: props.data || [] }, this);
        if (!common.init(this)) {
            //generate table head
            this.generateHead = function () {
                const ths = props.columns.map(function (column) {
                    if (!column.name) {
                        throw "column no 'name' property";
                    }
                    return React.createElement("th", {}, column.text || column.name);
                });
                if (!props.ignoreIndexColumn) {
                    ths.unshift(React.createElement("th", {}, "序列"));
                }
                self.head = React.createElement("thead", {},
                    React.createElement("tr", {}, ths));
            }
            //render default row
            this.defaultRowRender = function (data, row) {
                return data;
            }

            this.generateHead();

            this.paging = parts.paging({
                skipCallBack: function (body) {
                    this.state = { data: body.items };
                }.bind(self)
            });

            this.renderBody = function () {
                const rows = [];
                self.state.data.forEach(function (row, index) {
                    const tds = props.columns.map(function (column) {
                        //new Function("function(data){0}".format())
                        const __html = { __html: (column.render || self.defaultRowRender)(common.propertyAccessor(row, column.name), row) };
                        return React.createElement("td", { dangerouslySetInnerHTML: __html });
                    });
                    if (!props.ignoreIndexColumn) {
                        tds.unshift(React.createElement("td", {}, index + 1));
                    }
                    let className = index % 2 == 0 ? "odd" : "even";
                    rows.push(React.createElement("tr", { className: className }, tds));
                });
                if (rows.length == 0) {
                    rows.push(React.createElement("tr", {}, React.createElement("td", { colSpan: props.columns.length + (props.ignoreIndexColumn ? 0 : 1) }, "暂无数据!")));
                }
                return React.createElement("tbody", {}, rows);
            }

            this.render = function () {
                const table = React.createElement("table",
                    { className: "table table-bordered table-hover dataTable" },
                    this.head,
                    this.renderBody());

                return React.createElement("div", {}, table, this.paging);
            }.bind(this);
        }
        return this.render();
    };
    template.upload = function (props) {
        if (!props.url) {
            throw "Please specify the upload url";
        }

        react.useEffect(function () {
            const option = {
                id: props.id || common.uuid(),
                maxFileSize: props.maxFileSize,
                autoProceed: props.autoProceed,
                allowMultipleUploads: props.allowMultipleUploads,
                restrictions: {
                    maxNumberOfFiles: props.maxNumberOfFiles,
                    minNumberOfFiles: props.minNumberOfFiles,
                    allowedFileTypes: props.allowedFileTypes,
                },
                meta: props.meta,
            };
            const uppy = new Uppy().Core(option);
            uppy.use(uppy.Dashboard, {
                id: option.id,
                trigger: "#" + option.id,
                showProgressDetails: true,
            }).use(Tus, { endpoint: 'https://master.tus.io/files/' });
        });

        return React.createElement("div");
    };
    //paging component
    parts.paging = function (id, data) {
        if (id) {
            if (!common.isString(id)) {
                if (!data) {
                    data = id;
                }
                id = null;
            }
        }
        const dataBuilder = function (data) {
            if (!data) {
                data = {};
            }
            if (!data.pageIndex) {
                data.pageIndex = 1;
            }
            if (!data.pageNumber) {
                data.pageNumber = 9;
            }
            if (!data.skip) {
                const client = new httpClient("post");
                if (!data.skipCallBack) {
                    data.skipCallBack = function (body) {
                        console.info(body);
                    }
                }

                client.success = function (body) {
                    data.setPaging(body);
                    data.skipCallBack(body);
                }
                data.skip = function (index, event) {
                    client.option.url = window.location.href;
                    client.request();
                }
            }

            data.setPaging = function (body) {
                this.setPaging(body);
            }

            data.init = function (proxy) {
                data.setPaging = data.setPaging.bind(proxy);
            }
            return data;
        }

        const ele = template.paging.bind(common.empty());

        if (id) {
            const element = $(id).get(0);
            const pagingData = dataBuilder(data);
            ReactDOM.render(React.createElement(ele, pagingData), element);
        } else {
            return React.createElement(ele, dataBuilder(data));
        }
    }
    //data table componente
    parts.dataTable = function (id, data) {
        const ele = template.dataTable.bind(common.empty());
        if (id) {
            const element = $(id).get(0);
            if (!data) {
                throw "data not null";
            }

            if (!common.isArray(data.columns)) {
                throw "columns 'undefined' or columns is not a 'Array'";
            }

            ReactDOM.render(React.createElement(ele, data), element);
        } else {
            React.createElement(ele, data);
        }
    }
    //upload component
    parts.upload = function (id, data) {

    }
    return parts;
});
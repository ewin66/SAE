/// <reference path="common.js" />
/// <reference path="../lib/require/require.min.js" />
define(["react", "react-dom", "jquery", "common", "httpClient", "dataTables"],
function (React, ReactDOM, $, common, httpClient) {
    const parts = {};
    //pageing class
    class paging extends React.Component {
        constructor(props) {
            super(props)
            props.init(this);
            this.state = {
                pageIndex: this.props.pageIndex, pageCount: this.props.pageCount
            };
            this.skip = this.props.skip.bind(this);
            this.previous = this.previous.bind(this);
            this.next = this.next.bind(this);
            this.fistPage = this.fistPage.bind(this);
            this.lastPage = this.lastPage.bind(this);
        }
        //setting page data
        setPaging(pageData) {
            if (pageData.hasOwnProperty("pageIndex") && pageData.hasOwnProperty("pageCount")) {
                this.setState(pageData);
            } else {
                console.error("not exist 'pageIndex' and 'pageCount'");
            }
        }
        componentDidMount() {
            this.skip();
        }
        //internal skip function
        _skip(i, event) {
            i = parseInt(i);
            if (isNaN(i) || i < 1 || i == this.state.pageIndex || i > this.state.pageCount) {
                console.warn("number of pages '{0}' invalid".format(i));
            } else {
                common.setQueryString("pageindex", i);
                this.skip(i, event);
            }
        }
        //skip previous
        previous(event) {
            this._skip(this.state.pageIndex - 1, event);
        }
        //skip next
        next(event) {
            this._skip(this.state.pageIndex + 1, event);
        }
        //skip first page 
        fistPage(event) {
            this.skip(1, event);
        }
        //skip last page
        lastPage(event) {
            this.skip(this.state.pageCount, event);
        }

        render() {
            let self = this;

            const left = Math.ceil(this.props.pageNumber / 2);

            const node = function (data) {
                return React.createElement("li", { className: data.className },
                    React.createElement("a", { href: "#", onClick: data.skip || self._skip.bind(self, data.index) }, data.index));
            }

            let start = 0;

            const array = [];

            const className = "paginate_button {0}";
            const right = this.state.pageIndex + this.props.pageNumber - left;

            if (this.state.pageIndex - left >= start) {
                start = this.state.pageIndex - left;
                if (this.state.pageCount > this.props.pageNumber) {
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

            for (let i = 0; i < this.props.pageNumber; i++) {
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

            if (this.state.pageCount > this.props.pageNumber && start < this.state.pageCount) {
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
    }
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

        if (id) {
            const element = $(id).get(0);
            const pagingData = dataBuilder(data);
            ReactDOM.render(React.createElement(paging, pagingData), element);
        } else {
            return React.createElement(paging, dataBuilder(data));
        }
    }
    //data table class
    class dataTable extends React.Component {
        constructor(props) {
            super(props);
            this.state = { data: props.data || [] };
            this.generateHead();
            this.paging = parts.paging({
                skipCallBack: function (body) {
                    this.setState({ data: body.items });
                }.bind(this)
            });
        }

        generateHead() {
            const ths = this.props.columns.map(function (column) {
                if (!column.name) {
                    throw "column no 'name' property";
                }
                return React.createElement("th", {}, column.text || column.name);
            });
            if (!this.props.ignoreIndexColumn) {
                ths.unshift(React.createElement("th", {}, "序列"));
            }
            this.head = React.createElement("thead", {},
                React.createElement("tr", {}, ths));
        }

        defaultRowRender(data, row) {
            return data;
        }

        renderBody() {
            const rows = [];
            const self = this;
            this.state.data.forEach(function (row, index) {
                const tds = self.props.columns.map(function (column) {
                    return React.createElement("td", {}, (column.render || self.defaultRowRender)(row[column.name], row))
                });
                if (!self.props.ignoreIndexColumn) {
                    tds.unshift(React.createElement("td", {}, index + 1));
                }
                let className = index % 2 == 0 ? "odd" : "even";
                rows.push(React.createElement("tr", { className: className }, tds));
            });
            if (rows.length == 0) {
                rows.push(React.createElement("tr", {}, React.createElement("td", { colspan: this.props.columns.length + (self.props.ignoreIndexColumn ? 0 : 1) }, "暂无数据!")));
            }
            return React.createElement("tbody", {}, rows);
        }

        componentDidMount() {

        }

        componentWillUnmount() {

        }


        render() {
            const table = React.createElement("table",
                { className: "table table-bordered table-hover dataTable" },
                this.head,
                this.renderBody());

            return React.createElement("div", {}, table, this.paging);
        }
    }
    //data table component
    parts.dataTable = function (id, data) {
        if (id) {
            const element = $(id).get(0);
            if (!data) {
                throw "data not null";
            }

            if (!common.isArray(data.columns)) {
                throw "columns 'undefined' or columns is not a 'Array'";
            }

            ReactDOM.render(React.createElement(dataTable, data), element);
        } else {
            React.createElement(dataTable, data);
        }
    }

    return parts;
});
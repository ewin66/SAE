/// <reference path="common.js" />
/// <reference path="../lib/require/require.min.js" />
define(["react", "react-dom", "common"], function (React, ReactDOM, common) {
    const parts = {};
    //pageing class
    class paging extends React.Component {
        constructor(props) {
            super(props)
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
        //internal skip function
        _skip(i, event) {
            i = parseInt(i);
            if (isNaN(i) || i < 1 || i == this.state.pageIndex || i > this.state.pageCount) {
                console.warn("number of pages '{0}' invalid".format(i));
            } else {
                this.skip.call(this, i, event);;
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
                start -= right-this.state.pageCount;
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

            return React.createElement("ul", { className: "pagination" }, array);
        }
    }
    //paging component
    parts.paging = function (data) {

        if (!data) {
            data = {};
        }
        if (!data.pageIndex) {
            data.pageIndex = 1;
        }

        if (!data.pageCount) {
            data.pageCount = 100;
        }

        if (!data.pageNumber) {
            data.pageNumber = 9;
        }

        if (!data.skip) {
            data.skip = function (index, event) {
                this.setPaging({ pageIndex: index, pageCount: this.state.pageCount });
            }
        }

        return React.createElement(paging, data);
    }
    return parts;
});
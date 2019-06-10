/// <reference path="../../lib/require/require.js" />
/// <reference path="../../lib/react/umd/react.production.min.js" />
/// <reference path="../../lib/react/umd/react-dom.production.min.js" />
/// <reference path="../../lib/jquery/jquery.js" />
define(["jquery",
    "react",
    "react-dom",
    "component/container/form",
    "component/dropdown/selector",
    "component/listbox/simple",
    "layer",
    "adminlte"],
    function ($, React, ReactDOM, Form, Selector, Simple, layer) {
        const select = $("#type").get(0);

        const option = {
            url: "/component/types",
            transform: data => {
                return data.map(item => { return { id: item, text: item } });
            },
            class: "form-control",
            enableCreate: true
        };

        ReactDOM.render(<Selector {...option} />, select);

        const componentOption = {
            render: function (item) {
                return <div className="input-group">
                    <div className="input-group-btn">
                        <i className="btn btn-danger">{item.id}</i>
                    </div>
                    <input type="text" defaultValue={item.name} onChange={this.setValue.bind(this, item)} className="form-control" />
                </div>
            },
            add: item => {
                item.name = item.name || (item.id.lastIndexOf("/") > -1 ? item.id.substr(item.id.lastIndexOf("/") + 1) : item.id);
            },
            setValue: (item, e) => {
                item.name = e.target.value;
            },
            name: "components"
        };

        const componentDiv = $("#components").get(0);

        const simple = ReactDOM.render(<Simple {...componentOption} />, componentDiv);

        window.addComponent = function (id) {
            if (simple.data.findIndex(s => s.id == id) == -1) {
                simple.add({ id: id });
            }
        }

        $("#addComponentBtn").click(function () {
            layer.open({
                type: 2,
                title: '组件页',
                shadeClose: true,
                shade: 0.8,
                area: ['90%', '90%'],
                content: '/component/collection'
            });
        });

        const datasList = ReactDOM.render(<Simple name="datas" />, $("#datas")[0]);

        $("#addDatasBtn").click(function () {
            datasList.add("");
        });

        $("#previewBtn").click(function () {

        });
    });
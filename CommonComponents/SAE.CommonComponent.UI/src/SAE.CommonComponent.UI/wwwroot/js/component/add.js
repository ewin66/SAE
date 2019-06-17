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

        const libsSelect = $("#libs").get(0);

        const libOption = {
            url: "/component/libs",
            transform: data => {
                return data.map(item => { return { id: item, text: item } });
            },
            class: "form-control",
            multiple: true
        };

        const libs = ReactDOM.render(<Selector {...libOption} />, libsSelect);

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
            if (simple.getData().findIndex(s => s.id == id) == -1) {
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


        const formatName = (name) => {
            if (name.length < 1) {
                return name;
            }

            let formatName = name[0].toUpperCase();

            for (let i = 1; i < name.length; i++) {
                formatName += (name[i] == "-" && name.length > i + 1 ?
                    name[++i].toUpperCase() :
                    name[i]);
            }

            return formatName;
        };

        const mapsIndexOf = (name, cur) => {
            return cur == name;
        }

        let script;

        $("#previewBtn").click(function () {
            debugger;
            const libArray = libs.getData();
            const components = simple.getData();
            const maps = {};

            for (let i in libArray) {
                let name = libArray[i];
                let firstName = formatName(name);
                maps[name] = firstName;
            }

            for (let i in components) {
                let component = components[i];
                let firstName = formatName(component.name)
                maps[component.id] = firstName;
            }

            const keys = [];
            const values = [];

            for (let key in maps) {
                keys.push(key);
                const val = maps[key];
                if (values.indexOf(val) != -1) {
                    throw key + '的"' + val + '"已经存在';
                }
                values.push(val);
            }

            const componentName = $("#Name").val();

            script = 'define("' + componentName + '",' + (JSON.stringify(keys)) + ', function (' + (values.join()) + ') {';

            script += $("#Content").val();

            script += "});"

            script += "";

            let datas = ["{off:'关',on:'开'}", "{off:'禁用',on:'启用'}"];
            layer.open({
                type: 2,
                title: '组件页',
                shadeClose: true,
                shade: 0.8,
                area: ['90%', '90%'],
                content: "/component/preview"
            });
        });

        window.beready = function () {
            let js = script;
            const componentName = $("#Name").val();
            const data = datasList.getData();
            js += 'require(["' + componentName + '", "react", "react-dom"], function (' + componentName + ', React, ReactDOM) {';
            if (data.length == 0) {
                js += 'ReactDOM.render(React.createElement(' + componentName + ', {}), document.body);';
            } else {
                for (let i in data) {
                    js += 'const div' + i + '= document.createElement("div");';
                    js += 'document.body.appendChild(div' + i + ');';
                    js += 'ReactDOM.render(React.createElement(' + componentName+', ' + (data[i] || '{}') + '), div' + i + ');';
                }
            }
            js += '});';
            return js;
        }
    });
/// <reference path="../../lib/require/require.js" />
/// <reference path="../../lib/react/umd/react.production.min.js" />
/// <reference path="../../lib/react/umd/react-dom.production.min.js" />
/// <reference path="../../lib/jquery/jquery.js" />
define(["jquery", "react", "react-dom", "template/container/form", "template/dropdown/selector","template/listbox/simple","layer"], function ($, React, ReactDOM, Form, Selector,Simple,layer) {
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

    $(".btn-info").click(function () {
        layer.open({
            type: 2,
            title: '组件页',
            shadeClose: true,
            shade: 0.8,
            area: ['90%', '90%'],
            content: '/component/collection'
        });
    });

    const componentDiv = $("#components").get(0);
    const simple = ReactDOM.render(<Simple />, componentDiv);
    window.addComponent = function (id) {
        if (simple.data.findIndex(s => s.id == id) == -1) {
            simple.add({ id: id });
        }
    }
});
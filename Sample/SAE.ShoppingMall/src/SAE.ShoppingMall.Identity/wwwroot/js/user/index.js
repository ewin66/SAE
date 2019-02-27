/// <reference path="../../lib/jquery/jquery.min.js" />
/// <reference path="../../lib/require/require.min.js" />
/// <reference path="../../lib/react/umd/react.development.js" />
/// <reference path="../../lib/react/umd/react-dom.development.js" />
/// <reference path="../template.js" />
define(["template", "route"], function (parts, route) {
    parts.dataTable("#table_div", {
        columns: [ {
            name: "credentials.name",
            text:"名称"
        }, {
            name: "information.name",
            text:"用户名"
        }, {
            name: "information.sex",
            text: "性别",
            render: function (data) {
                return data == 0 ? "女" : "男";
            }
        },
        {
            name: "createTime",
            text: "创建时间",
            render: function (date) {
                return new Date(date).format("yyyy-MM-dd");
            }
        }, {
            name: "status",
            render: function (data) {
                let result;
                switch (data) {
                    case 0: result = "禁用"; break;
                    case 1: result = "启用"; break;
                    default: result = "已删除"; break;
                }
                return result;
            }
        }, {
            name: "id",
            text:"操作",
            render: function (id, row) {
                debugger;
                let html = '<a class="btn btn-block btn-success" href="{0}"><i class="fa fa-edit"></i></a>'.format(route.userEdit.format(id));
                return html;
            }
        }]
    });
});
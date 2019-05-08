/// <reference path="../../../lib/require/require.js" />
/// <reference path="../../../lib/react/umd/react.production.min.js" />
/// <reference path="../../../lib/react/umd/react-dom.production.min.js" />
/// <reference path="../../../lib/jquery/jquery.min.js" />
define(["jquery", "react", "react-dom"], function ($, React, ReactDOM) {
    class Form extends React.Component {
        constructor(props) {
            super(props);
        }

        render() {
            return React.createElement("form", this.props);
        }
    }

    return Form;
});
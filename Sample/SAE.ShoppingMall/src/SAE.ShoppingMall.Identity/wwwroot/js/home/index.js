/// <reference path="../../lib/jquery/jquery.min.js" />
/// <reference path="../../lib/react/cjs/react.production.min.js" />
/// <reference path="../../lib/react/cjs/react-dom.production.min.js" />
/// <reference path="../../lib/require/require.min.js" />
define(["react", "react-dom"], function (React, ReactDOM) {
    ReactDOM.render(
        React.createElement('p', {}, 'Hello, AMD!'),
        document.getElementById('root')
    );
})
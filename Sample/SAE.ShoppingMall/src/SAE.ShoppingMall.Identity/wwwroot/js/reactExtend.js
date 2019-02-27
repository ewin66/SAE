/// <reference path="common.js" />
/// <reference path="../lib/require/require.min.js" />
/// <reference path="../lib/react/umd/react.development.js" />
define(["originalReact"], function (React) {
    const useState = React.useState;
    React.useState = function (initialState,self) {
        const state = useState(initialState);
        if (self) {
            self.__defineGetter__("state", function () {
                return state[0];
            });
            self.__defineSetter__("state", function (val) {
                state[1](val);
            });
            return self;
        } else {
            return {
                get value() {
                    return state[0];
                },
                set value(val) {
                    state[1](val);
                }
            }
        }
    }
    return React;
})
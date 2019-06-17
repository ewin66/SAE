define(["jquery", "react", "react-dom", "bootstrap-switch"],function($, React, ReactDOM) {
    class Switch extends React.Component {
        constructor(props) {
            super(props);
            this.option = {
                state: props.state == null ? true : props.state,
                onColor: 'success',
                offColor: 'danger',
                onText: props.on || 'ON',
                offText: props.off || 'OFF',
            };

        }
        componentDidMount() {
            $(this.el).bootstrapSwitch(this.option);
        }

        componentWillUnmount() {
            $(this.el).bootstrapSwitch('destroy');
        }

        render() {
            return <input type="checkbox" ref={el => this.el = el} />
        }
    }
    return Switch;
})
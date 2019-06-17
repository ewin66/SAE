define(["jquery", "react", "react-dom"], function ($, React, ReactDOM) {
    class Simple extends React.Component {
        constructor(props) {
            super(props);
            this.state = { data: props.data || [] };
            this.itemRender = (props.render || this.defaultRender).bind(this);
            if (props.add) {
                this.addHandle = props.add.bind(this);
            }
            if (props.setValue) {
                this.setValueHandle = props.setValue.bind(this);
            }
        }

        defaultRender = (item, index) => {
            return <div className="input-group">
                <input type="text" defaultValue={item} name={this.getName(index)} onChange={this.setValue.bind(this, item, index)} className="form-control" />
            </div>
        }

        getName = index => {
            return (this.props.name || "") + "[" + index + "]";
        }

        setValue = (item, index,e) => {
            if (this.setValueHandle) {
                this.setValueHandle(item, e);
            } else {
                this.state.data[index] = e.target.value;
            }
        }

        add = item => {
            if (this.addHandle) {
                this.addHandle(item);
            }
            this.state.data.push(item);
            this.setState(this.state);
        }

        getData() {
            return this.state.data;
        }

        render = () => {
            return this.state.data.map(this.itemRender);
        }
    }

    return Simple;
});
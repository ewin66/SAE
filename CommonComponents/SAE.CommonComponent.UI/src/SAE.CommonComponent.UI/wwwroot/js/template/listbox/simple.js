define(["jquery", "react", "react-dom"], function ($, React, ReactDOM) {
    class Simple extends React.Component {
        constructor(props) {
            super(props);
            this.state = { data: props.data || [] };
            this.data = this.state.data;
        }

        defaultName = item => {
            return item.name || (item.id.lastIndexOf("/") > -1 ? item.id.substr(item.id.lastIndexOf("/")+1) : item.id)
        }

        defaultRender = item => {
            return <div className="input-group">
                <div className="input-group-btn">
                    <i className="btn btn-danger">{item.id}</i>
                </div>
                <input type="text" defaultValue={item.name} onChange={this.setValue.bind(this, item)} className="form-control" />
            </div>
        }

        

        setValue = (item, e) => {
            item.name = e.target.value;
        }

        add = item => {
            item.name = this.defaultName(item);
            this.state.data.push(item);
            this.setState(this.state);
        }

        render = () => {
            return this.state.data.map(this.defaultRender);
        }
    }

    return Simple;
});
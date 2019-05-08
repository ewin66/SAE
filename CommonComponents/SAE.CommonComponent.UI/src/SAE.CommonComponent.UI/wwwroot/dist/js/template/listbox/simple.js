function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

define(["jquery", "react", "react-dom"], function ($, React, ReactDOM) {
  class Simple extends React.Component {
    constructor(props) {
      super(props);

      _defineProperty(this, "defaultName", item => {
        return item.name || (item.id.lastIndexOf("/") > -1 ? item.id.substr(item.id.lastIndexOf("/") + 1) : item.id);
      });

      _defineProperty(this, "defaultRender", item => {
        return React.createElement("div", {
          className: "input-group"
        }, React.createElement("div", {
          className: "input-group-btn"
        }, React.createElement("i", {
          className: "btn btn-danger"
        }, item.id)), React.createElement("input", {
          type: "text",
          defaultValue: item.name,
          onChange: this.setValue.bind(this, item),
          className: "form-control"
        }));
      });

      _defineProperty(this, "setValue", (item, e) => {
        item.name = e.target.value;
      });

      _defineProperty(this, "add", item => {
        item.name = this.defaultName(item);
        this.state.data.push(item);
        this.setState(this.state);
      });

      _defineProperty(this, "render", () => {
        return this.state.data.map(this.defaultRender);
      });

      this.state = {
        data: props.data || []
      };
      this.data = this.state.data;
    }

  }

  return Simple;
});
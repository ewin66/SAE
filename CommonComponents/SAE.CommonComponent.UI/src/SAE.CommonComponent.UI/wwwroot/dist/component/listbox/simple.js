function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

define(["jquery", "react", "react-dom"], function ($, React, ReactDOM) {
  class Simple extends React.Component {
    constructor(props) {
      super(props);

      _defineProperty(this, "defaultRender", (item, index) => {
        return React.createElement("div", {
          className: "input-group"
        }, React.createElement("input", {
          type: "text",
          defaultValue: item,
          name: this.getName(index),
          onChange: this.setValue.bind(this, item, index),
          className: "form-control"
        }));
      });

      _defineProperty(this, "getName", index => {
        return (this.props.name || "") + "[" + index + "]";
      });

      _defineProperty(this, "setValue", (item, index, e) => {
        if (this.setValueHandle) {
          this.setValueHandle(item, e);
        } else {
          this.state.data[index] = e.target.value;
        }
      });

      _defineProperty(this, "add", item => {
        if (this.addHandle) {
          this.addHandle(item);
        }

        this.state.data.push(item);
        this.setState(this.state);
      });

      _defineProperty(this, "render", () => {
        return this.state.data.map(this.itemRender);
      });

      this.state = {
        data: props.data || []
      };
      this.data = this.state.data;
      this.itemRender = (props.render || this.defaultRender).bind(this);

      if (props.add) {
        this.addHandle = props.add.bind(this);
      }

      if (props.setValue) {
        this.setValueHandle = props.setValue.bind(this);
      }
    }

  }

  return Simple;
});
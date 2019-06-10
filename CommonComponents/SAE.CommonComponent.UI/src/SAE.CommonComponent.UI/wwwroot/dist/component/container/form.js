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
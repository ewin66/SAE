define(["jquery", "react", "react-dom", "select2"], function ($, React, ReactDOM) {
  //array
  //ajax
  //optionText
  //optionValue
  //multiple
  //enableCreate
  //createCallBack
  const transform = function (props) {
    const option = {
      optionText: props.optionText || "text",
      //显示
      optionValue: props.optionValue || "id",
      //值
      multiple: props.multiple ? true : false //多选
      //dropdownAutoWidth: true,
      //closeOnSelect: true,
      //selectOnClose: true

    };

    option.format = function (array) {
      //格式化
      if (array == null) return [];
      const items = array.map(item => {
        return {
          "id": item[option.optionValue],
          "text": item[option.optionText],
          data: item
        };
      });
      return items;
    };

    if (props.url) {
      $.ajax({
        url: props.url,
        success: data => {
          if (props.transform) {
            data = props.transform(data);
          }

          data = option.format(data);
          option.data = data;

          if (option.callback) {
            option.callback();
          }
        }
      });
    }

    if (props.data) {
      option.data = option.format(props.data);
    }

    if (props.enableCreate) {
      option.tags = true;

      option.createTag = params => {
        const term = $.trim(params.term);

        if (term === '') {
          return null;
        }

        const item = {
          id: term,
          text: term
        };

        if (props.createCallBack && props.createCallBack(item) === false) {
          return null;
        }

        item.newTag = true;
        return item;
      };
    }

    return option;
  };

  class Selector extends React.Component {
    constructor(props) {
      super(props);
    }

    componentDidMount() {
      this.option = transform(this.props);

      this.option.callback = () => {
        //if (!this.option.dropdownParent) {
        //    this.option.dropdownParent = $(this.parentElement);
        //}
        $(this.el).select2(this.option);
      };

      if (!this.props.url) {
        this.option.callback();
      }
    }

    componentWillUnmount() {
      $(this.el).select2("destroy");
    }

    getData() {
      return $(this.el).val();
    }

    render() {
      return React.createElement("select", {
        ref: el => this.el = el,
        className: this.props.class
      });
    }

  }

  return Selector;
});
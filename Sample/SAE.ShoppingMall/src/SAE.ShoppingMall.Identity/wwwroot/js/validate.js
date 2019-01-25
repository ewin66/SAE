/// <reference path="../lib/jquery/jquery.min.js" />
/// <reference path="../lib/require/require.min.js" />
/// <reference path="../lib/jquery-validate/jquery.validate.js" />
define(["jquery", "jquery-validate", "jquery-form", "httpClient"], function ($, validate, ajaxform, httpClient) {
    const verify = {};
    
    $.validator.setDefaults({
        ignore: ".ignore",//忽略.ignore
        errorClass: "help-block"
    });

    const getArgs = function (args) {
        const array = [];
        for (let i = 0; i < args.length; i++) {
            array.push(args[i]);
        }
        return array;
    }

    //重新加载表单验证
    verify.reload = function (form) {
        $((form || "form")).each(function () {

            const self = $(this);

            self.validate();

            if (self.attr("data-ajax")) {
                const func={
                    success:self.attr("data-ajax-success"),
                    error:self.attr("data-ajax-error"),
                    beforeSubmit:self.attr("data-ajax-begin")
                };
                
                const option = {
                    success: function () {
                        if (func.success) {
                            window[func.success].apply(null, getArgs(arguments));
                        }
                    },
                    beforeSubmit: function(){
                        if (func.beforeSubmit) {
                            window[func.beforeSubmit].apply(null, getArgs(arguments));
                        }
                    },
                    error: function () {
                        if (func.error) {
                            window[func.error].apply(null, getArgs(arguments));
                        } else {
                            httpClient.defaultError.apply(null,getArgs(arguments));
                        }
                    },
                }

                self.ajaxForm(option);
            }
        });
    }
    $(function () {
        verify.reload();
    });
    
    return verify;
});
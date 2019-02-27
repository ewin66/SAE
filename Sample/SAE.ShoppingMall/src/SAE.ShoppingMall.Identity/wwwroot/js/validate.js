/// <reference path="../lib/jquery/jquery.min.js" />
/// <reference path="../lib/require/require.min.js" />
/// <reference path="../lib/jquery-validate/jquery.validate.js" />
define(["jquery", "jquery-validate", "jquery-form", "httpClient","common"], function ($, validate, ajaxform, httpClient,common) {
    const verify = {};
    
    $.validator.setDefaults({
        ignore: ".ignore",//忽略.ignore
        errorClass: "help-block"
    });
    $.validator.messages.regexp = "格式错误,需要{0}的格式";
    $.validator.addMethod("regexp", function (value, element, param) {
        return this.optional(element) || RegExp(param).test(value);
    });
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
                            window[func.success].apply(null, common.getArgs(arguments));
                        }
                    },
                    beforeSubmit: function(){
                        if (func.beforeSubmit) {
                            window[func.beforeSubmit].apply(null, common.getArgs(arguments));
                        }
                    },
                    error: function () {
                        if (func.error) {
                            window[func.error].apply(null, common.getArgs(arguments));
                        } else {
                            debugger;
                            httpClient.defaultError.apply(null, common.getArgs(arguments));
                        }
                    },
                }

                self.ajaxForm(option);
            }
        });
    }

    verify.reload();
    
    return verify;
});
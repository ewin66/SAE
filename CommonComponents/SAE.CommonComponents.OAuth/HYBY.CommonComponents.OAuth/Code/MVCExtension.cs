using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponents.OAuth
{
    #region TagHelper
    /// <summary>
    /// 压缩tag，在debug下将会使用min后缀使用压缩文件。
    /// </summary>
    [HtmlTargetElement("script", Attributes = "asp-min", TagStructure = TagStructure.Unspecified)]
    [HtmlTargetElement("link", Attributes = "asp-min", TagStructure = TagStructure.Unspecified)]
    public class MinTagHelper : TagHelper
    {
        #region Private Member
        private const string Extend = ".min";
        private string url;

        #endregion

        #region Ctor
        public MinTagHelper()
        {
        }
        #endregion

        #region Public Property
        [HtmlAttributeName("src")]
        public string Src
        {
            get => this.url;
            set => this.url = value;
        }
        [HtmlAttributeName("href")]
        public string Href
        {
            get => this.url;
            set => this.url = value;
        }
        #endregion

        #region TagHelper Member
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var attribute = output.TagName.ToLower().CompareTo("link") == 0 ? "href" : "src";
            var path = Path.Combine(Path.GetDirectoryName(this.url),
                       $"{Path.GetFileNameWithoutExtension(this.url)}{Extend}{Path.GetExtension(this.url)}");
            output.Attributes.Add(attribute, path.TrimStart('~').Replace("\\", "/"));
        }
        #endregion
    }

    public class ValidationHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           
            //string result = string.Empty;
            //var modelMetadata = helper.ViewData.ModelMetadata;

            //if (helper.ViewContext.ClientValidationEnabled && !modelMetadata.IsReadOnly)
            //{
            //    StringBuilder sb = new StringBuilder(" ");
            //    foreach (var validator in modelMetadata.GetValidators(helper.ViewContext.Controller.ControllerContext))
            //    {
            //        Dictionary<string, object> dic = new Dictionary<string, object>();

            //        UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(validator.GetClientValidationRules(), dic);
            //        foreach (var node in dic)
            //        {
            //            sb.Append(" ")
            //                .Append(node.Key)
            //                .Append("=")
            //                .Append("\"")
            //                .Append(node.Value)
            //                .Append("\" ");
            //        }
            //    }
                //result = sb.ToString();
            }

            //base.Process(context, output);
        }
    
    #endregion

    #region HttpRequest
    
    public static class HttpRequestExtension
    {
        /// <summary>
        /// 请求是否来自于Ajax
        /// </summary>
        /// <param name="request">请求上下文</param>
        /// <returns>True来自于Ajax的请求</returns>
        public static bool IsAjax(this HttpRequest request) => request?.Headers != null &&
                           request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }

    #endregion

}

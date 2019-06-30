using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SAE.CommonComponent.UI
{
    [HtmlTargetElement("input", Attributes = "serialize")]
    public class SerializeTag : TagHelper
    {
        private readonly IJsonHelper _helper;

        public SerializeTag(IJsonHelper helper)
        {

            this._helper = helper;
        }

        [HtmlAttributeName("serialize")]
        public ModelExpression ModelExpression { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.ModelExpression.Model != null)
            {
                var value = new TagHelperAttribute("value", this._helper.Serialize(this.ModelExpression.Model), HtmlAttributeValueStyle.SingleQuotes);
                output.Attributes.SetAttribute(value);
            }
            output.Attributes.SetAttribute("id", this.ModelExpression.Name);
        }
    }
}
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Store.HtmlHelpers
{
    public static class PagingUtils
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
        PagingInfo pagingInfo,
        Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());

        }

        public static MvcHtmlString TextEditingFormControl<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).PropertyName;

            StringBuilder result = new StringBuilder();
            TagBuilder container = new TagBuilder("div");

            container.MergeAttribute("class", "form-group");

            TagBuilder label = new TagBuilder("label");
            label.InnerHtml = metadata;
            TagBuilder extraTag;

            extraTag = (metadata == "Description") ? new TagBuilder("textarea") : new TagBuilder("input");
            extraTag.MergeAttribute("class", "form-control");
            if (metadata == "Description")
            {
                extraTag.MergeAttribute("rows", "5");
            }
            else
            {
                extraTag.MergeAttribute("type", "text");
            }
            
            var mergedTag = string.Concat(label.ToString(), extraTag.ToString());
            container.InnerHtml = mergedTag;
            result.Append(container.ToString());

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
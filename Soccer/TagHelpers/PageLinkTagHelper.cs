using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Soccer.Models;

namespace Soccer.TagHelpers;

public class PageLinkTagHelper(IUrlHelperFactory urlHelperFactory) : TagHelper
{
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;
    public PageViewModel? PageModel { get; set; }
    public string PageAction { get; set; } = "";

    [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
    public Dictionary<string, object> PageUrlValues { get; set; } = []; // Колекційні вирази C# 12

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (PageModel == null) throw new Exception("PageModel не встановлено");
        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
        output.TagName = "div";

        // Набір посилань буде представляти список ul
        TagBuilder tag = new TagBuilder("ul");
        tag.AddCssClass("pagination");

        // Формуємо три посилання - на поточну, попередню та наступну сторінки
        TagBuilder currentItem = CreateTag(PageModel.PageNumber, urlHelper);

        // Створюємо посилання на попередню сторінку, якщо вона є
        if (PageModel.HasPreviousPage)
        {
            TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper);
            tag.InnerHtml.AppendHtml(prevItem);
        }

        tag.InnerHtml.AppendHtml(currentItem);

        // Створюємо посилання на наступну сторінку, якщо вона є
        if (PageModel.HasNextPage)
        {
            TagBuilder nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper);
            tag.InnerHtml.AppendHtml(nextItem);
        }
        output.Content.AppendHtml(tag);
    }

    private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
    {
        TagBuilder item = new TagBuilder("li");
        TagBuilder link = new TagBuilder("a");
        if (pageNumber == PageModel?.PageNumber)
        {
            item.AddCssClass("active");
        }
        else
        {
            PageUrlValues["page"] = pageNumber;
            link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
        }
        item.AddCssClass("page-item");
        link.AddCssClass("page-link");
        link.InnerHtml.Append(pageNumber.ToString());
        item.InnerHtml.AppendHtml(link);
        return item;
    }
}
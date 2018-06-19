using Cosmo.HtmlDocParser.Parser.Helpers;
using System.Collections.Generic;

namespace Cosmo.HtmlDocParser.Selectors
{
    public interface IHtmlSelectorService
    {
        Maybe<HtmlElement> GetElementById(HtmlDocument source, string id);
        IEnumerable<HtmlElement> GetElementByClass(HtmlDocument source,string className);
        IEnumerable<HtmlElement> GetElement(HtmlDocument source, string element);
        IEnumerable<HtmlElement> GetElementBySelector(HtmlDocument source, string selector);
    }
}

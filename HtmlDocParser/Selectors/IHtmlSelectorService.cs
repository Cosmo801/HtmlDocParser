using Cosmo.HtmlDocParser.Parser.Helpers;
using System.Collections.Generic;

namespace Cosmo.HtmlDocParser.Selectors
{
    public interface IHtmlSelectorService
    {

        Maybe<HtmlElement> GetElementById(IEnumerable<HtmlElement> source, string id);
        IEnumerable<HtmlElement> GetElementByClass(IEnumerable<HtmlElement> source, string className);
        IEnumerable<HtmlElement> GetElement(IEnumerable<HtmlElement> source, string element);
        IEnumerable<HtmlElement> GetElementBySelector(IEnumerable<HtmlElement> source, string selector);
    }
}

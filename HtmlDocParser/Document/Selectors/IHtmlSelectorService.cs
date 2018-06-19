using HtmlDocParser.Document.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Selectors
{
    public interface IHtmlSelectorService
    {
        Maybe<HtmlElement> GetElementById(HtmlDocument source, string id);
        IEnumerable<HtmlElement> GetElementByClass(HtmlDocument source,string className);
        IEnumerable<HtmlElement> GetElement(HtmlDocument source, string element);
        IEnumerable<HtmlElement> GetElementBySelector(HtmlDocument source, string selector);
    }
}

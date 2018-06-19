using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Selectors.SelectorTypes
{
    public interface ISelector
    {
        IEnumerable<HtmlElement> SelectElements(HtmlDocument doc);
        IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source);


    }
}

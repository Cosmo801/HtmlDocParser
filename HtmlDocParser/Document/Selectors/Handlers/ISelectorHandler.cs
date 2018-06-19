using HtmlDocParser.Document.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Selectors.Handlers
{
    public interface ISelectorHandler
    {
        ISelector GetSelector(string[] selectorString);
        ISelectorHandler SetNext(ISelectorHandler handler);
    }
}

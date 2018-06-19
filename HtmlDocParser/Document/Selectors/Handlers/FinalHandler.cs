using System;
using System.Collections.Generic;
using System.Text;
using HtmlDocParser.Document.Selectors.SelectorTypes;

namespace HtmlDocParser.Document.Selectors.Handlers
{
    public class FinalHandler : ISelectorHandler
    {
        public ISelector GetSelector(string[] selectorString)
        {
            return null;
        }

        public ISelectorHandler SetNext(ISelectorHandler handler)
        {
            return null;
        }
    }
}

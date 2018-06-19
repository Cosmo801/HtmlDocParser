using HtmlDocParser.Document.Parser.Helpers;
using HtmlDocParser.Document.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.Handlers
{
    public class ElementHandler : SingleSelectorHandler
    {
        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 1 && HtmlHelpers.IsHtmlElement(selectorString[0])) return new ElementSelector(selectorString[0]);

            return NextHandler.GetSelector(selectorString);
        }
    }
}

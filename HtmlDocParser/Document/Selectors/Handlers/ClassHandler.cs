using HtmlDocParser.Document.Parser.Helpers;
using HtmlDocParser.Document.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.Handlers
{
    public class ClassHandler : SingleSelectorHandler
    {
        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 1 && selectorString[0][0] == '.') return new ClassSelector(selectorString[0].Skip(1).CharsToString());

            return NextHandler.GetSelector(selectorString);
        }
    }
}

using HtmlDocParser.Document.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.Handlers
{
    public class ChildHandler : CombinatorSelectorHandler
    {
        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 3 && selectorString[1] == ">") return new ChildSelector(selectorString[0], selectorString[2]);

            return NextHandler.GetSelector(selectorString);
        }
    }
}

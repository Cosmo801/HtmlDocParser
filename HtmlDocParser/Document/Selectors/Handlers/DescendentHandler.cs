using HtmlDocParser.Document.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.Handlers
{
    public class DescendentHandler : CombinatorSelectorHandler
    {

        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 2) return new DescendentSelector(selectorString[0], selectorString[1]);

            return NextHandler.GetSelector(selectorString);
        }
    }
}

using HtmlDocParser.Document.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.Handlers
{
    public class MultiSelectorHandler : ISelectorHandler
    {
        protected ISelectorHandler NextHandler { get; private set; }

        public ISelector GetSelector(string[] selectorString)
        {
            if(selectorString.Count() > 2) return new MultiSelector(selectorString);

            return NextHandler.GetSelector(selectorString);


        }

        public ISelectorHandler SetNext(ISelectorHandler next)
        {
            NextHandler = next;
            return next;
        }
    }
}

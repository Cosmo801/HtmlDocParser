using HtmlDocParser.Document.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Selectors.Handlers
{
    public abstract  class CombinatorSelectorHandler : ISelectorHandler
    {
        protected ISelectorHandler NextHandler { get; private set; }

        public abstract ISelector GetSelector(string[] selectorString);
        
        public ISelectorHandler SetNext(ISelectorHandler next)
        {
            NextHandler = next;
            return next;
        }
    }
}

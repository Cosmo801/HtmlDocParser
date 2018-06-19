using Cosmo.HtmlDocParser.Selectors.SelectorTypes;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
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

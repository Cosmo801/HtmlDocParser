using Cosmo.HtmlDocParser.Selectors.SelectorTypes;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
{
    public abstract class CombinatorSelectorHandler : ISelectorHandler
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

using Cosmo.HtmlDocParser.Selectors.SelectorTypes;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
{
    public interface ISelectorHandler
    {
        ISelector GetSelector(string[] selectorString);
        ISelectorHandler SetNext(ISelectorHandler handler);
    }
}

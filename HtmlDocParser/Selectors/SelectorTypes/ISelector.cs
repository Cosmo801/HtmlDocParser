using System.Collections.Generic;

namespace Cosmo.HtmlDocParser.Selectors.SelectorTypes
{
    public interface ISelector
    {
        IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source);

    }
}

using Cosmo.HtmlDocParser.Parser.Helpers;
using Cosmo.HtmlDocParser.Selectors.SelectorTypes;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
{
    public class ElementHandler : HandlerBase
    {
        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 1 && HtmlHelpers.IsHtmlElement(selectorString[0])) return new ElementSelector(selectorString[0]);

            return NextHandler.GetSelector(selectorString);
        }
    }
}

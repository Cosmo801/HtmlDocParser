using Cosmo.HtmlDocParser.Parser.Helpers;
using Cosmo.HtmlDocParser.Selectors.SelectorTypes;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
{
    public class IdHandler : HandlerBase
    {
        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 1 && selectorString[0][0] == '#') return new IdSelector(selectorString[0].Skip(1).CharsToString());
            return NextHandler.GetSelector(selectorString);
        }
    }
}

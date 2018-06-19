using System.Linq;
using Cosmo.HtmlDocParser.Parser.Helpers;
using Cosmo.HtmlDocParser.Selectors.SelectorTypes;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
{
    public class ClassHandler : SingleSelectorHandler
    {
        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 1 && selectorString[0][0] == '.') return new ClassSelector(selectorString[0].Skip(1).CharsToString());

            return NextHandler.GetSelector(selectorString);
        }
    }
}

using Cosmo.HtmlDocParser.Selectors.SelectorTypes;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
{
    public class DescendentHandler : HandlerBase
    {

        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 2) return new DescendentSelector(selectorString[0], selectorString[1]);

            return NextHandler.GetSelector(selectorString);
        }
    }
}

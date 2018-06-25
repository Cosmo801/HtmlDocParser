using Cosmo.HtmlDocParser.Selectors.SelectorTypes;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
{
    public class ChildHandler : HandlerBase 
    {
        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() == 3 && selectorString[1] == ">") return new ChildSelector(selectorString[0], selectorString[2]);

            return NextHandler.GetSelector(selectorString);
        }
    }
}

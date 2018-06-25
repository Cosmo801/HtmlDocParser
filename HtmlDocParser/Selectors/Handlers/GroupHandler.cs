using Cosmo.HtmlDocParser.Selectors.Handlers;
using Cosmo.HtmlDocParser.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmo.HtmlDocParser.Selectors.Handlers
{
    public class GroupHandler : HandlerBase
    {
        public override ISelector GetSelector(string[] selectorString)
        {
            if (selectorString.Count() != 3) return NextHandler.GetSelector(selectorString);
            if (selectorString[1] != ",") return NextHandler.GetSelector(selectorString);

            return new GroupSelector(selectorString[0], selectorString[2]);
        }
    }
}

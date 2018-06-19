using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Selectors.SelectorTypes
{
    public class GroupSelector : ISelector
    {
        public IEnumerable<HtmlElement> SelectElements(HtmlDocument doc)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source)
        {
            throw new NotImplementedException();
        }
    }
}

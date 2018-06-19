using Cosmo.HtmlDocParser.Parser.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.SelectorTypes
{
    public class IdSelector : ISelector
    {
        private string _id;

        public IdSelector(string id)
        {
            _id = id;
        }

        public IEnumerable<HtmlElement> SelectElements(HtmlDocument doc)
        {
            var allElements = HtmlHelpers.GetAllElements(doc);

            return allElements.Where(e => e.Attributes.ContainsKey("id"))
                              .Where(e => e.Attributes["id"].Contains(_id));
        }

        public IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source)
        {
            return source.Where(e => e.Attributes.ContainsKey("id"))
                              .Where(e => e.Attributes["id"].Contains(_id));
        }
    }
}

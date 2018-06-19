using HtmlDocParser.Document.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.SelectorTypes
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

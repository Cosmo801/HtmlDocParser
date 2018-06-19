using HtmlDocParser.Document.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.SelectorTypes
{
    public class ElementSelector : ISelector
    {
        private string _element;

        public ElementSelector(string element)
        {
            _element = element;
        }

        public IEnumerable<HtmlElement> SelectElements(HtmlDocument doc)
        {
            var allElements = HtmlHelpers.GetAllElements(doc);

            return allElements.Where(e => e.ElementName == _element);
        }

        public IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source)
        {
            return source.Where(e => e.ElementName == _element);
        }
    }
}

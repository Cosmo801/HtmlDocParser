using Cosmo.HtmlDocParser.Parser.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.SelectorTypes
{
    public class AttributeSelector : ISelector
    {
        private string _attrName;
        private string _attrValue;
        private char _wildCard;
        private HtmlElement _element;
        private string attrName;

        public AttributeSelector(string attrName)
        {
            _attrName = attrName;
        }

        public AttributeSelector(string attrName, string attrValue)
        {
            _attrName = attrName;
            _attrValue = attrValue;
        }

        public AttributeSelector(string attrName, string attrValue, char wildCard)
        {
            _attrName = attrName;
            _attrValue = attrValue;
            _wildCard = wildCard;
        }

        public IEnumerable<HtmlElement> SelectElements(HtmlDocument doc)
        {
            var allElements = HtmlHelpers.GetAllElements(doc);

            if (string.IsNullOrEmpty(_attrValue))
            {
                return allElements.Where(e => e.Attributes.ContainsKey(_attrName));
            }

            if (char.IsWhiteSpace(_wildCard))
            {
                return allElements.Where(e => e.Attributes.ContainsKey(_attrName))
                                  .Where(e => e.Attributes[_attrName].Contains(_attrValue));
                                
            }

            return null;



        }

        public IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source)
        {
            return SelectElements(new HtmlDocument { RootElements = source.ToList() });
        }




 
    }
}

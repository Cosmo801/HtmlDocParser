using HtmlDocParser.Document.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.SelectorTypes
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




        //public IEnumerable<HtmlElement> SelectElements(Html element)
        //{
        //    _element = element;

        //    if(string.IsNullOrEmpty(_attrValue)) return SelectByName(element);
        //    if (_wildCard == '~' || _wildCard == '|' || _wildCard == '^' || _wildCard == '$' || _wildCard == '*') return SelectWithWildCard(_attrName, _attrValue, _wildCard);

        //   return SelectWithValue(_attrName, _attrValue);
        //}

        //private IEnumerable<HtmlElement> SelectWithValue(string attrName, string attrValue)
        //{
        //    var elements = SelectByName(_element);

        //    if (elements.Count() == 0) return elements;

        //    return elements.Where(e => e.Attributes[attrName].Contains(attrValue));

        //}

        //private IEnumerable<HtmlElement> SelectWithWildCard(string attrName, string attrValue, char wildCard)
        //{
        //    throw new NotImplementedException();
        //}

        //private IEnumerable<HtmlElement> SelectByName(HtmlElement element)
        //{
        //    var eleList = HtmlHelpers.GetElementAndChildren(_element);

        //    return eleList.Where(e => e.Attributes.ContainsKey(attrName));

        //}
    }
}

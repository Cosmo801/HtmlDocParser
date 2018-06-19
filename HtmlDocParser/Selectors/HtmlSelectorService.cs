using Cosmo.HtmlDocParser.Config;
using Cosmo.HtmlDocParser.Parser.Helpers;
using Cosmo.HtmlDocParser.Selectors.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors
{
    public class HtmlSelectorService : IHtmlSelectorService
    {
        public HtmlSelectorService()
        {
            _htmlConfig = new DebugHtmlConfig();
            
        }

        private IHtmlConfig _htmlConfig;
     

       

        public IEnumerable<HtmlElement> GetElement(HtmlDocument source, string element)
        {
            if (source == null) throw new ArgumentException(nameof(HtmlElement));
            if (!_htmlConfig.IsHtmlElement(element)) throw new ArgumentException("element is invalid");

            var allElements = HtmlHelpers.GetAllElements(source);

            return allElements.Where(e => e.ElementName == element);

        }

        public IEnumerable<HtmlElement> GetElementByClass(HtmlDocument source, string className)
        {
            if (source == null) throw new ArgumentException(nameof(HtmlElement));

            var eleList = HtmlHelpers.GetAllElements(source);
            var selectedElements = new List<HtmlElement>();

            foreach(var element in eleList)
            {
                if (!element.Attributes.ContainsKey("class")) continue;
                if (element.Attributes["class"].Contains(className))
                {
                    selectedElements.Add(element);
                }
            }

            return selectedElements;
        }

        public Maybe<HtmlElement> GetElementById(HtmlDocument source, string id)
        {
            if (source == null) throw new ArgumentException(nameof(HtmlElement));

            var eleList = HtmlHelpers.GetAllElements(source);

            foreach(var element in eleList)
            {
                if (!element.Attributes.ContainsKey("id")) continue;
                if (element.Attributes["id"].Contains(id)) return new Maybe<HtmlElement>(element);
            }

            return new Maybe<HtmlElement>();
        }

        public IEnumerable<HtmlElement> GetElementBySelector(HtmlDocument source, string selectorString)
        {

            if (source == null) throw new ArgumentNullException();
            if (selectorString == null) throw new ArgumentNullException();

            var selectorsArray = selectorString.Split(null);
            if (selectorsArray.Count() == 0) throw new ArgumentException("Invalid selector");

            var handlers = HandlerFactory.GetHandlers();

            var selector = handlers.GetSelector(selectorsArray);


            return selector.SelectElements(source);

            
           

        }

        

        private bool IsSelectorPattern(string leftSide, string rightSide)
        {
            if(leftSide == ">" || leftSide == "+" || leftSide == "," || leftSide == "~") return true;
            if (rightSide == ">" || rightSide == "+" || rightSide == "," || rightSide == "~") return true;

            return false;
        }

        
    }
}

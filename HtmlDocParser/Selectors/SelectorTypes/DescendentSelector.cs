using Cosmo.HtmlDocParser.Parser.Helpers;
using Cosmo.HtmlDocParser.Selectors.Factory;
using System.Collections.Generic;

namespace Cosmo.HtmlDocParser.Selectors.SelectorTypes
{
    public class DescendentSelector : ISelector
    {
        private string _leftSide;
        private string _rightSide;

        public DescendentSelector(string leftSide, string rightSide)
        {
            _leftSide = leftSide;
            _rightSide = rightSide;
        }

        public IEnumerable<HtmlElement> SelectElements(HtmlDocument doc)
        {
            
            var allElements = HtmlHelpers.GetAllElements(doc);
            return SelectElements(allElements);


        }

        public IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source)
        {
            var handlers = HandlerFactory.GetHandlers();


            var allSelectedElements = new List<HtmlElement>();

            var leftSelector = handlers.GetSelector(new string[] { _leftSide });

            //right selector error
            var rightSelector = handlers.GetSelector(new string[] { _rightSide });

            var leftSelectedElements = leftSelector.SelectElements(source);
           

            foreach(var element in leftSelectedElements)
            {
                var descendents = HtmlHelpers.GetElementAndDescendents(element);
                var selectedElements = rightSelector.SelectElements(descendents);

                allSelectedElements.AddRange(selectedElements);
            }

            return allSelectedElements;
        }

      
    }
}

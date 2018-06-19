using HtmlDocParser.Document.Parser.Helpers;
using HtmlDocParser.Document.Selectors.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.SelectorTypes
{
    public class ChildSelector : ISelector
    {
        private string _leftSide;
        private string _rightSide;

        public ChildSelector(string leftSide, string rightSide)
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
            //fix this

            var handlers = HandlerFactory.GetHandlers();
            var leftSelector = handlers.GetSelector(new string[] { _leftSide });
            var rightSelector = handlers.GetSelector(new string[] { _rightSide });

            var leftSelectedElements = GetLeftSelected(source, leftSelector);
      
            var rightSelectedElements = new List<HtmlElement>();


            foreach (var element in leftSelectedElements)
            {
                var rightSelected = rightSelector.SelectElements(element.Children);
                rightSelectedElements.AddRange(rightSelected);
            }

            return rightSelectedElements;
        }

        private IEnumerable<HtmlElement> GetLeftSelected(IEnumerable<HtmlElement> source, ISelector selector)
        {
            var allSelected = new List<HtmlElement>();

            foreach(var element in source)
            {
                var descendents = HtmlHelpers.GetElementAndDescendents(element);
                var selectedElements = selector.SelectElements(descendents);

                allSelected.AddRange(selectedElements);
            }

            return allSelected;
        }
    }
}

using Cosmo.HtmlDocParser.Parser.Helpers;
using Cosmo.HtmlDocParser.Selectors.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.SelectorTypes
{
    public class GroupSelector : ISelector
    {
        private string _leftSide;
        private string _rightSide;

        public GroupSelector(string leftSide, string rightSide)
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

            var leftSelector = handlers.GetSelector(new string[] { _leftSide });
            var rightSelector = handlers.GetSelector(new string[] { _rightSide });

            var leftSelected = leftSelector.SelectElements(source);

            var rightSelected = rightSelector.SelectElements(source);

            return leftSelected.Union(rightSelected);
        }
    }
}

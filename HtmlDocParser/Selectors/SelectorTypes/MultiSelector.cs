using Cosmo.HtmlDocParser.Parser.Helpers;
using Cosmo.HtmlDocParser.Selectors.Factory;
using System.Collections.Generic;
using System.Linq;

namespace Cosmo.HtmlDocParser.Selectors.SelectorTypes
{
    public class MultiSelector : ISelector
    {
        private string[] _selectors;

        public MultiSelector(string[] selectors)
        {
            _selectors = selectors;
        }


        private IEnumerable<ISelector> GetSelectors(string[] selectors)
        {
            var handlers = HandlerFactory.GetHandlers();
            var selectorObjs = new List<ISelector>();



            for (int i = 0; i < selectors.Length; i++)
            {
                var leftSide = selectors[i];

                if (selectors.Length < i + 2) break;

                var rightSide = selectors[i + 1];

                if(leftSide != ">" && rightSide != ">")
                {
                    var selector = handlers.GetSelector(selectors.Skip(i).Take(2).ToArray());
                    selectorObjs.Add(selector);

                    continue;
                }

                if (leftSide == ">") continue;
                
                if(rightSide == ">")
                {
                    if (selectors.Length < i + 3) break;
                    var selector = handlers.GetSelector(selectors.Skip(i).Take(3).ToArray());
                    selectorObjs.Add(selector);
                }


              

            }

            return selectorObjs;
        }

        public IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source)
        {
            
            var selectors = GetSelectors(_selectors);
            foreach (var selector in selectors)
            {
                var tempElements = selector.SelectElements(source);
                source = tempElements;
            }

            return source;
        }
    }
}

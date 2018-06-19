using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlDocParser.Config;
using HtmlDocParser.Document.Parser;
using HtmlDocParser.Document.Parser.Helpers;
using HtmlDocParser.Document.Parser.Html;
using HtmlDocParser.Document.Selectors.Factory;
using HtmlDocParser.Document.Selectors.SelectorTypes;

namespace HtmlDocParser.Document.Selectors
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

            //var eleList = GetElementAndChildren(source);
            //var selectors = GetSelectorsFromString(selectorsArray);

            //if (selectors.Count() == 0) throw new Exception();


            ////Treat HtmlElement as IEnumerable of HtmlElement ?
            ////IEnumerable<HtmlElement> result;

            //var result = selectors[0].SelectElements(source);


            //foreach(var d in selectors.Skip(1))
            //{
            //    result = d.SelectElements(result);
            //}

            //return result;

            return null;


        }

        //Refactor with design pattern
        //private List<ICssSelector> GetSelectorsFromString(string[] selectors)
        //{
        //    if(selectors.Length == 1)
        //    {
        //        var selector = GetSingleSelector(selectors.First());

        //        return new List<ICssSelector> { selector };
        //    }

        //    var selectorsList = new List<ICssSelector>();

        //    //Fix this

        //    for (int i = 0; i < selectors.Length; i++)
        //    {
        //        //Descendent selector

        //        if (selectors.Length < i + 2) break;

        //        var leftSide = selectors[i];
        //        var rightSide = selectors[i + 1];

        //        if (IsSelectorPattern(leftSide, rightSide) == false)
        //        {
        //            selectorsList.Add(new DescendentSelector(leftSide, rightSide));
        //            continue;
        //        }


        //        //Other selectors

        //        if (i == 0 && leftSide == ">" || leftSide == "+" || leftSide == "," || leftSide == "~") break;

        //        if(leftSide == ">")
        //        {
        //            selectorsList.Add(new ChildSelector(selectors[i -1], selectors[i +1]));
        //            i++;
        //            continue;
        //        }
        //        if (leftSide == "+")
        //        {
        //            //selectorsList.Add(new ChildSelector());
        //            continue;
        //        }
        //        if (leftSide == "~")
        //        {
        //            //selectorsList.Add(new ChildSelector());
        //            continue;
        //        }
        //        if (leftSide == ",")
        //        {
        //            //selectorsList.Add(new ChildSelector());
        //            continue;
        //        }

        //        if (rightSide == ">")
        //        {
        //            selectorsList.Add(new ChildSelector(selectors[i], selectors[i +2]));
        //            i = i + 2;
        //            continue;
        //        }
        //        if (rightSide == "+")
        //        {
        //            //selectorsList.Add(new ChildSelector());
        //            continue;
        //        }
        //        if (rightSide == "~")
        //        {
        //            //selectorsList.Add(new ChildSelector());
        //            continue;
        //        }
        //        if (rightSide == ",")
        //        {
        //            //selectorsList.Add(new ChildSelector());
        //            continue;
        //        }


        //    }


        //    return selectorsList;

            
        //}

        private bool IsSelectorPattern(string leftSide, string rightSide)
        {
            if(leftSide == ">" || leftSide == "+" || leftSide == "," || leftSide == "~") return true;
            if (rightSide == ">" || rightSide == "+" || rightSide == "," || rightSide == "~") return true;

            return false;
        }

        //private ICssSelector GetSingleSelector(string selector)
        //{
        //    //Element selector
        //    if (_htmlConfig.IsHtmlElement(selector)) return new ElementSelector(selector);

        //    if (selector.Length < 2) throw new ArgumentException();

        //    //Id Selector

        //    if(selector[0] == '#')
        //    {
        //        return new IdSelector(selector.Skip(1).CharsToString());
        //    }

        //    //Class Selector

        //    if(selector[0] == '.')
        //    {
        //        return new ClassSelector(selector.Skip(1).CharsToString());
        //    }

        //    //Attribute Selector
        //    if (selector[0] == '[')
        //    {

        //        var closeBracketFinder = new Regex("]");
        //        var closeBracketMatch = closeBracketFinder.Match(selector);

        //        if (!closeBracketMatch.Success) throw new ArgumentException(); 

        //        var equalsFinder = new Regex("=");
        //        var equalsMatch = equalsFinder.Match(selector);

        //        if (!equalsMatch.Success) return new AttributeSelector(selector.Take(closeBracketMatch.Index)
        //                                                                       .Skip(1)
        //                                                                       .CharsToString());


        //        var charBeforeEquals = selector[equalsMatch.Index - 1];
        //        if (!char.IsLetter(charBeforeEquals))
        //        {
        //            return new AttributeSelector(selector.Take(equalsMatch.Index)
        //                                                 .Skip(1)
        //                                                 .CharsToString(), 
                                                         
        //                                          selector.Skip(equalsMatch.Index + 1)
        //                                                  .Take(closeBracketMatch.Index - (equalsMatch.Index + 1))
        //                                                  .CharsToString());
        //        }

        //        return new AttributeSelector(selector.Take(equalsMatch.Index - 1)
        //                                             .Skip(1)
        //                                             .CharsToString(),

        //                                     selector.Skip(equalsMatch.Index + 1)
        //                                             .Take(closeBracketMatch.Index - (equalsMatch.Index + 1))
        //                                             .CharsToString(),

        //                                     $"{charBeforeEquals}");

                                                     
        //    }

        //    //Pseudo Selectors
        //    var colonFinder = new Regex(":");
        //    var colonMatch = colonFinder.Match(selector);

        //    if(colonMatch.Success && selector[colonMatch.Index + 1] != ':')
        //    {
        //        return new PseudoClassSelector(selector.Take(colonMatch.Index).CharsToString(), selector.Skip(colonMatch.Index + 1).CharsToString());
        //    }
        //    if (colonMatch.Success && selector[colonMatch.Index + 1] == ':')
        //    {
        //        return new PseudoElementSelector(selector.Take(colonMatch.Index).CharsToString(), selector.Skip(colonMatch.Index + 2).CharsToString());
        //    }


        //    throw new ArgumentException("No selector found");



        //}
    }
}

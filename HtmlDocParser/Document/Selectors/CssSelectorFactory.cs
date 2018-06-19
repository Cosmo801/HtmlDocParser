using HtmlDocParser.Config;
using HtmlDocParser.Document.Parser.Helpers;
using HtmlDocParser.Document.Selectors.SelectorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlDocParser.Document.Selectors
{
    public static class CssSelectorFactory
    {
        


        //public static ICssSelector GetSelector(IEnumerable<string> selectorString)
        //{
        //    if (selectorString.Count() == 1) return GetSingleSelector(selectorString.First());

        //    return null;
        //}

        //private static ICssSelector GetSingleSelector(string selector)
        //{
        //    if (HtmlHelpers.IsHtmlElement(selector)) return new ElementSelector(selector);

        //    var attributeSelectorRegex = new Regex("[");
        //    var pseudoSelectorRegex = new Regex(":");

        //    var attributeSelectorMatch = attributeSelectorRegex.Match(selector);
        //    var pseudoSelectorMatch = pseudoSelectorRegex.Match(selector);

        //    if(attributeSelectorMatch.Success == false && pseudoSelectorMatch.Success == false)
        //    {
        //        if (selector.First() == '#') return new IdSelector(selector.Skip(1).CharsToString());
        //        if (selector.First() == '.') return new ClassSelector(selector.Skip(1).CharsToString());

        //        throw new Exception();
        //    }
        //    if(attributeSelectorMatch.Success && attributeSelectorMatch.Index == 0) return GetAttributeSelector(selector);
        //    if(pseudoSelectorMatch.Success && pseudoSelectorMatch.Index == 0) return GetPseudoSelector(selector);


            

           
            



        //    throw new NotImplementedException();
        //}

        //private static ICssSelector GetPseudoSelector(string selector)
        //{
        //    if (selector[1] == ':') return new PseudoElementSelector(selector.Skip(2).CharsToString());
        //    return new PseudoClassSelector(selector.Skip(1).CharsToString());
        //}

        //private static ICssSelector GetAttributeSelector(string selector)
        //{
        //    var closeFinder = new Regex("]");
        //    var closeMatch = closeFinder.Match(selector);

        //    if (closeMatch.Success == false) throw new Exception();

        //    var equalsFinder = new Regex("=");
        //    var equalsMatch = equalsFinder.Match(selector);

        //    if (equalsMatch.Success == false) return new AttributeSelector(selector.Take(selector.Count() - 1)
        //                                                                           .Skip(1)
        //                                                                           .CharsToString());

        //    var wildCard = selector[equalsMatch.Index - 1];
        //    if (char.IsLetter(wildCard)) return new AttributeSelector(selector.Take(equalsMatch.Index - 1)
        //                                                                      .Skip(1)
        //                                                                      .CharsToString(),

        //                                                              selector.Skip(equalsMatch.Index + 1)
        //                                                                      .Take(selector.Count() - (equalsMatch.Index + 2))
        //                                                                      .CharsToString());

        //    return new AttributeSelector(selector.Take(equalsMatch.Index - 1)
        //                                         .Skip(1)
        //                                         .CharsToString(),

        //                                 selector.Skip(equalsMatch.Index + 1)
        //                                         .Take(selector.Count() - (equalsMatch.Index + 2))
        //                                         .CharsToString(),

        //                                 selector.Skip(equalsMatch.Index - 1)
        //                                         .CharsToString());

                                                 
                                                 
                                                
                                                                                     
                                                                                  

        //}
    }
}

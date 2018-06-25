using Cosmo.HtmlDocParser.Config;
using Cosmo.HtmlDocParser.Parser.Helpers;
using Cosmo.HtmlDocParser.Selectors.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

            //Fix this for invalid spacing etc



            if (source == null) throw new ArgumentNullException();
            if (selectorString == null) throw new ArgumentNullException();

            var selectorsArray = SplitExpressions(selectorString);

            if (selectorsArray.Count() == 0) throw new ArgumentException("Invalid selector");

            var handlers = HandlerFactory.GetHandlers();

            var selector = handlers.GetSelector(selectorsArray);


            return selector.SelectElements(source);

            
           

        }

        private string[] SplitExpressions(string selectorString)
        {

        

            var expressionStringBuilder = new StringBuilder();
            var expressionStringList = new List<string>();

            while(selectorString.Length > 0)
            {

                var currentChar = selectorString.First();

                if(currentChar == '[')
                {
                    var closeAttrFinder = Regex.Match(selectorString, "]");

                    if(closeAttrFinder.Success == false)
                    {
                        expressionStringBuilder.Append(currentChar);
                        selectorString = selectorString.Remove(0, 1);
                    }
                    else
                    {
                        var attrSubstring = selectorString.Substring(0, closeAttrFinder.Index);
                        expressionStringList.Add(attrSubstring);

                        selectorString = selectorString.Remove(0, attrSubstring.Length);
                    }

                    continue;

                }
                if (char.IsWhiteSpace(currentChar))
                {
                    if(expressionStringBuilder.Length == 0)
                    {
                        selectorString = selectorString.Remove(0, 1);
                        continue;
                    }


                    expressionStringList.Add(expressionStringBuilder.ToString());
                    expressionStringBuilder = new StringBuilder();

                    selectorString = selectorString.Remove(0, 1);

                    continue;
                }
                if (currentChar == '>' || currentChar == ',' || currentChar == '~' || currentChar == '+')
                {
                    if(expressionStringBuilder.Length == 0)
                    {
                        expressionStringList.Add(currentChar.ToString());

                        selectorString = selectorString.Remove(0, 1);

                        continue;
                    }


                    expressionStringList.Add(expressionStringBuilder.ToString());
                    expressionStringBuilder = new StringBuilder();

                    expressionStringList.Add(currentChar.ToString());

                    selectorString = selectorString.Remove(0, 1);          

                    continue;
                }

                expressionStringBuilder.Append(currentChar);

                selectorString = selectorString.Remove(0, 1);
                continue;


            }

            expressionStringList.Add(expressionStringBuilder.ToString());


            return expressionStringList.ToArray();

        }




        

            

        

        private bool IsSelectorPattern(string leftSide, string rightSide)
        {
            if(leftSide == ">" || leftSide == "+" || leftSide == "," || leftSide == "~") return true;
            if (rightSide == ">" || rightSide == "+" || rightSide == "," || rightSide == "~") return true;

            return false;
        }

        
    }
}

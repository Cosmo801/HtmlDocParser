using Cosmo.HtmlDocParser.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cosmo.HtmlDocParser.Parser.Helpers
{
    public static class HtmlHelpers
    {
        private static IHtmlConfig _config;

        static HtmlHelpers()
        {
            _config = new DebugHtmlConfig();
        }

        public static bool HasChildren(this HtmlElement htmlElement)
        {

            //Opening
            var openPatternOne = new Regex("<");
            var openPatternTwo = new Regex(">");

            var openMatchOne = openPatternOne.Match(htmlElement.InnerText);
            if (!openMatchOne.Success) return false;

            var openMatchTwo = openPatternTwo.Match(htmlElement.InnerText);
            if (!openMatchTwo.Success) return false;

            //Get Result
            var openIndexCount = (openMatchTwo.Index -1 ) - (openMatchOne.Index );
            var openingTag = htmlElement.InnerText.Substring(openMatchOne.Index + 1, openIndexCount);

            

            var htmlName = openingTag.Split(null).Where(o => o != string.Empty).First();

            var closePattern = $@"</{htmlName}>";
            var closeRegex = new Regex(closePattern);

            var closeMatch = closeRegex.Match(htmlElement.InnerText);

            return closeMatch.Success;
        }

        public static bool IsHtmlElement(string element)
        {
            if (element == null) throw new ArgumentNullException();

            return _config.IsHtmlElement(element);

        }

        public static bool IsEmptyElement(string element)
        {
            return _config.IsEmptyElement(element);
        }
      
        public static IEnumerable<HtmlElement> GetElementAndDescendents(HtmlElement source)
        {
            if (source == null) throw new ArgumentNullException(nameof(HtmlElement));
           

            var eleList = new List<HtmlElement> { source };

            if (source.Children == null) return eleList;


            foreach (var child in source.Children)
            {
                var childAndDescendents = GetElementAndDescendents(child);
                eleList.AddRange(childAndDescendents);
            }

            return eleList;

        }

        public static IEnumerable<HtmlElement> GetAllElements(IEnumerable<HtmlElement> elements)
        {
            if (elements == null) throw new ArgumentNullException(nameof(IEnumerable<HtmlElement>));

            var eleList = new List<HtmlElement>();
            if (elements.Count() == 0) return eleList;

            foreach(var rootElement in elements)
            {
                var allElements = GetElementAndDescendents(rootElement);
                eleList.AddRange(allElements);

            }

            return eleList;
            
        }

        public static List<HtmlElement> GetElementDescendents(HtmlElement source)
        {
            if (source == null) throw new ArgumentNullException(nameof(HtmlElement));

            var eleList = new List<HtmlElement>();

            if (source.Children.Count() == 0) return eleList;

            foreach (var child in source.Children)
            {
                var childAndDescendents = GetElementAndDescendents(child);
                eleList.AddRange(childAndDescendents);
            }

            return eleList;
        }

        public static List<HtmlElement> GetElementAndChildren(HtmlElement source)
        {
            if (source == null) throw new ArgumentNullException(nameof(HtmlElement));

            var eleList = new List<HtmlElement>
            {
                source
            };

            eleList.AddRange(source.Children);

            return eleList;


        }

        public static List<HtmlElement> GetElementChildren(HtmlElement source)
        {
            if (source == null) throw new ArgumentNullException(nameof(HtmlElement));

            return source.Children.ToList();
        }

        public static string RemoveEscapeCharacters(string text)
        {
            var escapes = new string[] { "\\", "\n", "\r", "\t"};

            foreach(var escape in escapes)
            {              
                while (true)
                {                   
                    if (text.IndexOf(escape) == -1) break;
                    text = text.Replace(escape, string.Empty);
                    
                }

            }



            return text;
            



            


        }
    }
}

using HtmlDocParser.Document.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Selectors
{
    public static class SelectorExtensions
    {
        private static HtmlSelectorService _selectorService;

        static SelectorExtensions()
        {
            _selectorService = new HtmlSelectorService();
        }

        //Element Extensions

        public static IEnumerable<HtmlElement> GetElementsByElement(this HtmlElement source, string elementName)
        {
            return _selectorService.GetElement(new HtmlDocument { RootElements = new List<HtmlElement> { source } }, elementName);
        }

        public static Maybe<HtmlElement> GetElementById(this HtmlElement source, string id)
        {
            return _selectorService.GetElementById(new HtmlDocument { RootElements = new List<HtmlElement> { source } }, id);
        }

        public static IEnumerable<HtmlElement> GetElementByClass(this HtmlElement source, string className)
        {
            return _selectorService.GetElementByClass(new HtmlDocument { RootElements = new List<HtmlElement> { source } }, className);
        }

        public static IEnumerable<HtmlElement> GetElementBySelector(this HtmlElement source, string selector)
        {
            return _selectorService.GetElementBySelector(new HtmlDocument { RootElements = new List<HtmlElement> { source } }, selector);
        }


        //Document Extensions

        public static IEnumerable<HtmlElement> GetElementsByElement(this HtmlDocument source, string elementName)
        {
            return _selectorService.GetElement(source, elementName);
        }

        public static Maybe<HtmlElement> GetElementById(this HtmlDocument source, string id)
        {
            return _selectorService.GetElementById(source, id);
        }

        public static IEnumerable<HtmlElement> GetElementByClass(this HtmlDocument source, string className)
        {
            return _selectorService.GetElementByClass(source, className);
        }

        public static IEnumerable<HtmlElement> GetElementBySelector(this HtmlDocument source, string selector)
        {
            return _selectorService.GetElementBySelector(source, selector);
        }



    }
}

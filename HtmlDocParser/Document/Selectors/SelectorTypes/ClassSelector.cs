﻿using HtmlDocParser.Document.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Selectors.SelectorTypes
{
    public class ClassSelector : ISelector
    {
        private string _className;

        public ClassSelector(string className)
        {
            _className = className;
        }

        public IEnumerable<HtmlElement> SelectElements(HtmlDocument doc)
        {
            var allElements = HtmlHelpers.GetAllElements(doc);

            return allElements.Where(e => e.Attributes.ContainsKey("class"))
                              .Where(e => e.Attributes["class"].Contains(_className));
        }

        public IEnumerable<HtmlElement> SelectElements(IEnumerable<HtmlElement> source)
        {
            return source.Where(e => e.Attributes.ContainsKey("class"))
                              .Where(e => e.Attributes["class"].Contains(_className));
        }
    }
}
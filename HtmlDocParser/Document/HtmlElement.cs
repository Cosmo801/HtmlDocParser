using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document
{
    public class HtmlElement
    {
        public HtmlElement()
        {

        }


        public HtmlElement(string elementName)
        {
            ElementName = elementName;
        }

        public HtmlElement(string elementName, string innerText)
        {
            ElementName = elementName;
            InnerText = innerText;
        }

        public Dictionary<string, List<string>> Attributes { get; set; }

        public string ElementName { get; set; }

        public string InnerText { get; set; }

        public string InnerTextParsed { get; set; }

        internal int DocumentStartIndex { get; set; }

        internal int DocumentEndIndex { get; set; }

        public IEnumerable<HtmlElement> Children { get; set; }

        public HtmlElement Parent { get; set; }

        

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Parser.Html
{
    public class HtmlElementGetResult
    {
        public HtmlElement Element { get; set; }
        public bool Success { get; set; }
        public string ParsedParentText { get; set; }

    }
}

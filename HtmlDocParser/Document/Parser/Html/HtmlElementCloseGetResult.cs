using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Parser.Html
{
    public class HtmlElementCloseGetResult
    {
        public bool Success { get; set; }
        public string ElementName { get; set; }
        public int CloseTagOpenIndex { get; set; }
        public int CloseTagCloseIndex { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Parser.Html
{
    public class HtmlElementOpenGetResult
    {
        public bool Success { get; set; }
        public string ElementName { get; set; }
        public int OpeningTagOpenIndex { get; set; }
        public int OpeningTagCloseIndex { get; set; }


        


    }
}

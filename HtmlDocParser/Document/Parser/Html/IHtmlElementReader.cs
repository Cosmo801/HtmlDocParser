using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Parser.Html
{
    public interface IHtmlElementReader
    {
        HtmlElement GetElement(string text);
    }
}

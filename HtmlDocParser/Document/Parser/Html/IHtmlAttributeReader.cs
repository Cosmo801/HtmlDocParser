using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Parser.Html
{
    public interface IHtmlAttributeReader
    {

        void GetAttributes(HtmlElement source, string attributeString);

    }
}

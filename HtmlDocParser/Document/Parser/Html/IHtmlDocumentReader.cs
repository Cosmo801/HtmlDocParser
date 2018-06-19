using HtmlDocParser.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Document.Parser.Html
{
    public interface IHtmlDocumentReader
    {
        HtmlDocument ParseDocument(string path);
        
    }
}

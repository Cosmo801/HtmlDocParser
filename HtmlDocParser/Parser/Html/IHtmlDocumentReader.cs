using System.Collections.Generic;

namespace Cosmo.HtmlDocParser.Parser.Html
{
    public interface IHtmlDocumentReader
    {
        //HtmlDocument ParseDocument(string path);
        IEnumerable<HtmlElement> ParseDocumentFromPath(string path);
        IEnumerable<HtmlElement> ParseDocumentFromRawHtml(string html);
        
    }
}

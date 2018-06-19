namespace Cosmo.HtmlDocParser.Parser.Html
{
    public interface IHtmlDocumentReader
    {
        HtmlDocument ParseDocument(string path);
        
    }
}

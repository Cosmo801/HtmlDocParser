namespace Cosmo.HtmlDocParser.Config
{
    public interface IHtmlConfig
    {
        bool IsHtmlElement(string elementName);
        bool IsEmptyElement(string elementName);

    }
}

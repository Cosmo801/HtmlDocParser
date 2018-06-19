using System.Threading.Tasks;

namespace Cosmo.HtmlDocParser.Parser.Getter
{
    public interface IHtmlDocumentGetter
    {
        Task<string> GetHtmlAsync();
        string GetHtml();
    }
}

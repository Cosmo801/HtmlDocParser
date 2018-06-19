using HtmlDocParser.Document;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HtmlDocParser.Document.Parser.Getter
{
    public interface IHtmlDocumentGetter
    {
        Task<string> GetHtmlAsync();
        string GetHtml();
    }
}

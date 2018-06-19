using System.IO;
using System.Threading.Tasks;

namespace Cosmo.HtmlDocParser.Parser.Getter
{
    public class FileHtmlDocumentGetter : IHtmlDocumentGetter
    {
        private string _path;

        public FileHtmlDocumentGetter(string path)
        {
            _path = path;
        }


        public string GetHtml()
        {
            return GetHtmlAsync().Result;
        }

        public async Task<string> GetHtmlAsync()
        {
            using(var textReader = new StreamReader(_path))
            {
                return await textReader.ReadToEndAsync();
            }
        }
    }
}

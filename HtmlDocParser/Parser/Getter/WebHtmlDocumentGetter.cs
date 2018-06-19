using System.Net.Http;
using System.Threading.Tasks;

namespace Cosmo.HtmlDocParser.Parser.Getter
{
    public class WebHtmlDocumentGetter : IHtmlDocumentGetter
    {
        private string _path;
        private HttpClient _client;

        public WebHtmlDocumentGetter(string path)
        {
            _path = path;
            _client = new HttpClient();
        }


        public string GetHtml()
        {
            return GetHtmlAsync().Result;
        }

        public async Task<string> GetHtmlAsync()
        {
            var response = await _client.GetAsync(_path).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return content;

        }
    }
}

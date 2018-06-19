﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlDocParser.Document;

namespace HtmlDocParser.Document.Parser.Getter
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

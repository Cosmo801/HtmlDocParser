using System;
using System.IO;

namespace Cosmo.HtmlDocParser.Parser.Getter
{
    public class HtmlDocumentGetterFactory
    {
        //Make is valid web address more tolerant


        public static IHtmlDocumentGetter GetDocumentGetter(string path)
        {
            if (IsFile(path)) return new FileHtmlDocumentGetter(path);
            if (IsValidWebAddress(path)) return new WebHtmlDocumentGetter(path);

            throw new ArgumentException("HTML file not found, if you are using a web address please include the full address incuding http");

        }

        private static bool IsValidWebAddress(string path)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(path, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;

        }

        private static bool IsFile(string path)
        {
            return File.Exists(path);
        }
    }
}

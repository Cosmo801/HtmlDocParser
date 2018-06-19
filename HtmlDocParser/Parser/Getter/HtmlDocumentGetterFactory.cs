using System;

namespace Cosmo.HtmlDocParser.Parser.Getter
{
    public class HtmlDocumentGetterFactory
    {

        public static IHtmlDocumentGetter GetDocumentGetter(string path)
        {
            if (IsValidWebAddress(path)) return new WebHtmlDocumentGetter(path);

            return new FileHtmlDocumentGetter(path);
        }

        private static bool IsValidWebAddress(string path)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(path, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;

        }
    }
}

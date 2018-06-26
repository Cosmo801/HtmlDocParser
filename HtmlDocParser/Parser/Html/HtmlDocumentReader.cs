using Cosmo.HtmlDocParser.Parser.Getter;
using System.Collections.Generic;

namespace Cosmo.HtmlDocParser.Parser.Html
{
    public class HtmlDocumentReader : IHtmlDocumentReader
    {
        private IHtmlElementReader _elementReader;

        public HtmlDocumentReader()
        {
            _elementReader = new HtmlElementReader();
        }

        public IEnumerable<HtmlElement> ParseDocumentFromPath(string path)
        {
            var htmlText = HtmlDocumentGetterFactory.GetDocumentGetter(path).GetHtml();
            return ParseDocumentFromRawHtml(htmlText);
        }

        public IEnumerable<HtmlElement> ParseDocumentFromRawHtml(string html)
        {
            var elementList = new List<HtmlElement>();


            while (true)
            {
                var currentElement = _elementReader.GetElement(html);
                if (currentElement == null) break;

                elementList.Add(currentElement);

                html = html.Remove(currentElement.DocumentStartIndex, currentElement.DocumentEndIndex - currentElement.DocumentStartIndex);
            }

            return elementList;
        }

     
           

        


     
    }
}

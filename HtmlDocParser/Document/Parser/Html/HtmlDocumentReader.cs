using System;
using System.Collections.Generic;
using System.Text;
using HtmlDocParser.Document;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using HtmlDocParser.Document.Parser.Helpers;
using HtmlDocParser.Config;
using HtmlDocParser.Document.Parser.Getter;

namespace HtmlDocParser.Document.Parser.Html
{
    public class HtmlDocumentReader : IHtmlDocumentReader
    {
        private IHtmlElementReader _elementReader;

        public HtmlDocumentReader()
        {

            _elementReader = new NewElementReader();
        }

        public HtmlDocument ParseDocument(string path)
        {
            var docString = HtmlDocumentGetterFactory.GetDocumentGetter(path).GetHtml();
            var rootElements = new List<HtmlElement>();
            

            while (true)
            {
                var element = _elementReader.GetElement(docString);
                if (element == null) break;

                docString = docString.Remove(element.DocumentStartIndex, element.DocumentEndIndex - element.DocumentStartIndex);

                rootElements.Add(element);

            }

            return new HtmlDocument
            {
                RootElements = rootElements
            };
        }


           

        


     
    }
}


using Cosmo.HtmlDocParser.Parser.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cosmo.HtmlDocParser.Parser.Html
{
    public class NewElementReader : IHtmlElementReader
    {
        private HtmlAttributeReader _attributeReader;

        public NewElementReader()
        {
            _attributeReader = new HtmlAttributeReader();
        }

        private HtmlElementGetResult GetElementResult(string text)
        {
            var empty = GetEmptyElement(text);
            if(empty.Count() == 1)
            {
                var emptyEle = empty.First();
                var parsedParentText = text.Remove(emptyEle.DocumentStartIndex, emptyEle.DocumentEndIndex - emptyEle.DocumentStartIndex);
                return new HtmlElementGetResult { Success = true, Element = emptyEle, ParsedParentText = parsedParentText };
            }

            var selfClosing = GetSelfClosingElement(text);
            if(selfClosing.Count() == 1)
            {
                var closeEle = selfClosing.First();
                var parsedParentText = text.Remove(closeEle.DocumentStartIndex, closeEle.DocumentEndIndex - closeEle.DocumentStartIndex);
                return new HtmlElementGetResult { Success = true, Element = closeEle, ParsedParentText = parsedParentText };
            }

            var openResult = GetElementOpening(text);
            if (openResult.Success == false) return new HtmlElementGetResult { Success = false, ParsedParentText = text };

            if (HtmlHelpers.IsHtmlElement(openResult.ElementName) == false) return GetElementResult(text.Remove(openResult.OpeningTagOpenIndex, openResult.OpeningTagCloseIndex - openResult.OpeningTagOpenIndex + 1));

            //not taking enough on some substring

            var closeResult = GetElementClosing(text, openResult);
            if(closeResult.Success == false)
            {
                var parsedText = text.Remove(openResult.OpeningTagOpenIndex, openResult.OpeningTagCloseIndex - openResult.OpeningTagOpenIndex);
                return new HtmlElementGetResult { Success = false, ParsedParentText = parsedText };
            }

            var elementText = text.Substring(openResult.OpeningTagOpenIndex + 1, openResult.OpeningTagCloseIndex - (openResult.OpeningTagOpenIndex + 1));
            var innerText = text.Substring(openResult.OpeningTagCloseIndex + 1, closeResult.CloseTagOpenIndex - (openResult.OpeningTagCloseIndex + 1));

            var element = new HtmlElement
            {
                ElementName = openResult.ElementName,
                DocumentStartIndex = openResult.OpeningTagOpenIndex,
                DocumentEndIndex = closeResult.CloseTagCloseIndex,
                InnerText = innerText,
                Attributes = new Dictionary<string, List<string>>()
            };

            GetChildren(element);
            _attributeReader.GetAttributes(element, elementText);

            return new HtmlElementGetResult
            {
                Success = true,
                Element = element,
                ParsedParentText = text.Remove(element.DocumentStartIndex, element.DocumentEndIndex - element.DocumentStartIndex)
            };

        }



        public HtmlElement GetElement(string text)
        {
            var result = GetElementResult(text);

            if (result.Success == false) return null;

            return result.Element;

    

        }

        private void GetChildren(HtmlElement element)
        {
            element.Children = new List<HtmlElement>();
            var innerTextSave = element.InnerText;


            while (true)
            {
                if (HasChildren(element) == false) break;

                var elementResult = GetElementResult(element.InnerText);
                element.InnerText = elementResult.ParsedParentText;

                if (elementResult.Success)
                {
                    ((List<HtmlElement>)element.Children).Add(elementResult.Element);
                    elementResult.Element.Parent = element;
                }



            }

            element.InnerText = innerTextSave;
            element.InnerTextParsed = HtmlHelpers.RemoveEscapeCharacters(innerTextSave);


        }

        private bool HasChildren(HtmlElement element)
        {
            if (GetEmptyElement(element.InnerText).Count() == 1) return true;
            if (GetSelfClosingElement(element.InnerText).Count() == 1) return true;

            var openResult = GetElementOpening(element.InnerText);
            if (openResult.Success == false) return false;

            var closeResult = GetElementClosing(element.InnerText, openResult);
            if (closeResult.Success == false) return false;

            return true;
        }

        private Maybe<HtmlElement> GetEmptyElement(string text)
        {
            if (text == null) return new Maybe<HtmlElement>();
        

            var openMatch = new Regex("<").Match(text);
            if (openMatch.Success == false) return new Maybe<HtmlElement>();

            var elementName = text.Skip(openMatch.Index + 1).TakeWhile(c => c != ' ').CharsToString();

            if (!HtmlHelpers.IsEmptyElement(elementName)) return new Maybe<HtmlElement>();

            var closeMatch = new Regex(">").Match(text.Skip(openMatch.Index).CharsToString());
            if (closeMatch.Success == false) return new Maybe<HtmlElement>();

            var elementText = text.Substring(openMatch.Index + 1, (closeMatch.Index + openMatch.Index) - (openMatch.Index + 2))
                                  .Skip(elementName.Count())
                                  .CharsToString();

            var element = new HtmlElement
            {
                ElementName = elementName,
                DocumentStartIndex = openMatch.Index,
                DocumentEndIndex = closeMatch.Index + openMatch.Index + 1

            };

            _attributeReader.GetAttributes(element, elementText);

            return new Maybe<HtmlElement>(element);

        }

        private Maybe<HtmlElement> GetSelfClosingElement(string text)
        {
            if (text == null) return new Maybe<HtmlElement>();


            var openMatch = new Regex("<").Match(text);
            if (openMatch.Success == false) return new Maybe<HtmlElement>();

            var elementName = text.Skip(openMatch.Index + 1).TakeWhile(c => c != ' ').CharsToString();

            var closeMatch = new Regex("/>").Match(text);
            if (closeMatch.Success == false) return new Maybe<HtmlElement>();

            var elementText = text.Substring(openMatch.Index + 1, closeMatch.Index - (openMatch.Index + 2))
                                  .Skip(elementName.Count())
                                  .CharsToString();

            var element = new HtmlElement
            {
                ElementName = elementName,
                DocumentStartIndex = openMatch.Index,
                DocumentEndIndex = closeMatch.Index + closeMatch.Length

            };

            _attributeReader.GetAttributes(element, elementText);

            return new Maybe<HtmlElement>();

        }

        private HtmlElementOpenGetResult GetElementOpening(string text)
        {
            var openMatch = new Regex("<").Match(text);
            if (openMatch.Success == false) return new HtmlElementOpenGetResult { Success = false };
            

            //error occurs here we need one more space taken
            var closeMatch = new Regex(">").Match(text);
            if (closeMatch.Success == false) return new HtmlElementOpenGetResult { Success = false };
            //Fix < in text string

            var elementName = text.Substring(openMatch.Index + 1, closeMatch.Index - (openMatch.Index + 1))
                                  .TakeWhile(c => c != ' ')
                                  .CharsToString();

            

            return new HtmlElementOpenGetResult
            {
                Success = true,
                ElementName = elementName,
                OpeningTagOpenIndex = openMatch.Index,
                OpeningTagCloseIndex = closeMatch.Index
            };

        }

        private HtmlElementCloseGetResult GetElementClosing(string text, HtmlElementOpenGetResult opening)
        {
            //get the corresponding closing somehow

            var closingMatches = new Regex($"</{opening.ElementName}>").Matches(text);
            var childOpenMatches = new Regex($"<{opening.ElementName}").Matches(text.Skip(opening.OpeningTagOpenIndex + 1).CharsToString());

            if (closingMatches.Count == 0) return new HtmlElementCloseGetResult { Success = false };
            if (childOpenMatches.Count == 0) return new HtmlElementCloseGetResult
            {
                ElementName = opening.ElementName,
                Success = true,
                CloseTagOpenIndex = closingMatches[0].Index,
                CloseTagCloseIndex = closingMatches[0].Index + closingMatches[0].Length
            };

            //invalid html will error here
            var finalCloseMatch = closingMatches[childOpenMatches.Count - 1];


            return new HtmlElementCloseGetResult
            {
                ElementName = opening.ElementName,
                Success = true,
                CloseTagOpenIndex = finalCloseMatch.Index,
                CloseTagCloseIndex = finalCloseMatch.Index + finalCloseMatch.Length

            };





        }
    }
}

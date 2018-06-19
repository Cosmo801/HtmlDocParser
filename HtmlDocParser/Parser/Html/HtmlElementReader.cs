using Cosmo.HtmlDocParser.Parser.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cosmo.HtmlDocParser.Parser.Html
{
    public class HtmlElementReader : IHtmlElementReader
    {
        private HtmlAttributeReader _attributeReader;

        public HtmlElementReader()
        {
            _attributeReader = new HtmlAttributeReader();
        }

       
        public HtmlElement GetElement(string text)
        {
            if (GetSelfClosingElement(text).Count() == 1) return GetSelfClosingElement(text).First();
            if (GetEmptyElement(text).Count() == 1) return GetEmptyElement(text).First();

            var opening = GetElementOpening(text);
            if (!opening.Success) return null;

            var closing = GetElementClosing(text, opening);
            if (!closing.Success) return null;

            var innerText = new string(text.Skip(opening.OpeningTagCloseIndex + 1)
                                          .Take((closing.CloseTagOpenIndex - 1) - (opening.OpeningTagCloseIndex))
                                          .ToArray());


            var element = new HtmlElement(opening.ElementName, innerText)
            {
                DocumentStartIndex = opening.OpeningTagOpenIndex,
                DocumentEndIndex = closing.CloseTagCloseIndex,
                
                Children = new List<HtmlElement>()
            };

           
            GetChildren(element);
           

            var attrString = text.Substring(opening.OpeningTagOpenIndex + 1, opening.OpeningTagCloseIndex - (opening.OpeningTagOpenIndex + 1));
            _attributeReader.GetAttributes(element, attrString);

            element.InnerTextParsed = HtmlHelpers.RemoveEscapeCharacters(element.InnerText);

            return element;



        }

        private void GetChildren(HtmlElement element)
        {
            //this method is causing issues change has Children
            var innerTextSave = element.InnerText;
            while (HasChildren(innerTextSave))
            {
                var child = GetElement(innerTextSave);
                innerTextSave = innerTextSave.Substring(child.DocumentEndIndex);

                if (child.Parent == null)
                {
                    child.Parent = element;
                }
                else ((IList<HtmlElement>)child.Parent).Add(element);

                if (element.Children == null)
                {
                    element.Children = new List<HtmlElement> { child };
                }
                else ((IList<HtmlElement>)element.Children).Add(child);


            }
        }

        private bool HasChildren(string text)
        {
          

            if (GetEmptyElement(text).Count() == 1) return true;
            if (GetSelfClosingElement(text).Count() == 1) return true;

            var openResult = GetElementOpening(text);
            if (openResult.Success == false) return false;

            var close = GetElementClosing(text, openResult);
            if (close.Success == false) return false;

            return true;

        }

        private HtmlElementOpenGetResult GetElementOpening(string text)
        {
            var openPatternOne = new Regex("<");
            var openPatternTwo = new Regex(">");

            //this is screwing up with links
            var openMatchOne = openPatternOne.Match(text);
            var openMatchTwo = openPatternTwo.Match(text);

            if (!openMatchOne.Success) return new HtmlElementOpenGetResult { Success = false };
            if (!openMatchTwo.Success) return new HtmlElementOpenGetResult { Success = false };

            //Is !DOCTYPE
            if (text[openMatchOne.Index + 1] == '!')
            {

                openMatchOne = openMatchOne.NextMatch();
                openMatchTwo = openMatchTwo.NextMatch();

                if (openMatchOne.Success == false) return new HtmlElementOpenGetResult { Success = false };


            }

            var openIndexCount = (openMatchTwo.Index - 1) - (openMatchOne.Index);
            var openingTag = text.Substring(openMatchOne.Index + 1, openIndexCount);

            var htmlName = openingTag.Split(null).Where(o => o != string.Empty).First();



            return new HtmlElementOpenGetResult
            {
                ElementName = htmlName,
                Success = true,
                OpeningTagOpenIndex = openMatchOne.Index,
                OpeningTagCloseIndex = openMatchTwo.Index
            };

           
        }

        private HtmlElementCloseGetResult GetElementClosing(string text, HtmlElementOpenGetResult open)
        {
            var searchString = $@"</{open.ElementName}>";

            var closeRegex = new Regex(searchString);

            var matches = closeRegex.Matches(text);
            Match lastMatch;

            if (matches.Count == 0) return new HtmlElementCloseGetResult { Success = false };
            else if (matches.Count > 1)
            {
                lastMatch = matches[matches.Count - 1];
            }
            else lastMatch = matches[0];

            return new HtmlElementCloseGetResult
            {
                ElementName = open.ElementName,
                Success = true,
                CloseTagOpenIndex = lastMatch.Index,
                CloseTagCloseIndex = lastMatch.Index + lastMatch.Length
            };
        }

        private Maybe<HtmlElement> GetSelfClosingElement(string text)
        {

            //This is causing problems with links could be the indexing
            var openFinder = new Regex("<");
            var closeFinder = new Regex("/>");

            var openingMatch = openFinder.Match(text);
            if (!openingMatch.Success) return new Maybe<HtmlElement>();

            var closeMatch = closeFinder.Match(text);
            if (!closeMatch.Success) return new Maybe<HtmlElement>();

            var invalidOpening = new Regex(">").Match(text);
            var invalidClosing = new Regex("</").Match(text);

            if (invalidOpening.Success)
            {
                if (invalidOpening.Index > openingMatch.Index && invalidOpening.Index < closeMatch.Index) return new Maybe<HtmlElement>();
            }
            if (invalidClosing.Success)
            {
                if (invalidClosing.Index > openingMatch.Index && invalidClosing.Index < closeMatch.Index) return new Maybe<HtmlElement>();
            }

            var elementText = text.Remove(openingMatch.Index + 1, closeMatch.Index - (openingMatch.Index + 1));
            var elementTextArray = elementText.Split(null);

            var elementName = elementTextArray.First();
            if (!HtmlHelpers.IsHtmlElement(elementName)) return new Maybe<HtmlElement>();
                    
            var element = new HtmlElement
            {
                ElementName = elementName,
                DocumentStartIndex = openingMatch.Index,
                DocumentEndIndex = closeMatch.Index + 1,
                Children = new List<HtmlElement>(),
                InnerText = string.Empty,
                InnerTextParsed = string.Empty
            };

            _attributeReader.GetAttributes(element, text.Substring(openingMatch.Index + 1, closeMatch.Index - (openingMatch.Index + 2)));

            return new Maybe<HtmlElement>(element);
        }

        private Maybe<HtmlElement> GetEmptyElement(string text)
        {
            var elementResult = GetElementOpening(text);
            if (!elementResult.Success) return new Maybe<HtmlElement>();

            if (!HtmlHelpers.IsEmptyElement(elementResult.ElementName)) return new Maybe<HtmlElement>();

            var newElement = new HtmlElement
            {
                ElementName = elementResult.ElementName,
                DocumentStartIndex = elementResult.OpeningTagOpenIndex,
                DocumentEndIndex = elementResult.OpeningTagCloseIndex,
                Children = new List<HtmlElement>(),
                InnerText = string.Empty,
                InnerTextParsed = string.Empty
                
            };

            var attributeString = text.Substring(newElement.DocumentStartIndex + 1, newElement.DocumentEndIndex - (newElement.DocumentStartIndex + 1));
            _attributeReader.GetAttributes(newElement, attributeString);
          

            return new Maybe<HtmlElement>(newElement);
        }


    }
}

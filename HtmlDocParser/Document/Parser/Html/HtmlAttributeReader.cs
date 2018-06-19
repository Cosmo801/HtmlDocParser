using HtmlDocParser.Document.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlDocParser.Document.Parser.Html
{
    public class HtmlAttributeReader : IHtmlAttributeReader
    {
        public void GetAttributes(HtmlElement source, string attributeString)
        {
            
            var attributes = new Dictionary<string, List<string>>();
            source.Attributes = attributes;

            if (string.IsNullOrEmpty(attributeString)) return;

            var equalsMatches = new Regex("=").Matches(attributeString);
            if (equalsMatches.Count == 0) return;

            foreach(var match in equalsMatches)
            {
                var matchObj = (Match)match;

                var nextChar = attributeString[matchObj.Index + 1];

                var attrName = attributeString.Take((matchObj.Index - 1) + 1).CharsToString().Reverse().TakeWhile(s => !char.IsWhiteSpace(s)).Reverse().CharsToString();

                if(nextChar == '\'' || nextChar == '\"')
                {
                    var quoteFinder = new Regex($"{nextChar}").Match(attributeString.Skip(matchObj.Index + 3).CharsToString());

                    var valueString = attributeString.Substring(matchObj.Index + 2, (quoteFinder.Index + matchObj.Index + 1) - (matchObj.Index));
                    var valueArray = valueString.Split(null);

                    if (attributes.ContainsKey(attrName)) attributes[attrName].AddRange(valueArray);
                    else attributes.Add(attrName, valueArray.ToList());
                }

                else
                {
                    

                    var attrValue = attributeString.Skip(matchObj.Index + 1).TakeWhile(c => char.IsLetterOrDigit(c)).CharsToString();
                    if (attributes.ContainsKey(attrName)) attributes[attrName].Add(attrValue);
                    else attributes.Add(attrName, new List<string> { attrValue });


                }



            }

            source.Attributes = attributes;

          
        }
    }
}

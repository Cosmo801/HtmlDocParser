using HtmlDocParser.Document.Parser;
using HtmlDocParser.Document.Parser.Getter;
using HtmlDocParser.Document.Parser.Html;
using HtmlDocParser.Document.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlDocParser.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
 
            //fix commenting
            //need to add better exception resistance

            //Add exception handling to Getter


            var reader = new HtmlDocumentReader();

            //apple still doesnt work
            var test = reader.ParseDocument("https://www.apple.com");

            var links = test.GetElementBySelector("link");
           
        }
    }
}

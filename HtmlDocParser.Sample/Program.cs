using Cosmo.HtmlDocParser.Parser.Html;
using Cosmo.HtmlDocParser;

namespace HtmlDocParser.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
 
            //fix commenting
            //need to add better exception resistance

            //Add exception handling to Getter

            //Do multi selectors

            var reader = new HtmlDocumentReader();

            //apple works
            var test = reader.ParseDocument("https://www.apple.com/au/");

            var links = test.GetElementBySelector("head > title");
           
        }
    }
}

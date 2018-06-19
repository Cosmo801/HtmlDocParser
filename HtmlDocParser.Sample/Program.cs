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


            var reader = new HtmlDocumentReader();

            //apple still doesnt work
            var test = reader.ParseDocument("https://www.apple.com");

            var links = test.GetElementBySelector("link");
           
        }
    }
}

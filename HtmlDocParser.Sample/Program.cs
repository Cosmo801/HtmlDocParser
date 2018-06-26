using Cosmo.HtmlDocParser.Parser.Html;
using Cosmo.HtmlDocParser;

namespace HtmlDocParser.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
 
            //Fix spacing for tags 
            //eg < div> </div >



            //fix commenting
            //need to add better exception resistance

            //Add exception handling to Getter

            //Do multi selectors

            var reader = new HtmlDocumentReader();

            //multi selectors are doing repeated calls to HtmlHelpers.GetAllElements() fix it

            //apple works
            var test = reader.ParseDocumentFromPath("https://www.apple.com/au/");

            var links = test.GetElementBySelector("head > title");
           
        }
    }
}

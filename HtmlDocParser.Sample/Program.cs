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
            //twitch throws exception because something isnt being removed from doctext
            //youtube works but performance is very bad
            var test = reader.ParseDocumentFromPath("https://www.youtube.com/");

            var links = test.GetElementBySelector("head > title");
           
        }
    }
}

namespace Cosmo.HtmlDocParser.Parser.Html
{
    internal class HtmlElementCloseGetResult
    {
        public bool Success { get; set; }
        public string ElementName { get; set; }
        public int CloseTagOpenIndex { get; set; }
        public int CloseTagCloseIndex { get; set; }


    }
}

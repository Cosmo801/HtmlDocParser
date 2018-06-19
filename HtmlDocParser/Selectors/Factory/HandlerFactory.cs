using Cosmo.HtmlDocParser.Selectors.Handlers;

namespace Cosmo.HtmlDocParser.Selectors.Factory
{
    public class HandlerFactory
    {
        public static ISelectorHandler GetHandlers()
        {
            var handler = new ElementHandler();
            var handler2 = handler.SetNext(new IdHandler());
            var handler3 = handler2.SetNext(new ClassHandler());
            var handler4 = handler3.SetNext(new DescendentHandler());
            var handler5 = handler4.SetNext(new ChildHandler());
            var handler6 = handler5.SetNext(new MultiSelectorHandler());
            var finalHandler = handler6.SetNext(new FinalHandler());

            return handler;

        }
    }
}

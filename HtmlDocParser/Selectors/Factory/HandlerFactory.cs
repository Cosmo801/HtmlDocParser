using Cosmo.HtmlDocParser.Selectors.Handlers;

namespace Cosmo.HtmlDocParser.Selectors.Factory
{
    public class HandlerFactory
    {
        public static ISelectorHandler GetHandlers()
        {
            var eleHandler = new ElementHandler();
            var idHandler = eleHandler.SetNext(new IdHandler());
            var classHandler = idHandler.SetNext(new ClassHandler());
            var descendentHandler = classHandler.SetNext(new DescendentHandler());
            var childHandler = descendentHandler.SetNext(new ChildHandler());
            var groupHandler = childHandler.SetNext(new GroupHandler()); 
            var multiHandler = groupHandler.SetNext(new MultiSelectorHandler());
            var finalHandler = multiHandler.SetNext(new FinalHandler());

            return eleHandler;

        }
    }
}

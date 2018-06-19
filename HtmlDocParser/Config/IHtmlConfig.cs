using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlDocParser.Config
{
    public interface IHtmlConfig
    {
        bool IsHtmlElement(string elementName);
        bool IsEmptyElement(string elementName);

    }
}

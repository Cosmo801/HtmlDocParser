﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlDocParser.Document.Parser.Helpers
{
    public static class IEnumerableExtensions
    {
        public static string CharsToString(this IEnumerable<char> source)
        {
            var stringBuilder = new StringBuilder();

            foreach(var entry in source)
            {
                stringBuilder.Append(entry);
            }

            return stringBuilder.ToString();
        }


    }
}

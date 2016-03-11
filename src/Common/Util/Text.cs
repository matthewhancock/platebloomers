using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Util {
    public static class Text {
        internal static string StripHtml(string HtmlString) {
            return System.Text.RegularExpressions.Regex.Replace(HtmlString, "<(.|\n)*?>", String.Empty).Replace("&nbsp;", " ");
        }
    }
}

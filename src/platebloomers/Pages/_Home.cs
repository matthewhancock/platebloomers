using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Util.Extensions.Tuple;

namespace platebloomers.Pages {
    public class Home : Common.Site.Page {
        public static string GenerateContent() {
            var sb = new StringBuilder();
            sb.Append("<div class=\"thumbnail_tile\">");
            var pictures = new List<Tuple<string, string, bool>> {
                { "1", "1", false }, { "2", "2", true }, { "2b", "2 - Side", true }, { "3", "3", false }, { "3b", "3 - Side", false }, { "4", "4", true },
                { "5", "5", true }, { "5b", "5 - Side", true }, { "6", "6", true }, { "6b", "6 - Side", true }, { "7", "7", true }, { "8", "8", false },
                { "9", "9", true }, { "10", "10", true }, { "11", "11", false }, { "12", "12", false }, { "13", "13", false }, { "14", "14", false },
                { "15", "15", true }, { "16", "16", true }, { "17", "17", false }, { "18", "18", false }, { "20", "20", false }, { "21", "21", true },
                { "22", "22", true }, { "23", "23", true }, { "24", "24", true }, { "25", "25", false }, { "m26", "26 [Medium]", false },
                { "m27", "27 [Medium]", false }, { "28", "28", true }, { "29", "29", false }, { "30", "30", true }, { "31", "31", true }, { "32", "32", false },
                { "33", "33", false }, { "m34", "34 [Medium]", false }, { "35", "35", false }, { "m36", "36 [Medium]", false }, { "37", "37", false },
                { "38", "38", false }, { "39", "39", false }, { "40", "40", false }, { "m41", "41 [Medium]", false }, { "m42", "42 [Medium]", false },
                { "m43", "43 [Medium]", false }, { "m44", "44 [Medium]", false }, { "m45", "45 [Medium]", false }, { "46", "46", false }, { "48", "48", false },
                { "49", "49", false }, { "m50", "50 [Medium]", false }, { "52", "52", false }, { "53", "53", false }, { "m54", "54 [Medium]", false },
                { "55", "55", false }, { "56", "56", false }, { "m60", "60 [Medium]", false }, { "61", "61", false }, { "62", "62", false }, { "63", "63", false },
                { "m64", "64 [Medium]", false }, { "m65", "65 [Medium]", false }, { "m66", "66 [Medium]", false }, { "m67", "67 [Medium]", false },
                { "69", "69", false }, { "m70", "70 [Medium]", false }, { "71", "71", false }, { "72", "72", false }
            };
            foreach (var i in pictures) {
                sb.Append($"<picture class=\"thumbnail\" style=\"background-image:url('/image/{i.Item1}s.jpg')\" onclick=\"return m.PreviewPicture('{i.Item1}','#{i.Item2}');\"><a href=\"javascript:;\" onclick=\"return m.PreviewPicture('{i.Item1}','#{i.Item2}');\">#{i.Item2}</a>");
                if (i.Item3) {
                    sb.Append("<aside class=\"sold\">Sold</aside>");
                }
                sb.Append("</picture>");
            }
            sb.Append("</div>");
            return sb.ToString();
        }

        private string _content;
        public override string Content {
            get {
                if (_content == null) {
                    _content = GenerateContent();
                }
                return _content;
            }
        }

        public override string Description {
            get {
                return "";
            }
        }

        public override string Key {
            get {
                return "home";
            }
        }
        public override string Path {
            get {
                return string.Empty;
            }
        }
        public override string Header {
            get {
                return "Home";
            }
        }

        public override string Title {
            get {
                return "Plate Bloomers";
            }
        }

        public override string TitleNav {
            get {
                return "Home";
            }
        }
    }
}

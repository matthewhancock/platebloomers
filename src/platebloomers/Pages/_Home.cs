using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace platebloomers.Pages {
    public class Home : Common.Site.Page {
        public static string GenerateContent() {
            var sb = new StringBuilder();
            sb.Append("<div class=\"thumbnail_tile\">");
            //var pictures = new string[] { "1", "2", "2b", "3", "3b", "4", "5", "5b", "6", "6b", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "20", "21", "22", "23", "24", "25", "28", "29", "30", "31", "32" };
            var pictures = new Dictionary<string, string>() { { "1", "1" }, { "2", "2" }, { "2b", "2 - Side" }, { "3", "3" }, { "3b", "3 - Side" }, { "4", "4" }, { "5", "5" }, { "5b", "5 - Side" }, { "6", "6" }, { "6b", "6 - Side" }, { "7", "7" }, { "8", "8" }, { "9", "9" }, { "10", "10" }, { "11", "11" }, { "12", "12" }, { "13", "13" }, { "14", "14" }, { "15", "15" }, { "16", "16" }, { "17", "17" },
                { "18", "18" }, { "20", "20" }, { "21", "21" }, { "22", "22" }, { "23", "23" }, { "24", "24" }, { "25", "25" }, { "28", "28" }, { "29", "29" }, { "30", "30" }, { "31", "31" }, { "32", "32" } };
            //var counter = 0;
            foreach (var i in pictures) {
                //if (counter++ % 4 != 0) {
                //    sb.Append("<aside class=\"thumbnail_spacer\"></aside>");
                //}
                sb.Append($"<picture class=\"thumbnail\" style=\"background-image:url('/image/{i.Key}s.jpg')\"><a href=\"javascript:;\" onclick=\"return m.PreviewPicture('{i.Key}','#{i.Value}');\">#{i.Value}</a></picture>");
            }
            sb.Append("</div>");
            return sb.ToString();
        }

        private string _content;
        public override string Content
        {
            get
            {
                if (_content == null) {
                    _content = GenerateContent();
                }
                return _content;
            }
        }

        public override string Description
        {
            get
            {
                return "";
            }
        }

        public override string Key
        {
            get
            {
                return "home";
            }
        }
        public override string Path
        {
            get
            {
                return string.Empty;
            }
        }
        public override string Header
        {
            get
            {
                return "Home";
            }
        }

        public override string Title
        {
            get
            {
                return "Plate Bloomers";
            }
        }

        public override string TitleNav
        {
            get
            {
                return "Home";
            }
        }
    }
}

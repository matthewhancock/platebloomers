using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace platebloomers.Pages {
    public class Contact : Common.Site.Page {
        public override string Content
        {
            get
            {
                return "<div class=\"tac\">Please feel free to email me at <a href=\"mailto:janis@platebloomers.com\">janis@platebloomers.com</a>.</div>";
            }
        }

        public override string Description
        {
            get
            {
                return "Contact Plate Bloomers about the plates or how to order them.";
            }
        }

        public override string Header { get { return null; } }

        public override string Key { get { return "contact"; } }

        public override string Path { get { return "Contact"; } }

        public override string Title { get { return "Contact - Plate Bloomers"; } }

        public override string TitleNav { get { return "Contact"; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Site;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;

namespace platebloomers {
    public class Startup : Common.StartupTemplate<Application> {
        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);


        private static string _css, _javascript = null;
        private static Dictionary<string, byte[]> _images = new Dictionary<string, byte[]>();
        public override async Task<string> Css(string[] Path) {
            if (_css == null) {
                _css = await Common.Util.File.LoadToString("_files/css/this.css");
            }
            return _css;
        }

        public override async Task<string> Javascript(string[] Path) {
            if (_javascript == null) {
                _javascript = await Common.Util.File.LoadToString("_files/js/this.js");
                //_javascript = await meshNet.Util.File.LoadToString("_files/js/this-typescript.min.js");
            }
            return _javascript;
        }

        public override async Task<byte[]> Image(string[] Path) {
            byte[] file = null;
            var pathl = Path.Length;
            var filename = Path[1];
            if (!_images.ContainsKey(filename)) {
                try {
                    file = await Common.Util.File.LoadToBuffer($"_files/images/{filename}");
                    _images.Add(filename, file);
                } catch { }
            } else {
                file = _images[filename];
            }
            return file;
        }

        private static class Pages {
            public static Page Home = new platebloomers.Pages.Home();
            public static Page About = new platebloomers.Pages.About();
            public static Page Contact = new platebloomers.Pages.Contact();
        }

        private static string tags = (new Common.Util.Html.Head.Tag("link", new Dictionary<string, string> { { "rel", "stylesheet" }, { "type", "text/css" }, { "href", "/css?v2" } })).Output() +
            (new Common.Util.Html.Head.Tag("link", new Dictionary<string, string> { { "rel", "stylesheet" }, { "type", "text/css" }, { "href", "https://cloud.typography.com/607958/7178952/css/fonts.css" } })).Output() +
            (new Common.Util.Html.Head.Tag.Javascript("/js")).Output(); // + (new Common.Util.Html.Head.Tag.Javascript("//platform.twitter.com/widgets.js")).Output() + (new Common.Util.Html.Head.Tag.Javascript("//connect.facebook.net/en_US/sdk.js#xfbml=1&appId=175259985884771&version=v2.0")).Output();
        private static string body_start = $"<header id=\"h\"><h1 id=\"h_title\">PLATE BLOOMERS</h1><nav id=\"n\" data-key=\"";
        private static string body_mid = $"\">{Pages.Home.NavLink}&emsp;|&emsp;{Pages.About.NavLink}&emsp;|&emsp;{Pages.Contact.NavLink}</nav></header><main id=\"m\"><section id=\"content\">";
        private static string body_end = $"</section></main><footer id=\"f\"></footer><div id=\"vo-div\"></div>";

        protected override async Task Page(HttpResponse Response, Page Page) {
            await Common.Util.Html.WriteOutput(Response, Page.Title, tags + "<meta name=\"description\" content=\"" + Page.Description + "\">", body_start + Page.Key + body_mid + Page.Content + body_end);
        }

        public override List<Page> RegisterPages() {
            var list = new List<Page>();
            //list.AddRange(Common.Util.Reflection.GetInheritorsOfClass<Page>());
            //var assembly = System.Reflection.Assembly.GetAssembly(typeof(Page));
            //var types = assembly.GetTypes().Where(q=> q.IsClass && !q.IsAbstract);
            //foreach (Type type in types.Where(q => q.IsSubclassOf(typeof(Page)) && q.IsClass && !q.IsAbstract)) {
            //    list.Add((Page)Activator.CreateInstance(type, null));
            //}

            list.Add(Pages.Home);
            list.Add(Pages.About);
            list.Add(Pages.Contact);

            return list;
        }
    }
}

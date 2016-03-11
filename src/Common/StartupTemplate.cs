using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Common.Util.Http;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;

namespace Common {
    public abstract class StartupTemplate<T> where T : Application, new() {
        private List<Site.Page> _pages;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        private static string[] _cacheHeader = new string[] { Util.Http.Header.Values.Cache.MaxAge_OneYear };
        public void Configure(IApplicationBuilder app, IApplicationEnvironment env) {
            app.UseIISPlatformHandler();

            // Find pages
            _pages = RegisterPages();

            var application = new T();
            application.RootLoadFromEnvironment(env);

            // load config
            var _config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            // load local config file if no environment variables
            var envn = _config["env"] ?? "local";
            if (envn == "local") {
                _config = new ConfigurationBuilder().AddJsonFile(env.ApplicationBasePath + "/config.local.json").Build();
            }
            application.RootLoadFromConfig(_config);

            //app.ForceHttps();

            app.Run(ProcessRequestAsync);
        }

        public async Task ProcessRequestAsync(HttpContext Context) {
            var rq = Context.Request;
            var rs = Context.Response;

            var path = rq.Path.Value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var pathc = path.Length;

            if (rq.Method == "POST") {
                if (pathc == 1) {
                    //await Handlers.Post.ProcessRequestAsync(Context, path[0]);
                }
            } else {
                if (pathc > 0) {
                    switch (path[0]) {
                        case "css":
                            Context.Response.Headers.Add(Header.Cache, _cacheHeader);
                            Context.Response.ContentType = Header.Values.ContentType.Css;
                            var css = await Css(path);
                            await Context.Response.WriteAsync(css);
                            break;
                        case "js":
                            Context.Response.Headers.Add(Header.Cache, _cacheHeader);
                            Context.Response.ContentType = Header.Values.ContentType.Javascript;
                            var js = await Javascript(path);
                            await Context.Response.WriteAsync(js);
                            break;
                        case "image":
                            Context.Response.Headers.Add(Header.Cache, _cacheHeader);
                            var file = await Image(path);
                            if (file != null) {
                                var filename = path[1];
                                var fileextension = filename.Substring(filename.IndexOf("."));
                                if (fileextension == "png") {
                                    Context.Response.ContentType = Header.Values.ContentType.Png;
                                } else {
                                    Context.Response.ContentType = Header.Values.ContentType.Jpg;
                                }

                                await Context.Response.Body.WriteAsync(file, 0, file.Length);
                            } else {
                                await Error.FileNotFound(Context.Response);
                            }
                            break;
                        case "json":
                            if (pathc == 2) {
                                await OutputPageJson(rs, path[1]);
                            } else if (pathc > 2) {
                                await OutputPageJson(rs, path[1]);
                            } else {
                                await Error.FileNotFound(rs);
                            }
                            break;
                        default:
                            if (pathc == 1) {
                                await OutputPage(rs, path[0]);
                            } else if (pathc > 1) {
                                await OutputPage(rs, path[0], path.Skip(1).ToArray());
                            } else {
                                await Error.FileNotFound(rs);
                            }
                            break;
                    }
                } else {
                    await OutputPage(rs, string.Empty);
                }
            }
        }


        protected virtual void Configure(IApplicationBuilder app) { } // Allow inheriting applications to do some configuration

        public abstract List<Site.Page> RegisterPages();

        public abstract Task<string> Css(string[] Path);
        public abstract Task<string> Javascript(string[] Path);
        public abstract Task<byte[]> Image(string[] Path);
        protected async Task Json(HttpResponse Response, Site.Page Page) {
            Response.ContentType = "text/javascript";
            var s = @"{""title"":""" + Util.Json.Fix(Page.Title) + @""",""description"":""" + Util.Json.Fix(Page.Description) + @""",""header"":""" + Util.Json.Fix(Page.Header) + @""",""key"":""" + Page.Key + @""",""content"":""" + Util.Json.Fix(Page.Content) + @"""}";
            await Response.WriteAsync(s);
        }

        protected abstract Task Page(HttpResponse Response, Site.Page Page);
        private async Task OutputPage(HttpResponse Response, string Path, string[] Parameters = null) {
            Site.Page page = _pages.Find((q) => q.Path == Path);
            if (page == null) {
                await Error.FileNotFound(Response);
            } else {
                Response.ContentType = Header.Values.ContentType.Html;
                await Page(Response, page);
            }
        }
        private async Task OutputPageJson(HttpResponse Response, string Key) {
            Site.Page page = _pages.Find((q) => q.Key == Key);
            if (page == null) {
                await Error.FileNotFound(Response);
            } else {
                await Json(Response, page);
            }
        }

        private static class Error {
            public static async Task FileNotFound(HttpResponse Response) {
                Response.StatusCode = 404;
                Response.Body.Close();
            }
        }
    }
}

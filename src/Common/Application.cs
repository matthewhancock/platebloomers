using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common {
    public abstract class Application {
        private static string _path;

        private static EnvironmentConfiguration _env;
        private static Configuration _config;

        internal void RootLoadFromEnvironment(IApplicationEnvironment env) {
            _path = env.ApplicationBasePath;
            LoadFromEnvironment(env);
        }
        public abstract EnvironmentConfiguration LoadFromEnvironment(IApplicationEnvironment env);
        internal void RootLoadFromConfig(IConfiguration Configuration) {
            _config = LoadFromConfig(Configuration);
        }
        public abstract Configuration LoadFromConfig(IConfiguration Configuration);

        public class EnvironmentConfiguration {
            public string ApplicationBasePath { get; internal set; }
        }
        public class Configuration {
        }
        public static class Environment {
            public static string ApplicationBasePath { get { return _path; } }
            public static EnvironmentConfiguration Configuration { get { return _env; } }
        }
    }
}

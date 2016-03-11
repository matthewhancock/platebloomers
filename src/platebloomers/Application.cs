using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;

namespace platebloomers {
    public class Application : Common.Application {
        public override Configuration LoadFromConfig(IConfiguration Configuration) {
            var c = new Configuration();

            return c;
        }

        public override EnvironmentConfiguration LoadFromEnvironment(IApplicationEnvironment env) {
            var e = new EnvironmentConfiguration();

            return e;
        }


    }
}

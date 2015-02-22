using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Xbox_Live_Stats.Startup))]
namespace Xbox_Live_Stats
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

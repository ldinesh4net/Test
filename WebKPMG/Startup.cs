using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebKPMG.Startup))]
namespace WebKPMG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

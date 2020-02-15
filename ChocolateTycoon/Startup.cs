using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChocolateTycoon.Startup))]
namespace ChocolateTycoon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

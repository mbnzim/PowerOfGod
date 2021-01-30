using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowerOfGod.Web.Startup))]
namespace PowerOfGod.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

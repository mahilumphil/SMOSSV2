using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartShopWebApp.Startup))]
namespace SmartShopWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgriExchange.Startup))]
namespace AgriExchange
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

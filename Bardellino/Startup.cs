using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bardellino.Startup))]
namespace Bardellino
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

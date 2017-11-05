using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Omall.Startup))]
namespace Omall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

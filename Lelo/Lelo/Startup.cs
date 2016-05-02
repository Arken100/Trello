using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lelo.Startup))]
namespace Lelo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

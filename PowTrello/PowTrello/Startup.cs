using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowTrello.Startup))]
namespace PowTrello
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

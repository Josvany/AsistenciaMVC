using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsistenciaMVC.Startup))]
namespace AsistenciaMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

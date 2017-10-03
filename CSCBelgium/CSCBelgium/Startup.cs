using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSCBelgium.Startup))]
namespace CSCBelgium
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

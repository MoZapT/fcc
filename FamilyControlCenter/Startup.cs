using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FamilyControlCenter.Startup))]
namespace FamilyControlCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

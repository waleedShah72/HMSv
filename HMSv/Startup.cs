using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HMSv.Startup))]
namespace HMSv
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

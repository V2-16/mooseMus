using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MooseMus.Startup))]
namespace MooseMus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

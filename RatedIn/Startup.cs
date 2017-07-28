using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RatedIn.Startup))]
namespace RatedIn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

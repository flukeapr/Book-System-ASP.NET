using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project13_web.Startup))]
namespace Project13_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

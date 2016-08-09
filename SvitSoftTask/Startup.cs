using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SvitSoftTask.Startup))]
namespace SvitSoftTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5Application1.Startup))]
namespace MVC5Application1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

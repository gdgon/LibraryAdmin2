using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryAdmin2.Startup))]
namespace LibraryAdmin2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

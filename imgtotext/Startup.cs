using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(imgtotext.Startup))]
namespace imgtotext
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

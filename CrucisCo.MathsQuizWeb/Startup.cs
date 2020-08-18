using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrucisCo.MathsQuizWeb.Startup))]
namespace CrucisCo.MathsQuizWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

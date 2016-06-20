using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Portugal_Insurance___PayPal.Startup))]
namespace Portugal_Insurance___PayPal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

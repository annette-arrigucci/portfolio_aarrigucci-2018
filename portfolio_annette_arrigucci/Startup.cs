using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(portfolio_annette_arrigucci.Startup))]
namespace portfolio_annette_arrigucci
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ATLConveyanceBill.Startup))]
namespace ATLConveyanceBill
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

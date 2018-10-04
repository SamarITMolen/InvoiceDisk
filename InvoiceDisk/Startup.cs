using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvoiceDisk.Startup))]
namespace InvoiceDisk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

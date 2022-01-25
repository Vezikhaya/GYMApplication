using GYMApplication.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GYMApplication.Startup))]
namespace GYMApplication
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRole();
            CreateUser();
        }
    }
}

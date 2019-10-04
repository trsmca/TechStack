using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stack.Startup))]
namespace Stack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZenCoderDemo.Startup))]
namespace ZenCoderDemo
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(DayzlightWebapp.Startup))]
namespace DayzlightWebapp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
            });
        }
    }
}
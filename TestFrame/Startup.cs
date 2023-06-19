using Microsoft.Extensions.DependencyInjection;
using TestFrame.Builder;
using TestFrame.Databases;

namespace TestFrame
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRestBuilder, RestBuilder>();
            services.AddScoped<IDbClient, DbClient>();
        }
    }
}

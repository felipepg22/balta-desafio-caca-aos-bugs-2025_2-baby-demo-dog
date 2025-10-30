using BugStore.Data;
using BugStore.Infra;
using Microsoft.Extensions.Options;

namespace BugStore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSqliteDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.BuildServiceProvider()
                 .GetRequiredService<IOptions<AppSettings>>()
                 .Value;

            if (settings is null)
            {
                throw new InvalidOperationException("AppSettings is not configured properly.");
            }

            if (settings.ConnectionStrings is null || string.IsNullOrEmpty(settings.ConnectionStrings.DefaultConnection))
            {
                throw new InvalidOperationException("DefaultConnection string is missing in AppSettings.");
            }

            services.AddSqlite<AppDbContext>(settings.ConnectionStrings.DefaultConnection);
        }
    }
}

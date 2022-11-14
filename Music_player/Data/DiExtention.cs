using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Music_player.Data
{
    public static class DiExtentions
    {
        public static void AddSQL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<MusicDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}

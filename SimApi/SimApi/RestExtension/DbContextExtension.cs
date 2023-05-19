using Microsoft.EntityFrameworkCore;
using SimApi.Data.Context;

namespace SimApi.Service.RestExtension;

public static class DbContextExtension
{
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
    {
        var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<SimDbContext>(opts =>
        opts.UseSqlServer(dbConfig));
        


    }
}

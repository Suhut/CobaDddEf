using DddEf.Application.Common;
using DddEf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DddEf.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection"); 

        services.AddDbContext<DddEfContext>(options =>
                  options.UseSqlServer(
                      configuration.GetConnectionString("DefaultConnection"),
                      b => b.MigrationsAssembly(typeof(DddEfContext).Assembly.FullName)).LogTo(Console.WriteLine));

        services.AddScoped<IDddEfContext>(provider => provider.GetService<DddEfContext>());


        return services;
    }
}

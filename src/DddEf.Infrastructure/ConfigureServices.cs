using DddEf.Application.Common.Abstractions;
using DddEf.Application.Common.Interfaces;
using DddEf.Infrastructure.Dapper;
using DddEf.Infrastructure.Persistence;
using DddEf.Infrastructure.Services;
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
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddSqlDapperClient(connectionString);


        return services;
    }
}

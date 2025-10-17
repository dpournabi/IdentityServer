using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Infrastructure.Persistence;
using IdentityServer.Infrastructure.Persistence.Interceptors;
using IdentityServer.Infrastructure.Services;
using IdentityServer8.Services;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, AppSettings appSetting)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();



        services.AddScoped<ConfigurationDbContext>();
        services.AddScoped<PersistedGrantDbContext>();
        string? migrationsAssembly = typeof(ApplicationDbContext).Assembly.FullName;
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(appSetting.ConnectionStrings.MSSQL, sqlserverOptions =>
            {
                sqlserverOptions.CommandTimeout(180); // 3 minutes
                sqlserverOptions.EnableRetryOnFailure(3);
                sqlserverOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });

            options.EnableDetailedErrors(true);
        }, ServiceLifetime.Scoped);
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddScoped<IProfileService, ProfileService>();

        //services.AddAuthorization(options =>
        //    options.AddPolicy("RootOnly", policy => policy.RequireRole("Root")));
        return services;
    }
}

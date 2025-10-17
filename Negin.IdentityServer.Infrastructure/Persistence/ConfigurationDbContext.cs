using IdentityServer8.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nsp.Common;

namespace IdentityServer.Infrastructure.Persistence
{
    public class ConfigurationDbContext : IdentityServer8.EntityFramework.DbContexts.ConfigurationDbContext
    {
        public ConfigurationDbContext(DbContextOptions<IdentityServer8.EntityFramework.DbContexts.ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
    }
    public class ConfigurationContextDesignTimeFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<IdentityServer8.EntityFramework.DbContexts.ConfigurationDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(AppSettings.Build().ConnectionStrings.MSSQL);
            return new ConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
        }
    }
}

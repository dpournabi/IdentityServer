using IdentityServer8.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nsp.Common;

namespace IdentityServer.Infrastructure.Persistence
{
    public class PersistedGrantDbContext : IdentityServer8.EntityFramework.DbContexts.PersistedGrantDbContext
    {
        public PersistedGrantDbContext(DbContextOptions<IdentityServer8.EntityFramework.DbContexts.PersistedGrantDbContext> options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
    }
    public class PersistedGrantContextDesignTimeFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<IdentityServer8.EntityFramework.DbContexts.PersistedGrantDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(AppSettings.Build().ConnectionStrings.MSSQL);
            return new PersistedGrantDbContext(optionsBuilder.Options, new OperationalStoreOptions());
        }
    }
}

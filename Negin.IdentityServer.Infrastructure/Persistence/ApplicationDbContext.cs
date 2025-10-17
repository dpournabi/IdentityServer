using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace IdentityServer.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IApplicationDbContext
{
    private readonly IMediator? _mediator;
    private readonly AuditableEntitySaveChangesInterceptor? _auditableEntitySaveChangesInterceptor;

    #region Ctor

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                IMediator? mediator = null,
                                AuditableEntitySaveChangesInterceptor? auditableEntitySaveChangesInterceptor = null)
            : base(options)
    {
    }

    #endregion

    public DbSet<Domain.Entities.Application> Applications { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<DiscountType> DiscountTypes { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<IndustryCompany> IndustryCompanies { get; set; }
    public DbSet<Licence> Licences { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<PlanType> PlanTypes { get; set; }
    public new DbSet<RoleClaim> RoleClaims { get; set; }
    public new DbSet<UserClaim> UserClaims { get; set; }
    public new DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserOTPs> UserOTPs { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public new DbSet<UserRole> UserRoles { get; set; }
    public new DbSet<UserToken> UserTokens { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<RoleGroup> RoleGroups { get; set; }
    public DbSet<ClientClaims> ClientClaims { get; set; }

    public DatabaseFacade GetDatabase() => this.Database;

    #region Configorations
    protected override async void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<IndustryCompany>().HasOne(v => v.Company).WithMany().HasForeignKey(v => v.CompanyId);
        builder.Entity<IndustryCompany>().HasOne(v => v.Industry).WithMany().HasForeignKey(v => v.IndustryId);

        base.OnModelCreating(builder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration _configuration = GetConfiguration();
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private static IConfiguration GetConfiguration()
    {
#if RELEASE
        IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Production.json")
            .Build();
#else
        IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json")
            .Build();
#endif

        return _configuration;
    }

    #endregion

    #region IDesignTimeDbContextFactory
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfiguration _configuration = GetConfiguration();
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
    #endregion
}


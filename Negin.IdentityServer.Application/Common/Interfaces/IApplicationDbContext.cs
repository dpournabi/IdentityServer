using IdentityServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IdentityServer.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade GetDatabase();
    DbSet<Domain.Entities.Application> Applications { get; }
    DbSet<Domain.Entities.ApplicationUser> ApplicationUsers { get; }
    DbSet<Domain.Entities.ApplicationRole> ApplicationRoles { get; }
    DbSet<Domain.Entities.Branch> Branches { get; }
    //DbSet<Domain.Entities.Client> Clients { get; }
    DbSet<Domain.Entities.Company> Companies { get; }
    DbSet<Domain.Entities.DiscountType> DiscountTypes { get; }
    DbSet<Domain.Entities.Industry> Industries { get; }
    DbSet<Domain.Entities.IndustryCompany> IndustryCompanies { get; }
    DbSet<Domain.Entities.Licence> Licences { get; }
    DbSet<Domain.Entities.Plan> Plans { get; }
    DbSet<Domain.Entities.PlanType> PlanTypes { get; }
    DbSet<Domain.Entities.RoleClaim> RoleClaims { get; }
    DbSet<Domain.Entities.UserClaim> UserClaims { get; }
    DbSet<Domain.Entities.UserLogin> UserLogins { get; }
    DbSet<Domain.Entities.UserOTPs> UserOTPs { get; }
    DbSet<Domain.Entities.UserProfile> UserProfiles { get; }
    DbSet<Domain.Entities.UserRole> UserRoles { get; }
    DbSet<Domain.Entities.UserToken> UserTokens { get; }
    DbSet<Group> Groups { get; set; }
    DbSet<UserGroup> UserGroups { get; set; }
    DbSet<RoleGroup> RoleGroups { get; set; }
    DbSet<ClientClaims> ClientClaims { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

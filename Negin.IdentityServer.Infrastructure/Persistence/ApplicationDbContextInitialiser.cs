using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Common;
using IdentityServer8.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nsp.Common;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace IdentityServer.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private static ApplicationDbContext _context = new(new DbContextOptions<ApplicationDbContext> { });
    public static void MigrateDatabase(IApplicationBuilder app)
    {
        IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        PersistedGrantDbContext? persistedGrantDbContext = serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>();
        if (persistedGrantDbContext != null && persistedGrantDbContext.Database != null)
        {
#if DEBUG
            persistedGrantDbContext.Database.Migrate();
#endif
        }

        ApplicationDbContext? applicationDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        if (applicationDbContext != null && applicationDbContext.Database != null)
        {
#if DEBUG
            applicationDbContext.Database.Migrate();
#endif
        }

        ConfigurationDbContext? configurationDbContext = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
        if (configurationDbContext != null && configurationDbContext.Database != null)
        {
#if DEBUG
            configurationDbContext.Database.Migrate();
#endif
        }
    }
    public static void SeedAsync(IApplicationBuilder app)
    {
        //TrySeedCompaniesAsync();
        TrySeedIndustriesAsync();
        TrySeedApiResources(app);
        TrySeedApiScopes(app);
        TrySeedApiScopes(app);
        TrySeedClientsAsync(app);
        TrySeedApplicationsAsync();
        TrySeedPlanTypesAsync();
        TrySeedPlans();
        TrySeedLicences();
        TrySeedRoles();
        TrySeedDefaultUsers(app);
        TrySeedIdentityResources(app);
    }
    //public static void TrySeedCompaniesAsync()
    //{
    //    if (!_context.Companies.Any())
    //    {
    //        if (!_context.Companies.Any())
    //        {
    //            _context.Companies.Add(new Company
    //            {
    //                Id = (int)Companies.NovinMeyar,
    //                Name = "نوین معیار آزمای آپادانا",
    //                RegisterNo = "532038",
    //                NationalCode = "10820101739",
    //                TellPhone = "021-44011820"
    //            });

    //            _context.Branches.Add(new Branch
    //            {
    //                Id = (int)NovinMeyarBranches.Kermanshah,
    //                CompanyId = (int)Companies.NovinMeyar,
    //                Code = "83",
    //                Name = "شعبه کرمانشاه",
    //                IsActive = true
    //            });

    //            _context.Branches.Add(new Branch
    //            {
    //                Id = (int)NovinMeyarBranches.Mazandaran,
    //                CompanyId = (int)Companies.NovinMeyar,
    //                Code = "11",
    //                Name = "شعبه مازندران",
    //                IsActive = true
    //            });

    //            _context.Branches.Add(new Branch
    //            {
    //                Id = (int)NovinMeyarBranches.Tehran,
    //                CompanyId = (int)Companies.NovinMeyar,
    //                Code = "21",
    //                Name = "دفتر مرکزی-شعبه تهران",
    //                IsActive = true
    //            });

    //            _context.Branches.Add(new Branch
    //            {
    //                Id = (int)NovinMeyarBranches.Hamedan,
    //                CompanyId = (int)Companies.NovinMeyar,
    //                Code = "81",
    //                Name = "شعبه همدان",
    //                IsActive = true
    //            });

    //            _context.Branches.Add(new Branch
    //            {
    //                Id = (int)NovinMeyarBranches.WestAzarbaijan,
    //                CompanyId = (int)Companies.NovinMeyar,
    //                Code = "44",
    //                Name = "شعبه آذربایجان غربی",
    //                IsActive = true
    //            });

    //            _context.Branches.Add(new Branch
    //            {
    //                Id = (int)NovinMeyarBranches.EastAzarbaijan,
    //                CompanyId = (int)Companies.NovinMeyar,
    //                Code = "41",
    //                Name = "شعبه آذربایجان شرقی",
    //                IsActive = true
    //            });

    //            _context.Branches.Add(new Branch
    //            {
    //                Id = (int)NovinMeyarBranches.Shiraz,
    //                CompanyId = (int)Companies.NovinMeyar,
    //                Code = "71",
    //                Name = "شعبه شیراز",
    //                IsActive = true
    //            });

    //            _context.SaveChanges();
    //        }
    //    }
    //}
    public static void TrySeedIndustriesAsync()
    {
        if (!_context.Industries.Any())
        {
            if (!_context.Industries.Any(x => x.Name == "نرم افزار"))
            {
                _context.Industries.Add(new Industry
                {
                    Id = 1,
                    Name = "نرم افزار",
                });

                _context.SaveChanges();
            }
        }
    }
    public static void TrySeedClientsAsync(IApplicationBuilder app)
    {
        IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        ConfigurationDbContext _configurationDbContext = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
        IConfiguration? _configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();
        if (!_configurationDbContext.Clients.Any())
        {
            foreach (IdentityServer8.Models.Client client in ClientStore.Get(_configuration))
            {
                _configurationDbContext.Clients.Add(client.ToEntity());
            }
            _configurationDbContext.SaveChanges();
        }
    }
    public static void TrySeedIdentityResources(IApplicationBuilder app)
    {
        IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        ConfigurationDbContext _configurationDbContext = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();

        if (!_configurationDbContext.IdentityResources.Any())
        {
            foreach (IdentityServer8.Models.IdentityResource resource in ResourceStore.GetIdentityResources())
            {
                _configurationDbContext.IdentityResources.Add(resource.ToEntity());
            }

            _configurationDbContext.SaveChanges();
        }
    }
    public static void TrySeedApiScopes(IApplicationBuilder app)
    {
        IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        ConfigurationDbContext _configurationDbContext = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();

        if (!_configurationDbContext.ApiScopes.Any())
        {
            foreach (IdentityServer8.Models.ApiScope scope in ScopeStore.GetApiScopes())
            {
                _configurationDbContext.ApiScopes.Add(scope.ToEntity());
            }
            _configurationDbContext.SaveChanges();
        }
    }
    public static void TrySeedApiResources(IApplicationBuilder app)
    {
        IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        ConfigurationDbContext _configurationDbContext = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();

        if (!_configurationDbContext.ApiResources.Any())
        {
            foreach (IdentityServer8.Models.ApiResource resource in ResourceStore.GetApiResources())
            {
                _configurationDbContext.ApiResources.Add(resource.ToEntity());
            }

            _configurationDbContext.SaveChanges();
        }
    }
    public static void TrySeedApplicationsAsync()
    {
        if (!_context.Applications.Any())
        {
            if (!_context.Applications.Any(x => x.Name == "سامانه جامع امنیت نگین سیستم"))
            {
                _context.Applications.Add(new Domain.Entities.Application
                {
                    Id = (int)Applications.SJA,
                    Name = "سامانه جامع امنیت نگین سیستم",
                });

                _context.SaveChanges();
            }
        }
    }
    public static void TrySeedPlanTypesAsync()
    {
        if (!_context.PlanTypes.Any())
        {
            if (!_context.PlanTypes.Any(x => x.TitleEn == "Free"))
            {
                _context.PlanTypes.Add(new Domain.Entities.PlanType
                {
                    Id = (int)PlanTypes.Free,
                    TitleEn = "Free",
                    Title = "رایگان"
                });

                _context.SaveChanges();
            }

            if (!_context.PlanTypes.Any(x => x.TitleEn == "Gold"))
            {
                _context.PlanTypes.Add(new PlanType
                {
                    Id = (int)PlanTypes.Gold,
                    TitleEn = "Gold",
                    Title = "طلایی"
                });

                _context.SaveChanges();
            }

            if (!_context.PlanTypes.Any(x => x.TitleEn == "Silver"))
            {
                _context.PlanTypes.Add(new PlanType
                {
                    Id = (int)PlanTypes.Silver,
                    TitleEn = "Silver",
                    Title = "نقره ای"
                });

                _context.SaveChanges();
            }

            if (!_context.PlanTypes.Any(x => x.TitleEn == "Bronze"))
            {
                _context.PlanTypes.Add(new PlanType
                {
                    Id = (int)PlanTypes.Bronze,
                    TitleEn = "Bronze",
                    Title = "برنزی"
                });

                _context.SaveChanges();
            }
        }
    }
    public static void TrySeedPlans()
    {
        if (!_context.Plans.Any())
        {
            if (!_context.Plans.Include(x => x.PlanType).Any(x => x.PlanType.TitleEn == "Free"))
            {
                _context.Plans.Add(new Plan
                {
                    Id = (int)Plans.PlanFree,
                    Title = "پلن رایگان",
                    PlanTypeId = (int)PlanTypes.Free,
                    Amount = 0,
                    ResourceSize = 30,
                    DurationMonth = int.MaxValue
                });

                _context.SaveChanges();
            }

            if (!_context.Plans.Include(x => x.PlanType).Any(x => x.PlanType.TitleEn == "Gold"))
            {
                _context.Plans.Add(new Plan
                {
                    Id = (int)Plans.PlanGold,
                    Title = "پلن طلایی",
                    PlanTypeId = (int)PlanTypes.Gold,
                    Amount = 350000000,
                    ResourceSize = 60,
                    DurationMonth = 12
                });

                _context.SaveChanges();
            }

            if (!_context.Plans.Include(x => x.PlanType).Any(x => x.PlanType.TitleEn == "Silver"))
            {
                _context.Plans.Add(new Plan
                {
                    Id = (int)Plans.PlanSilver,
                    Title = "پلن نقره ای",
                    PlanTypeId = (int)PlanTypes.Silver,
                    Amount = 180000000,
                    ResourceSize = 30,
                    DurationMonth = 6
                });

                _context.SaveChanges();
            }

            if (!_context.Plans.Include(x => x.PlanType).Any(x => x.PlanType.TitleEn == "Bronze"))
            {
                _context.Plans.Add(new Plan
                {
                    Id = (int)Plans.PlanBronze,
                    Title = "پلن برنزی",
                    PlanTypeId = (int)PlanTypes.Bronze,
                    Amount = 50000000,
                    ResourceSize = 5,
                    DurationMonth = 1
                });

                _context.SaveChanges();
            }
        }
    }
    public static void TrySeedLicences()
    {
        if (!_context.Licences.Any())
        {
            if (!_context.Licences.Any(x => x.CompanyId == (int)Companies.NovinMeyar))
            {
                _context.Licences.Add(new Domain.Entities.Licence
                {
                    Id = 1,
                    ApplicationId = 1,
                    CompanyId = 1,
                    PlanId = (int)Plans.PlanFree,
                    PaymentStatus = true,
                    StartDate = DateTime.Now.AddYears(-2),
                    EndDate = DateTime.MaxValue,
                    IsActive = true,
                });

                _context.SaveChanges();
            }
        }
    }
    public static void TrySeedRoles()
    {
        if (!_context.ApplicationRoles.Any())
        {
            if (!_context.ApplicationRoles.Any(x => x.Name == "Root"))
            {
                _context.ApplicationRoles.Add(new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Root",
                    LocalName = "سوپر ادمین"
                });

                _context.ApplicationRoles.Add(new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrator",
                    LocalName = "ادمین",
                });

                _context.ApplicationRoles.Add(new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "SaleManager",
                    LocalName = "مدیر فروش",

                });

                _context.SaveChanges();
            }
        }
    }
    public static void TrySeedDefaultUsers(IApplicationBuilder app)
    {
        IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        SignInManager<ApplicationUser> _signInManager = serviceScope.ServiceProvider.GetService<SignInManager<ApplicationUser>>();

        if (!_context.ApplicationUsers.Any())
        {
            //Root
            if (!_context.ApplicationUsers.Any(x => x.UserName == "9128890105"))
            {
                Guid applicationUserId = Guid.NewGuid();
                ApplicationUser user = new()
                {
                    Id = applicationUserId,
                    UserName = "9128890105",
                    PhoneNumber = "09128890105",
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };

                _ = _signInManager.UserManager
                    .CreateAsync(user, "Dp9128890105@").Result;

                UserRole userRole = new()
                {
                    RoleId = _context.ApplicationRoles.First(x => x.Name == AssessorsManager.Root).Id,
                    UserId = applicationUserId
                };
                _context.UserRoles.Add(userRole);

                UserProfile userProfile = new()
                {
                    ApplicationUserId = user.Id,
                    FirstName = "ادمین شرکت",
                    LastName = " نگین سیستم",
                    CellPhone = "09128890105",
                };
                _context.UserProfiles.Add(userProfile);
                _context.SaveChanges();
            }
        }
    }

    #region Enums
    private enum Applications
    {
        SJA = 1
    }
    private enum Companies
    {
        NovinMeyar = 1,
    }

    private enum Plans
    {
        PlanFree = 1,
        PlanGold = 2,
        PlanSilver = 3,
        PlanBronze = 4
    }
    private enum PlanTypes
    {
        Free = 1,
        Gold = 2,
        Silver = 3,
        Bronze = 4
    }
    #endregion

}

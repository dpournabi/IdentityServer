using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using IdentityServer8.Extensions;
using IdentityServer8.Models;
using IdentityServer8.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityServer.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly IApplicationDbContext _dbContext;

        public ProfileService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _dbContext = dbContext;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();

            ApplicationUser? user = await _userManager.FindByIdAsync(sub);

            ClaimsPrincipal principal = await _claimsFactory.CreateAsync(user);

            var baseRole = await _dbContext.ApplicationRoles.FirstOrDefaultAsync(x => x.Name == "Client");

            UserRole userRole = await (from ur in _dbContext.UserRoles
                                       join u in _dbContext.ApplicationUsers on ur.UserId equals u.Id
                                       where (baseRole == null || ur.RoleId != baseRole.Id) &&
                                             ur.UserId == user.Id &&
                                             u.IsActive
                                       select ur)
                                .FirstAsync();

            ApplicationRole? applicationRole = null;

            if (userRole != null)
            {
                applicationRole = await _dbContext.ApplicationRoles
                                                    .Include(x => x.Parent)
                                                    .FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
            }

            List<Claim> claims = principal.Claims.ToList();

            Company? company = null;
            if (user.CompanyId.HasValue)
            {
                company = await _dbContext.Companies.FirstAsync(x => x.Id == user.CompanyId);
                claims.Add(new Claim("CompanyId", user.CompanyId.Value.ToString()));
                claims.Add(new Claim("CompanyName", company.Name));
            }

            claims.AddRange(new[]
            {
                new Claim("IsActive", user.IsActive.ToString()),
                new Claim("Firstname", user.FirstName ?? string.Empty),
                new Claim("Lastname", user.LastName ?? string.Empty),
                new Claim("LocalRoleName", applicationRole?.LocalName ?? string.Empty)
            });

            if (applicationRole != null && applicationRole.ParentId.HasValue)
            {
                claims.Add(new Claim("ParentRoleId", applicationRole.ParentId.Value.ToString()));

                if (applicationRole.Parent != null)
                {
                    claims.Add(new Claim("ParentRoleName", applicationRole.Parent.Name.ToString()));
                }
            }

            var roleClaims = (from r in _dbContext.ApplicationRoles
                              join ur in _dbContext.UserRoles on r.Id equals ur.RoleId
                              join u in _dbContext.ApplicationUsers on ur.UserId equals u.Id
                              join rc in _dbContext.RoleClaims on r.Id equals rc.RoleId
                              where u.Id == user.Id
                              select new Claim(rc.ClaimType, rc.ClaimValue)
                            ).ToList();

            claims.AddRange(roleClaims);

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();

            ApplicationUser user = await _userManager.FindByIdAsync(sub);

            context.IsActive = user != null;
        }
    }
}

using IdentityModel;
using IdentityServer8.Models;

namespace IdentityServer.Infrastructure.Common
{
    internal static class ResourceStore
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    UserClaims = { JwtClaimTypes.Role }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "app.governorate",
                    Scopes = new[] { "app.governorate", JwtClaimTypes.Role },
                    UserClaims = new [] { JwtClaimTypes.Role }
                },
                new ApiResource
                {
                    Name = "app.shipping",
                    Scopes = new[] { "app.shipping", JwtClaimTypes.Role },
                    UserClaims = new [] { JwtClaimTypes.Role }
                }
            };
        }
    }
}

using IdentityModel;
using IdentityServer8.Models;
using Microsoft.Extensions.Configuration;
using Nsp.Common;
using static IdentityServer8.IdentityServerConstants;
using Client = IdentityServer8.Models.Client;
using GrantTypes = IdentityModel.OidcConstants.GrantTypes;

namespace IdentityServer.Infrastructure.Common
{
    public static class ClientStore
    {
        public static IEnumerable<Client> Get(IConfiguration configuration)
        {
            AppSettings _appSettings = configuration.Get<AppSettings>();
            List<Client> clients = new()
            {
                //Governorate of Hormozgan Province
                new Client
                {
                    ClientName = "Governorate of Hormozgan Province web client",
                    ClientId = "7868d015-6c79-4da4-b789-8b4d40611946",

                    AllowedGrantTypes = new string[] { GrantTypes.Password, GrantTypes.RefreshToken },
                    AllowedScopes =
                    {
                        "app.governorate",
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        StandardScopes.Phone,
                        "roles"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new("D3g4oVEM7kmk3E26yOPPeA".Sha256())
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = _appSettings.CORSTrustedOrigins.ToList(),
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = true,
                    RequirePkce = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    Enabled = true,
                    Claims = new ClientClaim[]
                    {
                        new(JwtClaimTypes.Role, AssessorsManager.Root),
                        new(JwtClaimTypes.Role, AssessorsManager.Administrator)
                    },

                    //Refresh token settings
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    // TokenExpiration description:
                    //      Sliding: when refreshing the token, the lifetime of the refresh token will be renewed (by the amount specified in SlidingRefreshTokenLifetime). The lifetime will not exceed AbsoluteRefreshTokenLifetime.
                    //      Absolute:the refresh token will expire on a fixed point in time (specified by the AbsoluteRefreshTokenLifetime). This is the default.

                    RefreshTokenUsage = TokenUsage.OneTimeOnly,//.OneTimeOnly,
                    SlidingRefreshTokenLifetime = 60 * 60 * 24, // 1 day
                    UpdateAccessTokenClaimsOnRefresh = true,
                },

                //Shipping and Ports Organization
                new Client
                {
                    ClientName = "Shipping and Ports Organization web client",
                    ClientId = "6bd073b8-cf26-43de-bac8-2f9f291cd56a",

                    AllowedGrantTypes = new string[] { GrantTypes.Password, GrantTypes.RefreshToken },
                    AllowedScopes =
                    {
                        "app.shipping",
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        StandardScopes.Phone,
                        "roles"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new("V4wAzzkxo0ueyLwDLnVRhw".Sha256())
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = _appSettings.CORSTrustedOrigins.ToList(),
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = true,
                    RequirePkce = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    Enabled = true,
                    Claims = new ClientClaim[]
                    {
                        new(JwtClaimTypes.Role, AssessorsManager.Root),
                        new(JwtClaimTypes.Role, AssessorsManager.Administrator)
                    },

                    //Refresh token settings
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    // TokenExpiration description:
                    //      Sliding: when refreshing the token, the lifetime of the refresh token will be renewed (by the amount specified in SlidingRefreshTokenLifetime). The lifetime will not exceed AbsoluteRefreshTokenLifetime.
                    //      Absolute:the refresh token will expire on a fixed point in time (specified by the AbsoluteRefreshTokenLifetime). This is the default.

                    RefreshTokenUsage = TokenUsage.OneTimeOnly,//.OneTimeOnly,
                    SlidingRefreshTokenLifetime = 60 * 60 * 24, // 1 day
                    UpdateAccessTokenClaimsOnRefresh = true,
                }
            };

            return clients;
        }
    }
}

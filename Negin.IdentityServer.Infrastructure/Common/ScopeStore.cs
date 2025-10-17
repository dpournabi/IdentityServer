using IdentityServer8.Models;
namespace IdentityServer.Infrastructure.Common;

public static class ScopeStore
{
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
            new ApiScope("app.governorate"),
            new ApiScope("app.shipping")
        };
    }

    public static Dictionary<string, string> GetApiScopesDictionary()
    {
        return new Dictionary<string, string>
        {
            //{ "app.governorate", "app.governorate" },
            { "app.shipping", "app.shipping" },
        };
    }
}

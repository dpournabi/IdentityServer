using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace IdentityServer.Application
{
    public static class Extentions
    {
        public static string ConvertToPersianDate(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return $"{persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date)}/{persianCalendar.GetDayOfMonth(date)}";
        }
        public static string UserName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserName");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}

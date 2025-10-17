namespace IdentityServer.Application.Common.Interfaces;

public interface IDateTimeService
{
    DateTime Now { get; }
    DateTime ConvertPersianToGregurianDate(string persianDate);
    string GetPersianDateString(DateTime dateTime);
    string GetPersianYearMonth(DateTime dateTime);
    int GetPersianYear(DateTime dateTime);
    int GetPersianMonth(DateTime dateTime);
}

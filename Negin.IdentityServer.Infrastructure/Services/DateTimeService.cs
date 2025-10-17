using System.Globalization;
using IdentityServer.Application.Common.Interfaces;

namespace IdentityServer.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
    private readonly PersianCalendar persianCalendar;

    public DateTimeService()
    {
        persianCalendar = new PersianCalendar();
    }
    public DateTime ConvertPersianToGregurianDate(string persianDate)
    {
        var dateParts = persianDate.Split(new char[] { '/', '-', '.' });
        return persianCalendar.ToDateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]), 0, 0, 0, 0);
    }

    public string GetPersianDateString(DateTime dateTime) => $"{persianCalendar.GetYear(dateTime)}/{persianCalendar.GetMonth(dateTime)}/{persianCalendar.GetDayOfMonth(dateTime)}";
    public string GetPersianYearMonth(DateTime dateTime) => $"{persianCalendar.GetYear(dateTime)}/{persianCalendar.GetMonth(dateTime)}";
    public int GetPersianYear(DateTime dateTime) => persianCalendar.GetYear(dateTime);
    public int GetPersianMonth(DateTime dateTime) => persianCalendar.GetMonth(dateTime);
}

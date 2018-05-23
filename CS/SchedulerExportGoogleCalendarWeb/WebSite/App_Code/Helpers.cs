using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Calendar.v3.Data;
using DevExpress.XtraScheduler.iCalendar.Native;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler;

public class ResourceFiller {
    public static string[] Users = new string[] { "Sarah Brighton", "Ryan Fischer", "Andrew Miller" };
    public static string[] Usernames = new string[] { "sbrighton", "rfischer", "amiller" };

    public static void FillResources(ASPxSchedulerStorage storage, int count) {
        ResourceCollection resources = storage.Resources.Items;
        storage.BeginUpdate();
        try {
            int cnt = Math.Min(count, Users.Length);
            for (int i = 1; i <= cnt; i++) {
                resources.Add(new Resource(Usernames[i - 1], Users[i - 1]));
            }
        }
        finally {
            storage.EndUpdate();
        }
    }
}

public static class CalendarApiHelper {
    public static DateTime ConvertDateTime(EventDateTime start) {
        if (start == null)
            return DateTime.MinValue;
        if (start.DateTime.HasValue)
            return start.DateTime.Value;
        return DateTime.Parse(start.Date);
    }
    public static bool IsAllDay(EventDateTime start) {
        if (start == null)
            return false;
        return !start.DateTime.HasValue;
    }
    public static string GetRawDate(DateTime date) {
        return date.ToString("yyyy-MM-dd");
    }
    public static void MakeAllDay(EventDateTime eventDate) {
        DateTime dateTime = eventDate.DateTime.Value;
        eventDate.DateTime = null;
        eventDate.DateTimeRaw = CalendarApiHelper.GetRawDate(dateTime.Date);
    }
   
    public static string GetVTimeZone(string timeZoneId)
    {
        return TimeZoneConverter.ConvertToVTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)).TimeZoneIdentifier.Value;
    }
    internal static void MakeDate(EventDateTime eventDate) {
        DateTime dateTime = ConvertDateTime(eventDate);
        eventDate.DateTime = dateTime;
    }
    public static EventDateTime MakeEventDate(DateTime dt)
    {
        EventDateTime evdt = new EventDateTime();
        evdt.DateTime = dt;
        return evdt;
    }
   
}

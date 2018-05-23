using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Calendar.v3.Data;
using DevExpress.XtraScheduler.iCalendar.Native;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler;

namespace GoogleCalendarAPI
{
    public static class CalendarApiHelper
    {
        public static DateTime ConvertDateTime(EventDateTime start)
        {
            if (start == null)
                return DateTime.MinValue;
            if (start.DateTime.HasValue)
                return start.DateTime.Value;
            return DateTime.Parse(start.Date);
        }
        public static bool IsAllDay(EventDateTime start)
        {
            if (start == null)
                return false;
            return !start.DateTime.HasValue;
        }
        public static string GetRawDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
        public static void MakeAllDay(EventDateTime eventDate)
        {
            DateTime dateTime = eventDate.DateTime.Value;
            eventDate.DateTime = null;
            eventDate.DateTimeRaw = CalendarApiHelper.GetRawDate(dateTime.Date);
        }

        public static string GetVTimeZone(string timeZoneId)
        {
            return TimeZoneConverter.ConvertToVTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)).TimeZoneIdentifier.Value;
        }
        internal static void MakeDate(EventDateTime eventDate)
        {
            DateTime dateTime = ConvertDateTime(eventDate);
            eventDate.DateTime = dateTime;
        }
        public static EventDateTime MakeEventDate(DateTime dt)
        {
            EventDateTime evdt = new EventDateTime();
            evdt.DateTime = dt;
            return evdt;
        }
        public static string GetStatusString(object id)
        {
            int value = (int)id;
            switch (value)
            {
                case 0:
                    return "confirmed";
                case 1:
                    return "tentative";
                case 2:
                    return "cancelled";
                default:
                    throw new Exception("Unknown Status Id");
            }
        }

    }
}

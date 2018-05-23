using System;
using System.Web.UI.WebControls;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler;
using System.Web.UI;
using System.ComponentModel;


public class AppointmentExporter
{
    ASPxScheduler control;
    CalendarService service;
    string calendarId;
    public AppointmentExporter(ASPxScheduler control, CalendarService service, string calendarId)
    {
        this.control = control;
        this.service = service;
        this.calendarId = calendarId;
    }
    private void AssignProperties(Appointment customEvent, Event newEvent)
    {
        newEvent.Summary = customEvent.Subject;
        newEvent.Description = customEvent.Description;
        newEvent.Location = customEvent.Location;
        newEvent.Start = CalendarApiHelper.MakeEventDate(customEvent.Start);
        newEvent.Start.TimeZone = CalendarApiHelper.GetVTimeZone(control.Storage.TimeZoneId);
        newEvent.End = CalendarApiHelper.MakeEventDate(customEvent.End);
        newEvent.End.TimeZone = CalendarApiHelper.GetVTimeZone(control.Storage.TimeZoneId);
    }

    private Event CreateEvent(Appointment apt)
    {
        Event instance = new Event();
        AssignProperties(apt, instance);
        return instance;
    }
    public void Export()
    {
        for (int i = 0; i < control.SelectedAppointments.Count; i++)
        {
            Appointment apt = control.SelectedAppointments[i];
            Event instance = CreateEvent(apt);
            if (apt.Type == AppointmentType.Normal)
            {
                this.service.Events.Insert(instance, this.calendarId).Execute();
            }

        }
    }
}

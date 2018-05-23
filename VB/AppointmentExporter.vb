Imports System
Imports System.Web.UI.WebControls
Imports Google.Apis.Services
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Calendar.v3
Imports Google.Apis.Calendar.v3.Data
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler
Imports System.Web.UI
Imports System.ComponentModel

Namespace GoogleCalendarAPI
    Public Class AppointmentExporter
        Private control As ASPxScheduler
        Private service As CalendarService
        Private calendarId As String
        Public Sub New(ByVal control As ASPxScheduler, ByVal service As CalendarService, ByVal calendarId As String)
            Me.control = control
            Me.service = service
            Me.calendarId = calendarId
        End Sub
        Private Sub AssignProperties(ByVal customEvent As Appointment, ByVal newEvent As [Event])
            newEvent.Summary = customEvent.Subject
            newEvent.Description = customEvent.Description
            newEvent.Location = customEvent.Location
            newEvent.Start = CalendarApiHelper.MakeEventDate(customEvent.Start)
            newEvent.Start.TimeZone = CalendarApiHelper.GetVTimeZone(control.Storage.TimeZoneId)
            newEvent.End = CalendarApiHelper.MakeEventDate(customEvent.End)
            newEvent.End.TimeZone = CalendarApiHelper.GetVTimeZone(control.Storage.TimeZoneId)
        End Sub

        Private Function CreateEvent(ByVal apt As Appointment) As [Event]
            Dim instance As New [Event]()
            AssignProperties(apt, instance)
            Return instance
        End Function
        Public Sub Export()
            For i As Integer = 0 To control.SelectedAppointments.Count - 1
                Dim apt As Appointment = control.SelectedAppointments(i)
                Dim instance As [Event] = CreateEvent(apt)
                If apt.Type = AppointmentType.Normal Then
                    Me.service.Events.Insert(instance, Me.calendarId).Execute()
                End If

            Next i
        End Sub
    End Class
End Namespace
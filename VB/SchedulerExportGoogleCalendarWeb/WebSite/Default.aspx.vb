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

Partial Public Class [Default]
    Inherits System.Web.UI.Page

    Private service As CalendarService
    Private ReadOnly Property Storage() As ASPxSchedulerStorage
        Get
            Return ASPxScheduler1.Storage
        End Get
    End Property
    Private calendarId As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Session("credential") Is Nothing Then
            Return
        End If

        service = New CalendarService(New BaseClientService.Initializer() With {.HttpClientInitializer = TryCast(Session("credential"), UserCredential), .ApplicationName = "GoogleApiTest"})

        If service Is Nothing Then
            Return
        End If
        If Not IsPostBack Then
            Dim calendarList As CalendarList = Me.service.CalendarList.List().Execute()
            For Each calendarItem In calendarList.Items
                lbCalendars.Items.Add(New ListItem(calendarItem.Summary, calendarItem.Id))
            Next calendarItem
            lbCalendars.SelectedIndex = 0
        End If
        Me.calendarId = lbCalendars.SelectedValue

        SetupMappings()
        ResourceFiller.FillResources(Me.ASPxScheduler1.Storage, 3)

        ASPxScheduler1.AppointmentDataSource = appointmentDataSource
        ASPxScheduler1.DataBind()

        ASPxScheduler1.GroupType = SchedulerGroupType.Resource
    End Sub

    Private Sub SetupMappings()
        Dim mappings As ASPxAppointmentMappingInfo = Storage.Appointments.Mappings
        Storage.BeginUpdate()
        Try
            mappings.AppointmentId = "Id"
            mappings.Start = "StartTime"
            mappings.End = "EndTime"
            mappings.Subject = "Subject"
            mappings.AllDay = "AllDay"
            mappings.Description = "Description"
            mappings.Label = "Label"
            mappings.Location = "Location"
            mappings.RecurrenceInfo = "RecurrenceInfo"
            mappings.ReminderInfo = "ReminderInfo"
            mappings.ResourceId = "OwnerId"
            mappings.Status = "Status"
            mappings.Type = "EventType"
        Finally
            Storage.EndUpdate()
        End Try
    End Sub

    Protected Sub appointmentsDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
        e.ObjectInstance = New CustomEventDataSource(GetCustomEvents())
    End Sub

    Private Function GetCustomEvents() As CustomEventList

        Dim events_Renamed As CustomEventList = TryCast(Session("ListBoundModeObjects"), CustomEventList)
        If events_Renamed Is Nothing Then
            events_Renamed = GenerateCustomEventList()
            Session("ListBoundModeObjects") = events_Renamed
        End If
        Return events_Renamed
    End Function

    Protected Sub ASPxScheduler1_AppointmentInserting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
        SetAppointmentId(sender, e)
    End Sub

    Private Sub SetAppointmentId(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)

        Dim storage_Renamed As ASPxSchedulerStorage = DirectCast(sender, ASPxSchedulerStorage)
        Dim apt As Appointment = CType(e.Object, Appointment)
        storage_Renamed.SetAppointmentId(apt, apt.GetHashCode())
    End Sub

    #Region "Random events generation"
    Private Function GenerateCustomEventList() As CustomEventList
        Dim eventList As New CustomEventList()
        Dim count As Integer = Storage.Resources.Count
        For i As Integer = 0 To count - 1
            Dim resource As Resource = Storage.Resources(i)
            Dim subjPrefix As String = resource.Caption & "'s "

            eventList.Add(CreateEvent(resource.Id, subjPrefix & "meeting", 2, 5))
            eventList.Add(CreateEvent(resource.Id, subjPrefix & "travel", 3, 6))
            eventList.Add(CreateEvent(resource.Id, subjPrefix & "phone call", 0, 10))
        Next i
        Return eventList
    End Function

    Private Function CreateEvent(ByVal resourceId As Object, ByVal subject As String, ByVal status As Integer, ByVal label As Integer) As CustomEvent
        Dim customEvent As New CustomEvent()
        customEvent.Subject = subject
        customEvent.OwnerId = resourceId
        Dim rnd As Random = rndInstance
        Dim rangeInHours As Integer = 48
        customEvent.StartTime = Date.Today + TimeSpan.FromHours(rnd.Next(0, rangeInHours))
        customEvent.EndTime = customEvent.StartTime.Add(TimeSpan.FromHours(rnd.Next(0, rangeInHours \ 8)))
        customEvent.Status = status
        customEvent.Label = label
        customEvent.Id = "ev" & customEvent.GetHashCode()
        Return customEvent
    End Function
    Private Shared rndInstance As New Random()
    #End Region




    Protected Sub ASPxScheduler1_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase)
        Dim exporter As New AppointmentExporter(ASPxScheduler1, service, calendarId)
        exporter.Export()


    End Sub
End Class

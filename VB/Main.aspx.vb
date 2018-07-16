Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Calendar.v3
Imports Google.Apis.Calendar.v3.Data
Imports Google.Apis.Services
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace GoogleCalendarAPI
    Partial Public Class Main
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

                service = New CalendarService(New BaseClientService.Initializer() With { _
                    .HttpClientInitializer = TryCast(Session("credential"), UserCredential), _
                    .ApplicationName = "GoogleApiTest" _
                })

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

                ASPxScheduler1.AppointmentDataSource = appointmentDataSource
                ASPxScheduler1.DataBind()
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
                    mappings.Status = "StatusId"
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

                For i As Integer = 0 To 1
                    eventList.Add(CreateEvent("meeting", 1, 5))
                    eventList.Add(CreateEvent("travel", 2, 6))
                    eventList.Add(CreateEvent("phone call", 0, 10))
                Next i
                Return eventList
            End Function

            Private Function CreateEvent(ByVal subject As String, ByVal status As Integer, ByVal label As Integer) As CustomEvent
                Dim customEvent As New CustomEvent()
                customEvent.Subject = subject
                Dim rnd As Random = rndInstance
                Dim rangeInHours As Integer = 48
                customEvent.StartTime = Date.Today + TimeSpan.FromHours(rnd.Next(0, rangeInHours))
                customEvent.EndTime = customEvent.StartTime.Add(TimeSpan.FromHours(rnd.Next(0, rangeInHours \ 8)))
                customEvent.StatusId = status
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


End Namespace
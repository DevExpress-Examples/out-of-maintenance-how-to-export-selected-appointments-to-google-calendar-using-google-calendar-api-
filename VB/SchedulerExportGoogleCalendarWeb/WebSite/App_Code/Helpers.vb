Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Google.Apis.Calendar.v3.Data
Imports DevExpress.XtraScheduler.iCalendar.Native
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler

Public Class ResourceFiller
    Public Shared Users() As String = { "Sarah Brighton", "Ryan Fischer", "Andrew Miller" }
    Public Shared Usernames() As String = { "sbrighton", "rfischer", "amiller" }

    Public Shared Sub FillResources(ByVal storage As ASPxSchedulerStorage, ByVal count As Integer)
        Dim resources As ResourceCollection = storage.Resources.Items
        storage.BeginUpdate()
        Try
            Dim cnt As Integer = Math.Min(count, Users.Length)
            For i As Integer = 1 To cnt
                resources.Add(New Resource(Usernames(i - 1), Users(i - 1)))
            Next i
        Finally
            storage.EndUpdate()
        End Try
    End Sub
End Class

Public NotInheritable Class CalendarApiHelper

    Private Sub New()
    End Sub

    Public Shared Function ConvertDateTime(ByVal start As EventDateTime) As Date
        If start Is Nothing Then
            Return Date.MinValue
        End If
        If start.DateTime.HasValue Then
            Return start.DateTime.Value
        End If
        Return Date.Parse(start.Date)
    End Function
    Public Shared Function IsAllDay(ByVal start As EventDateTime) As Boolean
        If start Is Nothing Then
            Return False
        End If
        Return Not start.DateTime.HasValue
    End Function
    Public Shared Function GetRawDate(ByVal [date] As Date) As String
        Return [date].ToString("yyyy-MM-dd")
    End Function
    Public Shared Sub MakeAllDay(ByVal eventDate As EventDateTime)
        Dim dateTime As Date = eventDate.DateTime.Value
        eventDate.DateTime = Nothing
        eventDate.DateTimeRaw = CalendarApiHelper.GetRawDate(dateTime.Date)
    End Sub

    Public Shared Function GetVTimeZone(ByVal timeZoneId As String) As String
        Return TimeZoneConverter.ConvertToVTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)).TimeZoneIdentifier.Value
    End Function
    Friend Shared Sub MakeDate(ByVal eventDate As EventDateTime)
        Dim dateTime As Date = ConvertDateTime(eventDate)
        eventDate.DateTime = dateTime
    End Sub
    Public Shared Function MakeEventDate(ByVal dt As Date) As EventDateTime
        Dim evdt As New EventDateTime()
        evdt.DateTime = dt
        Return evdt
    End Function

End Class

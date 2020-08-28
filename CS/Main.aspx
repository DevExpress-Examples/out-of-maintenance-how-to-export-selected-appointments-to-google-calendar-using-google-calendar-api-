<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="GoogleCalendarAPI.Main" %>



<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v15.2.Core, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>
    
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exporter to Google Calendar Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <table runat="server" >
            <tr>
                <td>
                       <asp:ListBox runat="server" ID="lbCalendars" AutoPostBack="true"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td style="height: 26px">
                    <dx:ASPxButton ID="btnExport" runat="server" AutoPostBack="False" Text="Export Selected Appointments To Google Calendar">
                        <ClientSideEvents Click="function(s, e) { scheduler.PerformCallback('EXPSELGC'); }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        
        
        <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" ClientInstanceName="scheduler"
            OnAppointmentInserting="ASPxScheduler1_AppointmentInserting" OnCustomCallback="ASPxScheduler1_CustomCallback" >
               <Views>
                <WeekView Enabled="false">
                </WeekView>
                <FullWeekView Enabled="true">
                </FullWeekView>
            </Views>
        </dxwschs:ASPxScheduler>
        
        <asp:ObjectDataSource ID="appointmentDataSource" runat="server" DataObjectTypeName="GoogleCalendarAPI.CustomEvent"
            TypeName="GoogleCalendarAPI.CustomEventDataSource" DeleteMethod="DeleteMethodHandler" SelectMethod="SelectMethodHandler"
            InsertMethod="InsertMethodHandler" UpdateMethod="UpdateMethodHandler" OnObjectCreated="appointmentsDataSource_ObjectCreated">
        </asp:ObjectDataSource>
    </form>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.1, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v15.1.Core, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
        </dxwschs:ASPxScheduler>
        
        <asp:ObjectDataSource ID="appointmentDataSource" runat="server" DataObjectTypeName="CustomEvent"
            TypeName="CustomEventDataSource" DeleteMethod="DeleteMethodHandler" SelectMethod="SelectMethodHandler"
            InsertMethod="InsertMethodHandler" UpdateMethod="UpdateMethodHandler" OnObjectCreated="appointmentsDataSource_ObjectCreated">
        </asp:ObjectDataSource>
    </form>
</body>
</html>

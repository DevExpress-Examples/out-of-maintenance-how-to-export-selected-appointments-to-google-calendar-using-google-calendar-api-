<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oauth2callback.aspx.cs" Inherits="oauth2callback" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autorization form</title>
    <script type="text/javascript">
        function OnBtClick(s, e) {
            ASPxUploadControl1.Upload();
        }
        function OnFileUploadComplete(s, e) {
            window.location.href = e.callbackData;
            debugger;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
            <Items>
                <dx:LayoutGroup Caption="Google API Credentials">
                    <Items>
                        <dx:LayoutItem Caption="User name:" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxTextBox ID="tbUserID" runat="server"   Width="100%" Text="youremail@email.com">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                               <dx:LayoutItem Caption="Select .json file with credentials to enable the Google Calendar API:">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxUploadControl runat="server" UploadMode="Advanced" ID="ASPxUploadControl1" ClientIDMode="Static" ClientInstanceName="ASPxUploadControl1"
                                   Width="280px"  FileUploadMode="OnPageLoad" OnFileUploadComplete="ASPxUploadControl1_FileUploadComplete">
                                    <ValidationSettings  AllowedFileExtensions=".json"></ValidationSettings>
                                  <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                              </dx:ASPxUploadControl>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutItem Caption="" VerticalAlign="Middle">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxButton ID="ASPxFormLayout1_E5" runat="server" ClientIDMode="Static" ClientInstanceName="btUploadFile" Text="Get 'Client ID' and 'Client secret' from file" AutoPostBack="false">
                                <ClientSideEvents Click="OnBtClick"/>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
              
            </Items>
        </dx:ASPxFormLayout>
    
    </div>
    </form>
</body>
</html>

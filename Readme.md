<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/134574208/15.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4667)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to export selected appointments to Google Calendar using Google Calendar API


<p>This example illustrates how to export selected appointments to a specific Google Calendar using the <strong>Google Calendar API</strong>. Google provides the corresponding guidelines regarding the use of this API:</p>
<p><a href="https://developers.google.com/google-apps/calendar/quickstart/dotnet">Google Calendar API</a> </p>
<p>Before using this API, make certain you have read and are in compliance with <a href="https://developers.google.com/site-policies">Google’s licensing terms</a>. Next, you’ll need to generate a corresponding <strong>JSON file</strong> with credentials to enable the <strong>Google Calendar API.</strong></p>
<p> </p>
<p>We have a corresponding KB article which contains a step-by-step description of how to generate this <strong>JSON file </strong>for the <a href="https://developers.google.com/identity/protocols/OAuth2">installed application</a> type:</p>
<p><a href="https://www.devexpress.com/Support/Center/p/T267842">How to enable the Google Calendar API to use it in your application</a></p>
<p><br>The the OAuth 2.0 flow both for authentication and for obtaining authorization is similar to the one for <a href="https://developers.google.com/api-client-library/python/auth/web-app">Web Server Applications</a> except three points:</p>
<p>1) When creating a client ID, you specify that your application is an Installed application. This results in a different value for the <em><code>redirect_uri</code></em> parameter. <br>2) The client ID and client secret obtained from the API Console are embedded in the source code of your application. In this context, the client secret is obviously not treated as a secret. <br>3) The authorization code can be returned to your application in the title bar of the browser or in the query string of an HTTP request to the local host. <br><br>Please refer to <a href="https://developers.google.com/api-client-library/python/auth/installed-app">Google OAuth 2.0</a> documentation for more information.</p>
<p><br>After you generate this JSON file, start the<em> "oauth2callback.aspx"</em> page for authorization. <br><br>1. Enter the email address you used to generate the JSON file.<br>2. Select the JSON file on the client machine by clicking the "Browse" button.<br>3. Click the "Get 'Client ID' and 'Client secret' from file" button to upload the selected file and enable the Google Calendar API.<br>4. The application should be navigated to the <em>"Default.aspx"</em> page.<br>5. Select a corresponding calendar to which the selected appointments are exported from the list.</p>
<p> </p>
<p><strong><br>P.S. To run this example's solution, include the corresponding "Google Calendar API" assemblies into the project.</strong></p>
<p><strong>For this, open the "Package Manager Console" (Tools - NuGet Package Manager) and execute the following command:</strong><br><br></p>
<pre class="prettyprint notranslate"><code>Install-Package Google.Apis.Calendar.v3</code></pre>
<p><strong>See Also:</strong></p>
<p><a href="https://www.devexpress.com/Support/Center/p/E502">Synchronizing with Google Calendar</a></p>
<p><a href="https://www.devexpress.com/Support/Center/p/E3218">How to import Google Calendar using Google Calendar API</a></p>


<h3>Description</h3>

<p>The appointments in this example are exported by using the custom&nbsp;<strong>AppointmentExporter&nbsp;</strong>class. A method to export the appointments is called in the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_CustomCallbacktopic">ASPxScheduler.CustomCallback&nbsp;</a>event handler.&nbsp;<strong>AppointmentExporter&nbsp;</strong>creates an&nbsp;<strong>Event</strong>&nbsp;(Google.Apis.Calendar.v3.Data) instance for each appointment and copies the appointment's properties to this object. Then the&nbsp;new <strong>Event</strong>&nbsp;object is pasted to the Google Calendar using the&nbsp;<strong>CalendarService&nbsp;</strong>Google service Insert method.&nbsp;</p>

<br/>



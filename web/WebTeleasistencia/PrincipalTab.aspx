<%@ Page Language="C#" Culture="auto" UICulture="auto" AutoEventWireup="true" CodeBehind="PrincipalTab.aspx.cs"
    Inherits="Nextgal.ECare.WebTeleasistencia.PrincipalTab" %>

<%@ Register Assembly="obout_Window_NET" Namespace="OboutInc.Window" TagPrefix="owd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/infowindow.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" language="JavaScript">
        function textCounter(txt, car, maxlimit) {
            if (txt.value.length > maxlimit) {
                txt.value = txt.value.substring(0, maxlimit);
            }
            car.innerText = maxlimit - txt.value.length;
        }
    </script>  
</head>
<body>

    <script type="text/javascript">

        function RedirectDefault() 
        {
            window.parent.location.href = '../Default.aspx';
        }

        function RedirectTraceRoute() 
        {
            var hidden = document.getElementById('hfFollow').value;
            var params = hidden.split(",");
            var idActive = params[0];
            var startDate = params[1];
            var startHour = params[2];
            var endDate = params[3];
            var endHour = params[4];
            //var redirectLocationParams = window.parent.location.href.split("/");
            var redirectLocation = document.getElementById('hfLocation').value;
            var path = 'MapTraceRoute.aspx?idActive=' + idActive + '&startDate=' + startDate + '&startHour=' + startHour + '&endDate=' + endDate + '&endHour=' + endHour + '&returnUrl=' + redirectLocation;
            window.parent.location.href = path;
        }

        function RedirectLastPosition() 
        {
            var hidden = document.getElementById('hfFollow').value;
            var params = hidden.split(",");
            var idActive = params[0];
            var startDate = params[1];
            var startHour = params[2];
            var endDate = params[3];
            var endHour = params[4];
            //var redirectLocationParams = window.parent.location.href.split("/");
            var redirectLocation = document.getElementById('hfLocation').value;
            var path = 'MapLastPosition.aspx?idActive=' + idActive + '&startDate=' + startDate + '&startHour=' + startHour + '&endDate=' + endDate + '&endHour=' + endHour + '&returnUrl=' + redirectLocation;
            window.parent.location.href = path;
        }

        function RedirectSchedule() 
        {
            var hidden = document.getElementById('hfSchedule').value;
            var params = hidden.split(",");
            var idActive = params[0];
            var startDate = params[1];
            var startHour = params[2];
            var endDate = params[3];
            var endHour = params[4];
            //var redirectLocationParams = window.parent.location.href.split("/");
            var redirectLocation = document.getElementById('hfLocation').value;
            var path = 'Schedule.aspx?idActive=' + idActive + '&startDate=' + startDate + '&startHour=' + startHour + '&endDate=' + endDate + '&endHour=' + endHour + '&returnUrl=' + redirectLocation;
            window.parent.location.href = path;
        }

        function RedirectMessages() 
        {
            var hidden = document.getElementById('hfMessages').value;
            var params = hidden.split(",");
            var idActive = params[0];
            var startDate = params[1];
            var startHour = params[2];
            var endDate = params[3];
            var endHour = params[4];
            //var redirectLocationParams = window.parent.location.href.split("/");
            var redirectLocation = document.getElementById('hfLocation').value;
            var path = 'Messages.aspx?idActive=' + idActive + '&startDate=' + startDate + '&startHour=' + startHour + '&endDate=' + endDate + '&endHour=' + endHour + '&returnUrl=' + redirectLocation;
            window.parent.location.href = path;
        }

        function RedirectAssisted() 
        {
            var hidden = document.getElementById('hfAssisted').value;
            var params = hidden.split(",");
            var idActive = params[0];
            var startDate = params[1];
            var startHour = params[2];
            var endDate = params[3];
            var endHour = params[4];
            //var redirectLocationParams = window.parent.location.href.split("/");
            var redirectLocation = document.getElementById('hfLocation').value;
            var path = 'UserProfile.aspx?idActive=' + idActive + '&startDate=' + startDate + '&startHour=' + startHour + '&endDate=' + endDate + '&endHour=' + endHour + '&returnUrl=' + redirectLocation;
            window.parent.location.href = path;
        }      
               
    </script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="principal">
        <table>
            <tr>
                <td>
                    <div id="activePhoto" style="width: 25%;">
                        <asp:Image ID="assistedImage" runat="server"></asp:Image>
                    </div>
                </td>
                <td style="padding-left: 15%;">
                    <div id="assistedData" style="float: left;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <asp:Label ID="lblAssistedName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <!--
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <asp:Label ID="lblAge" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10px">
                                    <asp:Image runat="server" ID="PhoneIcon" AlternateText="Teléfono" ToolTip="Teléfono" ImageUrl="images/Tfijo.gif" />
                                </td>
                                <td valign="bottom">
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <asp:Label ID="location" runat="server"></asp:Label>
                                </td>
                            </tr>
                            -->
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div style="z-index: 1; width: 100%; clear: both; background-image: url('images/linea_azul2.png'); background-repeat: repeat-x; height: 30px;">
            <div style="z-index: 2; background-image: url('images/datebox.png'); background-repeat: no-repeat; height: 27px; float: right; margin-right: 20px; width: 100px;">
                <div style="margin: 5px 0 0 6px">
                    <asp:Label ID="lblPosDate" Font-Size="12px" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </div>
        <div style="height: 10px; clear: both;">
        </div>
        <div style="clear: both; float: left; width: 70%;">
            <table>
                <tr>
                    <td align="left" style="width: 60px">
                        <asp:Localize ID="lclStatus" runat="server" meta:resourcekey="status"></asp:Localize>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 60px">
                        <asp:Localize ID="lclPosTime" runat="server" meta:resourcekey="hour"></asp:Localize>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblPosTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 60px">
                        <asp:Localize ID="lclCurrentPos" runat="server" meta:resourcekey="position"></asp:Localize>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 60px">
                        <asp:Localize ID="lclSpeed" runat="server" Visible="false" meta:resourcekey="speed"></asp:Localize>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblSpeed" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: right; width: 30%; text-align: right; z-index: 99">
            <table style="width: 100%">
                <tr style="text-align: right">
                    <td align="right">
                        <asp:Image ID="batLevel" runat="server" />
                        <asp:Image ID="imgStatusIcon" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="clear: both; height: 10px">
        </div>
        <div id="buttons1">
            <div style="width: 100%;">
                <div style="text-align: center;">
                    <asp:Button ID="btnSMS"  class="button_blue" meta:resourcekey="btnSaveSMS" runat="server" />
                    <asp:Button ID="btnRoutes" class="button_blue" meta:resourcekey="btnRoutes" OnClientClick="RedirectTraceRoute()" runat="server" />
                    <asp:Button ID="btnLastPos" class="button_blue" meta:resourcekey="btnStatus" OnClientClick="RedirectLastPosition()" runat="server" />
                    <asp:Button ID="Button3" class="button_blue" meta:resourcekey="btnAgend" OnClientClick="RedirectSchedule()" runat="server" />
                    <!-- <asp:Button ID="Button4" class="button_blue" meta:resourcekey="btnPersonalData" OnClientClick="RedirectAssisted()" runat="server" /> -->
                    <!-- <asp:Button ID="Button5" class="button_blue" meta:resourcekey="btnMessages" OnClientClick="RedirectMessages()" runat="server" /> -->
                </div>
            </div>
        </div>
    </div>
    <input id="hfPosition" type="hidden" runat="server" />
    <input id="hfFollow"   type="hidden" runat="server" />
    <input id="hfSchedule" type="hidden" runat="server" />
    <input id="hfAssisted" type="hidden" runat="server" />
    <input id="hfMessages" type="hidden" runat="server" />
    <input id="hfIdActive" type="hidden" runat="server" />
    <input id="hfLocation" type="hidden" runat="server" />

    <script src="<%=ConfigurationManager.AppSettings["apiGoogle"]+"&sensor=false" %>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var geocoder = new google.maps.Geocoder();
        var input = document.getElementById("hfPosition").value;
        var latlngStr = input.split(",", 2);
        var lat = parseFloat(latlngStr[0]);
        var lng = parseFloat(latlngStr[1]);
        var latlng = new google.maps.LatLng(lat, lng);
        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    document.getElementById('lblAddress').innerHTML = results[0].formatted_address;
                }
            } else {
                //alert("Geocoder failed due to: " + status);
                document.getElementById('lblAddress').innerHTML = '';
            }
        });
    </script>

    <owd:Window ID="Window1" Width="340" Height="220" runat="server" IsResizable="false" StyleFolder="css/obout/windowstyles/dogma"
        VisibleOnLoad="false">
        <div style="margin: 20px 10px 10px 10px; width: 100%">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="phone"></asp:Label>
                        <asp:Label ID="txtMessageNumber" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="characters" runat="server" Text="140"></asp:Label>/140
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtMessageText" Text="" TextMode="MultiLine" MaxLength="140" runat="server"
                            Width="300px" Height="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="text-align: left; width: 180px; height: 30px;">
                            <asp:Label ID="lblMessage" runat="server"  Visible="true" ></asp:Label>
                        </div>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" CssClass="button_blue" runat="server" meta:resourcekey="btnSave" OnClientClick="SentSms()" OnClick="Send_Message" />
                    </td>
                </tr>
            </table>
        </div>
    </owd:Window>

    <script type="text/javascript">
        function FillParameters(phone) {
            document.getElementById('Window1_txtMessageText').value = '';
            //document.getElementById('Window1_lblMessage').innerHTML = '';
            document.getElementById('Window1_txtMessageNumber').innerText = phone;
            document.getElementById('Window1_txtMessageText').focus();
        }
        function ClearSms(lbl){
            document.getElementById('Window1_txtMessageText').value = '';
            lbl.innerText = '';
        }
        function ClearSms() {
            document.getElementById('Window1_txtMessageText').value = '';
            document.getElementById('Window1_lblMessage').innerText = '';
        }

        function SentSms() {

            document.getElementById('Window1_lblMessage').innerText = 'El mensaje ha sido enviado';
            //document.getElementById('Window1_txtMessageText').value = '';
        }


    </script>

    </form>
</body>
</html>

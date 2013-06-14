<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonitoringReport.ascx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.UserControls.MonitoringReport" %>
    
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>

<script src="<%=ConfigurationManager.AppSettings["apiGoogle"] + "&sensor=" + ConfigurationManager.AppSettings["apiGoogleSensor"] %>" type="text/javascript"></script>

<asp:UpdatePanel ID="UpdatePanel7" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hfPositions" runat="server"></asp:HiddenField>
        <input type="hidden" id="hfAddress" runat="server" />
        <input type="hidden" id="hfAlready" runat="server" />
        <input type="button" id="btnHidden" style="display: none" runat="server" onserverclick="imputHide_Click" />
        
        <script language="javascript" type="text/javascript">
            
            var delay = 10;
            var positions = [];
            var unipos = [];
            var data = [];
            var i = 0;
            var finalAddresses = [];
            var geocoder = new google.maps.Geocoder();

            function getAddress(search, index, hfPositions, hfAddress, count) {
                //search = '43.353517@-8.423365';
                //alert('Original: ' + search);
                var latlngStr = search.split("@", 2);
                var lat = parseFloat(latlngStr[0]);
                var lng = parseFloat(latlngStr[1]);
                latLng = new google.maps.LatLng(lat, lng);
                //alert('Parseado: ' + latLng);
                geocoder.geocode({'latLng': latLng}, function(results, status) {
                //geocoder.geocode(search, function(response) {
                    if (status != google.maps.GeocoderStatus.OK) {
                        hfAddress = hfAddress.concat("No disponible");
                        hfAddress = hfAddress.concat('&');
                        hfAddress = hfAddress.concat(index);
                        hfAddress = hfAddress.concat("#");
                    }
                    else {
                         if (results[0]) {
                             direccion = results[0].formatted_address;
                             //direccion = response.Placemark[0];
                             hfAddress = hfAddress.concat(direccion);
                             hfAddress = hfAddress.concat('&');
                             hfAddress = hfAddress.concat(index);
                             hfAddress = hfAddress.concat("#");
                         }
                    }
                    i++;
                    if (i == positions.length - 1 && document.getElementById('ctl00_MainContent_MonitoringReport1_hfAlready').value == "False") {
                        document.getElementById('ctl00_MainContent_MonitoringReport1_hfAddress').value = hfAddress;
                        var fin = hfAddress;
                        var btn = document.getElementById("ctl00_MainContent_MonitoringReport1_btnHidden");
                        i = 0;
                        btn.click();
                    }
                    var j = parseInt(count) + 1;
                    theNext(hfPositions, hfAddress, j);
                });
            }

            function theNext(hfPositions, hfAddress, count) {
                if (hfPositions.length > 0) {
                    //Separo cada posición que hay en el hiddenfield positions
                    //alert(hfPositions);
                    positions = hfPositions.split(";");
                    //alert('Positions: ' + positions);
                    if (count < positions.length) {
                        data = positions[count].split("#");
                        if (data.length > 1) {
                            //unipos = data[1].split("@");
                            var aux = parseInt(data[0]);
                            //var lat = unipos[0];
                            //var lon = unipos[1];
                            //alert('antes de point');
                            //var point = new google.maps.LatLng(lat, lon);
                            //alert('después de Point: ' + point);
                            setTimeout('getAddress("' + data[1] + '","' + aux + '","' + hfPositions + '","' + hfAddress + '","' + count + '")', delay);
                        }
                    }
                }
            }

            function initialize() {
                delay = 10;
                positions = [];
                unipos = [];
                data = [];
                i = 0;
                finalAddresses = [];
                geocoder = new google.maps.Geocoder(); ;
            }
        </script>
        
        <div>
            <asp:PlaceHolder ID="phGrid1" runat="server"></asp:PlaceHolder>
        </div>
        <asp:Timer ID="Timer1" runat="server" Enabled="true" Interval="60000" OnTick="Timer1_Tick">
        </asp:Timer>
</ContentTemplate>
</asp:UpdatePanel>




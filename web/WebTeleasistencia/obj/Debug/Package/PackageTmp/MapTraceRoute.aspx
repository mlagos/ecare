<%@ Page Language="C#" Culture="auto" UICulture="auto" EnableEventValidation="true" ResponseEncoding="utf-8" AutoEventWireup="true"
    MasterPageFile="~/Templates/Menu.Master" CodeBehind="MapTraceRoute.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.MapTraceRoute" %>

<%@ Register Assembly="obout_Window_NET" Namespace="OboutInc.Window" TagPrefix="owd" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register TagPrefix="spl" Namespace="OboutInc.Splitter2" Assembly="obout_Splitter2_Net" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">

    <link href="css/MapLeftMenu.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" type="text/css" href="css/private.css" />
    <script src="js/NextgalInfoWindow.js" type="text/javascript"></script>

    <style type="text/css">        
        .link
        {
            text-decoration: none;
        }        
    </style>

    <script type="text/javascript" language="javascript">
        function seleccionar_todo(component) 
        {
            var div_cb = null;
            if (document.getElementById) 
            { // DOM3 = IE5, NS6 
                div_cb = document.getElementById(component);
            }
            else 
            {
                if (document.layers) 
                { // Netscape 4 
                    div_cb = document.cb_actives;
                }
                else 
                { // IE 4 
                    div_cb = document.all.cb_actives;
                }
            }
            if (div_cb != null) 
            {
                var ele = div_cb.getElementsByTagName("*");
                for (i = 0; i < ele.length; i++)
                    if (ele[i].type == "checkbox")
                    ele[i].checked = 1;
            }
        }

        function deseleccionar_todo(component) 
        {
            var div_cb = null;
            if (document.getElementById) 
            {   // DOM3 = IE5, NS6 
                div_cb = document.getElementById(component);
            }
            else 
            {
                if (document.layers) 
                {   // Netscape 4 
                    div_cb = document.cb_actives;
                }
                else 
                {   // IE 4 
                    div_cb = document.all.cb_actives;
                }
            }
            if (div_cb != null) 
            {
                var ele = div_cb.getElementsByTagName("*");
                for (i = 0; i < ele.length; i++)
                    if (ele[i].type == "checkbox")
                    ele[i].checked = 0;
            }
        }

        function closeWindows(gmap) {

        }
    </script>    
    
</asp:Content>
<asp:Content ID="ContentPageTitle" ContentPlaceHolderID="PageTitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <spl:Splitter CollapsePanel="left" HideBeforeLoad="left" CookieDays="0" ID="Splitter1"
        runat="server" StyleFolder="css/obout" OnSplitterResize="Splitter1_CollapseExpand">
        <LeftPanel WidthMin="190" WidthMax="200">
            <Content>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="MapLeftMenu_PanelContainer" style="margin-top: 33px">
                            <asp:Panel ID="DeviceHeaderPanel" runat="server">
                                <div class="MapLeftMenu_CollapsePanelHeader">
                                    <span><asp:Localize ID="lclDevices" runat="server" meta:resourcekey="asists"></asp:Localize></span>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="DevicesPanel" runat="server">
                                <div class="MapLeftMenu_CollapsePanelContent">
                                     <div style="margin-top: 10px; margin-left: 4px;">
                                        <p>
                                            <a href="javascript:seleccionar_todo('cb_actives')"><%=GetGlobalResourceObject("Resources","all") %></a>&nbsp;|&nbsp; 
                                            <a href="javascript:deseleccionar_todo('cb_actives')"><%=GetGlobalResourceObject("Resources","none") %></a>
                                        </p>
                                    </div>
                                    <div id="cb_actives" style="height: 150px; overflow: auto;">
                                        <asp:CheckBoxList ID="assistedsCheckBoxList" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                    <hr />
                                    <div style="height: 20px; vertical-align: middle; margin: 0 4px 0 4px;">
                                        <div style="float: left; text-align: center; width: 100px">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img src="images/load_mini_red.gif" border="0" alt="Cargando..." />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                        <div style="text-align: right;">
                                            <asp:Button ID="ButtonApply" CssClass="button_blue" runat="server" OnClick="ApplyFilters" meta:resourcekey="applyBtn"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <cc2:CollapsiblePanelExtender ID="DevicesPanel_CollapsiblePanelExtender" CollapseControlID="DeviceHeaderPanel"
                                runat="server" Enabled="True" TargetControlID="DevicesPanel" ExpandControlID="DeviceHeaderPanel">
                            </cc2:CollapsiblePanelExtender>
                        </div>
                        <div class="MapLeftMenu_PanelContainer" style="margin-top: 10px">
                            <asp:Panel ID="FiltersPanelHeader" runat="server">
                                <div class="MapLeftMenu_CollapsePanelHeader">
                                    <span>
                                        <asp:Localize ID="lclFilters" runat="server" meta:resourcekey="filters"></asp:Localize>
                                   </span>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="FiltersPanel" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="MapLeftMenu_CollapsePanelContent">
                                            <p>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Localize ID="Localize4" runat="server" meta:resourcekey="startDate"></asp:Localize>
                                                        </td>
                                                        <td>
                                                            <cc2:CalendarExtender ID="CalendarExtender1" PopupPosition="TopLeft" TargetControlID="txtStartDate" runat="server" Format="dd/MM/yyyy">
                                                            </cc2:CalendarExtender>
                                                            <cc3:OboutTextBox runat="server" ID="txtStartDate" Width="90" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%;">
                                                            <asp:Localize ID="Localize5" runat="server" meta:resourcekey="startHour"></asp:Localize>
                                                        </td>
                                                        <td style="width: 80%;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <cc3:OboutDropDownList ID="startHour" Height="200" Width="50" MenuWidth="70" runat="server">
                                                                        </cc3:OboutDropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Localize ID="Localize6" runat="server" Text=":"></asp:Localize>
                                                                    </td>
                                                                    <td>
                                                                        <cc3:OboutDropDownList ID="startMinutes" Height="200" Width="50" MenuWidth="70" runat="server">
                                                                        </cc3:OboutDropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Localize ID="Localize2" runat="server" meta:resourcekey="endDate"></asp:Localize>
                                                        </td>
                                                        <td>
                                                            <cc2:CalendarExtender ID="CalendarExtender2" PopupPosition="TopLeft" TargetControlID="txtEndDate"
                                                                runat="server" Format="dd/MM/yyyy">
                                                            </cc2:CalendarExtender>
                                                            <cc3:OboutTextBox runat="server" ID="txtEndDate" Width="90" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%;">
                                                            <asp:Localize ID="Localize7" runat="server" meta:resourcekey="endHour"></asp:Localize>
                                                        </td>
                                                        <td style="width: 80%;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <cc3:OboutDropDownList ID="endHour" Height="200" Width="50" MenuWidth="70" runat="server"></cc3:OboutDropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Localize ID="Localize1" runat="server" Text=":"></asp:Localize>
                                                                    </td>
                                                                    <td>
                                                                        <cc3:OboutDropDownList ID="endMinutes" Height="200" Width="50" MenuWidth="70" runat="server"></cc3:OboutDropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </p>
                                            <div style="text-align: center;">
                                                <cc3:OboutButton runat="server" ID="minButton1" Text="15" OnClick="minButton1_Click" />
                                                <cc3:OboutButton runat="server" ID="minButton2" Text="30" OnClick="minButton2_Click" />
                                                <cc3:OboutButton runat="server" ID="minButton3" Text="60" OnClick="minButton3_Click" />
                                                <cc3:OboutButton runat="server" ID="minButton4" Text="90" OnClick="minButton4_Click" />
                                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <cc2:CollapsiblePanelExtender ID="FiltersPanel_CollapsiblePanelExtender" CollapseControlID="FiltersPanelHeader"
                                runat="server" Enabled="True" TargetControlID="FiltersPanel" ExpandControlID="FiltersPanelHeader">
                            </cc2:CollapsiblePanelExtender>
                        </div>
                        <div class="MapLeftMenu_PanelContainer" style="margin-left: 0px;">
                            <asp:Panel ID="PoisPanelHeader" runat="server">
                                <div class="MapLeftMenu_CollapsePanelHeader">
                                    <span>PDIs</span>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PoisPanel" runat="server">
                            </asp:Panel>
                            <cc2:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" CollapseControlID="PoisPanelHeader" runat="server" Enabled="True" TargetControlID="PoisPanel" ExpandControlID="PoisPanelHeader" Collapsed="true"></cc2:CollapsiblePanelExtender>
                        </div>                        
                        <div class="MapLeftMenu_PanelContainer" style="margin-left: 0px;">
                            <asp:Panel ID="ZonesPanelHeader" runat="server">
                                <div class="MapLeftMenu_CollapsePanelHeader">
                                    <span><%=GetGlobalResourceObject("Resources","zones")%></span>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="ZonesPanel" runat="server">
                                <div class="MapLeftMenu_CollapsePanelContent">
                                    <div style="margin-top: 5px; margin-left: 4px">
                                        <p>
                                            <a href="javascript:seleccionar_todo('cb_zones')"><%=GetGlobalResourceObject("Resources","all")%></a> 
                                                &nbsp;|&nbsp; 
                                            <a href="javascript:deseleccionar_todo('cb_zones')"><%=GetGlobalResourceObject("Resources","none")%></a>
                                        </p>
                                    </div>
                                    <div id="cb_zones" style="height: 120px; overflow: auto;">
                                        <asp:CheckBoxList ID="zonesCheckBoxList" runat="server"></asp:CheckBoxList>
                                    </div>
                                    <div style="padding-top:10px; text-align:center;">
                                        <asp:Button ID="lnkViewZones" CssClass="button_blue" runat="server" meta:resourcekey="viewZones" PostBackUrl="Zones.aspx"></asp:Button>
                                        <asp:Button ID="lnkNewZone" CssClass="button_blue" runat="server" meta:resourcekey="newZone" PostBackUrl="UpdateZone.aspx"></asp:Button>
                                    </div>
                                </div>
                            </asp:Panel>
                            <cc2:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" CollapseControlID="ZonesPanelHeader" runat="server" Enabled="True" TargetControlID="ZonesPanel" ExpandControlID="ZonesPanelHeader" Collapsed="true"></cc2:CollapsiblePanelExtender>
                        </div>
                        <div style="margin-top: 15px; width: 190px;">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </Content>
        </LeftPanel>
        <RightPanel>
            <Content>               
                <div style="padding-top: 23px; width: 100%">
                    <cc1:GMap ID="GMap1" runat="server" enableServerEvents="False" EnableTheming="false"
                        enableHookMouseWheelToZoom="True" enableDoubleClickZoom="True" enableContinuousZoom="true"
                        enableGKeyboardHandler="True" enableGetGMapElementById="False" enableGTrafficOverlay="False"
                        enablePostBackPersistence="True" enableStore="False" Height="100%" Width="100%" />
                </div>
            </Content>
        </RightPanel>
    </spl:Splitter>
    <asp:HiddenField ID="hfParameters" runat="server" EnableViewState="true"></asp:HiddenField>
    <asp:HiddenField ID="hfRouteName" runat="server" EnableViewState="true"></asp:HiddenField>
    <asp:HiddenField ID="hfGroup" runat="server" EnableViewState="true"></asp:HiddenField>

    <script type="text/javascript" language="JavaScript">
        //Add load event
        function addLoadEvent(func) 
        {
            var oldonload = window.onload;
            if (typeof window.onload != 'function') 
            {
                window.onload = func;
            }
            else 
            {
                window.onload = function() 
                {
                    if (oldonload) 
                    {
                        oldonload();
                    }
                    func();
                }
            }
        }

        function fillHf(data) 
        {
            document.getElementById('ctl00_MainContent_hfParameters').value = data;
        }

        function RedirectTraceRoute(idActive, day, month, year, startHour, startMinutes, endHour, endMinutes) 
        {
            var path = 'MapTraceRoute.aspx?idActive=' + idActive + '&date=' + day + '/' + month + '/' + year + '&startHour=' + startHour + ":" + startMinutes + '&endHour=' + endHour + ":" + endMinutes;
            window.parent.location.href = path;
        }
        
    </script>

    <input type="hidden" id="hfPositions" runat="server" />
    <input type="hidden" id="hfAddress" runat="server" />
    <input type="hidden" id="hfPoints" runat="server" />

    <script language="javascript" type="text/javascript">
        var delay = 50;
        var positions = [];
        var unipos = [];
        var data = [];
        var i = 0;
        var finalAddresses = [];
        var geocoder = new google.maps.Geocoder(); ;

        function getAddress(search, index, hfPositions, hfAddress, count) {
            geocoder.getLocations(search, function(response) {
                if (!response || response.Status.code != 200) {
                    hfAddress = hfAddress.concat("No disponible");
                    hfAddress = hfAddress.concat('&');
                    hfAddress = hfAddress.concat(index);
                    hfAddress = hfAddress.concat("#");
                }
                else {
                    direccion = response.Placemark[0];
                    hfAddress = hfAddress.concat(direccion.address);
                    hfAddress = hfAddress.concat('&');
                    hfAddress = hfAddress.concat(index);
                    hfAddress = hfAddress.concat("#");
                }
                i++;
                if (i == positions.length - 2) {
                    document.getElementById('ctl00_MainContent_hfAddress').value = hfAddress;
                    var fin = hfAddress;
                    var btn = document.getElementById("ctl00_MainContent_Window1_btnHidden");
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
                positions = hfPositions.split(";");
                if (count < positions.length) {
                    data = positions[count].split("#")
                    if (data.length > 1) {
                        unipos = data[1].split(",");
                        var aux = parseInt(data[0]);
                        var lat = unipos[0];
                        var lon = unipos[1];
                        var point = new google.maps.LatLng(lat, lon);
                        setTimeout('getAddress("' + point + '","' + aux + '","' + hfPositions + '","' + hfAddress + '","' + count + '")', delay);
                    }
                }
            } 
        }
        
    </script>
    <script type="text/javascript">

        function resizeMap() 
        {
             //Calculamos el ancho y largo de la ventana del explorador web    
             var scnWid, scnHei;
             if (self.innerHeight) // all except Explorer
             {
                 scnWid = self.innerWidth;
                 scnHei = self.innerHeight;
             }
             else if (document.documentElement && document.documentElement.clientHeight)
             // Explorer 6 Strict Mode
             {
                 scnWid = document.documentElement.clientWidth;
                 scnHei = document.documentElement.clientHeight;
             }
             else if (document.body) // other Explorers
             {
                 scnWid = document.body.clientWidth;
                 scnHei = document.body.clientHeight;
             }

             //Calculamos el ancho del div superior
             var menuHeight = 6;

             //Calculamos el ancho del div inferior del mapa
             //var footerHeight = document.getElementById("ctl00_Splitter1_ctl01_ctl01_MainContent_UpdatePanel1").offsetHeight;

             //cambiamos el tamaño del mapa para ocupar lo que queda libre de la ventana
             var map = document.getElementById('ctl00_MainContent_Splitter1_ctl01_ctl01_GMap1');
             //var htmlheight = document.body.clientHeight;  
             var htmlheight = scnHei - (menuHeight);
             map.style.height = htmlheight + "px";
         }

         function resizeMap(map, window) {

             //Calculamos el ancho y largo de la ventana del explorador web    
             var scnWid, scnHei;
             if (self.innerHeight) // all except Explorer
             {
                 scnWid = self.innerWidth;
                 scnHei = self.innerHeight;
             }
             else if (document.documentElement && document.documentElement.clientHeight)
             // Explorer 6 Strict Mode
             {
                 scnWid = document.documentElement.clientWidth;
                 scnHei = document.documentElement.clientHeight;
             }
             else if (document.body) // other Explorers
             {
                 scnWid = document.body.clientWidth;
                 scnHei = document.body.clientHeight;
             }

             //Calculamos el ancho del div superior
             var menuHeight = 6;

             //cambiamos el tamaño del mapa para ocupar lo que queda libre de la ventana
             var map = document.getElementById('ctl00_MainContent_Splitter1_ctl01_ctl01_GMap1');

             //var htmlheight = document.body.clientHeight

             var htmlheight;
             if (window == 'yes') {
                 htmlheight = scnHei - (menuHeight + 200);
                 map.style.height = htmlheight + "px";
             }
             else {
                 htmlheight = scnHei - (menuHeight);
                 map.style.height = htmlheight + "px";
             }
         }
    </script>
    <script type="text/javascript">
        function Splitter1_CollapseExpand() 
        {
            var splWidth = Splitter1.GetPanelSize("LeftContent", "width");
            var oboutWindowWidth = getOboutWindowWidth(splWidth);
            Window1.setSize(oboutWindowWidth, 200);
            Window1.setPosition(splWidth+5, GetOboutWindowTopMargin());
        }
    
    </script>
</asp:Content>

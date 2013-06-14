<%@ Page Language="C#" Culture="auto" UICulture="auto" ResponseEncoding="utf-8" AutoEventWireup="true"
    MasterPageFile="~/Templates/Menu.Master" CodeBehind="MapLastPosition.aspx.cs"
    Inherits="Nextgal.ECare.WebTeleasistencia.MapLastPosition" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register TagPrefix="spl" Namespace="OboutInc.Splitter2" Assembly="obout_Splitter2_Net" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="css/MapLeftMenu.css" rel="stylesheet" type="text/css" />
    <script src="./js/NextgalInfoWindow.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function seleccionar_todo(component) {
            var div_cb = null;
            if (document.getElementById) {// DOM3 = IE5, NS6 
                div_cb = document.getElementById(component);
            }
            else 
            {
                if (document.layers) { // Netscape 4 
                    div_cb = document.cb_actives;
                } 
                else { // IE 4 
                    div_cb = document.all.cb_actives;
                }
            }
            if (div_cb != null) {
                var ele = div_cb.getElementsByTagName("*");
                for (i = 0; i < ele.length; i++)
                    if (ele[i].type == "checkbox")
                    ele[i].checked = 1;
            }
        }

        function deseleccionar_todo(component) {
            var div_cb = null;
            if (document.getElementById) {
                // DOM3 = IE5, NS6 
                div_cb = document.getElementById(component);
            }
            else {
                if (document.layers) { // Netscape 4 
                    div_cb = document.cb_actives;
                }
                else { // IE 4 
                    div_cb = document.all.cb_actives;
                }
            }
            if (div_cb != null) {
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
    <spl:Splitter CollapsePanel="left" CookieDays="0" ID="Splitter1" runat="server" StyleFolder="css/obout">
        <LeftPanel WidthMin="190" WidthMax="200">
            <Content>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="MapLeftMenu_PanelContainer" style="padding-top: 35px">
                            <asp:Panel ID="DeviceHeaderPanel" runat="server">
                                <div class="MapLeftMenu_CollapsePanelHeader">
                                    
                                    <span><%=GetGlobalResourceObject("Resources","asists")%></span>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="DevicesPanel" runat="server">
                                <div class="MapLeftMenu_CollapsePanelContent">
                                    <div style="margin-top: 5px; margin-left: 4px">
                                        <p>
                                            <a href="javascript:seleccionar_todo('cb_actives')"><%=GetGlobalResourceObject("Resources","all") %></a> &nbsp;|&nbsp; 
                                            <a href="javascript:deseleccionar_todo('cb_actives')"><%=GetGlobalResourceObject("Resources","none") %></a>
                                        </p>
                                    </div>
                                    <div id="cb_actives" style="height: 150px; overflow: auto;">                                       
                                            <asp:CheckBoxList ID="activesCheckBoxList" runat="server"></asp:CheckBoxList>
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
                                            <asp:Button ID="ButtonApply" CssClass="button_blue" runat="server" meta:resourcekey="btnApply" OnClick="ApplyDevices" />
                                        </div>
                                    </div>
                                </div>
                                <cc2:CollapsiblePanelExtender ID="DevicesPanel_CollapsiblePanelExtender" CollapseControlID="DeviceHeaderPanel"
                                    runat="server" Enabled="True" TargetControlID="DevicesPanel" ExpandControlID="DeviceHeaderPanel">
                                </cc2:CollapsiblePanelExtender>
                            </asp:Panel>
                        </div>
                        <div class="MapLeftMenu_PanelContainer">
                            <asp:Panel ID="OthersPanelHeader" runat="server">
                                <div class="MapLeftMenu_CollapsePanelHeader">
                                    <span><%=GetGlobalResourceObject("Resources","options")%></span>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="OthersPanel" runat="server">
                                <div class="MapLeftMenu_CollapsePanelContent">
                                    <div style="padding: 5px 0 5px 0;">
                                        <asp:CheckBox ID="chkAllowRefresh" runat="server" OnCheckedChanged="chkAllowRefresh_CheckedChanged" AutoPostBack="true" meta:resourcekey="chkAllowRefresh"/>
                                    </div>
                                </div>
                            </asp:Panel>
                            <cc2:CollapsiblePanelExtender ID="OthersPanel_CollapsiblePanelExtender" CollapseControlID="OthersPanelHeader"
                                runat="server" Enabled="True" TargetControlID="OthersPanel" ExpandControlID="OthersPanelHeader">
                            </cc2:CollapsiblePanelExtender>
                        </div>
                        <div class="MapLeftMenu_PanelContainer">
                            <asp:Panel ID="PoisPanelHeader" runat="server">
                                <div class="MapLeftMenu_CollapsePanelHeader">
                                    <span>PDIs</span>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PoisPanel" runat="server"></asp:Panel>
                            <cc2:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" CollapseControlID="PoisPanelHeader"
                                runat="server" Enabled="True" TargetControlID="PoisPanel" ExpandControlID="PoisPanelHeader"
                                Collapsed="true">
                            </cc2:CollapsiblePanelExtender>
                        </div>                        
                        <div class="MapLeftMenu_PanelContainer">
                            <asp:Panel ID="ZonesPanelHeader" runat="server">
                                <div class="MapLeftMenu_CollapsePanelHeader">
                                    <span><%=GetGlobalResourceObject("Resources","zones")%></span>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="ZonesPanel" runat="server">
                                <div class="MapLeftMenu_CollapsePanelContent">
                                    <div style="margin-top: 5px; margin-left: 4px">
                                        <p>
                                            <a href="javascript:seleccionar_todo('cb_zones')"><%=GetGlobalResourceObject("Resources","all") %></a> 
                                                &nbsp;|&nbsp; 
                                            <a href="javascript:deseleccionar_todo('cb_zones')"><%=GetGlobalResourceObject("Resources","none") %></a>
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
                </ContentTemplate>
                </asp:UpdatePanel>
            </Content>
        </LeftPanel>
        <RightPanel>
            <Content>
                <div style="padding-top: 23px; width: 100%;">
                    <cc1:GMap ID="GMap1" runat="server" enableServerEvents="False" EnableTheming="false"
                        enableHookMouseWheelToZoom="True" enableDoubleClickZoom="True" enableContinuousZoom="true"
                        enableGKeyboardHandler="True" enableGetGMapElementById="False" enableGTrafficOverlay="False"
                        enablePostBackPersistence="False" enableStore="False" Height="100%" Width="100%" />
                </div>
            </Content>
        </RightPanel>
    </spl:Splitter>
    <div style="display: none">
        <asp:UpdatePanel ID="UpdatePanelRefreshMap" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="TimerRefreshMap" runat="server" OnTick="TimerRefreshMap_Tick">
                </asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">
        function drawCircle(lat, lng, radius, strokeColor, strokeWidth, strokeOpacity, fillColor, fillOpacity) 
        {
            var d2r = Math.PI / 180;
            var r2d = 180 / Math.PI;
            var Clat = radius * 0.014483;
            var Clng = Clat / Math.cos(lat * d2r);
            var Cpoints = [];
            for (var i = 0; i < 33; i++) {
                var theta = Math.PI * (i / 16);
                Cy = lat + (Clat * Math.sin(theta));
                Cx = lng + (Clng * Math.cos(theta));
                var P = google.maps.LatLng(Cy, Cx);
                Cpoints.push(P);
            }
            var polygon = new GPolygon(Cpoints, strokeColor, strokeWidth, strokeOpacity, fillColor, fillOpacity);
            var map = document.getElementById("subgurim_GMap1");
            map.addOverlay(polygon);
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
            //cambiamos el tamaño del mapa para ocupar lo que queda libre de la ventana
            var map = document.getElementById('ctl00_MainContent_Splitter1_ctl01_ctl01_GMap1');
            //var htmlheight = document.body.clientHeight;
            var htmlheight = scnHei+4;
            map.style.height = htmlheight + "px";
        }
    </script>   

</asp:Content>

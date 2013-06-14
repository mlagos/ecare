<%@ Page Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Templates/Menu.Master" AutoEventWireup="true"
    CodeBehind="Alerts.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.Alerts"
    Title="Untitled Page" %>

<%@ Register Src="UserControls/UserData.ascx" TagName="UserData" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/FormWindow.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div1" style="margin-top: 80px">
        <uc1:UserData ID="userData" runat="server" />
    </div>
    <div id="global" style="margin-top: 20px">
        <div class="FormWindow_CollapsePanelContent">
            <asp:Panel ID="AlertsHeaderPanel" runat="server">
                <div class="FormWindow_CollapsePanelHeader">
                    <span class="FormWindow_CollapsePanelContent_label"><%=GetGlobalResourceObject("Resources","alerts")%></span>
                </div>
            </asp:Panel>
            <asp:Panel ID="AlertsPanel" runat="server">
                <div class="FormWindow_PanelContainer">
                    <div style="margin: 20px 0 20px 0; text-align: center">
                        <div style="margin-right: 80px; display: inline;">
                            <asp:Label ID="lblZone" runat="server" Text="<%$Resources:Datagrid, zone%>"></asp:Label>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlZones" runat="server" Width="250px">
                            </asp:DropDownList>
                        </div>
                        <div style="margin-right: 10px; display: inline">
                            <asp:CheckBox ID="cbxInAlarm" Text="<%$Resources:Datagrid, input%>" runat="server" />
                        </div>
                        <div style="margin-right: 10px; display: inline">
                            <asp:CheckBox ID="cbxOutAlarm" Text="<%$Resources:Datagrid, output%>" runat="server" />
                        </div>
                        <div style="margin-right: 10px; display: inline">
                            <!--
                            <asp:CheckBox ID="cbxSms" Text="<%$Resources:Datagrid, sms%>" runat="server" />
                            -->
                        </div>
                        <div style="margin-right: 10px; display: inline">
                            <asp:CheckBox ID="cbxMail" Text="<%$Resources:Datagrid, mail%>" runat="server" />
                        </div>
                        <asp:Button ID="Button1" runat="server" Text="<%$Resources:Resources, addBtn%>" CssClass="button_blue" OnClick="guardar_Click" />
                    </div>
                    <div style="text-align: center;">
                        <asp:GridView ID="gridAlarms" runat="server" Width="100%" AutoGenerateColumns="false">
                            <HeaderStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center" BorderWidth="1px"
                                BorderColor="Black" VerticalAlign="Bottom" BackColor="LightGray"></HeaderStyle>
                            <Columns>
                                <asp:BoundField HeaderText="" Visible="false" DataField="idNotification" />
                                <asp:BoundField HeaderText="<%$Resources:Datagrid, zone%>" DataField="zoneName" ItemStyle-Width="50%"></asp:BoundField>
                                <asp:CheckBoxField HeaderText="<%$Resources:Datagrid, input%>" DataField="inZone" ItemStyle-Width="11%" />
                                <asp:CheckBoxField HeaderText="<%$Resources:Datagrid, output%>" DataField="outZone" ItemStyle-Width="11%" />
                                <asp:CheckBoxField HeaderText="<%$Resources:Datagrid, mail%>" DataField="email" ItemStyle-Width="11%" />
                                <asp:TemplateField ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="deleteButton" CommandName="delete" CommandArgument='<%# Eval("idNotification") %>'
                                            runat="server" OnCommand="ProcessCommand" ImageUrl="~/images/grid_delete.gif"
                                            ToolTip="Eliminar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                </div>
            </asp:Panel>
        </div>
        <cc2:CollapsiblePanelExtender Enabled="false" ID="AlertsPanel_CollapsiblePanelExtender"
            CollapseControlID="AlertsHeaderPanel" runat="server" TargetControlID="AlertsPanel"
            ExpandControlID="AlertsHeaderPanel">
        </cc2:CollapsiblePanelExtender>
    </div>
    <div id="global" style="margin-top: 20px">
        <div class="FormWindow_CollapsePanelContent">
            <asp:Panel ID="GeneralAlertsHeaderPanel" runat="server">
                <div class="FormWindow_CollapsePanelHeader">
                    <span class="FormWindow_CollapsePanelContent_label"><%=GetGlobalResourceObject("Resources","generalAlerts")%></span>
                </div>
            </asp:Panel>
            <asp:Panel ID="GeneralAlertsPanel" runat="server">
                <div class="FormWindow_PanelContainer">
                    <asp:Panel ID="alertLine" runat="server">
                    
                    <div style="margin: 20px 0 20px 0; text-align: left">
                        <div style="margin-left: 22px; display: inline">
                            <asp:CheckBox ID="cbxBatteryLevel" meta:resourcekey="cbxBatteryLevel" runat="server" />
                        </div>
                        <div style="margin-right: 10px; margin-left:420px;  display: inline">
                            <!--
                            <asp:CheckBox ID="cbxSms2" meta:resourcekey="cbxSms" runat="server" />
                            -->
                        </div>
                        <div style="margin-right: 10px; display: inline">
                            <asp:CheckBox ID="cbxEmail2" meta:resourcekey="cbxMail" runat="server" />
                        </div>
                        <div style="margin-right:3px; display:inline">
                            <asp:Button ID="Button2" runat="server" Text="<%$Resources:Resources, addBtn%>" CssClass="button_blue" OnClick="guardar2_Click" />
                        </div>

                    </div>
                    </asp:Panel>
                    <div style="text-align: left;" >
                        <asp:GridView ID="batteryGrid" runat="server" Width="100%" AutoGenerateColumns="false">
                            <HeaderStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center" BorderWidth="1px"
                                BorderColor="Black" VerticalAlign="Bottom" BackColor="LightGray"></HeaderStyle>
                            <Columns>
                                <asp:BoundField HeaderText="" Visible="false" DataField="idNotification" />
                                <asp:BoundField HeaderText="<%$Resources:Datagrid, alertType%>" DataField="alarmType"></asp:BoundField>
                                <asp:CheckBoxField HeaderText="<%$Resources:Datagrid, mail%>" DataField="email" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="deleteButton2" CommandName="delete" CommandArgument='<%# Eval("idNotification") %>'
                                            runat="server" OnCommand="ProcessCommand" ImageUrl="~/images/grid_delete.gif"
                                            ToolTip="<%$Resources:Resources, delete%>"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                </div>
            </asp:Panel>
        </div>
        <cc2:CollapsiblePanelExtender Enabled="False" ID="GeneralAlertsPanel_CollapsiblePanelExtender"
            CollapseControlID="GeneralAlertsHeaderPanel" runat="server" TargetControlID="GeneralAlertsPanel"
            ExpandControlID="GeneralAlertsHeaderPanel">
        </cc2:CollapsiblePanelExtender>
    </div>
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 73%; padding-left: 20%;">
                    <asp:Label ID="msg" runat="server"></asp:Label>
                </td>
                <td align="left" style="padding-left: 10px;">
                    <asp:Button ID="volver" runat="server" Text="<%$Resources:Resources, returnBtn%>" CssClass="button_blue" OnClick="volverBT_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

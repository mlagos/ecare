<%@ Page Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Templates/Menu.Master"
    AutoEventWireup="true" CodeBehind="Zones.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.Zones" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
    <asp:Label ID="LabelPageTitle" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="divTabs" style="padding-top:30px;">
                <cc1:TabContainer ID="tabContainer1" runat="server" ActiveTabIndex="0">
                    <cc1:TabPanel runat="server" ID="tabZones">
                        <HeaderTemplate>
                            <asp:Localize ID="lclZones" runat="server" meta:resourcekey="zones"></asp:Localize>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:PlaceHolder ID="phGrid1" runat="server"></asp:PlaceHolder>
                            <div style="text-align: right; margin: 5px 5px 5px 0">
                                <asp:Label ID="lblSaveData" runat="server"></asp:Label>
                                <asp:Button ID="lnkNewZone" CssClass="button_blue" runat="server" Text="<%$Resources:Resources, newZoneBtn%>" PostBackUrl="UpdateZone.aspx"></asp:Button>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

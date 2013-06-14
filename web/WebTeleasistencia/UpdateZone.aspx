<%@ Page Language="C#" MasterPageFile="~/Templates/Menu.Master" AutoEventWireup="true"
    CodeBehind="UpdateZone.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.UpdateZone"
    Culture="auto" UICulture="auto" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">

    <script src="../js/resizeUpdateMenu.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="ContentPageTitle" ContentPlaceHolderID="PageTitleContent" runat="server">
    e<asp:Label ID="LabelPageTitle" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="window-content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dataFieldSet" style="padding: 30px 0 10px 0;">
                    <fieldset id="updatefieldSet">
                        <legend id="legend"><%=GetGlobalResourceObject("Resources","zoneProfile")%></legend>
                        <table width="100%" style="margin: 10px 0 10px 5px; height: 75px;">
                            <tr>
                                <td>
                                    <asp:Localize ID="lclName" runat="server" meta:resourcekey="name"></asp:Localize>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" CssClass="textbox" runat="server" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="name" runat="server" ControlToValidate="txtName"
                                        Display="Dynamic" Text="<%$Resources:Resources, requiredField%>" ValidationGroup="Save" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Localize ID="lclAddress" runat="server" meta:resourcekey="address"></asp:Localize>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtAddress" CssClass="textbox" Width="250px" runat="server"></asp:TextBox>
                                    <input type="button" id="btnAddress" class="buttonGreen" onclick="searchClick()"
                                        value="<%=GetGlobalResourceObject("Resources","findBtn")%>" />
                                </td>
                                <td>
                                    <asp:Label ID="lblSaveData" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td style="padding-right: 10px; text-align: right;">
                                    <asp:Button ID="Button1" runat="server" CssClass="buttonGreen" OnClick="btnUpdate_Click"
                                        Text="<%$Resources:Resources, saveBtn%>" ValidationGroup="Save"></asp:Button>
                                    <input type="button" class="buttonRed" id="Button3" value="<%=GetGlobalResourceObject("Resources","clearBtn")%>" onclick="subgurim_Remove()" />
                                    <asp:Button ID="Button2" CausesValidation="false" runat="server" CssClass="button"
                                        OnClick="btnBack_Click" Text="<%$Resources:Resources, returnBtn%>" />
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfPoints" runat="server" EnableViewState="True" />
                        <asp:HiddenField ID="hfZoom" runat="server" EnableViewState="True" />
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upMap" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="map">
                    <cc1:GMap ID="GMap1" Width="100%" Height="100%" runat="server" enableServerEvents="true"
                        enableStore="true" Version="3.0" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script src="<%=ConfigurationManager.AppSettings["apiGoogle"]+"&sensor=false" %>" type="text/javascript"></script>

    <script type="text/javascript">
        resizeUpdateMenu();
    </script>

</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Templates/Menu.Master" Culture="auto" UICulture="auto" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.Messages" Title="Untitled Page" %>

<%@ Register src="UserControls/UserData.ascx" tagname="UserData" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/FormWindow.css" rel="stylesheet" type="text/css" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="global" style="width:768px; margin-left: auto; margin-right: auto; margin-top:85px">
    <table>
        <tr>
            <td>
                <uc1:UserData  ID="UserData1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <br />
                    <asp:PlaceHolder ID="phGrid1" runat="server"></asp:PlaceHolder>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align:right;">
                <asp:Button ID="Button1" CssClass="button_blue" runat="server" meta:resourcekey="btnBack" OnClick="back" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

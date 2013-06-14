<%@ Control Language="C#"  AutoEventWireup="true" CodeBehind="HeaderMenu.ascx.cs" Inherits="Nextgal.PocketLocator.Web.UserControls.HeaderMenu" %>
<%@ Register Assembly="obout_Window_NET" Namespace="OboutInc.Window" TagPrefix="owd" %>

<link href="css/HeaderMenu.css" rel="stylesheet" type="text/css" />

<div id="toppanel">
    <div class="image_tab">
    </div>
    <div class="tab">
        <ul class="login menu">
            <li class="left">&nbsp;</li>
            <li class="leftLogo">
                <div id="hmenu_image">
                </div>
            </li>
            <li class="addBord"><a href='#' onclick='WindowProfile.Open();WindowProfile.screenCenter();'>
                <asp:Label ID="Label1" runat="server"><%=GetLocalResourceObject("myProfile")%></asp:Label>
            </a></li>
            <li class="addBord"><a href="State.aspx">
                <asp:Label ID="lblTrace" runat="server"><%=GetLocalResourceObject("status")%></asp:Label>
            </a></li>
            <li class="addBord"><a href="MapLastPosition.aspx">
                <asp:Label ID="lblSummary" runat="server"><%=GetLocalResourceObject("map")%></asp:Label>
            </a></li>
            <li class="addBord"><a href="MapTraceRoute.aspx">
                <asp:Label ID="lblMap" runat="server"><%=GetLocalResourceObject("monitoring")%></asp:Label>
            </a></li>
            <li class="addBord">
                <asp:LinkButton ID="lnkLogout" runat="server" CausesValidation="false" onclick="lnkLogout_Click"><%=GetLocalResourceObject("goOut")%></asp:LinkButton></li>
            <li class="right">&nbsp;</li>
        </ul>
    </div>
    
    <owd:Window ID="WindowProfile" Width="450" Height="280" runat="server" StyleFolder="css/obout/windowstyles/dogma" IsResizable="false" VisibleOnLoad="false" IsModal="True" meta:resourcekey="userProfile" >
     <div style="border-color: #CCccFF #000099 #000099 #CCccFF; width: 480px; margin-left: auto; margin-right: auto; padding: 20px; text-align:left;">
       
            <table width="100%" border="0">
                <tr>
                    <td>
                        
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="login" Font-Names="Verdana" Font-Size="9pt" ForeColor="#120A8F" Font-Bold="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="loginTB" runat="server" Columns="20" ReadOnly="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            ControlToValidate="loginTB"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="password" Font-Names="Verdana" Font-Size="9pt" ForeColor="#120A8F" Font-Bold="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password" ID="passTB" runat="server" Columns="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="passTB"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="name" Font-Names="Verdana" Font-Size="9pt" ForeColor="#120A8F" Font-Bold="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="nameTB" runat="server" Columns="45"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            ControlToValidate="nameTB"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="surname" Font-Names="Verdana" Font-Size="9pt" ForeColor="#120A8F" Font-Bold="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="surnameTB" runat="server" Columns="45"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                            ControlToValidate="surnameTB"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="phone" Font-Names="Verdana" Font-Size="9pt" ForeColor="#120A8F" Font-Bold="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="phoneTB" runat="server" Columns="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                            ControlToValidate="phoneTB"></asp:RequiredFieldValidator>                        
                    </td>
                    </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="mail" Font-Names="Verdana" Font-Size="9pt" ForeColor="#120A8F" Font-Bold="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="emailTB" runat="server" Columns="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                            ControlToValidate="emailTB"></asp:RequiredFieldValidator>                        
                    </td>
                </tr>
            </table>
        </div>

    <div style="margin-left: auto; margin-right: auto; width: 400px; text-align: right">
        
    <table width="100%">
    <tr>
        <td align="left"><asp:Label ID="msg" runat="server" Visible="false"></asp:Label></td>
        <td align="right"><asp:Button ID="guardar" runat="server" meta:resourcekey="saveBtn" CssClass="button_blue" OnClick="guardar_Click" /></td>
    </tr>
    </table>
    </div>

</owd:Window>
    
</div>

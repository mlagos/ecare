<%@ Page Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Templates/Menu.Master" AutoEventWireup="true" 
CodeBehind="UpdateUser.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.UpdateUser" Title="Untitled Page"
%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="global" style="margin-top: 100px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    
    
     <fieldset style="border-color: #CCccFF #000099 #000099 #CCccFF; width: 900px; margin-left: auto; margin-right: auto; padding: 20px">
            <legend><%=GetGlobalResourceObject("Resources","userProfile")%></legend>            
            <table width="100%" border="0">
            <br />
                <tr>
                    <td>
                        <%=GetGlobalResourceObject("Resources","login")%>
                    </td>
                    <td>
                        <asp:TextBox ID="loginTB" runat="server" Columns="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            ControlToValidate="loginTB"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <%=GetGlobalResourceObject("Resources","password")%>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password" ID="passTB"  runat="server" Columns="20" ></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>
                        <%=GetGlobalResourceObject("Resources","name")%>
                    </td>
                    <td>
                        <asp:TextBox ID="nameTB" runat="server" Columns="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            ControlToValidate="nameTB"></asp:RequiredFieldValidator>                        
                    </td>

                    <td>
                        <%=GetGlobalResourceObject("Resources","surname")%>
                    </td>
                    <td>
                        <asp:TextBox ID="surnameTB" runat="server" Columns="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                            ControlToValidate="surnameTB"></asp:RequiredFieldValidator>                       
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=GetGlobalResourceObject("Resources","phone2")%>
                    </td>
                    <td>
                        <asp:TextBox ID="phoneTB" runat="server" Columns="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                            ControlToValidate="phoneTB"></asp:RequiredFieldValidator>                        
                    </td>
                    <td>
                        <%=GetGlobalResourceObject("Resources","message")%>
                    </td>
                    <td>
                        <asp:TextBox ID="emailTB" runat="server" Columns="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                            ControlToValidate="emailTB"></asp:RequiredFieldValidator>                        
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
     
<br />
    <div style="margin-left: auto; margin-right: auto; width: 940px; text-align: right">
        
    <table width="100%">
    <tr>
        <td align="left"><asp:Label ID="msg" runat="server"></asp:Label></td>
        <td align="right"><asp:Button ID="guardar" runat="server" Text="<%$Resources:Resources, saveBtn%>" CssClass="button_blue" OnClick="guardar_Click" /></td>
    </tr>
    </table>
    
    

  
</ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>

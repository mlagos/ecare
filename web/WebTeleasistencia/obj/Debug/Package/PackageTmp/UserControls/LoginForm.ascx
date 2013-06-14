<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.ascx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.UserControls.LoginForm" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="formLogin">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnLogin" DefaultButton="Button1" runat="server">
                <table border="0">
                    <tr>
                        <td align="right">
                            <asp:Literal ID="_user" runat="server" meta:resourcekey="user" />
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxLogin" runat="server" MaxLength="32" Width="100px" 
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLogin" runat="server" ControlToValidate="TextBoxLogin"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="_password" runat="server" meta:resourcekey="password" />
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" MaxLength="32"
                                Width="100px" ValidationGroup="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="button" 
                                meta:resourcekey="enter" onclick="Button1_Click" ValidationGroup="1" /> 
                            <!--<asp:Button ID="Button2" runat="server" CssClass="button" meta:resourcekey="register" />-->
                            <button ID="_ButtonMoreInfo" runat="server" class="button" 
                                onclick="Window1.Open();Window1.screenCenter();" causesvalidation="False" 
                                validationgroup="0">+ info</button>
                        </td>
                        <td></td>
                    </tr>
                    <!--
                    <tr>
                        <td colspan="3"><br /><br /><%=GetLocalResourceObject("losePassword")%> | <%=GetLocalResourceObject("contact")%> | <%=GetLocalResourceObject("requestInfo")%></td>
                    </tr>
                    -->
                </table>
                <script type="text/javascript">
                    // Respond to the click
                    $(".open-dialog").click(function (e) {
                        e.preventDefault();
                        // Open the dialog
                        $(".basic-dialog").dialog("open");
                    });
                </script>

            </asp:Panel>
            <div id="Error">
                <asp:Label ID="LabelError" runat="server" Height="60px" Width="400px"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
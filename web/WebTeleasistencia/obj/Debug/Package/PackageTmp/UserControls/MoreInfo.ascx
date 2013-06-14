<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MoreInfo.ascx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.UserControls.MoreInfo" %>

<h2>Family eCare</h2>
<p>
Family eCare es un sistema para ayudar a las familias con personas dependientes, dándoles más seguridad y tranquilidad usando para ello dispositivos móviles, información de localización GPS y otras tecnologías.
</p>
<p>
Actualmente el proyecto está en fase de pruebas. Si estás interesado en el mismo o quieres probarlo, por favor, ponte en contacto con nosotros y te proporcionaremos más información.
</p>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div>
    <br/>
    <asp:Panel ID="Panel1" runat="server">
    <table>
        <tr>
            <td>Nombre:
                <asp:TextBox ID="TextBoxName" runat="server" ValidationGroup="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TextBoxName" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
            </td>
            <td align="right">e-mail:
                <asp:TextBox ID="TextBoxEMail" runat="server" ValidationGroup="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="TextBoxEMail" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Comentarios:<br/>
                <asp:TextBox ID="TextBoxComments" runat="server" Height="108px" 
                    TextMode="MultiLine" Width="455px" ValidationGroup="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                    Text="Cancelar" />
                <asp:Button ID="Button1" runat="server" Text="Enviar" onclick="Button1_Click" 
                    ValidationGroup="2" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Visible="false">
        <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        <div>
            <asp:Button ID="Button2" runat="server" Text="Aceptar" 
                onclick="Button2_Click" />
        </div>

    </asp:Panel>
</div>
</ContentTemplate>
</asp:UpdatePanel>
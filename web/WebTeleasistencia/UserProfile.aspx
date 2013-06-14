<%@ Page Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Templates/Menu.Master" AutoEventWireup="true"
    CodeBehind="UserProfile.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.UserProfile"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/FormWindow.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 256px;
        }
        .style3
        {
            width: 178px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="global" style="margin-top: 100px">
        <div class="FormWindow_CollapsePanelContent">
            <asp:Panel ID="UserHeaderPanel" runat="server">
                <div class="FormWindow_CollapsePanelHeader">
                    <span class="FormWindow_CollapsePanelContent_label"><%=GetGlobalResourceObject("Resources","dataAssisted")%></span>
                </div>
            </asp:Panel>
            <asp:Panel ID="UserPanel" runat="server" Height="181px">
                <div class="FormWindow_PanelContainer">
                    <table width="100%" border="0" style="height: 175px">
                        <tr>
                            <td class="style3">
                                <%=GetGlobalResourceObject("Resources","identifier")%>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="identificadorTB" runat="server" Columns="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                    ControlToValidate="identificadorTB"></asp:RequiredFieldValidator>
                            </td>
                            <td rowspan="4" style="min-width: 400px; text-align: center">
                                <asp:Image ID="imagen" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <%=GetGlobalResourceObject("Resources","name")%>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="nombreTB" runat="server" Columns="25"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="nombreTB"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <%=GetGlobalResourceObject("Resources","surname")%>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="apellidosTB" runat="server" Columns="30" Width="204px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                    ControlToValidate="apellidosTB"></asp:RequiredFieldValidator>                        
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <%=GetGlobalResourceObject("Resources","dateBorn")%>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="fechaTB" runat="server" Columns="8"></asp:TextBox>
                                <cc1:CalendarExtender ID="fechaTB_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="fechaTB" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                    ControlToValidate="fechaTB"></asp:RequiredFieldValidator>                       
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <%=GetGlobalResourceObject("Resources","phone")%>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="telefonoTB" runat="server" Columns="12"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                    ControlToValidate="telefonoTB"></asp:RequiredFieldValidator>                        
                            </td>
                            <td style="text-align: center">
                                <%=GetGlobalResourceObject("Resources","image")%>
                                <asp:FileUpload ID="subeImagen" runat="server" />
                                
                            </td>
                        </tr>
                        <tr>
                        <td class="style3"></td>
                        <td class="style2"></td>
                        <td style="text-align: center"><i><%=GetGlobalResourceObject("Resources","filterImage")%></i></td>
                        </tr>
                    </table>
                    <br/>
                </div>
            </asp:Panel>
        </div>
        <cc1:CollapsiblePanelExtender ID="UserPanel_CollapsiblePanelExtender" CollapseControlID="UserHeaderPanel"
                runat="server" Enabled="False" TargetControlID="UserPanel" ExpandControlID="UserHeaderPanel">
        </cc1:CollapsiblePanelExtender>
        
        <br />
        
        <div class="FormWindow_CollapsePanelContent">
            <asp:Panel ID="PanelEmNumbersHeader" runat="server">
                <div class="FormWindow_CollapsePanelHeader">
                    <span class="FormWindow_CollapsePanelContent_label"><%=GetGlobalResourceObject("Resources","emergencyNumber")%></span>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelEmNumbers" runat="server" Height="87px">
                <div class="FormWindow_PanelContainer">
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <%=GetGlobalResourceObject("Resources","number")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosNumero1" runat="server" Width="70px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                    ControlToValidate="sosNumero1"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","name")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosNombre1" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                    ControlToValidate="sosNombre1"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","recallCount")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosRecall1" runat="server" Width="35px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                    ControlToValidate="sosRecall1"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","activated")%><asp:CheckBox ID="sosActivado1" runat="server" Checked="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=GetGlobalResourceObject("Resources","number")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosNumero2" runat="server" Width="70px"></asp:TextBox>
                                <asp:Label ID="sosN2" runat="server" Text="*" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","name")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosNombre2" runat="server" ></asp:TextBox>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","recallCount")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosRecall2" runat="server" Width="35px"></asp:TextBox>
                                <asp:Label ID="sosR2" runat="server" Text="*" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","activated")%><asp:CheckBox ID="sosActivado2" runat="server" Checked="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=GetGlobalResourceObject("Resources","number")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosNumero3" runat="server" Width="70px"></asp:TextBox>
                                <asp:Label ID="sosN3" runat="server" Text="*" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","name")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosNombre3" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","recallCount")%>
                            </td>
                            <td>
                                <asp:TextBox ID="sosRecall3" runat="server" Width="35px"></asp:TextBox>
                                <asp:Label ID="sosR3" runat="server" Text="*" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <%=GetGlobalResourceObject("Resources","activated")%><asp:CheckBox ID="sosActivado3" runat="server" Checked="false"/>
                            </td>
                        </tr>
                    </table>
                    <br/>
                </div>
            </asp:Panel>
        </div>
        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" CollapseControlID="PanelEmNumbersHeader"
                runat="server" Enabled="False" TargetControlID="PanelEmNumbers" ExpandControlID="PanelEmNumbersHeader">
        </cc1:CollapsiblePanelExtender>
        <br/>
        
        <div class="FormWindow_CollapsePanelContent">
            <asp:Panel ID="PanelConfigurationGHeader" runat="server">
                <div class="FormWindow_CollapsePanelHeader">
                    <span class="FormWindow_CollapsePanelContent_label"><%=GetGlobalResourceObject("Resources","configuration")%></span>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelConfiguration" runat="server" Height="37px">
                <div class="FormWindow_PanelContainer">
                    <table width="20%" border="0">
                        <tr>
                            <td align="left">
                                <%=GetGlobalResourceObject("Resources","allowToMinimize")%>
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="allowMinimize" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" CollapseControlID="PanelEmNumbersHeader"
                runat="server" Enabled="False" TargetControlID="PanelEmNumbers" ExpandControlID="PanelEmNumbersHeader">
        </cc1:CollapsiblePanelExtender>
    </div>
    <br />

    <div style="margin-left: auto; margin-right: auto; width: 766px; text-align: right">
        
    <table width="100%">
    <tr>
        <td align="left"><asp:Label ID="msg" runat="server" Visible="false"></asp:Label></td>
        <td align="right" width="150px"> <asp:Button ID="volver" runat="server" Text="<%$Resources:Resources, returnBtn%>" OnClick="volver_Click" CssClass="button_blue" CausesValidation="False" />
        <asp:Button ID="guardar" runat="server" Text="<%$Resources:Resources, saveBtn%>" CssClass="button_blue" OnClick="guardar_Click" /></td>
    </tr>
    </table>
    </div>
</asp:Content>

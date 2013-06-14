<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserData.ascx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.UserControls.UserData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
            <div id="global" style="margin-top: 0px">
                <div class="FormWindow_CollapsePanelContent">
                    <asp:Panel ID="UserHeaderPanel" runat="server">
                                <div class="FormWindow_CollapsePanelHeader">
                                    <span class="FormWindow_CollapsePanelContent_label">Usuario</span>
                                </div>
                    </asp:Panel>
                     <asp:Panel ID="UserPanel" runat="server">
                        <div class="FormWindow_PanelContainer">
                    <table width="100%">
                        <tr>
                            <td>
                                <%=GetLocalResourceObject("name")%>:
                            </td>
                            <td>
                                <asp:Label ID="nombreLB" runat="server"></asp:Label>
                            </td>
                            <td>
                                <%=GetLocalResourceObject("surname")%>:
                            </td>
                            <td>
                                <asp:Label ID="apellidosLB" runat="server"></asp:Label>
                            </td>
                            <td rowspan="3" >
                                <asp:Image ID="foto" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=GetLocalResourceObject("dateBorn")%>:
                            </td>
                            <td>
                                <asp:Label ID="nacimientoLB" runat="server"></asp:Label>
                            </td>
                            <td>
                                <%=GetLocalResourceObject("identifier")%>:
                            </td>
                            <td>
                                <asp:Label ID="identificadorLB" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=GetLocalResourceObject("phone")%>:
                            </td>
                            <td>
                                <asp:Label ID="telefonoLB" runat="server"></asp:Label>
                            </td>
                            <td>
                                
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
                </asp:Panel>
                </div>
                <cc2:CollapsiblePanelExtender ID="UserPanel_CollapsiblePanelExtender" CollapseControlID="UserHeaderPanel"
                    runat="server" Enabled="True" TargetControlID="UserPanel" ExpandControlID="UserHeaderPanel">
                </cc2:CollapsiblePanelExtender>
                </div>

<%@ Page Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Templates/Menu.Master" AutoEventWireup="true"
    CodeBehind="Schedule.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.Schedule"
%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="OboutInc.Scheduler" Assembly="obout_Scheduler_NET"%>
<%@ Register src="UserControls/UserData.ascx" tagname="UserData" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/FormWindow.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="JavaScript">
        function textCounter(txt, car, maxlimit) {
            car.innerText = maxlimit - txt.value.length;
        }
    </script>
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="global" style="margin-top: 85px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc1:UserData ID="UserData1" runat="server" />
                <br />
                <div style="width: 766px; margin-left: auto; margin-right: auto;">
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="<%$Resources:Resources, eventPlanification%>">
                            <ContentTemplate>
                                <div style="margin: 15px 15px 15px 15px;">
                                    <asp:GridView ID="gridEventos" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gridEventosDataBound">
                                        <HeaderStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center" BorderWidth="1px"
                                            BorderColor="Black" VerticalAlign="Bottom" BackColor="LightGray"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundField HeaderText="<%$Resources:DataGrid, date%>" DataField="Date"></asp:BoundField>
                                            <asp:BoundField HeaderText="<%$Resources:DataGrid, text%>" DataField="Text"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="deleteButton" CommandName="delete" CommandArgument='<%# Eval("IdEvent") %>'
                                                        runat="server" OnCommand="ProcessCommand" ImageUrl="~/images/grid_delete.gif"
                                                        CausesValidation="False" ToolTip="<%$Resources:Resources, deleteBtn%>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <%--<div style="margin: 15px 15px 15px 15px;">
                                    <cc1:Scheduler ID="sch" runat="server"
                                        Width="100%" Height="400"
                                        StyleFolder="css/obout/schedulerstyles/default"
                                        ProviderName="System.Data.SqlClient"
                                        ConnectionString="server=PCINFIMP;database=ecare;uid=usrecare;pwd=usrecare2"
                                        EventsTableName="SchedulerEvents"
                                        UserSettingsTableName="SchedulerUserSettings" 
                                        CategoriesTableName="SchedulerCategories" >
                                    </cc1:Scheduler>  
                                </div>--%>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="<%$Resources:Resources, eventCreate%>">
                            <ContentTemplate>
                                <div>
                                    <table>
                                        <tr>
                                            <td style="width: 70px">
                                                <%=GetGlobalResourceObject("Datagrid","date")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="fechaEvt" runat="server" Columns="8" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                    ControlToValidate="fechaEvt"></asp:RequiredFieldValidator>
                                                <cc1:CalendarExtender ID="fechaEvt_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="fechaEvt" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                                <span><%=GetGlobalResourceObject("Resources","hour")%></span>
                                                <asp:DropDownList ID="horaDDL" runat="server">
                                                </asp:DropDownList>
                                                :
                                                <asp:DropDownList ID="minutoDDL" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%=GetGlobalResourceObject("Resources","message")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="textoEvt" runat="server" Columns="60" Height="60" CausesValidation="True"
                                                    MaxLength="148" TextMode="MultiLine"></asp:TextBox>
                                                <asp:Label ID="characters" runat="server" Text="148"></asp:Label>/148
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                    ControlToValidate="textoEvt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="nuevoEventoBT" runat="server" CssClass="button_blue" Text="<%$Resources:Resources, addEventBtn%>"
                                                    OnClick="nuevoEventoBT_Click" />
                                                &nbsp;<asp:Label ID="msg" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                            </ContentTemplate>
                        </cc2:TabPanel>
                    </cc1:TabContainer>
                    <br />
                    <div style="width: 766px; margin-left: auto; margin-right: auto; text-align: right;">
                        <asp:Button ID="volverBT" CssClass="button_blue" runat="server" Text="<%$Resources:Resources, returnBtn%>" OnClick="volverBT_Click"
                            CausesValidation="False" />
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

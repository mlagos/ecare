<%@ Page Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Templates/Menu.Master" AutoEventWireup="true" CodeBehind="State.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.State" Title="Untitled Page" %>

<%@ Register Src="~/UserControls/MonitoringReport.ascx" TagName="MonitoringReport" TagPrefix="uc1" %>
<%@ Register Assembly="obout_Window_NET" Namespace="OboutInc.Window" TagPrefix="owd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<SCRIPT LANGUAGE="JavaScript">
    function textCounter(txt, car, maxlimit) {
        if (txt.value.length > maxlimit) {
            txt.value = txt.value.substring(0,maxlimit);
        }
        car.innerText = maxlimit - txt.value.length;
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <table width="100%" style="margin:85px 0 0 0;" >
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <uc1:MonitoringReport ID="MonitoringReport1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfIdActive" runat="server"/>
    <asp:HiddenField ID="hfPhone" runat="server" />
    <owd:Window ID="Window1" Width="340" Height="220" runat="server" StyleFolder="css/obout/windowstyles/dogma" IsResizable="false" VisibleOnLoad="false">
        <div style="margin:20px 10px 10px 10px; width:100%">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:Resources, phone%>"></asp:Label>
                        <asp:Label ID="LabelMsgNumber" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="characters" runat="server" Text="140"></asp:Label>/140
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtMessageText" TextMode="MultiLine" MaxLength="140" runat="server" Width="300px" Height="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="text-align: left; width:180px; height:30px;">
                            <asp:Label ID="lblMessage" runat="server" Visible="true"></asp:Label>
                        </div>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" CssClass="button_blue" Text="<%$Resources:Resources, sendBtn%>" OnClick="Send_Message"/>
                    </td>
                </tr>
            </table>
        </div>
    </owd:Window>
    
    <script type="text/javascript">
        function FillParameters(id, phone) {
            document.getElementById('ctl00_MainContent_Window1_txtMessageText').value= '';
            document.getElementById('ctl00_MainContent_Window1_lblMessage').innerHTML = '';
            document.getElementById('ctl00_MainContent_hfIdActive').value = id;
            document.getElementById('ctl00_MainContent_hfPhone').value = phone;
            document.getElementById('ctl00_MainContent_Window1_LabelMsgNumber').innerHTML = phone;
            document.getElementById('ctl00_MainContent_Window1_txtMessageText').focus();
        }


    </script>
    <div style="text-align:right; margin-right:15px" >
        <asp:Button ID="newAssisted" runat="server" CssClass="button_blue" Text="<%$Resources:Resources, newAssistBtn%>" OnClick="New_Assisted"
             /></div>
</asp:Content>

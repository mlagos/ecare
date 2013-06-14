<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Scheduler.ascx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.UserControls.Scheduler" %>

<script type="text/javascript" language="JavaScript">


    function dayClick() {
        document.getElementById("ctl00_MainContent_TabContainer1_TabPanel1_Scheduler1_activeDay").value = 1;
        document.getElementById("bodyCal").style.display = "none";
        document.getElementById("addEvent").style.display = "table";

    }
    function saveEvent() {
        document.getElementById("bodyCal").style.display = "table";
        document.getElementById("addEvent").style.display = "none";

    }
</script>

<asp:HiddenField ID="activeDay" runat="server" />
<div id="global" style="background-color: #999999; border: 2px solid #444444; width: 100%;
    height: 400px">
    <div id="header" style="background-position: left; background-image: url('../css/obout/cal.jpg');
        color: White; width: 100%; height: 30px; text-align: center; font-size: 20px;
        background-repeat: repeat-x;">
        <asp:Label ID="month" runat="server" Text="Label"></asp:Label>
    </div>
    <div id="bodyCal" style="background-color: #999999; width: 100%; height: 370px; display: table">
        <div style="display: table-row">
            <div id="d1" style="border: 1px solid #999999; border-left: 2px solid #999999; background-color: #ffffff;
                width: 14%; height: 16%; display: table-cell" onclick="dayClick();">
                <div id="cellheader" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    background-repeat: repeat-x; color: Black; width: 100%; height: 12px; text-align: center;
                    font-size: 10px;">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></div>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
            <div id="d2" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div36" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="d3" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div37" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="d4" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div38" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="d5" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div39" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="d6" style="border: 1px solid #999999; background-color: #f8f8f8; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div40" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="d7" style="border: 1px solid #999999; border-right: 2px solid #999999; background-color: #f8f8f8;
                width: 14%; height: 16%; display: table-cell">
                <div id="Div41" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></div>
            </div>
        </div>
        <div style="display: table-row">
            <div id="Div1" style="border: 1px solid #999999; border-left: 2px solid #999999;
                background-color: #ffffff; width: 14%; height: 16%; display: table-cell">
                <div id="Div2" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div3" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div4" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div5" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div6" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div7" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div8" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div9" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div10" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div11" style="border: 1px solid #999999; background-color: #f8f8f8; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div12" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div13" style="border: 1px solid #999999; border-right: 2px solid #999999;
                background-color: #f8f8f8; width: 14%; height: 16%; display: table-cell">
                <div id="Div14" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label></div>
            </div>
        </div>
        <div style="display: table-row">
            <div id="Div15" style="border: 1px solid #999999; border-left: 2px solid #999999;
                background-color: #ffffff; width: 14%; height: 16%; display: table-cell">
                <div id="Div16" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div17" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div18" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div19" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div20" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div21" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div22" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div23" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div24" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div25" style="border: 1px solid #999999; background-color: #f8f8f8; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div26" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div27" style="border: 1px solid #999999; border-right: 2px solid #999999;
                background-color: #f8f8f8; width: 14%; height: 16%; display: table-cell">
                <div id="Div28" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label21" runat="server" Text="Label"></asp:Label></div>
            </div>
        </div>
        <div style="display: table-row">
            <div id="Div29" style="border: 1px solid #999999; border-left: 2px solid #999999;
                background-color: #ffffff; width: 14%; height: 16%; display: table-cell">
                <div id="Div30" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div31" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div32" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label23" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div33" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div34" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div35" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div42" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div43" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div44" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div45" style="border: 1px solid #999999; background-color: #f8f8f8; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div46" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div47" style="border: 1px solid #999999; border-right: 2px solid #999999;
                background-color: #f8f8f8; width: 14%; height: 16%; display: table-cell">
                <div id="Div48" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label28" runat="server" Text="Label"></asp:Label></div>
            </div>
        </div>
        <div style="display: table-row">
            <div id="Div49" style="border: 1px solid #999999; border-left: 2px solid #999999;
                background-color: #ffffff; width: 14%; height: 16%; display: table-cell">
                <div id="Div50" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label29" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div51" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div52" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label30" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div53" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div54" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label31" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div55" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div56" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label32" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div57" style="border: 1px solid #999999; background-color: #ffffff; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div58" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label33" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div59" style="border: 1px solid #999999; background-color: #f8f8f8; width: 14%;
                height: 16%; display: table-cell">
                <div id="Div60" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label34" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div61" style="border: 1px solid #999999; border-right: 2px solid #999999;
                background-color: #f8f8f8; width: 14%; height: 16%; display: table-cell">
                <div id="Div62" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label35" runat="server" Text="Label"></asp:Label></div>
            </div>
        </div>
        <div style="display: table-row">
            <div id="Div63" style="border: 1px solid #999999; border-left: 2px solid #999999;
                border-bottom: 2px solid #999999; background-color: #ffffff; width: 14%; height: 16%;
                display: table-cell">
                <div id="Div64" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label36" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div65" style="border: 1px solid #999999; border-bottom: 2px solid #999999;
                background-color: #ffffff; width: 14%; height: 16%; display: table-cell">
                <div id="Div66" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label37" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div67" style="border: 1px solid #999999; border-bottom: 2px solid #999999;
                background-color: #ffffff; width: 14%; height: 16%; display: table-cell">
                <div id="Div68" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label38" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div69" style="border: 1px solid #999999; border-bottom: 2px solid #999999;
                background-color: #ffffff; width: 14%; height: 16%; display: table-cell">
                <div id="Div70" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label39" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div71" style="border: 1px solid #999999; border-bottom: 2px solid #999999;
                background-color: #ffffff; width: 14%; height: 16%; display: table-cell">
                <div id="Div72" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label40" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div73" style="border: 1px solid #999999; border-bottom: 2px solid #999999;
                background-color: #f8f8f8; width: 14%; height: 16%; display: table-cell">
                <div id="Div74" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label41" runat="server" Text="Label"></asp:Label></div>
            </div>
            <div id="Div75" style="border: 1px solid #999999; border-right: 2px solid #999999;
                border-bottom: 2px solid #999999; background-color: #f8f8f8; width: 14%; height: 16%;
                display: table-cell">
                <div id="Div76" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <asp:Label ID="Label42" runat="server" Text="Label"></asp:Label></div>
            </div>
        </div>
    </div>
    <div id="addEvent" style="width: 100%; height: 368px; display: none;">
        <div style="display: table-row;">
            <div style="margin: auto; border-left: 2px solid #999999; border-right: 1px solid #999999;
                background-color: #ffffff; width: 50%; height: 368px; display: table-cell">
                <div id="Div77" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <%=GetLocalResourceObject("event")%></div>
                <div style="margin-left: 20px; margin-top: 20px;">
                    <table width="95%">
                        <tr>
                            <td>
                                <asp:Label ID="nameEvt" runat="server" meta:resourcekey="name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label44" runat="server" meta:resourcekey="hour"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label45" runat="server" meta:resourcekey="place"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label46" runat="server" meta:resourcekey="description"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button ID="Button1" runat="server" meta:resourcekey="saveBtn" OnClick="saveEventBT_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="margin: auto; border-left: 1px solid #999999; border-right: 2px solid #999999;
                background-color: #ffffff; width: 50%; height: 100%; display: table-cell">
                <div id="Div78" style="background-position: left; background-image: url('../css/obout/cal2.jpg');
                    color: Black; width: 100%; height: 12px; text-align: center; font-size: 10px;">
                    <%=GetLocalResourceObject("repetitions")%></div>
                <div style="margin-left: 20px; margin-top: 20px;">
                    <table width="95%">
                        <tr>
                            <td>
                                <asp:Label ID="Label47" runat="server" meta:resourcekey="from"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label48" runat="server" meta:resourcekey="to"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label49" runat="server" meta:resourcekey="week"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="RadioButtonList1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label50" runat="server" meta:resourcekey="month"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="RadioButtonList1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label51" runat="server" meta:resourcekey="day"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioButton3" runat="server" GroupName="RadioButtonList1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label52" runat="server" meta:resourcekey="dayJob"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioButton4" runat="server" GroupName="RadioButtonList1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label53" runat="server" meta:resourcekey="weekend"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioButton5" runat="server" GroupName="RadioButtonList1" />
                            </td>
                        </tr>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        </asp:RadioButtonList>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>

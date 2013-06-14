<%@ Page Language="C#" AutoEventWireup="true" Inherits="OboutInc.Scheduler.Templates.Settings" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" media="all" href="res/Settings.css" />
</head>
<body>

    <div>
    <table style="width:100%;background-color:#C6DBFF;">
        <tr>
            <td valign="top">
            <input id="btnBack" class="btnBack" type="button" value="Voler" />
            <input id="btnSave" class="btnSave" type="button" value="Guardar" />
            <input id="btnCancel" class="btnCancel" type="button" value="Cancelar" />
            </td>
        </tr>
    </table>
    <div id="divContent" style="width:100%;overflow:auto;">
    <table style="width:100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:5px;height:5px;"></td>
            <td></td>            
            <td style="width:5px;height:5px;"></td>            
        </tr>
        <tr>
            <td></td>
            <td>
                <table style="width:100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                             <table style="width:100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width:1px;"><img src="res/SettingsTopLeft.gif" /></td>
                                    <td class="tdBackground" style="width:*;"></td>
                                    <td style="width:1px;" align="right"><img src="res/SettingsTopRight.gif" /></td>
                                </tr>
                                <tr>
                                    <td class="tdBackground"></td>
                                    <td class="tdBackground"><b><%=GetGlobalResourceObject("SchedulerStyles","scheduleConfiguration")%></b></td>
                                    <td class="tdBackground"></td>                                    
                                </tr>
                                <tr>
                                    <td class="tdBackground"></td>
                                    <td class="tdBackground"  style="padding-left:0px;">
                                        <table cellpadding="0" cellspacing="1">
                                            <tr>
                                                <td id="tabGeneral" class="MenuSelected"><%=GetGlobalResourceObject("SchedulerStyles","general")%></td>
                                                <td id="tabCategory" class="MenuUnselected"><%=GetGlobalResourceObject("SchedulerStyles","categories")%></td>
                                            </tr>
                                        </table>
                                                                                
                                    </td>
                                    <td class="tdBackground"></td>                                    
                                </tr>
                                
                             </table>

                        
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="tdInnerBackground" style="padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;">
                            <div id="divCategory"  style="display:none;">
                                <table style="width:100%;" cellpadding="0" cellspacing="0">                                
                                    <tr>
                                        <td class="tdHeader"><b><%=GetGlobalResourceObject("SchedulerStyles","category")%></b></td>
                                        <td class="tdHeader tdHideShow"><b><%=GetGlobalResourceObject("SchedulerStyles","show")%></b></td>
                                        <td class="tdHeader tdTheme"><b><%=GetGlobalResourceObject("SchedulerStyles","theme")%></b></td>
                                        <td class="tdHeader tdDelete"><b><%=GetGlobalResourceObject("SchedulerStyles","clear")%></b></td>
                                    </tr>
                                    <tr><td colspan="4" class="tdBackground" style="height:2px;"></td></tr>                                
                                </table>
                                
                                
                                <table id="tableCategories" style="width:100%;" cellpadding="0" cellspacing="0">                                
                                </table>
                                <br />
                                <span style="font-size:10pt;font-weight:bold;"><%=GetGlobalResourceObject("SchedulerStyles","createNewCategory")%>:</span>
                                <br />
                                <input id="txtNewCategory" type="text" style="width:200px" />
                                <input id="btnCreate" type="button" value="Crear" />
                                <input id="txtCatName" type="text" class="txtCatName" style="display:none;"/>                                
                                
                            </div>                        
                            <div id="divGeneral" style="display:block;">
                                <table style="width:100%;" cellpadding="0" cellspacing="0">
                                    
                                    <tr style="display:none;">
                                        <td class="tdOption" style="width:30%;">
                                            <b><%=GetGlobalResourceObject("SchedulerStyles","language")%>:</b>
                                        </td>
                                        <td class="tdOption">
                                            <select id="ddLanguage" class="ddLanguage">
                                                <option value="us-en">English-US</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr  style="display:none;"><td colspan="2" class="tdBackground" style="height:2px;"></td></tr>

                                    <tr  style="display:none;">
                                        <td class="tdOption" style="width:30%;">
                                            <b>Timezone:</b>
                                        </td>
                                        <td class="tdOption">
                                            <select id="ddTimezone" class="ddTimezone">
                                                <option value="7">(GMT +07:00)</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr  style="display:none;"><td colspan="2" class="tdBackground" style="height:2px;"></td></tr>
                                    
                                    <tr>
                                        <td class="tdOption">
                                            <b><%=GetGlobalResourceObject("SchedulerStyles","dateFormat")%>:</b>
                                        </td>
                                        <td class="tdOption">
                                            <select id="ddDateFormat" class="ddDateFormat">
                                                <option value="dd/mm/yyyy">31/12/2010</option>
                                                <option value="mm/dd/yyyy">12/31/2010</option>
                                                <option value="yyyy-mm-dd">2010-12-31</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr><td colspan="2" class="tdBackground" style="height:2px;"></td></tr>
                                    

                                    <tr>
                                        <td class="tdOption">
                                            <b><%=GetGlobalResourceObject("SchedulerStyles","hourFormat")%>:</b>
                                        </td>
                                        <td class="tdOption">
                                            <select id="ddTimeFormat" class="ddTimeFormat">
                                                <option value="TwelveHours">1:00 am</option>
                                                <option value="TwentyFourHours">13:00</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr><td colspan="2" class="tdBackground" style="height:2px;"></td></tr>

                                    <tr>
                                        <td class="tdOption">
                                            <b><%=GetGlobalResourceObject("SchedulerStyles","startWeek")%>:</b>
                                        </td>
                                        <td class="tdOption">
                                            <select id="ddWeekStart" class="ddWeekStart">
                                                <option value="0"><%=GetGlobalResourceObject("SchedulerStyles","sunday")%></option>
                                                <option value="1"><%=GetGlobalResourceObject("SchedulerStyles","monday")%></option>
                                                <option value="6"><%=GetGlobalResourceObject("SchedulerStyles","saturday")%></option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr><td colspan="2" class="tdBackground" style="height:2px;"></td></tr>
                                
                                    <tr>
                                        <td class="tdOption">
                                            <b><%=GetGlobalResourceObject("SchedulerStyles","showWeekEnd")%>:</b>
                                        </td>
                                        <td class="tdOption">
                                            <select id="ddShowWeekend" class="ddShowWeekend">
                                                <option value="1"><%=GetGlobalResourceObject("SchedulerStyles","yes")%></option>
                                                <option value="0"><%=GetGlobalResourceObject("SchedulerStyles","no")%></option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr><td colspan="2" class="tdBackground" style="height:2px;"></td></tr>



                                    <tr>
                                        <td class="tdOption">
                                            <b><%=GetGlobalResourceObject("SchedulerStyles","customView")%>:</b>
                                        </td>
                                        <td class="tdOption">
                                            <select id="ddDefaultView" class="ddDefaultView">
                                                <option value="0"><%=GetGlobalResourceObject("SchedulerStyles","day")%></option>
                                                <option value="1"><%=GetGlobalResourceObject("SchedulerStyles","week")%></option>
                                                <option value="2"><%=GetGlobalResourceObject("SchedulerStyles","month")%></option>
                                                <option value="3"><%=GetGlobalResourceObject("SchedulerStyles","customView")%></option>
                                                <option value="5"><%=GetGlobalResourceObject("SchedulerStyles","configuration")%></option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr><td colspan="2" class="tdBackground" style="height:2px;"></td></tr>

                                    <tr>
                                        <td class="tdOption">
                                            <b><%=GetGlobalResourceObject("SchedulerStyles","customView")%>:</b>
                                        </td>
                                        <td class="tdOption">
                                            <select id="ddCustomViewDayNumber" class="ddCustomViewDayNumber">
                                                <option value="2"><%=GetGlobalResourceObject("SchedulerStyles","following")%> 2 <%=GetGlobalResourceObject("SchedulerStyles","days")%></option>
                                                <option value="3"><%=GetGlobalResourceObject("SchedulerStyles","following")%> 3 <%=GetGlobalResourceObject("SchedulerStyles","days")%></option>
                                                <option value="4"><%=GetGlobalResourceObject("SchedulerStyles","following")%> 4 <%=GetGlobalResourceObject("SchedulerStyles", "days")%></option>
                                                <option value="5"><%=GetGlobalResourceObject("SchedulerStyles","following")%> 5 <%=GetGlobalResourceObject("SchedulerStyles", "days")%></option>
                                                <option value="6"><%=GetGlobalResourceObject("SchedulerStyles","following")%> 6 <%=GetGlobalResourceObject("SchedulerStyles", "days")%></option>                                            
                                                <option value="7"><%=GetGlobalResourceObject("SchedulerStyles","following")%> 7 <%=GetGlobalResourceObject("SchedulerStyles", "days")%></option>
                                            </select>
                                        </td>
                                    </tr>                            
                                </table>
                            </div>

                        </td>
                    </tr>
                    <tr>
                        <td>
                             <table style="width:100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tdBackground" style="height:20px;"></td>
                                    <td class="tdBackground">
                                    &nbsp;
                                    </td>
                                    <td class="tdBackground"></td>
                                </tr>
                                <tr>
                                    <td style="width:1px;"><img src="res/SettingsBottomLeft.gif" /></td>
                                    <td class="tdBackground"></td>
                                    <td style="width:1px;" align="right"><img src="res/SettingsBottomRight.gif" /></td>
                                </tr>
                             </table>
                        
                        </td>
                    </tr>
                    
                </table>
            </td>
            <td></td>
        </tr>        
    </table>                
    <br />
    </div>
    
    <div style="display:none ">
        <select id="selTheme" style="width:100%;">
            <option value="default"><%=GetGlobalResourceObject("SchedulerStyles","tipical")%></option>        
            <option value="blue"><%=GetGlobalResourceObject("SchedulerStyles","blue")%></option>
            <option value="orange"><%=GetGlobalResourceObject("SchedulerStyles","orange")%></option> 
            <option value="green"><%=GetGlobalResourceObject("SchedulerStyles","green")%></option>                                                            
            <option value="violet"><%=GetGlobalResourceObject("SchedulerStyles","violet")%></option>                                                            

        </select>
    </div>
    
    </div>
    <%=EmbededScript%>    
</body>
</html>

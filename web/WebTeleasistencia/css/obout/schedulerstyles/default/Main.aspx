<%@ Page Language="C#" AutoEventWireup="true" Inherits="OboutInc.Scheduler.Templates.Main" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scheduler</title>
    <link rel="Stylesheet" media="all" href="res/Main.css" />
</head>
<body style="margin:0px;">
    <div>
    <table id="Scheduler_MainTable" class="Scheduler_MainTable_Invi" cellpadding="0" cellspacing="0">
        <tr>
            <td id="TopHeader" style="height:1px;" colspan="3">
                <table id="Scheduler_MainTable_Selection" style="width:100%;height:25px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="Td_Css" valign="bottom"><span id="btnDayBack" class="btnDayBack_Css"><img src="res/DayBack.gif"></span></td>
                                    <td class="Td_Css" valign="bottom"><span id="btnDayForward" class="btnDayForward_Css"><img src="res/DayNext.gif"></span></td>
                                    <td class="Td_Css" valign="bottom"><input id="btnToday" type="button" value="Hoy" class="btnToday_Css" /></td>
                                    <td class="Td_Css" valign="bottom"><span id="txtDate" class="txtDate_Css"></span></td>
                                    
                                </tr>
                            </table>
                        </td>
                        <td align="right" valign="bottom">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="Td_Css"><span id="btnViewDay" class="btnView_Down_Css"><%=GetGlobalResourceObject("SchedulerStyles","day")%></span></td>
                                    <td class="Td_Css"><span id="btnViewWeek" class="btnView_Css"><%=GetGlobalResourceObject("SchedulerStyles","week")%></span></td>
                                    <td class="Td_Css"><span id="btnViewMonth" class="btnView_Css"><%=GetGlobalResourceObject("SchedulerStyles","month")%></span></td>
                                    <td class="Td_Css"><span id="btnViewNext4Days" class="btnView_Css"><%=GetGlobalResourceObject("SchedulerStyles","following")%> <span id="txtCustomViewDayNumber"></span>&nbsp;días</span></td>
                                    <td class="Td_Css" style="display:none;"><span id="btnViewAgenda" class="btnView_Css"><%=GetGlobalResourceObject("SchedulerStyles","schedule")%></span></td>
                                    <td class="Td_Css"><span id="btnViewSettings" class="btnView_Css"><%=GetGlobalResourceObject("SchedulerStyles","configuration")%></span></td>
                                    
                                    <td style="width:3px;"></td>
                                </tr>
                            </table>                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td rowspan="2" style="width:10px;">
                <table style="width:100%;height:100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="height:1px;"><img src="res/CalendarTopLeft.gif" /></td>
                        <td style="width:7px;background-color:#C6DBFF;"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="background-color:#C6DBFF;" valign="top" align="center">
                            <div id="btnSmallCalendar" class="btnSmallCalendar">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="Header" style="height:1px;">
                <iframe id="IframeHeader" frameborder="0" scrolling="no" style="width:100%;height:100%;display:none;"></iframe>
            </td>
            <td  rowspan="2">
                <table style="width:100%;height:100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="height:1px;"><img src="res/CalendarTopRight.gif" /></td>
                    </tr>
                    <tr>
                        <td style="width:3px;background-color:#C6DBFF;"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="Content" class="ContentContainer">
                <iframe id="IframeDays" frameborder="0" scrolling="auto" style="width:100%;height:100%;overflow-x:hidden;display:none;" ></iframe>
                <iframe id="IframeMonth" frameborder="0" scrolling="no" style="width:100%;height:100%;display:none;" ></iframe>
                <iframe id="IframeCreateEvent" frameborder="0" scrolling="yes" style="width:100%;height:100%;display:none;" ></iframe>
                <iframe id="IframeSettings" frameborder="0" scrolling="no" style="width:100%;height:100%;display:none;" ></iframe>
                <iframe id="IframeAgenda" frameborder="0" scrolling="no" style="width:100%;height:100%;display:none;" ></iframe>
            </td>
        </tr>
        <tr>
            <td id="Bottom" colspan="3" style="height:22px;">
                <table style="width:100%;height:100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height:19px;background-color:#C6DBFF;"></td>
                        <td style="height:19px;background-color:#C6DBFF;padding-left:5px;" valign="bottom">
                            <span id="txtStatus" class="txtStatus"></span>
                        </td>
                        <td style="height:19px;background-color:#C6DBFF;"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width:1px;"><img src="res/CalendarBottomLeft.gif" /></td>
                        <td style="height:3px;background-color:#C6DBFF;"><img src="res/none.gif" /></td>
                        <td align="right" style="width:1px;"><img src="res/CalendarBottomRight.gif" /></td>
                    </tr>
                </table>                
            </td>
        </tr>
    </table>
    
    <div id="CreateEventBox" class="CreateEventBox" style="width:350px;height:130px;top:100px;left:100px;display:none;">
        <table style="width:100%;font-family:Arial;font-size:10pt;">
            <tr>
                <td><span id="CreateEventBoxTitle" class="CreateEventBoxTitle">Fri, January 11, 07:00 – 08:30</span></td>
                <td align="right" valign="top"><img id="btnCreateEventClose" class="btnCreateEventClose" src="res/Close.gif" /></td>
            </tr>
            <tr><td style="height:5px;"></td></tr>
            <tr>
                <td colspan="2">
                
                    <table style="width:100%;font-family:Arial;font-size:10pt;">
                        <tr>
                            <td><%=GetGlobalResourceObject("SchedulerStyles","account")%>:</td>
                            <td><input id="txtSubject" type="text" class="txtSubject" /></td>
                        </tr>
                        <tr>
                            <td><%=GetGlobalResourceObject("SchedulerStyles","category")%>:</td>
                            <td>
                                <select id="selCategories" class="selCategories">
                                </select>                            
                            </td>
                        </tr>                        
                    </table>
                </td>
            </tr>
            <tr><td style="height:10px;"></td></tr>
            <tr>
                <td colspan="2">
                <input id="btnCreateEvent" class="btnCreateEvent" type="button" value="Crear" />
                <a id="btnCreateBox_EditEventDetail" class="btnCreateBox_EditEventDetail" href="javascript:;">Editar detalles»</a>
                </td>
            </tr>
        </table>
    </div>
    
    <div id="EditEventBox" class="EditEventBox" style="width:350px;height:100px;top:400px;left:100px;display:none;">
        <table style="width:100%;font-family:Arial;font-size:10pt;">
            <tr>
                <td><div id="EditEventBoxSubject" class="EditEventBoxSubject" style="width:320px;"><%=GetGlobalResourceObject("SchedulerStyles","account")%></div></td>
                <td align="right"><img id="btnEditEventClose" class="btnEditEventClose" src="res/Close.gif" /></td>
            </tr>
            <tr><td style="height:5px;"></td></tr>
            <tr>
                <td colspan="2"><span id="EditEventTime" class="EditEventTime">Fri, January 11, 07:00 – 08:30</span></td>            
            </tr>
            <tr><td style="height:5px;"></td></tr>
            <tr>
                <td colspan="2">[<a id="btnDelete" class="btnDelete" href="javascript:;">Borrar</a>]</td>
            </tr>
            <tr>
                <td colspan="2">
                <a id="btnEditBox_EditEventDetail" class="btnEditBox_EditEventDetail" href="javascript:;">Editar detalles»</a>
                </td>
            </tr>
        </table>        
    </div>
    
    
    </div>
    
    <div id="divLoadingPanel" class="divLoadingPanel" style="display:none;top:0px;left:0px;">
        <table style="width:100%;height:100%">
            <tr>
                <td align="center" valign="middle">
                
                    <table style="width:200px;">
                        <tr>
                            <td align="left"><img src="res/loading.gif" /></td>
                        </tr>
                        <tr>
                            <td align="left" style="padding-left:3px;"><span id="txtLoading" class="txtLoading">Cargando ...</span></td>
                        </tr>
                    </table>
                
                </td>
            </tr>
        </table>
        
    </div>
    
    <div id="divSmallCalendar" style="position:absolute;top:100px;left:0px;width:166px;height:171px;display:none;">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width:1px;"><img src="res/SmallCalendarTopLeft.gif" /></td>
                <td style="width:160px;background-color:#739EBD;"></td>
                <td style="width:1px;"><img src="res/SmallCalendarTopRight.gif" /></td>
            </tr>
            <tr>
                <td style="background-color:#739EBD"></td>
                <td style="width:160px;height:165px;background-color:#739EBD;" align="center" valign="top"><iframe id="IframeCalendar" style="width:150px;height:160px;" frameborder="0" scrolling="no"  ></iframe></td>
                <td style="background-color:#739EBD"></td>
            </tr>
            <tr>
                <td style="width:1px;"><img src="res/SmallCalendarBottomLeft.gif" /></td>
                <td style="background-color:#739EBD;"></td>
                <td style="width:1px;"><img src="res/SmallCalendarBottomRight.gif" /></td>
            </tr>
        </table>
    </div>
    
    
    
    <%=EmbededScript%>
    
    <script type="text/javascript">
        document.getElementById("Header").style.width=(document.getElementById("Scheduler_MainTable").offsetWidth - 13)+"px";
    </script>
</body>
</html>

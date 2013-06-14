<%@ Page Language="C#" AutoEventWireup="true" Inherits="OboutInc.Scheduler.Templates.CreateEvent" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="Stylesheet" media="all" href="res/CreateEvent.css" />
</head>
<body>
    <div>
    <table style="width:100%;background-color:#C6DBFF;">
        <tr>
            <td valign="top">
            <a id="btnBack" class="btnBack" href="javascript:;">«<%=GetGlobalResourceObject("SchedulerStyles","returnCalendar")%></a>
            <input id="btnSave" class="btnSave" type="button" value="Guardar" />
            <input id="btnCancel" class="btnCancel" type="button" value="Cancelar" />
            
            </td>
        </tr>
    </table>
    
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
                            <td style="width:1px;"><img src="res/CreateEventTopLeft.gif" /></td>
                            <td class="tdBackground"></td>
                            <td style="width:1px;"><img src="res/CreateEventTopRight.gif" /></td>            
                        </tr>
                        <tr>
                            <td class="tdBackground"></td>
                            <td class="tdBackground">
                                    <table style="width:95%;" cellpadding="0" cellspacing="0">
                                        <tr><td class="CssSeparator"></td></tr>
                                        <tr>
                                            <td style="width:100px;">&nbsp;<b><%=GetGlobalResourceObject("SchedulerStyles","account")%></b></td>
                                            <td>
                                                <input type="text" id="txtSubject" class="txtSubject" />
                                            </td>
                                        </tr>
                                        <tr><td class="CssSeparator"></td></tr>
                                        <tr>
                                            <td>&nbsp;<b><%=GetGlobalResourceObject("SchedulerStyles","date")%></b></td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td><input type="text" id="txtStartDate" class="txtStartDate" /></td>
                                                        <td id="tdStartTime">
                                                            &nbsp;<input type="text" id="txtStartTime" class="txtStartTime" />
                                                            <div id="ddStartTime" class="customDropDown" style="width:98px;height:120px;display:none;">
                                                                <table id="tableddStartTime" style="width:100%;" cellpadding="0" cellspacing="0">
                                                                    <tbody></tbody>
                                                                </table>
                                                            </div>
                                                        
                                                        </td>
                                                        <td>&nbsp;to</td>
                                                        <td id="tdEndTime">
                                                            &nbsp;<input type="text" id="txtEndTime" class="txtEndTime" />
                                                            <div id="ddEndTime" class="customDropDown" style="width:150px;height:120px;display:none;">
                                                                <table id="tableddEndTime" style="width:100%;" cellpadding="0" cellspacing="0">
                                                                    <tbody></tbody>
                                                                </table>
                                                            </div>                                                        
                                                        </td>
                                                        <td>&nbsp;<input type="text" id="txtEndDate" class="txtEndDate" /></td>
                                                        <td>&nbsp;<input type="checkbox" id="chkAllDay" /><label for="chkAllDay"><%=GetGlobalResourceObject("SchedulerStyles","allDay")%></label></td>
                                                        
                                                    </tr>
                                                </table>
                                                <iframe id="iframeCalendar" style="display:none;position:absolute;width:150px;height:160px;" frameborder="0" scrolling="no"  ></iframe>
                                            </td>
                                        </tr>
                                        <tr><td class="CssSeparator"></td></tr>                                        
                                        <tr>
                                            <td></td>
                                            <td>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width:20px;"></td>
                                                        <td>
                                                        
                                                        
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                    
                                                                        <table >
                                                                            <tr>
                                                                                <td >
                                                                                <b><%=GetGlobalResourceObject("SchedulerStyles","repetition")%>:</b>
                                                                                </td>
                                                                                <td>
                                                                                    <select id="ddRepeats" class="ddRepeats">
                                                                                        <option value="no"><%=GetGlobalResourceObject("SchedulerStyles","noRepeat")%></option>
                                                                                        <option value="daily"><%=GetGlobalResourceObject("SchedulerStyles","daily")%></option>
                                                                                        <option value="weekly"><%=GetGlobalResourceObject("SchedulerStyles","weekly")%></option>
                                                                                        <option value="monthly"><%=GetGlobalResourceObject("SchedulerStyles","monthly")%></option>
                                                                                        <option value="yearly"><%=GetGlobalResourceObject("SchedulerStyles","annual")%></option>
                                                                                    </select>
                                                                                
                                                                                
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    
                                                                    </td>
                                                                </tr>
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr><td class="RepeatSeparator"></td></tr>
                                                            </table>
                                                            
                                                            <div id="divRepeatSumary" style="display:none;">
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="height:44px;">
                                                                        
                                                                        <span id="txtRepeatSumary" class="txtRepeatSumary"><%=GetGlobalResourceObject("SchedulerStyles","daily")%></span>
                                                                    </td>
                                                                </tr>
                                                                <tr><td class="RepeatSeparator"></td></tr>
                                                            </table>
                                                            </div>
                                                            <div id="divRepeatFrequency" style="display:none;">
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr>
                                                                    <td >
                                                                        <b><%=GetGlobalResourceObject("SchedulerStyles","repeatEvery")%>:</b>
                                                                        <select id="ddRepeatFrequency" style="width:40px;" >
                                                                            <option value="1">1</option>                                                                            
                                                                            <option value="2">2</option>
                                                                            <option value="3">3</option>
                                                                            <option value="4">4</option>
                                                                            <option value="5">5</option>
                                                                            <option value="6">6</option>
                                                                            <option value="7">7</option>
                                                                            <option value="8">8</option>
                                                                            <option value="9">9</option>
                                                                            <option value="10">10</option>
                                                                            <option value="11">11</option>
                                                                            <option value="12">12</option>
                                                                            <option value="13">13</option>
                                                                            <option value="14">14</option>
                                                                            
                                                                        </select>
                                                                        <span id="txtRepeatFrequencyUnit"><%=GetGlobalResourceObject("SchedulerStyles","day")%></span>
                                                                        
                                                                    </td>
                                                                </tr>
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr><td class="RepeatSeparator"></td></tr>
                                                            </table>
                                                            </div>
                                                            <div id="divRepeatBy" style="display:none;">
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr>
                                                                    <td >
                                                                        <b><%=GetGlobalResourceObject("SchedulerStyles","repeatFor")%>:</b>
                                                                        <br />
                                                                        <table>
                                                                            <tr>
                                                                                <td class="indent10px"></td>
                                                                                <td>                                                                                
                                                                                    <input id="rdDayOfMonth" type="radio" checked="checked" name="RepeatByType" />
                                                                                    <label for="rdDayOfMonth"><%=GetGlobalResourceObject("SchedulerStyles","dayOfMonth")%></label>
                                                                                </td>
                                                                                <td>                                                                                
                                                                                    <input id="rdDayOfWeek" type="radio" name="RepeatByType" />
                                                                                    <label for="rdDayOfWeek"><%=GetGlobalResourceObject("SchedulerStyles","dayOfWeek")%></label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>              
                                                                    </td>
                                                                </tr>
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr><td class="RepeatSeparator"></td></tr>
                                                            </table>
                                                            </div>
                                                            <div id="divRepeatOn" style="display:none;">
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr>
                                                                    <td >
                                                                        <b><%=GetGlobalResourceObject("SchedulerStyles","repeatIn")%>:</b>
                                                                        <br />
                                                                        <table>
                                                                            <tr>
                                                                                <td class="indent10px"></td>
                                                                                <td>
                                                                                    <input type="checkbox" id="chkSun" name="chkRepeatOnGroups" /><label for="chkS"><%=GetGlobalResourceObject("SchedulerStyles","sunday")%></label>
                                                                                    <input type="checkbox" id="chkMon" name="chkRepeatOnGroups" /><label for="chkS"><%=GetGlobalResourceObject("SchedulerStyles","monday")%></label>
                                                                                    <input type="checkbox" id="chkTue" name="chkRepeatOnGroups" /><label for="chkS"><%=GetGlobalResourceObject("SchedulerStyles","thuesday")%></label>
                                                                                    <input type="checkbox" id="chkWed" name="chkRepeatOnGroups" /><label for="chkS"><%=GetGlobalResourceObject("SchedulerStyles","wednesday")%></label>
                                                                                    <input type="checkbox" id="chkThu" name="chkRepeatOnGroups" /><label for="chkS"><%=GetGlobalResourceObject("SchedulerStyles","thursday")%></label>
                                                                                    <input type="checkbox" id="chkFri" name="chkRepeatOnGroups" /><label for="chkS"><%=GetGlobalResourceObject("SchedulerStyles","friday")%></label>
                                                                                    <input type="checkbox" id="chkSat" name="chkRepeatOnGroups" /><label for="chkS"><%=GetGlobalResourceObject("SchedulerStyles","saturday")%></label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </td>
                                                                </tr>
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr><td class="RepeatSeparator"></td></tr>
                                                            </table>
                                                            </div>
                                                            <div id="divRepeatRange" style="display:none;">
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr>
                                                                    <td >
                                                                        <b><%=GetGlobalResourceObject("SchedulerStyles","range")%>:</b>
                                                                        <table>
                                                                            <tr>
                                                                                <td class="indent10px"></td>
                                                                                <td>
                                                                                    <%=GetGlobalResourceObject("SchedulerStyles","start")%>: <input type="text" id="txtRepeatStartDate" class="textRepeatStartDate" disabled="disabled" />
                                                                                </td>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td rowspan="2"><%=GetGlobalResourceObject("SchedulerStyles","end")%>:</td>
                                                                                            <td>
                                                                                                
                                                                                                <input id="rdNever" type="radio" checked="checked" name="endType" >
                                                                                                <label for="rdNever"><%=GetGlobalResourceObject("SchedulerStyles","never")%></label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table style="height:25px;" cellpadding="0" cellspacing="0">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <input id="rdUntil" type="radio" name="endType" >
                                                                                                            <label for="rdUntil"><%=GetGlobalResourceObject("SchedulerStyles","even")%></label>
                                                                                                        </td>
                                                                                                        <td id="tdRangeUntilDate"  style="display:none;">
                                                                                                            &nbsp;<input type="text" id="txtRangeUntilDate" class="txtRangeUntilDate" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                                                                                                           
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>                                                                                
                                                                            </tr>
                                                                        </table>                                                                        
                                                                    </td>
                                                                </tr>
                                                                <tr><td class="CssSeparator"></td></tr>
                                                                <tr><td class="RepeatSeparator"></td></tr>
                                                            </table>
                                                            </div>
                                                        
                                                        </td>
                                                    </tr>
                                                </table>
                                            
                                            </td>
                                        </tr>
                                        
                                        <tr><td class="CssSeparator"></td></tr>
                                        <tr>
                                            <td>&nbsp;<b><%=GetGlobalResourceObject("SchedulerStyles","wher")%></b></td>
                                            <td>
                                                <input type="text" id="txtPlace" class="txtPlace"/>
                                            </td>
                                        </tr>
                                        <tr><td class="CssSeparator"></td></tr>
                                        <tr>
                                            <td>&nbsp;<b><%=GetGlobalResourceObject("SchedulerStyles","category")%></b></td>
                                            <td>
                                                <select id="selCategories" style="width:50%;"></select>
                                            </td>
                                        </tr>
                                                    
                                        <tr><td class="CssSeparator"></td></tr>
                                        <tr>
                                            <td valign="top">&nbsp;<b><%=GetGlobalResourceObject("SchedulerStyles","description")%></b></td>
                                            <td>
                                                <textarea id="txtDescription" class="txtDescription" rows="5" ></textarea>
                                            </td>
                                        </tr>
                                                    
                                        <tr><td class="CssSeparator"></td></tr>
                                        
                                    </table>
                            </td>
                            <td class="tdBackground"></td>            
                        </tr>
                        <tr>
                            <td style="width:1px;"><img src="res/CreateEventBottomLeft.gif" /></td>
                            <td class="tdBackground"></td>
                            <td style="width:1px;"><img src="res/CreateEventBottomRight.gif" /></td>            
                        </tr>        
                    </table>
            </td>
            <td></td>
        </tr>        
    </table>
    
    
    
    
    </div>
    <%=EmbededScript%>    
</body>
</html>

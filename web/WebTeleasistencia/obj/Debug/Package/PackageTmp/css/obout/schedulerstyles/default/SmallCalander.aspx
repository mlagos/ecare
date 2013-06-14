<%@ Page Language="C#" AutoEventWireup="true" Inherits="OboutInc.Scheduler.Templates.SmallCalendar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link rel="Stylesheet" media="all" href="res/SmallCalendar.css" />
</head>
<body> 
        <div style="background-color:#739EBD;">
        <table style="width:100%;height:21px;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width:5px;"></td>
                <td align="left" style="width:3px;"><a id="btnPrev" class="btnPrev" href="javascript:;">«</a></td>
                <td align="center"><a id="btnMonth" href="javascript:;" class="btnMonth" >Enero 2010</a></td>    
                <td align="right" style="width:3px;"><a id="btnNext" class="btnNext" href="javascript:;">»</a></td>
                <td style="width:5px;"></td>            
            </tr>
        </table>
        <table id="tableDayHeader" style="width:100%;height:20px;" cellpadding="0" cellspacing="0">
        <tbody></tbody>
        </table>
        <table id="tableMonthView" style="width:100%;" class="tableMonth" cellpadding="0" cellspacing="0">
        <tbody></tbody>
        </table>
        
        <div id="CoverContainer">
        
        </div>
          
        </div>
        <%=EmbededScript%>
</body>
</html>
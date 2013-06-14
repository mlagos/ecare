<%@ Page Language="C#" AutoEventWireup="true" Inherits="OboutInc.Scheduler.Templates.Month" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="header" runat="server">
    <title>Mes</title>
    <link rel="Stylesheet" media="all" href="res/Month.css" />
</head>
<body>
    <div>
        <table id="MonthTable" class="MonthTable" cellpadding="0" cellspacing="0">
        </table>
        

        
        <div id="CoversPool">
        
        </div>
        
        <div id="EventBoxesPool">
        
        </div>        
        
        <div id="DayEventsContainer">
        </div>
            <div id="EventDetailBox" style="display:none;position:absolute;width:100px;border:solid 2px gray;">
            <table cellpadding="0" cellspacing="0" style="width:100%;">
                <tr style="background-color:#DDDDDD;">
                    <td valign="top" style="height:16px;padding-left:2px;" > 
                        <span id="txtEventDetailHeaderDate" class="txtEventDetailHeaderDate"></span>
                        <span id="txtEventDetailHeaderDay" class="txtEventDetailHeaderDay"></span>
                    </td>
                    <td align="right" valign="middle" style="padding-right:1px;"><img id="btnEventDetailClose" src="res/Close.gif" style="cursor:pointer;" /></td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" style="background-color:White;">
                    <div style="width:100%;">
                        <table id="tableEventDetail" cellpadding="0" cellspacing="0" style="width:100%;">
                        </table>
                    </div>                    
                    </td>
                </tr>
            </table>
            </div>
    </div>
    
    
    
    <div style="display:none;">
        <table id="DayHeaderTable" class="DayHeaderTable" style="height:17px;" cellpadding="0" cellspacing="0">
            <tr>
                <td id="DayHeaderContent" class="DayHeaderContent" align="right">
                </td>
            </tr>
        </table>
        
        
        
            <table id="TableDayEvent" cellpadding="0" cellspacing="0" style="position:absolute;height:1px;">
            </table>
        
        <!--SmallBox template-->
        <table id="SmallBoxTable" class="SmallBoxTable" style="position:absolute;height:16px;" cellpadding="0" cellspacing="0">
            <tr>
                <td id="SmallBoxLeft" class="SmallBoxLeft" style="width:2px;"></td>
                <td id="SmallBoxExpandLeft" class="SmallBoxExpand" align="left" style="width:1px;" ><img id="SmallBoxArrowLeft" class="SmallBoxArrowLeft" src="res/none.gif" style="display:none;"/></td>                
                <td id="SmallBoxCenter" class="SmallBoxCenter" ><div id="SmallBoxContent" class="SmallBoxContent" style="width:50px;height:16px;"></div></td>
                <td id="SmallBoxExpandRight" class="SmallBoxExpand" align="right" style="width:1px;"><img id="SmallBoxArrowRight" class="SmallBoxArrowRight" src="res/none.gif" style="display:none;"/></td>              
                <td id="SmallBoxRight" class="SmallBoxRight" style="width:2px;"></td>
            </tr>
        </table>
        <!--SmallBox template-->
        
        
     </div>
    <%=EmbededScript%>
</body>
</html>

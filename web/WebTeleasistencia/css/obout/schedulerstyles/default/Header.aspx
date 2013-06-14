<%@ Page Language="C#" AutoEventWireup="true" Inherits="OboutInc.Scheduler.Templates.Header" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="header" runat="server">
    <title></title>
    <link rel="Stylesheet" media="all" href="res/header.css" />
  
</head>
<body>
    <div>
        <div id="divDaysHeader">
        <table id="DaysHeaderTable" cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td rowspan="2" style="width:40px;" ></td>
                <td>
                        <table id="DateHeaderTable" class="DateHeaderTable" cellpadding="0" cellspacing="0">
                        </table>                
                </td>
                <td rowspan="2" style="width:16px;"></td>
            </tr>
            <tr>
                <td>
                        <table id="AllDayEventTable" class="AllDayEventTable" cellpadding="0" cellspacing="0">
                        </table>
                        <div id="EventBoxesContainer"></div>
                        <div id="grayCover" class="grayCover"></div>
                
                </td>
            </tr>
        </table>
        </div>
        <div id="divMonthHeader">
            <table id="MonthHeaderTable" class="MonthHeaderTable" cellpadding="0" cellspacing="0" >
            </table>
        </div>
        
    </div>
    <div style="display:none;">
        <!--SmallBox template-->
        <table id="SmallBoxTable" class="SmallBoxTable" cellpadding="0" cellspacing="0" style="position:absolute;">
            <tr>
                <td id="SmallBoxLeft" class="SmallBoxLeft" style="width:2px;"></td>
                <td id="SmallBoxExpandLeft" class="SmallBoxExpand" style="width:1px;"><img id="SmallBoxArrowLeft" class="SmallBoxArrowLeft" src="res/none.gif" style="display:none;"/></td>                
                <td id="SmallBoxCenter" class="SmallBoxCenter"><div id="SmallBoxContent" class="SmallBoxContent" style="width:50px;height:16px;"></div></td>
                <td id="SmallBoxExpandRight" class="SmallBoxExpand" style="width:1px;"><img id="SmallBoxArrowRight" class="SmallBoxArrowRight" src="res/none.gif" style="display:none;"/></td>              
                <td id="SmallBoxRight" class="SmallBoxRight" style="width:2px;"></td>
            </tr>
        </table>
        <!--SmallBox template-->
    </div>
    
    <%=EmbededScript%>
    <script type="text/javascript">
    

    </script>     

</body>
</html>

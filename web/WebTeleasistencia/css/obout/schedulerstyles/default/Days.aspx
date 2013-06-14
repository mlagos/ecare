<%@ Page Language="C#" AutoEventWireup="true" Inherits="OboutInc.Scheduler.Templates.Days" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="header" runat="server">
    <title>Días</title>
    <link rel="Stylesheet" media="all" href="res/Days.css" />
</head>
<body style="margin:0px;background-color:White;">
    <div id="content">
        <table id="theTable" style="width:100%;" cellspacing="0" cellpadding="0">
            <tbody id="theTbody">
            </tbody>
        </table>
        
        <div id="EventBoxesContainer"></div>
        <div id="grayCover" class="grayCover"></div>
    </div>
    
    <div style="display:none">
        <!-- Box template-->
        <table id="Box" class="Box" style="position:absolute;" cellpadding="0" cellspacing="0">
            <tr>
                <td class="BoxTopLeft" id="BoxTopLeft"></td>
                <td class="BoxHeader" id="BoxHeader"><span id="BoxTitle" class="BoxTitle"></span><span id="BoxProperty"></span></td>
                <td class="BoxTopRight" id="BoxTopRight"></td>                
            </tr>
            <tr>
                <td class="BoxMiddleLeft"></td>
                <td class="BoxMiddleCenter"><div id="BoxContent" class="BoxContent"></div></td>
                <td class="BoxMiddleRight"></td>                
            </tr>
            <tr>
                <td class="BoxMiddleLeft"></td>
                <td id="BoxHandleContainer" class="BoxHandleContainer" align="center"><center><div id="BoxHandle" class="BoxHandle"></div></center></td>
                <td class="BoxMiddleRight"></td>                
            </tr>            
            <tr>
                <td class="BoxBottomLeft" ></td>
                <td id="BoxBottomCenter" class="BoxBottomCenter" ></td>
                <td class="BoxBottomRight"></td>                
            </tr>            
        </table>    
        <!-- Box template-->
    </div>
    
    
    <%=EmbededScript%>
    
    <!--
    <input type="button" value="gen" onclick="generate(new Date(2008,0,1),4);" />    
    -->
</body>
</html>

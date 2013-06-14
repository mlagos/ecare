<%@ Page Language="C#" Culture="auto" UICulture="auto" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>
<%@ Register Assembly="obout_Window_NET" Namespace="OboutInc.Window" TagPrefix="owd" %>
<%@ Register src="UserControls/LoginForm.ascx" tagname="LoginForm" tagprefix="uc1" %>
<%@ Register src="UserControls/MoreInfo.ascx" tagname="MoreInfo" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Family eCare</title>
    <style type="text/css">
    body
    {
        background-color:#FFFFFF; 
        font-family: Arial Narrow;
    }
	
	#formLogin{width:400px; height:173px; position:relative; color:#FFFFFF; left: 520px; top: 20px; font-family:Verdana; font-size:10pt;}
    #formLogin #Error {color: red; width:350px; height:20px; margin: 10px 0 0 0;}
    #formLogin #formTitle
    {
    	font-weight: bold;
    	font-size: 12pt;
        margin: 0 0 10px 0;    	
    }
 	
 	/*
	.button {
	    font-family: Verdana, Arial, Helvetica, sans-serif;
	    color: #FFFFFF;
	    font-size: 10px;
	    font-weight:bold;
	    border: 1px solid #00344A;
	    height: 17px;
	    background-repeat: repeat;
	    background-image: url(images/button_background.gif);
	    cursor: hand;
	    background-color: #DADADA;
    }*/
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="home_content">
		<div id="ic_title"></div>
	    <uc1:LoginForm ID="LoginForm1" runat="server" />

        <owd:Window ID="Window1" Width="640" Height="480" runat="server" StyleFolder="css/obout/windowstyles/dogma" IsResizable="false" VisibleOnLoad="false">
        <div style="margin:20px 10px 10px 10px; width:100%">
           <uc2:MoreInfo ID="MoreInfo1" runat="server" />
        </div>
    </owd:Window>

	</div>
    </form>
</body>
</html>

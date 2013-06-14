<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loading.aspx.cs" Inherits="Nextgal.ECare.WebTeleasistencia.Loading" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        #loading
        {
        	position:absolute;
        	top:50%;
		    left:50%;
        	width: 16px;
        	height: 16px;
        	margin-top: -10px;
        	margin-left: -10px;
        	text-align:center;
        	vertical-align: middle;
        }
    </style>
</head>
<body onload="body_load();">
    <div id="loading">
        <asp:Image id="Image1" runat="server"
            ImageUrl="images/load_mini_red.gif"
            AlternateText="Cargando..." />
    </div>

    <script language="Javascript" type="text/javascript">
        function body_load() {
            window.location.replace('<%=newPath %>');
         }
    </script>
</body>
</html>

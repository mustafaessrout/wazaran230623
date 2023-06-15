<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_loginsms2.aspx.cs" Inherits="fm_loginsms2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   meta name="description" content=""/>
	<meta name="author" content=""/>
	<!--[if lt IE 9]>
		<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
	
	<!-- Mobile Specific Metas -->
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" /> 
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>
	<!-- Stylesheets -->
	<link rel="stylesheet" href="css/base.css"/>
	<link rel="stylesheet" href="css/skeleton.css"/>
	<link rel="stylesheet" href="css/layout.css"/>
	<link href="css/anekabutton.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
   
    <div class="container">
	<div class="form-bg">  
        
              <h3 style="padding:20px 20px 20px 20px;border:2px solid groove;background-color:silver;box-shadow:inset; font-family: Calibri"> FILL TOKEN TO CONTINUE / الرجاء تعبئة SMS CODE<asp:TextBox ID="txsms" runat="server" Width="8em" TextMode="SingleLine"></asp:TextBox> </h3>
              <div class="navi"><asp:Button ID="btsubmit" runat="server" Text="SUBMIT" OnClick="btsubmit_Click" CssClass="button2 add" /></div>
              <div style="text-align:center;font-family:Calibri;font-size:smaller;padding-top:20px">Your Token has sent to SMS and Email if first time in a day</div>
    </div>
    </div>
    </form>
</body>
</html>

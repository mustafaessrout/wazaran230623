<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_login2.aspx.cs" Inherits="fm_login2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <!-- General Metas -->
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>	<!-- Force Latest IE rendering engine -->
	<title>Login Form</title>
	<meta name="description" content=""/>
	<meta name="author" content=""/>
	<!--[if lt IE 9]>
		<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
	
	<!-- Mobile Specific Metas -->
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" /> 
	
	<!-- Stylesheets -->
	<link rel="stylesheet" href="css/base.css"/>
	<link rel="stylesheet" href="css/skeleton.css"/>
	<link rel="stylesheet" href="css/layout.css"/>
	<link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
 
</head>

<body>
   <div class="notice">
		<a href="#" class="close">close</a>
		<p class="warn" style="font-size:large">
            <asp:Label ID="lbnotice" runat="server" Text="Welcome To Wazaran System"></asp:Label></p>
	</div>



	<!-- Primary Page Layout -->
    <form runat="server">
	<div class="container">
		<div class="form-bg">
			
				<h2 style="font-family:Calibri;color:white;font-weight:bolder">Login</h2>
				<p>
                   <asp:TextBox ID="txusername" runat="server"></asp:TextBox></p>
				<p>
                    <asp:TextBox ID="txpassword" runat="server" TextMode="Password"></asp:TextBox></p>
				<label for="remember">
				  <input type="checkbox" id="remember" value="remember" />
				  <span>Remember me on this computer</span>
				</label>
                <asp:Button ID="btlogin" runat="server" Text="LOGIN" OnClick="Button1_Click" CssClass="button2 save"/>
			
		</div>
        <p class="forgot">Forgot your password? <a href="forgot.aspx">Click here to reset it.</a></p>
	</div><!-- container --></form>
	<!-- JS  -->
	<%--<script src="//ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.js"></script>
	<script>window.jQuery || document.write("<script src='js/jquery-1.5.1.min.js'>\x3C/script>")</script>
	<script src="/app.js"></script>--%>
	
<!-- End Document -->
    </body>
</html>

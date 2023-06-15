<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="fm_loginadm.aspx.cs" Inherits="admin_fm_loginadm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
     <link rel="stylesheet" href="css/reset.css"/>
     <%--<link rel='stylesheet prefetch' href='http://fonts.googleapis.com/css?family=Roboto:400,100,300,500,700,900|RobotoDraft:400,100,300,500,700,900'/>
     <link rel='stylesheet prefetch' href='http://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css'/>--%>
     <link rel="stylesheet" href="css/style.css"/>
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
     
<!-- Form Mixin-->
<!-- Input Mixin-->
<!-- Button Mixin-->
<!-- Pen Title-->
<div class="pen-title">
  <h1>Wazaran ERP</h1><span>@ <i class='fa fa-paint-brush'></i> + <i class='fa fa-code'></i> by <a href='#'>IAG</a></span>
</div>
<!-- Form Module-->
<div class="module form-module">
  <div class="toggle"><i class="fa fa-times fa-pencil"></i>
    <div class="tooltip">Click Me</div>
  </div>
   
  <div class="form">
    <h2>Login to your account</h2>
   
      <asp:TextBox ID="txusername" runat="server"></asp:TextBox>
      <asp:TextBox ID="txpassword" runat="server" TextMode="Password"></asp:TextBox>
      <asp:Button ID="btlogin" runat="server" Text="Login" OnClick="btlogin_Click" />
   
  </div>
  <div class="form">
    <h2>Create an account</h2>
    
      <input type="text" placeholder="Username"/>
      <input type="password" placeholder="Password"/>
      <input type="email" placeholder="Email Address"/>
      <input type="tel" placeholder="Phone Number"/>
      <button>Register</button>
   
  </div>
  
  <div class="cta"><a href="#">Forgot your password?</a></div>
</div>
    <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>
<script src='http://codepen.io/andytran/pen/vLmRVp.js'></script>

        <script src="js/index.js"></script>
    </div>
    </form>
</body>
</html>

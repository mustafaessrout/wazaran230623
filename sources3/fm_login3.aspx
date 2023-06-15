<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_login3.aspx.cs" Inherits="fm_login3" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Login </title>
 <link href="css/login_css.css" rel="stylesheet" />
        <!-- All the files that are required -->
 <link href="css/font-awesome.min.css" rel="stylesheet" />
 <link href="css/login_css.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
</head>
<body>

 
<!-- Where all the magic happens -->
<!-- LOGIN FORM -->
 
   <div style="text-align:center;font-family:Calibri;font-weight:normal;font-size:medium;">
    <div class="text-center" style="padding:50px 0">
    <div class="logo">Wazaran System Login</div>
    <!-- Main Form -->
    <div class="login-form-1">
        <form name="login-form" class="text-left" runat="server">
             <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div class="login-form-main-message"></div>
            <div class="main-login-form">
                <div class="login-group">
                    <div class="form-group">
                        <label for="lg_username" class="sr-only">Username</label>
                        <asp:TextBox ID="txusername" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="lg_password" class="sr-only">Password</label>
                        <asp:TextBox ID="txpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="form-group login-group-checkbox">
                        <input type="checkbox" id="lg_remember" name="lg_remember"/>
                        <label for="lg_remember">remember</label>
                    </div>
                </div>
                <asp:Button ID="btlogin" runat="server"  CssClass="login-button" OnClick="btlogin_Click"/><i class="fa"><asp:Label ID="lbnotice" runat="server" ForeColor="Red"></asp:Label></i>
            </div>
            <div class="etc-login-form">
                <p>forgot your password? <a href="forgot.aspx">click here</a></p>
            </div>
        </form>
    </div>
    <!-- end:Main Form -->
</div>
       </div>
</body>
</html>

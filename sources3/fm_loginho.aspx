<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_loginho.aspx.cs" Inherits="fm_loginho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
</head>
<body>
   <div class="container">
    <div class="row">
        <div class="col-sm-8 col-md-4 col-md-offset-4">
            <h1 class="text-center login-title">Wazaran ERP H.O</h1>
            
            <div class="account-wall">
                <img class="profile-img" src="logossbtc.jpg"
                    alt="" style="width:100%;height:100%"/>
                <form class="form-signin" id="fmain" runat="server">
                    <asp:TextBox ID="txusername" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox ID="txpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:Button ID="btlogin" runat="server" Text="Login" CssClass="btn btn-lg btn-primary btn-block" OnClick="btlogin_Click" />
                    Sign in
                <label class="checkbox pull-left">
                    <input type="checkbox" value="remember-me"/>
                    Remember me
                </label>
                <a href="#" class="pull-right need-help">Need help? </a><span class="clearfix"></span>
                </form>
            </div>
            <a href="#" class="text-center new-account">Create an account </a>
        </div>
    </div>
</div>
</body>
</html>

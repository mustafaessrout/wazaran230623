<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_login.aspx.cs" Inherits="promotor_fm_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/Login-Form-Dark.css" rel="stylesheet" />
    <link href="fonts/ionicons.min.css" rel="stylesheet" />
    <script src="Content/jquery.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="../js/sweetalert.min.js"></script>
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <style>
        #cbexhibition option {
            color:black;
        }
    </style>
</head>
<body>
       <div class="login-dark">
        <form method="post" runat="server">
            <h2 style="font-family:Calibri">Login Form</h2>
            <div class="illustration"><i class="icon ion-ios-locked-outline"></i></div>
             <div class="form-group">
                <asp:DropDownList ID="cbexhibition" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txuserid" runat="server" placeholder="User Name" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
               <asp:TextBox ID="txpassword"  runat="server" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
            </div>
           
            <div class="form-group">
                <asp:LinkButton ID="btlogin" CssClass="btn btn-primary btn-lg" runat="server" OnClick="btlogin_Click">Login</asp:LinkButton>
            </div><a href="#" class="forgot">Forgot your email or password?</a></form>
    </div>
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>

</body>
</html>

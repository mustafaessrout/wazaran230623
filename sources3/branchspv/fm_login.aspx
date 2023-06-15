<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_login.aspx.cs" Inherits="eis_fm_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/Google-Style-Login.css" rel="stylesheet" />
    <link href="assets/css/styles.css" rel="stylesheet" />
</head>
<body>
    <%--<form id="fmmain" runat="server">--%>
    <div class="login-card"><img src="assets/img/avatar_2x.png" class="profile-img-card"/>
        <p class="profile-name-card">Executive Information </p>
        <form class="form-signin" runat="server"><span class="reauth-email"> </span>
            <input class="form-control" type="text" required="" placeholder="Email address" autofocus="" id="inputEmail"/>
            <input class="form-control" type="password" required="" placeholder="Password" id="inputPassword"/>
            <div class="checkbox">
                <div class="checkbox">
                    <label>
                       <input type="checkbox" runat="server" />Remember Me

                    </label>
                </div>
            </div>
            <asp:LinkButton ID="btlogin" CssClass="btn btn-primary btn-block btn-lg btn-signin" runat="server" OnClick="btlogin_Click">Login</asp:LinkButton>
            
        </form><a href="#" class="forgot-password">Forgot your password?</a></div>
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>

    <%--</form>--%>

</body>
</html>

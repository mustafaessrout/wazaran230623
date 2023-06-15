<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_login4.aspx.cs" Inherits="fm_login4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="css/loginstyle.css" rel="stylesheet" />--%>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <!--custom css-->
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
  
   

    <!--custom js-->
    <script src="js/index.js"></script>

</head>
<body class="login4 center middle" style="height:100%;">
     
    <div class="content">
         <div class="logo">
            <img src="image/logo1.jpg" alt="logo sbtc" />
        </div>
       <form id="frm" runat="server" class="login-form">
           <h3 class="title text-center">Login Wazaran ERP System</h3>
           
            <div class="login-inputs">
                <p>Sign In</p>
                <div class="border-white tops">
                    <asp:TextBox ID="txusername" runat="server" placeholder="Username" required data-toggle="tooltip" title="At least 6 character!"></asp:TextBox>
                </div>
                 <div class="border-white bottoms">
                    <asp:TextBox ID="txpassword" runat="server" TextMode="Password" placeholder="Password" required data-toggle="tooltip" title="Use upper and lowercase lettes as well"></asp:TextBox>
                </div>

                <div class="notice"><asp:Label ID="lbnotice" runat="server" CssClass="text-red"></asp:Label></div>

                <asp:Button ID="btlogin" runat="server" Text="Login" OnClick="btlogin_Click" CssClass="btn btn-white full"/>
            </div>
        
        </form>
    </div> 

</body>
</html>

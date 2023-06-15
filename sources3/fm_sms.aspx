<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_sms.aspx.cs" Inherits="fm_sms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
       <form id="frm" runat="server"  class="login-form">
 
          <h3 class="title text-center">SMS Verification</h3>
           
            <div class="login-inputs">
                <div class="border-white">
                    <asp:TextBox ID="txsms" runat="server" autocomplete="off"></asp:TextBox>
                </div>
              <div class="help padding-top-4 block">At least 4 character</div>
                <div>
                    <p class="inline-block padding-right" style="margin-top:10px;">Enter SMS sent by Wazaran</p>
                    <asp:Button ID="btlogin" runat="server" Text="Process" CssClass="btn btn-primary pull-right" OnClick="btlogin_Click" />
                    
                   <%-- <asp:Button ID="btnSMSFailed" runat="server" Text="SMS Failed" CssClass="btn btn-primary pull-right" OnClick="btnSMSFailed_Click" />--%>
                </div>
              
              
            </div>
        </form>

    </div>

</body>
</html>

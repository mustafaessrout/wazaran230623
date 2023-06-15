<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_forgotPassword.aspx.cs" Inherits="fm_forgotPassword" %>



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
    <link href="css/font-face/khula.css" rel="stylesheet" />



    <!--custom js-->
    <script src="js/index.js"></script>
    <style>
        input[type="text"] {
            border-radius: 0 !Important;
        }
    </style>
</head>
<body class="login4 center middle" style="height: 100%;">

    <div class="content">
        <div class="logo">
            <img src="image/logo_tw.png" alt="logo transworld"/>
        </div>
        <form id="frm" runat="server" class="login-form">
            <h3 class="title text-center">Forgot Password</h3>

            <div class="login-inputs">
                <label id="Label1" runat="server" class="control-label col-sm-12" style="padding-top: 17px;">Enter Your User ID:</label>
            </div>
            <div class="border-white border-tops">
                <div class="border-white border-bottoms" style="margin-bottom: 15px">

                    <asp:TextBox ID="UserID" runat="server" CssClass="form-control" Style="background: none; color: #fff;" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
            </div>
            <div class="login-inputs">
                <label id="Label2" runat="server" class="control-label col-sm-12" style="padding-top: 17px;">Enter Your Email:</label>
            </div>
            <div class="border-white border-tops">
                <div class="border-white border-bottoms" style="margin-bottom: 15px">

                    <asp:TextBox ID="Email" runat="server" CssClass="form-control" Style="background: none; color: #fff;" AutoCompleteType="Disabled"></asp:TextBox>

                </div>
            </div>

            <div style="margin-bottom: 25px">
                <asp:Label ID="lblMessage" runat="server" />
            </div>

            <div style="margin-bottom: 15px">
                <asp:Button ID="btSubmit" runat="server" Text="Submit" OnClick="btSubmit_Click" CssClass="btn btn-white full" />
            </div>
            <div>
                <asp:Button ID="btInfo" runat="server" Text="Add Personal Information" OnClick="btInfo_Click" CssClass="btn btn-white full" Visible="False" />
            </div>
            <%--<div>
                    <asp:Button ID="btClose" runat="server" Text="Close" OnClick="btClose_Click" CssClass="btn btn-white full"  />
                </div>--%>
        </form>
    </div>

</body>
</html>






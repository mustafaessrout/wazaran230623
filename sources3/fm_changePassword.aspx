<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_ChangePassword.aspx.cs" Inherits="fm_ChangePassword" %>



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
    <script>
        $(document).ready(function () {
            $get('<%=NewPassword.ClientID%>').attr('autocomplete', 'off');
        });
    </script>

</head>
<body class="login4 center middle" style="height: 100%;" >

    <div class="content">
        <div class="logo">
            <img src="image/logo_tw.png" alt="logo transworld"/>
        </div>
        <form id="frm" runat="server" class="login-form">
            <h3 class="title text-center">Change Password</h3>

            <div class="login-inputs">
                <label id="Label5" runat="server" class="control-label col-sm-12" style="padding-top: 17px;">Enter Your User ID:</label>
            </div>
            <div class="border-white border-tops">
                <div class="border-white border-bottoms" style="margin-bottom: 15px">
                    <asp:TextBox ID="UserID" runat="server" CssClass="form-control" Style="background: none; color: #fff;" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
            </div>
            <div class="login-inputs">
                    <label id="Label3" runat="server" class="control-label col-sm-12" style="padding-top: 17px;">Enter Your Old Password:</label>
                </div>
                <div class="border-white border-tops">
                    <div class="border-white border-bottoms" style="margin-bottom: 15px">
                        <asp:TextBox ID="txpassword" AutoComplete="off" runat="server" TextMode="Password" placeholder="Password" title="The Password should be Minimum 6 characters at least 1 Capital , 1 Number  Ex: D4ry123 " Style="background: none; color: #fff;" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="login-inputs">
                    <label id="Label2" runat="server" class="control-label col-sm-12" style="padding-top: 17px;">Enter Your New Password:</label>
                </div>
                <div class="border-white border-tops">
                    <div class="border-white border-bottoms" style="margin-bottom: 15px">
                        <asp:TextBox ID="NewPassword" Text="" runat="server"  TextMode="Password" Style="background: none; color: #fff;" AutoCompleteType="Disabled"
                                 placeholder="New Password" title="Use Minimum 6 characters atleast 1 Alphabet, 1 Number"  ></asp:TextBox><br />

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="NewPassword"
                                ValidationExpression="^(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$"
                                ErrorMessage="The new Password should be Minimum 6 characters at least 1 Capital , 1 Number  Ex: D4ry123 " ForeColor="Red" />
                    </div>
                </div>

                <div class="login-inputs">
                    <label id="Label4" runat="server" class="control-label col-sm-12" style="padding-top: 17px;">Confirm New Password:</label>
                </div>
                <div class="border-white border-tops">
                    <div class="border-white border-bottoms" style="margin-bottom: 15px">
                        <asp:TextBox ID="ConfirmNewPassword" Text="" runat="server" TextMode="Password" 
                                Style="background: none; color: #fff;" AutoCompleteType="Disabled" placeholder="Confirm New Password" 
                                title="Use Minimum 6 characters atleast 1 Alphabet, 1 Number"  autocomplete="off"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ConfirmNewPassword"
                                ValidationExpression="^(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$" />
                            <asp:CompareValidator ID="ComValCNP" runat="server" ControlToCompare="NewPassword" CssClass="padding-top-4 block text-bold " Style="color: red; font-size: 17px;" ControlToValidate="ConfirmNewPassword">New Password and Confirm New Password does not match</asp:CompareValidator>
                    </div>
                </div>

            <div style="margin-bottom: 25px">
                <asp:Label ID="lblMessage" runat="server" />
            </div>

            <div style="margin-bottom: 15px">
                <asp:Button ID="btConfirm" runat="server" Text="Confirm" OnClick="btConfirm_Click" CssClass="btn btn-white full" />
            </div>

        </form>
    </div>






</body>
</html>






<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_login5.aspx.cs" Inherits="fm_login5" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="css/loginstyle.css" rel="stylesheet" />--%>
    <script src="js/jquery-1.9.1.min.js"></script>
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
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

        $(document).ready(function () {
            //alert("sd")
            try {
                var network = new ActiveXObject('WScript.Network');
                // Show a pop up if it works
                alert(network.computerName);
            }
            catch (e) { }
        });

        function GetComputerName() {
            try {
                var network = new ActiveXObject('WScript.Network');
                // Show a pop up if it works
                alert(network.computerName);
            }
            catch (e) { }
        }
    </script>
</head>
<body class="login4 center middle" style="height: 100%;">

    <div class="content">
        <div class="logo">
            <img src="image/logo1.jpg" alt="logo sbtc" />
        </div>
        <form id="frm" runat="server" class="login-form" autocompletetype="Disabled">
            <h3 class="title text-center">Login Wazaran ERP System</h3>

            <div class="login-inputs">
                <p>Sign In</p>
                <div class="border-white border-tops">
                    <asp:TextBox ID="txusername" runat="server" placeholder="Username" title="At least 6 character!" AutoCompleteType="Disabled"></asp:TextBox>
                </div>


                <div class="border-white  border-bottom">
                    <asp:TextBox ID="txpassword" AutoComplete="off" runat="server" TextMode="Password" placeholder="Password" title="The Password should be Minimum 6 characters at least 1 Capital , 1 Number  Ex: D4ry123 "></asp:TextBox>

                </div>



                <asp:ScriptManager EnablePartialRendering="true"
                    ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="notice">
                                <asp:Label ID="lbnotice" runat="server" CssClass="text-red"></asp:Label>
                            </div>
                            <asp:Button ID="btlogin" runat="server" Text="Login" OnClick="btlogin_Click" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-white full" />
                            <div style="margin-bottom: 15px">
                                <asp:Button ID="btchangepswd" runat="server" Text="Change Password" OnClick="btchangepswd_Click" CssClass="btn btn-white full" Visible="False" />
                            </div>
                            <div>
                                <asp:Button ID="btcontinue" runat="server" Text="Continue" OnClick="btcontinue_Click" CssClass="btn btn-white full" Visible="False" />
                            </div>
                            <div>
                                <asp:Button ID="btInfo" runat="server" Text="Add Personal Information" OnClick="btInfo_Click" CssClass="btn btn-white full" Visible="False" />
                            </div>
                            <div style="margin-top: 15px">
                                <asp:LinkButton ID="lbforgot" Text="forgot Password ?" runat="server" OnClick="lbforgot_Click" CssClass="text-primary"></asp:LinkButton>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btlogin" EventName="Click"  />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </form>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>






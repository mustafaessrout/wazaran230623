<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_smsFailed.aspx.cs" Inherits="fm_smsFailed" %>

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
    <link href="css/font-face/khula.css" rel="stylesheet" />



    <!--custom js-->
    <script src="js/index.js"></script>
</head>
<body class="login4 center middle" style="height: 100%;">
    <div class="content">
        <div class="logo">
            <img src="image/logo1.jpg" alt="logo sbtc" />
        </div>
        <form id="frm" runat="server" class="login-form">
            <div class="form-horizontal">
                <h4 class="jajarangenjang">Login Verification</h4>
                <div class="h-divider"></div>
                <div class="container">
                    <div class="form-group">
                        <div class="row">
                            <label class="control-label col-md-1">Name</label>
                            <div class="col-md-2">

                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>

                            </div>
                            <label class="control-label col-md-1">Phone</label>
                            <div class="col-md-2">

                                <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label class="control-label col-md-1">First Question</label>
                            <div class="col-md-2    drop-down">
                                <asp:DropDownList ID="ddlFirstQuestion" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFirstQuestion_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                            <label class="control-label col-md-1">Second Question</label>
                            <div class="col-md-2    drop-down">
                                <asp:DropDownList ID="ddlSecondQuestion" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSecondQuestion_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                            <label class="control-label col-md-1">Third Question</label>
                            <div class="col-md-2    drop-down">

                                <asp:DropDownList ID="ddlThirdQuestion" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlThirdQuestion_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="control-label col-md-1">First Answer</label>
                                <div class="col-md-2">
                                   <asp:TextBox ID="txtFirstAnswer" runat="server" AutoPostBack="true" OnTextChanged="txtFirstAnswer_TextChanged"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-1">Second Answer</label>
                                <div class="col-md-2">
                                   <asp:TextBox ID="txtSecondAnswer" runat="server" AutoPostBack="true" OnTextChanged="txtSecondAnswer_TextChanged"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-1">Third Answer</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtThirdAnswer" runat="server" AutoPostBack="true" OnTextChanged="txtThirdAnswer_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </form>

    </div>

</body>
</html>

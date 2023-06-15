<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .button2 btn.save {
            background: #f3f3f3 url('css/5Fm069k.png') no-repeat 10px 7px;
            padding-left: 30px;
            border-radius: 2px;
        }

        .button2 btn {
            color: #6e6e6e;
            font: bold 12px Helvetica, Arial, sans-serif;
            text-decoration: none;
            /* padding: 7px 12px; */
            padding: 3px 8px;
            /* position: relative; */
            display: inline-block;
            text-shadow: 0 1px 0 #fff;
            -webkit-transition: border-color .218s;
            -moz-transition: border .218s;
            -o-transition: border-color .218s;
            transition: border-color .218s;
            background: #f3f3f3;
            background: -webkit-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
            background: -moz-linear-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
            border: solid 1px #dcdcdc;
            border-radius: 6px;
            -webkit-border-radius: 6px;
            -moz-border-radius: 6px;
            margin-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Change Password</div>
    <div class="h-divider"></div>
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">

                <div class="form-group clearfix no-margin-bottom">
                    <label id="Label1" runat="server" class="control-label col-sm-3 text-right" style="padding-top: 17px;">Current Password</label>
                    <div class="col-sm-9 margin-bottom">
                        <%--<asp:RequiredFieldValidator ID="ReqFVCP" runat="server" ControlToValidate="CurrentPassword"
                            ErrorMessage="Please Enter Current Password" CssClass="text-danger pull-right">required</asp:RequiredFieldValidator>--%>
                        <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" Width="100%" CssClass="form-control" />
                    </div>

                    <label id="Label4" runat="server" class="control-label col-sm-3" style="padding-top: 5px;">New Password</label>
                    <div class="col-sm-9 margin-bottom">
                        <%--<asp:RequiredFieldValidator ID="ReqFVNP" runat="server" ErrorMessage="Please Enter New Password" ControlToValidate="NewPassword" CssClass="text-danger pull-right">required</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NewPassword"
                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&^,.~_£:;'(\)\-])[A-Za-z\d$@$!%*#?&^,.~_£:;'(\)\-]{6,}$"
                            ErrorMessage="The new Password should be Minimum 6 characters at least 1 Alphabet, 1 Number and 1 Special Character Ex: d4ry4nto&@# " ForeColor="Red" />
                        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>--%>
                        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" CssClass="form-control" title="The new Password should be Minimum 6 characters at least 1 Capital , 1 Number  Ex: D4ry123"></asp:TextBox><br />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NewPassword"
                            ValidationExpression="^(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$"
                            ErrorMessage="The new Password should be Minimum 6 characters at least 1 Capital , 1 Number  Ex: D4ry123 " ForeColor="Red" />
                    </div>


                    <label id="Label5" runat="server" class="control-label col-sm-3" style="padding-top: 5px;">Confirm New Password</label>
                    <div class="col-sm-9 margin-bottom">
                        <%--<asp:RequiredFieldValidator ID="ReqFVCNP" runat="server" ErrorMessage="Please Confirm New Password" ControlToValidate="ConfirmNewPassword" CssClass="text-danger pull-right">required</asp:RequiredFieldValidator>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ConfirmNewPassword"
                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&^,.~_£:;'(\)\-])[A-Za-z\d$@$!%*#?&^,.~_£:;'(\)\-]{6,}$" />
                            <asp:CompareValidator ID="ComValCNP" runat="server" ControlToCompare="NewPassword" CssClass="padding-top-4 block text-bold " Style="color: red; padding-top: 15px;" ControlToValidate="ConfirmNewPassword">New Password and Confirm New Password does not match</asp:CompareValidator>
                        <asp:CompareValidator ID="ComValCNP" runat="server" ControlToCompare="NewPassword" CssClass="padding-top-4 block text-bold text-red" ControlToValidate="ConfirmNewPassword">New Password and Confirm New Password does not match</asp:CompareValidator>--%>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" CssClass="form-control" title="The new Password should be Minimum 6 characters at least 1 Capital , 1 Number  Ex: D4ry123 "></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ConfirmNewPassword"
                            ValidationExpression="^(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$" />
                        <asp:CompareValidator ID="ComValCNP" runat="server" ControlToCompare="NewPassword" CssClass="padding-top-4 block text-bold " Style="color: red; padding-top: 15px;" ControlToValidate="ConfirmNewPassword">New Password and Confirm New Password does not match</asp:CompareValidator>
                    </div>
                </div>
                <div style="margin-bottom: 20px;" class="col-md-offset-3 col-md-9">
                    <asp:Label ID="lblMessage" runat="server" />
                    <%--<asp:ValidationSummary ID="ValSumm" runat="server" CssClass="text-bold text-red"/>--%>
                </div>


                <div class="navi margin-bottom text-center">
                    <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning btn-save " OnClick="btsave_Click" />
                    <asp:Button ID="btclose" runat="server" CssClass="btn btn-danger btn-danger" OnClick="btclose_Click" Text="Close" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>


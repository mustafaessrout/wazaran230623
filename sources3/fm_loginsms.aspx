<%@ Page Title="" Language="C#" MasterPageFile="~/site.master"  AutoEventWireup="true" CodeFile="fm_loginsms.aspx.cs" Inherits="fm_loginsms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .forsms {
            width: 70px;
            height: 73px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="background-image:url('/Indomie_logo.jpg');" class="forsms"></div>
    <div style="text-align:center;padding:10px;font-size:x-large">
        ENTER SMS CODE TO CONTINUE / يرجى كتابة رسالة SMS من
    </div>
    <div style="text-align:center;padding:10px;font-size:large;vertical-align:central">
        <asp:TextBox ID="txsms" runat="server"></asp:TextBox><asp:Button ID="btSend" runat="server" Text="SUBMIT" CssClass="button2 save" OnClick="btSend_Click" style="left: 0px; top: 4px" />
    </div>
</asp:Content>


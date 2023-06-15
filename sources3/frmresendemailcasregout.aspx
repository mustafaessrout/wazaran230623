<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmresendemailcasregout.aspx.cs" Inherits="frmresendemailcasregout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


.button2.save {
    background: #f3f3f3 url('css/5Fm069k.png') no-repeat 10px 7px;
    padding-left: 30px;
    border-radius:2px;
}

.button2 {
    color: #6e6e6e;
    font: bold 12px Helvetica, Arial, sans-serif;
    text-decoration: none;
    /* padding: 7px 12px; */
    padding:3px 8px;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table>
            <tr>
                <td>CasregoutCD</td>
                <td>:</td>
                <td><asp:TextBox ID="txcashregout_cd" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
    <asp:Button ID="btemail" runat="server" Text="cashregoutemail" Width="185px" OnClick="btemail_Click" CssClass="button2 save" style="left: 0px; top: 0px" />
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>


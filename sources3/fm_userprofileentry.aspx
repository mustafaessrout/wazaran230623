<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_userprofileentry.aspx.cs" Inherits="fm_userprofileentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        .auto-style2 {
            text-align: right;
            font-size: small;
        }
        .auto-style3 {
            height: 23px;
            text-align: right;
            font-size: small;
        }
        .auto-style4 {
            text-align: right;
            height: 26px;
            font-size: small;
        }
        .auto-style5 {
            height: 26px;
        }
        .auto-style6 {
            font-size: small;
        }
        .auto-style7 {
            height: 23px;
            font-size: small;
        }
        .auto-style8 {
            height: 26px;
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>New User Entry</h3>
    <table style="width: 100%;">
        <tr>
            <td class="auto-style4">User ID</td>
            <td class="auto-style8">:</td>
            <td class="auto-style5">
                <asp:TextBox ID="txusrid" runat="server" CssClass="auto-style6" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">Full Name</td>
            <td class="auto-style7">:</td>
            <td class="auto-style1">
                <asp:TextBox ID="txfullname" runat="server" CssClass="auto-style6" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Email</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="txemail" runat="server" CssClass="auto-style6" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Mobile</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="txmobile" runat="server" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Home</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="txhome" runat="server" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Password expiration</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="dtexp" runat="server" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Resign Date</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="dtresign" runat="server" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">password</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="txpassword" runat="server" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">whatsapp No.</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="txwa" runat="server" Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3"></td>
            <td class="auto-style7"></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td>
                <asp:Button ID="btsave" runat="server" OnClick="btsave_Click" Text="SAVE" Width="96px" />
            </td>
        </tr>
    </table>
</asp:Content>


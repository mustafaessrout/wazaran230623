<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="grp_vendor_entry.aspx.cs" Inherits="grp_vendor_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Entry Group Vendor</h3>
    <table>
        <tr style="text-align: left">
            <td style="text-align: left">
                Group Code
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txcode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="text-align: left">
            <td style="text-align: left">
                Group Name
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txname" runat="server" Height="16px" Width="256px"></asp:TextBox>
            </td>
        </tr>
        <tr style="text-align: left">
            <td style="text-align: left">
                Optional 1
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txopt1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="text-align: left">
            <td style="text-align: left">
                Optional 2
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txopt2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="text-align: left">
            <td style="text-align: left">
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr style="text-align: left">
            <td style="text-align: left">
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btSave" runat="server" Height="26px" OnClick="btSave_Click" Text="SAVE" Width="106px" CssClass="button2 save" />
            </td>
        </tr>
    </table>
    </asp:Content>


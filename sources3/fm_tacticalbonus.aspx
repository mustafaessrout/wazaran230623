<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_tacticalbonus.aspx.cs" Inherits="fm_tacticalbonus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Tactical Bonus Entry
    </div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <table><tr><td>Contract No.</td><td>:</td><td style="margin-left: 40px">
            <asp:TextBox ID="txcontractno" runat="server" CssClass="ro">NEW</asp:TextBox></td>
            <td>
                Contract Date</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="dtcontract" runat="server" CssClass="makeitreadonly"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td class="auto-style1">Start Date</td><td class="auto-style1">:</td><td class="auto-style1">
                <asp:TextBox ID="dtstart" runat="server" CssClass="makeitreadonly"></asp:TextBox></td>
                <td class="auto-style1">
                    End Date</td>
                <td class="auto-style1">
                    :</td>
                <td class="auto-style1">
                    <asp:TextBox ID="dtend" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                </td>
               </tr>
            <tr>
            <td>&nbsp;</td><td>&nbsp;</td><td>
                &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
               </tr>
            <tr>
            <td>&nbsp;</td><td>&nbsp;</td><td>
                &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
               </tr>
            <tr>
            <td>&nbsp;</td><td>&nbsp;</td><td>
                &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
               </tr></table>
    </div>
</asp:Content>


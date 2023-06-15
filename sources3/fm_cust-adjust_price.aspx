<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cust-adjust_price.aspx.cs" Inherits="fm_cust_adjust_price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 62px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        Customer Type - Adjustment Price by Supervisour Report
    </div>
    <img src="div2.png" class="divid" />

    <table class="auto-style1">
        <tr>
            <td class="auto-style2">Report:</td>
            <td>
                <asp:DropDownList ID="cbreport" runat="server" Width="300px">
                    <asp:ListItem Value="0">Customer Type Price</asp:ListItem>
                    <asp:ListItem Value="1">Customer Adjustment Price</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">SuperVisour:</td>
            <td>
                <asp:DropDownList ID="cbsupervisour" runat="server" Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>

    <div>
    </div>
    <div class="navi">
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
</asp:Content>


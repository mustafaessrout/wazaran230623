<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_itemprice.aspx.cs" Inherits="fm_itemprice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            margin-left: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>Price Level</strong>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>
                Price Level Code
            </td>
            <td>
                :</td>
            <td class="auto-style1">
                <asp:TextBox ID="txpricelevelcode" runat="server"></asp:TextBox>
            </td>
            <td> &nbsp;</td>
            <td> Price Level Name</td>
            <td> :</td>
            <td> 
                <asp:TextBox ID="txpricename" runat="server"></asp:TextBox>
            </td>
            <td> &nbsp;</td>
        </tr>
        <tr>
            <td>
                Start Date</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="txpricelevelcode0" runat="server"></asp:TextBox>
            </td>
            <td> &nbsp;</td>
            <td> End Date</td>
            <td> :</td>
            <td> 
                <asp:TextBox ID="txpricename0" runat="server"></asp:TextBox>
            </td>
            <td> &nbsp;</td>
        </tr>
        <tr>
            <td>
                UOM</td>
            <td>
                :</td>
            <td>
                <asp:DropDownList ID="cbuom" runat="server">
                </asp:DropDownList>
            </td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
        </tr>
        <tr>
            <td>
                ITEM</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txsearch" runat="server" Height="17px" Width="225px"></asp:TextBox>
            </td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
            <td> &nbsp;</td>
        </tr>
    </table>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salescanvasser.aspx.cs" Inherits="fm_salescanvasser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Sales Canvasser
    </div>
    <img src="div2.png" />
    <table>
        <tr>
            <td>

                Date</td>
            <td>

                :</td>
            <td>

                <asp:TextBox ID="dtcanvas" runat="server" CssClass="makeitreadonly"></asp:TextBox>

            </td>
            <td>

                Canvas No.</td>
            <td>

                :</td>
            <td>

                <asp:TextBox ID="txcanvasno" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>

                Salesman

            </td>
            <td>

                :</td>
            <td>

                <asp:DropDownList ID="cbsalesman" runat="server" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>

                Customer</td>
            <td>

                :</td>
            <td>

                <asp:TextBox ID="txcustomer" runat="server" Width="200px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
        </tr>
    </table>
</asp:Content>


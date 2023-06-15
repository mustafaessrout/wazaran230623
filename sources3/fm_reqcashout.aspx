<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqcashout.aspx.cs" Inherits="fm_reqcashout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Reques Cash Out
    </div>
    <img src="div2.png" class="divid" />
    <div>
        <table>
            <tr>
                <td>
                    Request No.
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txreqno" runat="server"></asp:TextBox>
                </td>
                <td>
                    Request Date
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="dtrequest" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


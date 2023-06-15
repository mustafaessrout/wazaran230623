<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reportdlg.aspx.cs" Inherits="fm_reportdlg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Reporting
    </div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>
                Report To Be Generated
            </td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbreport" runat="server" Width="200px">
            <asp:ListItem Value="iv">Invoice</asp:ListItem>
            <asp:ListItem Value="cs">Customer Statement</asp:ListItem>
            <asp:ListItem Value="is">Invoice By Salesman</asp:ListItem>
        </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Salesman
            </td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbsalesman" runat="server" Width="200px"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <img src="div2.png" class="divid" />
     <div class="navi">
         <asp:Button ID="btreport" runat="server" Text="Print" OnClick="btreport_Click" CssClass="button2 print" />
     </div>
    
</asp:Content>


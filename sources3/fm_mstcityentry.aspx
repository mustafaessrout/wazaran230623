<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcityentry.aspx.cs" Inherits="fm_mstcityentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Entry City</h3>
    <img src="line.gif" class="divid" />
    <table class="aspgrid">
        <tr>
            <td class="kiri1">City Code</td>
            <td class="tengah1">:</td>
            <td class="kanan1">
                <asp:TextBox ID="txcitycode" runat="server"></asp:TextBox>
            </td>
            <td class="kanan1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txcitycode" ErrorMessage="City Code can not empty !" CssClass="warn"></asp:RequiredFieldValidator>
            </td>
            <td class="kiri1">City Name</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txcityname" runat="server" Width="177px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="kiri1">Arabic</td>
            <td class="tengah1">:</td>
            <td class="kanan1">
                <asp:TextBox ID="txarabic" runat="server"></asp:TextBox>
            </td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="kiri1">Region</td>
            <td class="tengah1">:</td>
            <td class="kanan1"> 
                <asp:DropDownList ID="cbregion" runat="server" Height="16px" Width="192px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="kiri1">&nbsp;</td>
            <td class="tengah1">&nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="kiri1">&nbsp;</td>
            <td class="tengah1">&nbsp;</td>
            <td class="kanan1"> 
                &nbsp;</td>
        </tr>
        <tr>
            <td class="kiri1">&nbsp;</td>
            <td class="tengah1">&nbsp;</td>
            <td class="kanan1">
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click"/>
            </td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="kiri1">&nbsp;</td>
            <td class="tengah1">&nbsp;</td>
            <td class="kanan1"> 
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>


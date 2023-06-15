<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_customerpriceadj.aspx.cs" Inherits="fm_customerpriceadj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Adjustment Price</div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <table>
            <tr><td>Item </td><td>:</td><td style="margin-left: 40px">
                <asp:TextBox ID="txitemsearch" runat="server" Width="20em"></asp:TextBox></td><td>
                    Adjustment Price for</td><td>
                    :</td><td>
                    <asp:RadioButtonList ID="rdcust" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True">
                        <asp:ListItem Value="C">Customer</asp:ListItem>
                        <asp:ListItem Value="G">Group </asp:ListItem>
                    </asp:RadioButtonList>
                </td></tr>
            <tr><td>&nbsp;</td><td>&nbsp;</td><td colspan="3" style="margin-left: 40px">
                <asp:TextBox ID="txcust" runat="server" Width="20em" Visible="False"></asp:TextBox>
                <asp:DropDownList ID="cbgroup" runat="server" Width="20em" Visible="False">
                </asp:DropDownList>
                </td><td>
                    <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" />
                </td></tr>
            <tr><td>&nbsp;</td><td>&nbsp;</td><td style="margin-left: 40px">
                <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Code"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust / Group"></asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                </td><td>
                    </td><td>
                    &nbsp;</td><td>
                    &nbsp;</td></tr>
            <tr><td>Unit Price</td><td>:</td><td style="margin-left: 40px">
                <asp:TextBox ID="txunitprice" runat="server"></asp:TextBox>
                </td><td>
                    &nbsp;</td><td>
                    &nbsp;</td><td>
                    &nbsp;</td></tr>
            <tr><td>&nbsp;</td><td>&nbsp;</td><td style="margin-left: 40px">
                &nbsp;</td><td>
                    &nbsp;</td><td>
                    &nbsp;</td><td>
                    &nbsp;</td></tr>
        </table>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" />
    </div>
</asp:Content>


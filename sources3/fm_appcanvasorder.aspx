<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appcanvasorder.aspx.cs" Inherits="fm_appcanvasorder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        List Canvas Order Need approval
    </div>
    <img src="div2.png" class="divid" />
    <asp:GridView ID="grdcanvas" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Canvas No">
                <ItemTemplate>
                    <asp:Label ID="lbsocode" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Manual No"><ItemTemplate><%# Eval("manual_no") %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Date"><ItemTemplate><%# Eval("so_dt","{0:d/M/yyyy}") %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Cust">
                <ItemTemplate>
                    <%# Eval("cust_cd") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CL"></asp:TemplateField>
            <asp:TemplateField HeaderText="Outstanding INV"></asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</asp:Content>


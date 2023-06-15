<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockopnamelist.aspx.cs" Inherits="fm_stockopnamelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Stock Opname List
    </div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="75%" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" CellPadding="0" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Stock Opname No.">
                    <ItemTemplate>
                        <asp:Label ID="lbstockno" runat="server" Text='<%# Eval("stock_no") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%# Eval("stock_dt","{0:d/M/yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Warehouse">
                    <ItemTemplate><%# Eval("whs_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bin">
                    <ItemTemplate><%# Eval("bin_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark">

                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
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
    </div>
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="button2 add" OnClick="btnew_Click" />
    </div>
</asp:Content>


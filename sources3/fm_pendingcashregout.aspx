<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_pendingcashregout.aspx.cs" Inherits="fm_pendingcashregout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Pending List Cash Register Approval
    </div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Cashout Register No.">
                    <ItemTemplate>
                        <asp:Label ID="lbcashregout" runat="server" Text='<%# Eval("casregout_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%#Eval("cashout_dt","{0:d/M/yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate><%# Eval("cashout_typ_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lbitemcashoutcode" runat="server" Text='<%# Eval("itemco_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate><%# Eval("itemco_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Value">
                    <ItemTemplate><%# Eval("amt","{0:f2}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PIC">
                    <ItemTemplate><%# Eval("emp_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approval">
                    <ItemTemplate><%# Eval("app_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField SelectText="Send Again Reminding" ShowSelectButton="True" />
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
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_custbalance.aspx.cs" Inherits="fm_custbalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Customer Balance Report
    </div>
    <img src="div2.png" class="divid" />
    <div>
        Select MonthEnd Period : <asp:DropDownList ID="cbperiod" runat="server" Width="30em" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Cust Cd">
                    <ItemTemplate>
                        <%# Eval("cust_cd") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cust Name">
                    <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Addr">
                    <ItemTemplate><%# Eval("addr") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prev Balance">
                    <ItemTemplate><%# Eval("prevbal_amt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Balance">
                    <ItemTemplate>
                        <%# Eval("inv_amt") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment">
                    <ItemTemplate><%# Eval("coll_amt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Return">
                    <ItemTemplate><%# Eval("return_amt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CN">
                    <ItemTemplate><%# Eval("cn_amt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DN">
                    <ItemTemplate><%# Eval("dn_amt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ENd Balance">
                    <ItemTemplate><%# Eval("balance_amt") %></ItemTemplate>
                </asp:TemplateField>
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
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
</asp:Content>


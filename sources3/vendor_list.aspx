<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="vendor_list.aspx.cs" Inherits="vendor_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>Vendor List</strong>
    <asp:GridView ID="grd" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="Code">
                <ItemTemplate>
                    <asp:Label ID="lbvendorcode" runat="server" Text='<%# Eval("vendor_cd") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Vendor Name">
                <ItemTemplate><%# Eval("vendor_cd") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address">
                <ItemTemplate><%# Eval("vendor_nm") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City">
                <ItemTemplate><%# Eval("city_nm") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Post Code">
                <ItemTemplate><%# Eval("post_code") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact">
                <ItemTemplate><%# Eval("contact") %></ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />        
    </asp:GridView>
    <div>
        <div class="navi">
            <asp:Button ID="btadd" runat="server" Text="NEW" OnClick="btadd_Click" CssClass="button2 add" /></div>
        </div>
        
</asp:Content>


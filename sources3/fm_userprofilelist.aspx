<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_userprofilelist.aspx.cs" Inherits="fm_userprofilelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>List User</h3>
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" style="font-size: small" Width="100%" OnRowEditing="grd_RowEditing">
        <Columns>
            <asp:TemplateField HeaderText="USER ID">
                <ItemTemplate><%# Eval("usr_id") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FULL NAME">
                <ItemTemplate><%# Eval("fullname") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EMAIL">
                <ItemTemplate><%# Eval("email") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MOBILE NO">
                <ItemTemplate>
                    <%# Eval("mobile_no") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField HeaderText="ACTION" ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>


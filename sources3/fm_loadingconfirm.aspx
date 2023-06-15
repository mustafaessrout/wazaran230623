<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_loadingconfirm.aspx.cs" Inherits="fm_loadingconfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
       Confirmation Print Loading
    </div>
    <img src="div2.png" class="divid" />
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Item Cd">
                <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Nme">
                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Arabic">
                <ItemTemplate><%# Eval("item_arabic") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Size">
                <ItemTemplate><%# Eval("size") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Branded">
                <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QTY Order"></asp:TemplateField>
            <asp:TemplateField HeaderText="QTY Ship"></asp:TemplateField>
            <asp:TemplateField HeaderText="AVL Stock"></asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>


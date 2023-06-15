<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstitemlist.aspx.cs" Inherits="fm_mstitemlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Master Item List</h3>
     <hr style="width:100%;border:dotted" />
    <div style="width:100%;text-align:left;padding:5px 5px 5px 5px">Search : <asp:TextBox ID="txsearch" runat="server" Width="214px"></asp:TextBox><asp:Button ID="btsearch" runat="server" Text="Search" OnClick="btsearch_Click" /></div>
    <div>

        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" style="font-size: small" AllowPaging="True" GridLines="Horizontal" BorderStyle="None" OnRowEditing="grd_RowEditing" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15">
            <Columns>
                <asp:TemplateField HeaderText="ITEM CODE">
                    <ItemTemplate>
                        <asp:Label ID="lbitemcode" runat="server" Text=' <%# Eval("item_cd") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="ITEM NAME"><ItemTemplate><%# Eval("item_nm") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="ARABIC"><ItemTemplate><%# Eval("item_arabic") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="ITEM TYPE"></asp:TemplateField>
                <asp:TemplateField HeaderText="ITEM CODE VENDOR"></asp:TemplateField>
                <asp:TemplateField HeaderText="VENDOR CODE"></asp:TemplateField>
                <asp:CommandField HeaderText="ACTION" ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    <div style="text-align: center;padding-top:10px">
        <asp:Button ID="btadd" runat="server" Text="NEW ITEM" OnClick="btadd_Click" />
     <asp:Button ID="btprint" runat="server" Text="PRINT" /></div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_polist.aspx.cs" Inherits="fm_polist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>PO LIST</h3>
    <asp:GridView ID="grdpo" runat="server" AutoGenerateColumns="False"  Width="100%" OnSelectedIndexChanging="grdpo_SelectedIndexChanging" GridLines="Horizontal">
        <Columns>
            <asp:TemplateField HeaderText="PO Number"><ItemTemplate>
                <asp:Label ID="lbpono" runat="server" Text='<%# Eval("po_no") %>'></asp:Label></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Date"><ItemTemplate><%# Eval("po_dt","{0:dd-MMM-yyyy}") %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Sales Point"></asp:TemplateField>
            <asp:TemplateField HeaderText="Remark">
                <ItemTemplate><%# Eval("remark") %></ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <div>&nbsp;</div>
    <div>
        <asp:UpdatePanel ID="uppnl" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="Horizontal">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate><%# Eval("unit_price") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="grdpo" EventName="SelectedIndexChanging"/></Triggers>
        </asp:UpdatePanel>
    </div>
  
    <div style="text-align: center;padding:5px 5px 5px 5px">
        <asp:Button ID="btadd" runat="server" Text="NEW PO" OnClick="btadd_Click" /></div>
</asp:Content>


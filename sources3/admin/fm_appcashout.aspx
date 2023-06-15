<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_appcashout.aspx.cs" Inherits="admin_fm_appcashout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
       <h3>Approval Cashout</h3>
    </div>
    <div class="col-md-12">
        <asp:GridView ID="grd" runat="server" CellPadding="0" CssClass="myclass" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Cashout No">
                    <ItemTemplate><%# Eval("casregout_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name"></asp:TemplateField>
                <asp:TemplateField HeaderText="Approval"></asp:TemplateField>
                <asp:TemplateField HeaderText="Reason"></asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>


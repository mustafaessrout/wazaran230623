<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_apporder.aspx.cs" Inherits="fm_apporder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <h3>Approval Take Order</h3>
        <div class="col-md-8">
        <asp:GridView ID="grd" runat="server" CssClass="form-control-static mygrid" AutoGenerateColumns="False" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="TO No">
                    <ItemTemplate>
                        <asp:Label ID="lbsocd" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%# Eval("so_dt","{0:d/M/yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cust">
                    <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salesman">
                    <ItemTemplate><%# Eval("salesman_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate><%# Eval("app_sta_nm") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="cbstatus" runat="server"></asp:DropDownList></EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>
      </div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_appcashier.aspx.cs" Inherits="admin_fm_appcashier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <h3>Cashier Approval</h3>
        <div class="col-md-12">
            <asp:GridView ID="grd" runat="server" CssClass="mygrid" AutoGenerateColumns="False" Width="100%" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="Close No">
                       <ItemTemplate>
                           <asp:Label ID="lbchclosingno" runat="server" Text='<%# Eval("chclosingno") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate><%# Eval("chclosing_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amt">
                        <ItemTemplate><%# Eval("amount") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="500">
                        <ItemTemplate><%# Eval("amt500") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="100">
                         <ItemTemplate><%# Eval("amt100") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="50">
                         <ItemTemplate><%# Eval("amt50") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="20">
                         <ItemTemplate><%# Eval("amt20") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="10">
                         <ItemTemplate><%# Eval("amt10") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="5">
                         <ItemTemplate><%# Eval("amt5") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="1">
                         <ItemTemplate><%# Eval("amt1") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="0.5">
                         <ItemTemplate><%# Eval("amt05") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="0.25">
                         <ItemTemplate><%# Eval("amt025") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="0.1">
                         <ItemTemplate><%# Eval("amt01") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate><%# Eval("acknowledge") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cback" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>


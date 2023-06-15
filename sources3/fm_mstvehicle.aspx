<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstvehicle.aspx.cs" Inherits="fm_mstvehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>Vehicle Master</strong>
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" BorderStyle="None" GridLines="Horizontal">
        <Columns>
            <asp:TemplateField HeaderText="Code">
                <ItemTemplate><%# Eval("vhc_cd") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate><%# Eval("vhc_nm") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Plate Number">
                <ItemTemplate></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Salespoint">
                <ItemTemplate></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PIC"></asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>


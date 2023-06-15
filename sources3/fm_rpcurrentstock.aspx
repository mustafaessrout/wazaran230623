<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rpcurrentstock.aspx.cs" Inherits="fm_rpcurrentstock" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Print Current Stock</div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        Date Stock to be Checked : <asp:TextBox ID="dtbalance" runat="server" Width="10em"></asp:TextBox>
        <asp:CalendarExtender ID="dtbalance_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtbalance">
        </asp:CalendarExtender>
    </div>
    <div class="navi">
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptvanstock.aspx.cs" Inherits="fm_rptvanstock" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Van Stock
    </div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        Current Stock : <asp:TextBox ID="dtstock" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="dtstock_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstock">
        </asp:CalendarExtender>
    </div>
    <div class="navi">
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
</asp:Content>


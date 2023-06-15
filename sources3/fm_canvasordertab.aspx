<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_canvasordertab.aspx.cs" Inherits="fm_canvasordertab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        List Of Tablet Transaction By Salesman
    </div>
    <img src="div2.png" />
    <div>
        Salesman : <asp:DropDownList ID="cbsalesman" runat="server" Width="20em"></asp:DropDownList>
    </div>
</asp:Content>


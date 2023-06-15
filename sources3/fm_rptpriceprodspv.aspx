<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptpriceprodspv.aspx.cs" Inherits="fm_rptpriceprodspv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Price List Report for Product Supervisor
    </div>
    <img src="div2.png" class="divid" />
    <div>
        Group Product : <asp:DropDownList ID="cbprod" runat="server"></asp:DropDownList>
    </div>
    <div class="divgrid">

    </div>
</asp:Content>


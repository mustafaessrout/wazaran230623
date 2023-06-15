<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstemployee3.aspx.cs" Inherits="fm_mstemployee3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="admin/js/bootstrap.min.js"></script>
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container">
    <div class="form-horizontal">
        <h3>Personal Data</h3>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Emp ID</label>
            <div class="col-md-2">
                <asp:TextBox ID="txempcode" runat="server" CssClass="form-control input-sm"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
</asp:Content>


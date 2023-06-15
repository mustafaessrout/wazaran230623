<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_coamapping.aspx.cs" Inherits="fm_coamapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="admin/js/bootstrap.min.js"></script>
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <h4>COA Mapping</h4>
        <div class="h-divider"></div>
        <div class="form-horizontal">
            <div class="form-group">
                <label class="control-label col-md-1 input-sm">Credit</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="cbcredit" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1 input-sm">Debit</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="cbdebit" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>
            </div>
        </div>
    
    </div>
</asp:Content>


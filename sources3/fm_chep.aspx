<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_chep.aspx.cs" Inherits="fm_chep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h3>Chep File Generation</h3>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-md-1">Click This</label>
                    <div class="form-control col-md-2">
                        <asp:Button ID="btgen" runat="server" Text="Generate Now" CssClass="form-control" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


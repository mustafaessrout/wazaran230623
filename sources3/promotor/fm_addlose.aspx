<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_addlose.aspx.cs" Inherits="promotor_fm_addlose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Adjustment Stock</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Adj No</label>
                <div class="col-md-2">
                    <asp:Label ID="lbaddjustno" runat="server" Text="" CssClass="form-control"></asp:Label>
                </div>
                <label class="control-label col-md-1">Date</label>
                <div class="col-md-2">

                </div>
            </div>
        </div>
    </div>
</asp:Content>


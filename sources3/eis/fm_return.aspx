<%@ Page Title="" Language="C#" MasterPageFile="~/eis/eis.master" AutoEventWireup="true" CodeFile="fm_return.aspx.cs" Inherits="eis_fm_return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Customer Sales Return</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Branches</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
    </div>
</asp:Content>


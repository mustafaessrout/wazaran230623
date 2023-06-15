<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockmovement.aspx.cs" Inherits="fm_stockmovement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="alert alert-info text-body">Stock Movement</div>
    <div class="container block-shadow-info">
        <div class="row margin-bottom margin-top">
            <label class="col-sm-1 control-label input-sm">Start Date</label>
            <div class="col-sm-2">
                <asp:DropDownList ID="cbperiod" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
        </div>
    </div>
</asp:Content>


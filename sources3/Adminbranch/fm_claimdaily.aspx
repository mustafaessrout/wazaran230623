<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_claimdaily.aspx.cs" Inherits="Adminbranch_fm_claimdaily" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function InvSelected(sender, e) {
            $get('<%=hdinv.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdinv" runat="server" />
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Daily Confirmation Document Upload</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Branch</label>
            <div class="col-md-3">
                <asp:Label ID="lbbranch" runat="server" CssClass="form-control"></asp:Label>
            </div>
            <label class="control-label col-md-1">Invoice No</label>
            <div class="col-md-3">
                <asp:TextBox ID="txInvoice" runat="server" CssClass="form-control"></asp:TextBox> 
                <asp:AutoCompleteExtender OnClientItemSelected="InvSelected" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txInvoice" UseContextKey="True" MinimumPrefixLength="1" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10">
                </asp:AutoCompleteExtender>               
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Order Invoice</label>
            <div class="col-md-3">
                <asp:FileUpload ID="uplo" runat="server" />
            </div>
            <label class="control-label col-md-1">Free Invoice</label>
            <div class="col-md-3">
                <asp:FileUpload ID="uplf" runat="server"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
                <asp:Button ID="btsave" runat="server" Text="UPLOAD" CssClass="btn btn-primary" OnClick="btsave_Click" />
            </div>
        </div>
    </div>
</asp:Content>


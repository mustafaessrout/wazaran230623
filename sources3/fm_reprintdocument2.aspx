<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reprintdocument2.aspx.cs" Inherits="fm_reprintdocument2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="alert alert-info text-bold">Reprint Document Ver 2.0</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Customer</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txcustomersearch" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomersearch_AutoCompleteExtender" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" EnableCaching="false" FirstRowSelected="false" UseContextKey="true" ServiceMethod="GetCustomerList" OnClientItemSelected="CustSelected" runat="server" TargetControlID="txcustomersearch">
                </asp:AutoCompleteExtender>
                
            </div>
        </div>
    </div>
 <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


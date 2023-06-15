<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptrpstarget.aspx.cs" Inherits="fm_rptrpstarget" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold"></div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="control-label col-sm-1">RPS Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:CalendarExtender Format="d/M/yyyy" ID="dtrps_CalendarExtender" runat="server" TargetControlID="dtrps">
                </asp:CalendarExtender>
                <asp:TextBox ID="dtrps" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:LinkButton ID="btprint" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_navisionfeed_cashout.aspx.cs" Inherits="fm_navisionfeed_cashout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    <style>
        th {
            position: sticky;
            top: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Navision Feed Cashout</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="col-sm-1 input-sm control-label">Date Feed</label>
            <div class="col-sm-2 drop-down-date">
                <asp:CalendarExtender Format="d/M/yyyy" ID="dtfeed_CalendarExtender" runat="server" TargetControlID="dtfeed">
                </asp:CalendarExtender>
                <asp:TextBox ID="dtfeed" CssClass="form-control input-sm input-sm" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:LinkButton ID="btsearch" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 overflow-y" style="max-height: 360px;">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server"></asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btfeed" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btfeed_Click">Feed</asp:LinkButton>
            </div>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


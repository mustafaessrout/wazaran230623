<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_synctab.aspx.cs" Inherits="fm_synctab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="admin/css/bootstrap.min.css" rel="stylesheet" />--%>
    <script src="admin/js/bootstrap.min.js"></script>
    <script src="css/jquery-1.9.1.js"></script>
    <script>
        function showmsgx()
        {
            //$("pnlmsg").css("display", "normal");
            $("#pnlmsg").show();
       
        }
        function hidemsg() {
            //$("pnlmsg").css("display", "normal");
            $("#pnlmsg").hide();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Tablet Synchronization</div>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row">
            <div class="margin-bottom navi">
                <asp:Button ID="btsync" runat="server" Text="Sync Now" CssClass="btn btn-primary" OnClientClick="javascript:showmsgx();" OnClick="btsync_Click" />
            </div>
        </div>
    </div>
   
    <div class="divmsg loading-cont" id="pnlmsg" style="display:none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


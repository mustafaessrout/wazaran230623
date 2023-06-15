<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_AccCashAdvanceApproval.aspx.cs" Inherits="fm_AccCashAdvanceApproval" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script src="admin/js/bootstrap.min.js"></script>
    <script type = "text/javascript" >

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <style>
        .hidobject{
            display:none;
        }
        </style>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_tranStock.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

<%--        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }--%>
    </script>
<%--    <script>
        function Selecteditem(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>--%>
    
 <script>
     function ShowProgress() {
         $('#pnlmsg').show();
     }

     function HideProgress() {
         $("#pnlmsg").hide();
         return false;
     }
     $(document).ready(function () {
         $('#pnlmsg').hide();
     });

    </script>
    <style type="text/css">
        .divmsg{
       /*position:static;*/
       top:30%;
       right:50%;
       left:50%;
       width: 50px;
        height: 45px;
       position:fixed;
       /*background-color:greenyellow;*/
       overflow-y:auto;
  }
        .divhid {
            display:none;
        }

        .divnormal {
            display:normal;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="divheader">Cash Advance Approval</div>

    <div>
        <asp:UpdatePanel ID="UpdatePanel13" runat="server" >
            <ContentTemplate>
                <asp:Label ID="lblDocNo" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <%--<div class="container-fluid top-devider">--%>
    <div>
        <div class="navi row margin-bottom margin-top">
            <asp:UpdatePanel ID="UpdatePanel18" runat="server" class="top-devider">
                <ContentTemplate>
                    <asp:Button ID="btapprove" runat="server" CssClass="btn-warning btn btn-save" OnClick="btapprove_Click" Text="Approve"/>
                    <asp:Button ID="btreject" runat="server" CssClass="btn-warning btn btn-save" OnClick="btreject_Click" Text="Reject"/>
                </ContentTemplate>
             </asp:UpdatePanel>
            <br/>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="top-devider">
                <ContentTemplate>
                    <asp:Button ID="btcancel" runat="server" CssClass="btn-warning btn btn-save" OnClick="btcancel_Click" Text="Back to List" OnClientClick="javascript:ShowProgress();"/>
                </ContentTemplate>
             </asp:UpdatePanel>
        </div>
    </div>
  <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
</asp:Content>


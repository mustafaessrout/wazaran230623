<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_AccClosePeriod.aspx.cs" Inherits="fm_accClosePeriod" %>
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
<%--        function openwindow() {
            var oNewWindow = window.open("fm_lookup_tranStock.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }--%>
    </script>
    <script>
        function Selecteditem(sender, e) {
            //$get('<//%=//hditem.ClientID%//>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    
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
        /**/
        .previous {
            background-color: #f1f1f1;
            color: black;
        }

        .next {
            background-color: #4CAF50;
            color: white;
        }

        .round {
            border-radius: 50%;
        }

    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Accounting Close Period</div>

    <div class="h-divider"></div>

    <br/>
    <br/>
    
    
    <div class="panel panel-default" style="background-color:lightgrey">        
        <div class="panel-body">
            <div class="col-md-20">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <label class="col-sm-1 control-label">Current Period :</label>
                        <asp:label id="lblPeriod" class="control-label" style="font-size:xx-large" runat="server"></asp:label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <br/>
            <br/>
            <br/>
            <br/>
    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate>
                    <span class="previous">&laquo;</span>
                    <%--<asp:Button ID="Button1" runat="server" CssClass="btn-basic btn btn-save previous round" OnClick="btReopenPeriod_Click" Text="Reopen Period" OnClientClick="javascript:ShowProgress();"/>--%>
                    <asp:Button ID="btReopenPeriod" runat="server" CssClass="btn-basic btn btn-save previous round" OnClick="btReopenPeriod_Click" Text="Reopen Period" />

                    <asp:Button ID="btClosePeriod" runat="server" CssClass="btn-success btn btn-save next round" OnClick="btClosePeriod_Click" Text="Close Period" />
                    <span class="next">&raquo;</span>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>


    <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


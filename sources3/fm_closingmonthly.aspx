<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_closingmonthly.aspx.cs" Inherits="fm_closingmonthly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>
     <style>
       .showmessage {
           position: fixed;
           top: 50%;
           left: 50%;
           margin-top: -50px;
           margin-left: -50px;
           width: 300px;
           height: 300px;
           background: url(loader.gif) no-repeat center;
           display:normal;
           z-index:3;
           /*-webkit-filter: blur(5px);
          -moz-filter: blur(5px);
          -o-filter: blur(5px);
           filter: blur(5px);*/
       }

        .hidemessage {
           position: absolute;
           top: 50%;
           left: 50%;
           margin-top: -50px;
           margin-left: -50px;
           width: 150px;
           height: 150px;
           background: url(load.gif) no-repeat center;
           display:none;
       }
       </style>
     <script>
         function vEnableShow() {
             $get('showmessagex').className = "showmessage";
             //  alert('mpret');
         }

         function vDisableShow() {
             $get('showmessagex').className = "hidemessage";
         }
         function confirmInvoice(Val) {
             $get('<%=hdconfirm.ClientID%>').value = Val;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Closing Monthly Period : 
        <asp:Label ID="lbmonth" runat="server"></asp:Label>
        <asp:HiddenField ID="hdmonth" runat="server" />
        <asp:HiddenField ID="hdyear" runat="server" />
        <asp:HiddenField ID="hdconfirm" runat="server" />
    </div>
    <div class="h-divider"></div>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        </asp:UpdatePanel>
       
    </div>
    <div class="navi margin-bottom">
        <asp:Button ID="btprocess" runat="server" Text="Process" CssClass="btn-primary btn btn-process" OnClick="btprocess_Click" OnClientClick="javascript:vEnableShow();" />
        <asp:LinkButton ID="btpostpone" OnClientClick="x=confirm ('Postpone all Invoice ?');if (x==true) { confirmInvoice('true');} else {confirmInvoice('false'); }" CssClass="btn-primary btn btn-danger" runat="server" OnClick="btpostpone_Click">POSTPONED INVOICE</asp:LinkButton>
    </div>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="showmessagex" class="hidemessage">
            </div> 
        </ContentTemplate>
       
    </asp:UpdatePanel>
</asp:Content>


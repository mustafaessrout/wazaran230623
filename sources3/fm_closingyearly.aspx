<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_closingyearly.aspx.cs" Inherits="fm_closingyearly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Closing Yearly : 
        <asp:Label ID="lbyear" runat="server"></asp:Label>
        <asp:HiddenField ID="hdmonth" runat="server" />
        <asp:HiddenField ID="hdyear" runat="server" />
    </div>
    
    <div class="h-divider"></div>

    <div class="divgrid">
<div>
        <table style="width:100%">

            <tr>
                <td class="checkbox no-margin auto-style1" style="margin-left: 80px">
                    <asp:CheckBox ID="chbackup" runat="server" Enabled="False" Text="Backup Database Before Process" />
                </td>
            </tr>
            <tr>
                <td class="checkbox no-margin auto-style1" style="margin-left: 80px">
                    <asp:CheckBox ID="chreturnPending" runat="server" Enabled="False" Text="Check Return Pending" />
                </td>
            </tr>
   
            <tr>
            <td class="checkbox no-margin" style="margin-left: 80px">
                <asp:CheckBox ID="chTOPending" runat="server" Text="Check TO Pending" Enabled="False" />
            </td>
            </tr>
            <tr>
                <td class="checkbox no-margin" style="margin-left: 80px">
                    <asp:CheckBox ID="chCheckCustomerReceived" runat="server" Enabled="False" Text="Check Check Customer Received" />
                </td>
            </tr>
            <tr>
                <td class="checkbox no-margin" style="margin-left: 80px">
                    <asp:CheckBox ID="Chstock" runat="server" Enabled="False" Text="Check Stock" />
                </td>
            </tr>
            <tr>
                <td class="checkbox no-margin" style="margin-left: 80px">
                    <asp:CheckBox ID="chcqduedate" runat="server" Enabled="False" Text="Check Cheque/Transfer Due Date" />
                </td>
            </tr>
            <tr>
                <td class="checkbox no-margin" style="margin-left: 80px">
                    <asp:CheckBox ID="chclaimpending" runat="server" Enabled="False" Text="Check Claim Pending" />
                </td>
            </tr>
            <tr>
                <td class="checkbox no-margin" style="margin-left: 80px">
                    <asp:CheckBox ID="chnosusp" runat="server" Enabled="False" Text="Check NO Suspense" />
                </td>
            </tr>
            <tr>
                <td style="margin-left: 80px">&nbsp;</td>
            </tr>
            
        </table>
           
        
    </div>
       
    </div>
    <div class="navi">
        <asp:Button ID="btprocess" runat="server" Text="Process" CssClass="btn btn-primary" OnClick="btprocess_Click" OnClientClick="javascript:vEnableShow();" />
    </div>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="showmessagex" class="hidemessage">
            </div> 
        </ContentTemplate>
       
    </asp:UpdatePanel>
</asp:Content>


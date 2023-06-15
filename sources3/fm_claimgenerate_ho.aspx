<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimgenerate_ho.aspx.cs" Inherits="fm_claimgenerate_ho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script>
        function vEnableShow() {
            $get('showmessagex').className = "showmessage";
        }

        function vDisableShow() {
            $get('showmessagex').className = "hidemessage";
        }
    </script>
    <style>
       .showmessage {
           position: fixed;
           top: 50%;
           left: 50%;
           margin-top: -60px;
           margin-left: -60px;
           border-radius:10px;
           width: 125px ;
           height: 125px;
           background: url(loader.gif) fixed center;
           display:normal;
       }

        .hidemessage {
           position: absolute;
           top: 50%;
           left: 50%;
           margin-top: 0px;
           margin-left: 0px;
           width: 150px;
           height: 150px;
           background: url(loader.gif) no-repeat center;
           display:none;
       }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Generate Claim Data HO</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="row">
                <div class="col-md-5">
                    <div class="form-group">
                        <label for="period" class="col-xs-2 col-form-label col-form-label-sm">Period</label>
                        <div class="col-xs-10">
                            <label for="ccnr" class="col-xs-2 col-form-label col-form-label-sm">Month</label>
                            <div class="col-xs-4">
                                <div class="drop-down-sm">
                                    <asp:DropDownList ID="cbMonth" runat="server" CssClass="form-control input-sm">
                                        <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                        <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                        <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                        <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                        <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <label for="ccnr" class="col-xs-2 col-form-label col-form-label-sm">Year</label>
                            <div class="col-xs-4">
                                <div class="drop-down-sm">
                                    <asp:DropDownList ID="cbYear" runat="server"   CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="checkClaim" class="col-xs-7 col-form-label">Data Claim HO Confirm</label>
                        <div class="col-xs-5">
                            <asp:Button ID="btncheck" runat="server" Text="Check" CssClass="btn btn-primary btn-block btn-sm" OnClick="btncheck_Click" OnClientClick="vEnableShow();" />
                        </div>
                    </div>
                </div>
                <div class="col-md-7">
                    <div class="form-group">
                        <ul class="list-group">
                          <li class="list-group-item">Cover Letter (HO) <asp:Label runat="server" ID="lbCoverLetter" Text="OK" CssClass="badge"></asp:Label></li>
                          <li class="list-group-item">Pro-Forma Invoice (HO) <asp:Label runat="server" ID="lbchInvoice" Text="" CssClass="badge"></asp:Label></li>
                          <li class="list-group-item">Pro-Forma Cashout (HO) <asp:Label runat="server" ID="lbchCashout" Text=""  CssClass="badge"></asp:Label></li> 
                          <li class="list-group-item">Credit Memo (HO) <asp:Label runat="server" ID="lbchcontract" Text=""  CssClass="badge"></asp:Label></li>
                        </ul>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="list-group">
                            <a href="fm_claimconfirm_ho.aspx" class="list-group-item" runat="server" id="listTO">
                                <h4 class="list-group-item-heading">Pro-Forma Invoice (HO)</h4>
                                <p class="list-group-item-text"><asp:Label runat="server" ID="lblistTO" Text=""></asp:Label></p>
                            </a>
                            <a href="fm_claimconfirmco_ho.aspx" class="list-group-item" runat="server" id="listCashout">
                                <h4 class="list-group-item-heading">Pro-Forma Cashout (HO)</h4>
                                <p class="list-group-item-text"><asp:Label runat="server" ID="lblistCashout" Text=""></asp:Label></p>
                            </a>
                            <a href="fm_contractKA.aspx" class="list-group-item" runat="server" id="listContract">
                                <h4 class="list-group-item-heading">Credit Memo3 (HO)</h4>
                                <p class="list-group-item-text"><asp:Label runat="server" ID="lblistContract" Text=""></asp:Label></p>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
                </ContentTemplate>       
            </asp:UpdatePanel>
              
        </div>
        <asp:UpdatePanel ID="loader" runat="server">
            <ContentTemplate>
                <div id="showmessagex" class="hidemessage">
                </div> 
            </ContentTemplate>       
        </asp:UpdatePanel>

    </div>

</asp:Content>


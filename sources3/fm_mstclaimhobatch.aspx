<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstclaimhobatch.aspx.cs" Inherits="fm_mstclaimhobatch" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>   

    <script>
        function openwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=800,height=600");
            mywindow.moveTo(400, 200);
        }
        function ClaimSelected(sender, e) {
            $get('<%=hdclaim.ClientID%>').value = e.get_value();
            $get('<%=btclaim.ClientID%>').click();
        }
        function vEnableShow() {
            $get('showmessagex').className = "showmessage";
        }

        function vDisableShow() {
            $get('showmessagex').className = "hidemessage";
        }
    </script>
    <style type="text/css">
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 5px 0;
            border-radius: 2px;
        }
 
        .pagination-ys table > tbody > tr > td {
            display: inline;
        }
 
        .pagination-ys table > tbody > tr > td > a,
        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #dd4814;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }
 
        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;    
            margin-left: -1px;
            z-index: 2;
            color: #aea79f;
            background-color: #f5f5f5;
            border-color: #dddddd;
            cursor: default;
        }
 
        .pagination-ys table > tbody > tr > td:first-child > a,
        .pagination-ys table > tbody > tr > td:first-child > span {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }
 
        .pagination-ys table > tbody > tr > td:last-child > a,
        .pagination-ys table > tbody > tr > td:last-child > span {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }
 
        .pagination-ys table > tbody > tr > td > a:hover,
        .pagination-ys table > tbody > tr > td > span:hover,
        .pagination-ys table > tbody > tr > td > a:focus,
        .pagination-ys table > tbody > tr > td > span:focus {
            color: #97310e;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
    </style>
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

    <asp:HiddenField ID="hdclaim" runat="server" />
    
    <div class="container-fluid">

        <div class="page-header">
            <h3>Batch Data Claim HO</h3>
        </div>

        <div class="form-horizontal">

        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="status" class="col-xs-2 col-form-label col-form-label-sm">Status</label>    
                    <div class="col-xs-4">
                        <asp:DropDownList ID="cbstatus" runat="server" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%" OnClientClick="vEnableShow();"></asp:DropDownList> 
                    </div>
                    <div class="col-xs-6">
                        <asp:CheckBox ID="chall" CssClass="form-check form-check-input" runat="server" OnCheckedChanged="chall_CheckedChanged" AutoPostBack="True" Text="All" OnClientClick="vEnableShow();" /> 
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="branch" class="col-xs-2 col-form-label col-form-label-sm">Branch</label>    
                    <div class="col-xs-4">
                        <asp:DropDownList ID="cbbranch" runat="server" OnSelectedIndexChanged="cbbranch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%" OnClientClick="vEnableShow();"></asp:DropDownList> 
                    </div>
                    <div class="col-xs-6">                        
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="branch" class="col-xs-2 col-form-label col-form-label-sm">Vendor / Principal</label>    
                    <div class="col-xs-4">
                        <asp:DropDownList ID="cbvendor" runat="server" OnSelectedIndexChanged="cbvendor_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%" OnClientClick="vEnableShow();"></asp:DropDownList> 
                    </div>
                    <div class="col-xs-6">                        
                    </div>
                </div>
            </div>
        </div>
            <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="branch" class="col-xs-2 col-form-label col-form-label-sm">Payment Type</label>    
                    <div class="col-xs-4">
                        <asp:DropDownList ID="cbpaytype" runat="server"  AutoPostBack="true" CssClass="form-control-static" Width="100%" OnClientClick="vEnableShow();">
                            <asp:ListItem Value="FG">Freegood</asp:ListItem>
                            <asp:ListItem Value="CH">Cash</asp:ListItem>
                        </asp:DropDownList> 
                    </div>
                    <div class="col-xs-6">                        
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="year" class="col-xs-2 col-form-label col-form-label-sm">By Month</label>    
                    <div class="col-xs-2">
                        <asp:DropDownList ID="cbMonth" runat="server" styleCssClass="form-control-static" Width="100%" OnSelectedIndexChanged="cbmonth_SelectedIndexChanged" AutoPostBack="true" OnClientClick="vEnableShow();">
                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                    </div>
                    <label for="year" class="col-xs-2 col-form-label col-form-label-sm">By Year</label>    
                    <div class="col-xs-2">
                        <asp:DropDownList ID="cbYear" runat="server" styleCssClass="form-control-static" Width="100%" OnSelectedIndexChanged="cbyear_SelectedIndexChanged" AutoPostBack="true" OnClientClick="vEnableShow();">
                            
                        </asp:DropDownList> 
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">   
                    <label for="propNo" class="col-xs-2 col-form-label col-form-label-sm">Claim Ref / Claim / CCNR / Prop No. / Payment No.</label>    
                    <div class="col-xs-7">
                        <asp:TextBox ID="txsearch" runat="server" CssClass="" Width="100%"></asp:TextBox>
                        <%--<asp:AutoCompleteExtender ID="txsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetListClaim" TargetControlID="txsearch" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="ClaimSelected">
                        </asp:AutoCompleteExtender>   --%>                      
                    </div>
                    <div class="col-xs-3"> 
                        <asp:LinkButton ID="btnSearch" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btnSearch_Click" OnClientClick="vEnableShow();"> <span aria-hidden="true" class="glyphicon glyphicon-search"></span>Search </asp:LinkButton>        
                    </div>
                </div>
                
                </div>
            </div>
        </div>
        </div>
    <div class="row">
        <div class="col-xs-3"> 
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
               <asp:Button ID="btaprorpay" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btaprorpay_Click" AutoPostBack="true"/>
             </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <div class="table-responsive">
                        <asp:GridView ID="grdclaim" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" OnRowCommand="grdclaim_RowCommand" AllowPaging="true" DataKeyNames="claim_no" OnPageIndexChanging="grdclaim_PageIndexChanging" >  
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:checkbox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect"></asp:checkbox>
                                        </ItemTemplate>
                                        <HeaderTemplate>  
                                            <asp:CheckBox ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkAllSelect" runat="server" /> 
                                        </HeaderTemplate>  
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim No">
                                        <ItemTemplate>
                                            <a href="javascript:openwindow('fm_claiminfoho.aspx?dc=<%# Eval("claim_no") %>&sp=<%# Eval("salespointcd") %>');">
                                            <asp:Label ID="lbclaim" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCNR No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbccnr" runat="server" Text='<%# Eval("ccnr_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbmec" runat="server" Text='<%# Eval("discount_mec").ToString() == "FG" ? "Free Good" : "Cash Amount" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proposal No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprop" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <%# Eval("remarka") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Date">
                                    <ItemTemplate><%# Eval("claim_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# Eval("status") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:ButtonField ButtonType="Link"  Text="<i aria-hidden='true' class='glyphicon glyphicon-edit'></i>Edit" ControlStyle-CssClass="btn default btn-xs purple" CommandName="edit" ></asp:ButtonField>--%>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:GridView ID="grdclaimheader" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" OnRowCommand="grdclaim_RowCommand" DataKeyNames="clh_no" OnPageIndexChanging="grdclaimheader_PageIndexChanging" PageSize="50" >  
                            <Columns>
                                 <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:checkbox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect"></asp:checkbox>
                                        </ItemTemplate>
                                        <HeaderTemplate>  
                                            <asp:CheckBox ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkAllSelect" runat="server" /> 
                                        </HeaderTemplate>  
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Reference No. ">
                                    <ItemTemplate>
                                        <a href="javascript:openwindow('fm_claiminfoh.aspx?dc=<%# Eval("clh_no") %>');">
                                        <asp:Label ID="lbclh" runat="server" Text='<%# Eval("clh_no") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbclno" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCNR No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbccnr" runat="server" Text='<%# Eval("ccnr_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proposal No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprop" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget">
                                        <ItemTemplate>
                                            <asp:Label ID="lbbudget" runat="server" Text='<%# Eval("budget") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Date">
                                        <ItemTemplate><%# Eval("claim_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <%# Eval("remark") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <%# Eval("amount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnAction" runat="server" Text='<%# Eval("status_nm").ToString() == "Received (Prc)" ? "Print" : Eval("status_nm").ToString() == "Approved-Partial (Prc)" ? "Pay" : Eval("status_nm").ToString() == "Approved-Full (Prc)" ? "Pay" : Eval("status_nm").ToString() == "Approved Unpaid" ? "Pay" : Eval("status_nm").ToString() == "Approved Paid" ? "Check"
                                        //(Eval("tot_diff").ToString() == "0.00" ? "Check" : "Pay" ) 
                                        : "-" %>' CssClass="btn default btn-sm" CommandName='<%# Eval("status_nm").ToString() == "Received (Prc)" ? "Print" : Eval("status_nm").ToString() == "Approved-Partial (Prc)" ? "Pay" : Eval("status_nm").ToString() == "Approved-Full (Prc)" ? "Pay" : Eval("status_nm").ToString() == "Approved Unpaid" ? "Pay" : Eval("status_nm").ToString() == "Approved Paid" ? "Check"
                                        //(Eval("tot_diff").ToString() == "0.00" ? "Check" : "Pay" ) 
                                        : "-" %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# Eval("status_nm").ToString() == (<asp:Button ID=\"btnApprove\" runat=\"server\" Text=\"Approve\" CssClass=\"btn default\" CommandName=\"approve\" \" />) ? "Check" : "Print" %>
                                    </ItemTemplate>
                                </asp:TemplateField> --%> 
                                <%--<asp:ButtonField ButtonType="Link" Text='<%# Eval("status_nm") %>' ControlStyle-CssClass="btn default btn-xs purple" CommandName="print" ></asp:ButtonField>--%>
                                <%--<asp:ButtonField ButtonType="Link" Text="<i aria-hidden='true' class='glyphicon glyphicon-print'></i>Print" ControlStyle-CssClass="btn default btn-xs purple" CommandName="print" ></asp:ButtonField>--%>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbstatus" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbbranch" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btclaim" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cbMonth" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbYear" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>  
            </div>
        </div>

        <!-- Modal Claim Branch Sending To Principal-->
        <div class="modal fade" id="sendingClaim" role="dialog">
        <div class="modal-dialog">    
            <!-- Modal content-->
            <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Claim Branch Send To Principal </h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="dtSending" class="col-xs-2 col-form-label col-form-label-sm">Date</label>    
                                <div class="col-xs-8">
                                    <asp:TextBox ID="dtSendClaim" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="attn" class="col-xs-2 col-form-label col-form-label-sm">To</label>    
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                    <asp:TextBox ID="txto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel> 
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="attn" class="col-xs-2 col-form-label col-form-label-sm">Attn</label>    
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                    <asp:ListBox ID="lstAttn" runat="server" SelectionMode="Multiple">
                                    </asp:ListBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel> 
                                    <%--<asp:TextBox ID="txAttn" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>--%>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="cc" class="col-xs-2 col-form-label col-form-label-sm">CC</label>    
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                    <asp:ListBox ID="lstCC" runat="server" SelectionMode="Multiple" CssClass="form-control form-control-static">
                                    </asp:ListBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <%--<asp:TextBox ID="txCC" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>--%>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="remarks" class="col-xs-2 col-form-label col-form-label-sm">Remarks</label>   
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                    <asp:TextBox ID="txRemarks" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSending" runat="server" CssClass="btn btn-primary" OnClick="btSendClaim_Click" Text="Send" />
                <button type="button" id="btnClose" runat="server" class="btn btn-default" data-dismiss="modal" >Close</button>
            </div>
            </div>      
        </div>
        </div>

        <!-- Modal Sales Sending -->
        <div class="modal fade" id="sendingSales" role="dialog">
        <div class="modal-dialog">    
            <!-- Modal content-->
            <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Sales Department Approval (Sending) </h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="dtSending" class="col-xs-2 col-form-label col-form-label-sm">Date</label>    
                                <div class="col-xs-8">
                                    <asp:TextBox ID="dtSalesSnd" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="txRemarkSending" class="col-xs-2 col-form-label col-form-label-sm">Remark</label>   
                                <div class="col-xs-8">
                                    <asp:TextBox ID="txRemarkSalesSnd" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btSaveSalesSnd" runat="server" CssClass="btn btn-primary" OnClick="btSaveSales_Click" Text="Save" />
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
            </div>      
        </div>
        </div>

        <!-- Modal Sales Receiving -->
        <div class="modal fade" id="receivingSales" role="dialog">
        <div class="modal-dialog">    
            <!-- Modal content-->
            <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Sales Department Approval (Receiving) </h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="dtReceiving" class="col-xs-2 col-form-label col-form-label-sm">Date</label>    
                                <div class="col-xs-8">
                                    <asp:TextBox ID="dtSalesRcv" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="txRemarkReceiving" class="col-xs-2 col-form-label col-form-label-sm">Remark</label>
                                <div class="col-xs-8">
                                    <asp:TextBox ID="txRemarkSalesRcv" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btSaveSalesRcv" runat="server" CssClass="btn btn-primary" OnClick="btSaveSales_Click" Text="Save" />
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
            </div>      
        </div>
        </div>
    </div>
     <!-- Bootstrap Modal Approve Dialog -->
        <div class="modal fade" id="myApprove" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group" runat="server" id="issueGroup">
                                            <label class="col-md-3 control-label">Group Issue</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="cbgroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="issueList">
                                            <label class="col-md-3 control-label">Issue List</label>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                <asp:DropDownList ID="cbissue" runat="server" AutoPostBack="True" CssClass="form-control">
                                                </asp:DropDownList>
                                                </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cbgroup" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="payment">
                                            <label class="col-md-3 control-label">Payment Type</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="cbPayment" runat="server" AutoPostBack="True" CssClass="form-control">
                                                    <asp:ListItem Value="FG">Freegood</asp:ListItem>
                                                    <asp:ListItem Value="CH">Cash</asp:ListItem>
                                                    <asp:ListItem Value="DN">Debit Note</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                                                         
                                        <div class="form-group" runat="server" id="remarks">
                                            <label class="col-md-3 control-label">Remarks</label>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txappvRemark" runat="server"  CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                     </ContentTemplate>
                                                    </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="uploadfile">
                                            <label class="col-md-3 control-label">Upload File</label>
                                            <div class="col-md-9">
                                                <asp:FileUpload ID="upl" runat="server"></asp:FileUpload>
                                                <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                                                <asp:Label ID="lblocfile" runat="server" CssClass="form-control" Text='Sales Document'></asp:Label></asp:HyperLink>
                                            </div>
                                        </div>   
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btapprove" runat="server"  type="button" Text="Approve" CssClass="btn btn-default" OnClick="btapprove_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                                <asp:HiddenField runat="server" ID="lbstatusCl" />
                                <%--<button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>--%>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    <%--End Modal Approve--%>
     <!-- Bootstrap Modal Pay Dialog -->
        <div class="modal fade" id="myPay" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upPay" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title"><asp:Label ID="lblModalPayTitle" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group" runat="server" id="Div1">
                                            <label class="col-md-3 control-label">Bank List</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="cbbankcq" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <div class="form-group" runat="server" id="Div3">
                                            <label class="col-md-3 control-label">Payment Type</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="cbdoctype" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="CDV">CDV</asp:ListItem>
                                                    <asp:ListItem Value="CH">Cheque</asp:ListItem>
                                                    <asp:ListItem Value="GDN">GDN</asp:ListItem>
                                                    <asp:ListItem Value="PCV">PCV/RV</asp:ListItem>
                                                    <asp:ListItem Value="CN">CN/DN</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="Div6">
                                            <label class="col-md-3 control-label">Payment Proposal</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="cbpaytyp" runat="server" AutoPostBack="True" CssClass="form-control">
                                                    <asp:ListItem Value="FG">Freegood</asp:ListItem>
                                                    <asp:ListItem Value="CH">Cash</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="Div8">
                                            <label class="col-md-3 control-label">otlcd Price</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="cbpriceotlcd" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                            <div class="form-group" runat="server" id="Div7">   
                                                <label class="col-md-3 control-label">Date Payment</label>    
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txdatePayment" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                     <asp:CalendarExtender CssClass="date" ID="txdatePayment_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txdatePayment">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        <div class="form-group" runat="server" id="Div2">
                                            <label class="col-md-3 control-label">Ref No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txRefNoPay" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>                                
                                        <div class="form-group" runat="server" id="Div4">
                                            <label class="col-md-3 control-label">Remarks</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txpayremark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="file" class="col-md-3 col-form-label col-form-label-sm">File</label>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                <div class="form-group" runat="server" id="Div5">
                                                <asp:FileUpload ID="fup" runat="server" />
                                                <p class="help-block"> Upload file GDN, CDV, Cheque </p>
                                                </div>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>                                                        
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btpay" runat="server"  type="button" Text="Pay" CssClass="btn btn-default" OnClick="btpay_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                <%--<button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>--%>
                            </div>
                        </div>
                    </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btpay" />
                        </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    <%--End Modal Pay--%>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>
    <div id="btnMenu" runat="server">
    <div class="row">
      <div class="col-sm-12">
        <div class="text-center">
            <button type="submit" class="btn btn-info btn-sm" runat="server" id="claimSending" data-toggle="modal" onserverclick="send_Click" >
              <span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span> Send Claim to Principal
            </button>
            <%--<button type="submit" class="btn btn-info btn-sm" runat="server" id="salesRcv" data-toggle="modal" >
              <span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span> Sales Rcv/Snd
            </button>--%>
            <%--<button type="submit" class="btn btn-info btn-sm" runat="server" id="vendorApp" onserverclick="btprint_Click" >
              <span class="glyphicon glyphicon-ok" aria-hidden="true"></span> Vendor Approval
            </button>
            <button type="submit" class="btn btn-info btn-sm" runat="server" id="Button1" onserverclick="btprint_Click" >
              <span class="glyphicon glyphicon-book" aria-hidden="true"></span> Claim To Vendor
            </button>--%>
        </div>
      </div>
    </div>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged" />
    </Triggers>
    </asp:UpdatePanel> 
    <br \ />
    <div class="row">
      <div class="col-sm-12">
        <div class="text-center">
            <button style="display:none" type="submit" class="btn btn-default btn-sm" runat="server" id="btnew" onserverclick="btnew_Click" >
              <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> New</button>
            <button style="display:none" type="submit" class="btn btn-default btn-sm" runat="server" id="btprint" onserverclick="btprint_Click" >
              <span class="glyphicon glyphicon-print" aria-hidden="true"></span> Print
            </button>
            <div id="button" style="display: none">
                <asp:Button ID="btclaim" runat="server" Text="Button" OnClick="btclaim_Click" />
            </div> 
        </div>
      </div>
    </div>
    <asp:UpdatePanel ID="loader" runat="server">
            <ContentTemplate>
                <div id="showmessagex" class="hidemessage">
                </div> 
            </ContentTemplate>       
        </asp:UpdatePanel>
</asp:Content>


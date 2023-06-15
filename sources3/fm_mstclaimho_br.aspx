<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstclaimho_br.aspx.cs" Inherits="fm_mstclaimho_br" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="hdclaim" runat="server" />
    
    <div class="container-fluid">

        <div class="page-header">
            <h3>Data Claim BR</h3>
        </div>

        <div class="form-horizontal">

        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="status" class="col-xs-2 col-form-label col-form-label-sm">Status</label>    
                    <div class="col-xs-4">
                        <asp:DropDownList ID="cbstatus" runat="server" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
                    </div>
                    <div class="col-xs-6">
                        <asp:CheckBox ID="chall" CssClass="form-check form-check-input" runat="server" OnCheckedChanged="chall_CheckedChanged" AutoPostBack="True" Text="All" /> 
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="branch" class="col-xs-2 col-form-label col-form-label-sm">Branch</label>    
                    <div class="col-xs-4">
                        <asp:DropDownList ID="cbbranch" runat="server" OnSelectedIndexChanged="cbbranch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
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
                        <asp:DropDownList ID="cbvendor" runat="server" OnSelectedIndexChanged="cbvendor_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
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
                        <asp:DropDownList ID="cbMonth" runat="server" styleCssClass="form-control-static" Width="100%" OnSelectedIndexChanged="cbmonth_SelectedIndexChanged" AutoPostBack="true">
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
                        <asp:DropDownList ID="cbYear" runat="server" styleCssClass="form-control-static" Width="100%" OnSelectedIndexChanged="cbyear_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                            <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                            <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                            <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                            <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                            <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                            <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                        </asp:DropDownList> 
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">   
                    <label for="propNo" class="col-xs-2 col-form-label col-form-label-sm">Prop No / Claim No / CCNR No</label>
                    <div class="col-xs-5">
                        <asp:TextBox ID="txsearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        <%--<asp:AutoCompleteExtender ID="txsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetListClaim" TargetControlID="txsearch" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="ClaimSelected">
                        </asp:AutoCompleteExtender>  --%>                       
                    </div>
                    <div class="col-xs-5"> 
                        <asp:LinkButton ID="btnSearch" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btnSearch_Click"> <span aria-hidden="true" class="glyphicon glyphicon-search"></span>Search </asp:LinkButton>        
                    </div>
                </div>
            </div>
        </div>

        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <div class="table-responsive">
                        <asp:GridView ID="grdclaim" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" OnRowCommand="grdclaim_RowCommand" AllowPaging="true" DataKeyNames="claim_no" OnPageIndexChanging="grdclaim_PageIndexChanging" PageSize="50" >   
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
                                            <asp:Label ID="lbmec" runat="server" Text='<%# Eval("discount_mec").ToString() == "FG" ? "Free Good" : Eval("discount_mec").ToString() == "CH" ? "Cash Amount" : "Cash Out" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proposal No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprop" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Budget">
                                        <ItemTemplate>
                                            <asp:Label ID="lbbudget" runat="server" Text='<%# Eval("budget") %>'></asp:Label>
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnAction" runat="server" Text='<%# Eval("claim_no").ToString().Substring(0,3) == "CLO" ? "Print" : "Print" %>' CommandName='<%# Eval("discount_mec").ToString()== "CSH" ? "Print CashOut" :  "Print" %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:ButtonField ButtonType="Link"  Text="<i aria-hidden='true' class='glyphicon glyphicon-edit'></i>Edit" ControlStyle-CssClass="btn default btn-xs purple" CommandName="edit" ></asp:ButtonField>--%>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:GridView ID="grdclaimheader" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" OnRowCommand="grdclaim_RowCommand" AllowPaging="true" DataKeyNames="clh_no" OnPageIndexChanging="grdclaimheader_PageIndexChanging" >  
                            <Columns>
                                <asp:TemplateField HeaderText="Claim Reference No. ">
                                    <ItemTemplate>
                                        <a href="javascript:openwindow('fm_claiminfoh.aspx?dc=<%# Eval("clh_no") %>');">
                                        <asp:Label ID="lbclh" runat="server" Text='<%# Eval("clh_no") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proposal No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprop" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# Eval("status_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnAction" runat="server" Text='<%# Eval("status_nm").ToString() == "Received (Prc)" ? "Print" : Eval("status_nm").ToString() == "Approved-Partial (Prc)" ? "Pay" : Eval("status_nm").ToString() == "Approved-Full (Prc)" ? "Pay" : Eval("status_nm").ToString() == "Approved Unpaid" ? "Check" : Eval("status_nm").ToString() == "Approved Paid" ? "Check" : "-" %>' CssClass="btn default btn-sm" CommandName='<%# Eval("status_nm").ToString() == "Received (Prc)" ? "Print" : Eval("status_nm").ToString() == "Approved-Partial (Prc)" ? "Pay" : Eval("status_nm").ToString() == "Approved-Full (Prc)" ? "Pay" : Eval("status_nm").ToString() == "Approved Unpaid" ? "Check" : Eval("status_nm").ToString() == "Approved Paid" ? "Check" : "-" %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                <asp:Button ID="btnSending" runat="server" CssClass="btn btn-primary" OnClick="btSendClaim_Click" Text="Send" />
                </ContentTemplate>
                </asp:UpdatePanel>
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
              <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> New
            </button>
            <button style="display:none" type="submit" class="btn btn-default btn-sm" runat="server" id="btprint" onserverclick="btprint_Click" >
              <span class="glyphicon glyphicon-print" aria-hidden="true"></span> Print
            </button>
            <div id="button" style="display: none">
                <asp:Button ID="btclaim" runat="server" Text="Button" OnClick="btclaim_Click" />
            </div> 
        </div>
      </div>
    </div>
    
</asp:Content>


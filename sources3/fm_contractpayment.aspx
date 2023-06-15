<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_contractpayment.aspx.cs" EnableEventValidation="true" Inherits="fm_contractpayment" %> 

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script>
        function RefreshData(status) {
            $get('<%=lhStatus.ClientID%>').value = status;
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>

    <script>
        function dataPopUp() {
            var dd = 0;
            $('.row-pop-up').each(function () {
                $(this).find('.data-table-popup').parent('td').attr("data-toggle", "modal");
                $(this).find('.data-table-popup').parent('td').attr("data-target", "#modalAjax");
            })

            $('.row-pop-up > td').click(function () {
                var content = undefined;
                var title = undefined;
                content = [];
                title = [];
                var ht_cont = "<div class='brd'>";

                $(this).parents('table').children('thead').find('th').each(function () {
                    title.push($(this).attr('aria-label'));
                })

                $(this).parent().find('td').children('div,span').each(function () {
                    if ($(this).attr('true-data') == undefined) {
                        content.push($(this).text());
                    } else {
                        content.push($(this).attr('true-data'));
                    }
                });

                console.log(title);

                for (var i = 0; i < title.length; i++) {
                    if (i == 8) {
                        title[i] = title[i + 1];
                        title.splice([i + 1]);
                    }
                    console.log(title[i] + "   " + content[i]);
                    ht_cont += "<div class='ttl'>" + title[i] + "</div><div class='cnt'>" + content[i] + "</div>";
                }

                ht_cont += "</div>"

                $('.data-popup-modal').each(function () {
                    $(this).find('.modal-body').html(ht_cont);
                })
            })
        }
    </script>
    <style>
        .s150{
            width: 250px;
        }
        .ttl{
            background: #5D7B9D;
            padding:8px 5px;
            color: #fff;
        }
        .cnt{
            padding:5px;
            min-height:25px;
        }
        .brd{
            border: 1px solid rgba(0,0,0,.5);
            border-radius:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="lhStatus" runat="server" />
    <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" Style="display: none" />
    <div class="divheader">
        Payment for Business Agreement <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red"></asp:Label>
    </div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="form-horizontal">

            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label col-form-label-sm">Status</label>    
                        <div class="col-sm-4 drop-down">
                            <asp:DropDownList ID="cbstatus" runat="server" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm" Width="100%">
                                <asp:ListItem Value="N">New</asp:ListItem>
                                <asp:ListItem Value="U">Not Yet Approve</asp:ListItem>
                                <asp:ListItem Value="A">Approved</asp:ListItem>
                                <asp:ListItem Value="DR">Driver Receipt</asp:ListItem>
                                <asp:ListItem Value="CR">Customer Receipt</asp:ListItem>
                                <asp:ListItem Value="C">Completed</asp:ListItem>
                                <asp:ListItem Value="R">Returned</asp:ListItem>
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label  class="col-sm-2 col-form-label col-form-label-sm">Proposal</label>    
                        <div class="col-sm-4">
                            <asp:TextBox ID="txsearch" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                        </div>
                        <div class="col-sm-3"> 
                            <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-sm btn-primary"    OnClick="btnSearch_Click"> <span aria-hidden="true" class="fa fa-search" style="padding-right:10px;"></span>Search </asp:LinkButton>        
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 margin-bottom" >
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <div class="table-page-fixer">
                            <div class="table-responsive overflow-y" style="max-height:320px;width:100%;">
                            <asp:GridView ID="grdpayment" runat="server" CssClass="table table-striped table-page-fix table-bordered table-hover table-fix" data-table-page="#copytable" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdpayment_RowDataBound" OnRowCommand="grdpayment_RowCommand" OnPageIndexChanging="grdpayment_PageIndexChanging"   AllowPaging="True" PageSize="50">
                                <Columns>
                                    <asp:TemplateField HeaderText="Contract No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbagreecode" runat="server" Text='<%# Eval("contract_no") %>' CssClass="data-table-popup"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proposal No">
                                        <ItemTemplate>
                                            <div class="data-table-popup">
                                                <%# Eval("prop_no") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <div class="data-table-popup">
                                                <%# Eval("type_contract") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <div class="data-table-popup">
                                                <%# Eval("customer") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="s150" HeaderText="Item">
                                        <ItemTemplate>
                                            <div class="data-ellapsis data-table-popup" data-ell-length="30" data-del-space="true">
                                                <%# Eval("item") %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="s150" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free Item">
                                        <ItemTemplate>
                                            <div class="data-ellapsis data-popup data-table-popup" data-ell-length="30" data-del-space="true" data-pop-up-title="Free Item" data-pop-up-content="true-data" >
                                                <%# Eval("freeitem") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <%# Eval("start_dt") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <%# Eval("end_dt") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Date">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <%# Eval("due_dt") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <%# String.Format("{0:n} {1}", Eval("amount"), Eval("uom") )%>
                                            </div>        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <a class="example-image-link" href="/images/contract_doc/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>">
                                                    <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                                </a>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>                
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload Agreement" CssClass="btn btn-primary" CommandName="upload" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                    <ItemTemplate>                
                                        <asp:Button ID="btnPay" runat="server" Text="Pay Now" CssClass="btn btn-primary" CommandName="paynow" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                                        <ItemTemplate>          
                                            <asp:Button ID="btnPostpone" runat="server" Text="Postpone" CssClass="btn btn-warning" CommandName="postpone" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>          
                                            <asp:Button ID="btnDriverRcp" runat="server" Text="Driver Receipt" CssClass="btn btn-primary" CommandName="driver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>          
                                            <asp:Button ID="btnCustRcp" runat="server" Text="Customer Receipt" CssClass="btn btn-default" CommandName="customer" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                    <ItemTemplate>                
                                        <asp:Button ID="btnPrint" runat="server" Text="RePrint Inv" CssClass="btn btn-info" CommandName="reprint" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>                
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success" CommandName="approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>                
                                            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>                
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" CommandName="cncel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>                
                                            <asp:Button ID="btnReturn" runat="server" Text="Return" CssClass="btn btn-danger" CommandName="return" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>                
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <div  class="data-table-popup" style="line-height: 13px;">
                                                <%# Eval("remark") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle  CssClass="row-pop-up" />
                                <SelectedRowStyle CssClass="table-edit" />
                            </asp:GridView>
                            </div>
                        </div>
                        <div class="table-copy-page-fix" id="copytable"></div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myPostpone" role="dialog" aria-hidden="true">
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
                                         <div class="form-group">
                                            <label class="col-md-3 control-label">Contract No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txagreecd" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Due Date</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="dtdue" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Postpone Date</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="dtpostpone" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                <asp:CalendarExtender ID="dtpostpone_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtpostpone">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btSave" runat="server" Text="Postpone" CssClass="btn blue" OnClick="btsave_Click" />
                                <%--<button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>--%>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myPaid" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModalPaid" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" id="close" class="close" data-dismiss="modal" aria-hidden="true" runat="server" onserverclick="close_ServerClick">&times;</button>
                                    <h4 class="modal-title"><asp:Label ID="lbModalTitlePaid" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grdschedule" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdschedule_RowDataBound" OnRowCommand="grdschedule_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seq No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>   
                                                        <asp:TemplateField>
                                                            <ItemTemplate>                
                                                                <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btn default" CommandName="print" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>            
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="grdscheduleg" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdscheduleg_RowDataBound" OnRowCommand="grdscheduleg_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seq No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer">
                                                            <ItemTemplate><asp:Label ID="lbcustomer" runat="server" Text='<%# Eval("customer") %>'></asp:Label></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Paid">
                                                            <ItemTemplate>
                                                                <%#  String.Format("{0:n} {1}", Eval("iqty"), Eval("uom")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>   
                                                        <asp:TemplateField>
                                                            <ItemTemplate>                
                                                                <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btn default" CommandName="print" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>            
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                             <div class="form-group">
                                                <label class="col-md-3 control-label">Contract No</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txagreecdPaid" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Sequence No</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txseqnoPaid" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Due Date</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="dtduePaid" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Paid Date</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="dtpay" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="dtpay_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtpay">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Manual No Inv</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txmanual" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="customer">
                                                <label class="col-md-3 control-label">Customer</label>
                                                <div class="col-md-9">
                                                    <asp:DropDownList ID="cbcustomer" runat="server" CssClass="form-control input-sm" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Warehouse</label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="cbwhs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged" CssClass="form-control input-sm" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="col-md-3 control-label">Bin</label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="cbbin" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbbin_SelectedIndexChanged" CssClass="form-control input-sm" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Driver</label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="cbdriver" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbdriver_SelectedIndexChanged" CssClass="form-control input-sm" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="col-md-3 control-label">Vehicle</label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="cbvehicle" runat="server" CssClass="form-control input-sm" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Total Payment</label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txamount" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="lbuomPaid" runat="server" class="control-label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grditem" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seq No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbitempay" runat="server" Text='<%# String.Format("{0}_{1}", Eval("item_cd"), Eval("item_nm")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Current Stock">
                                                            <ItemTemplate><%# Eval("stock") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Input Payment">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txamountpay" Text="0" runat="server" Width="5em"></asp:TextBox></ItemTemplate>
                                                        </asp:TemplateField>       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="btnPaid" runat="server" Text="Add Payment" CssClass="btn blue" OnClick="btnPaid_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myUpload" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title"><asp:Label ID="lbModalTitleUpload" runat="server" Text="Upload Document"></asp:Label></h4>
                            </div>
                            
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                         <div class="form-group">
                                            <label class="col-md-3 control-label">Contract No</label>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="upModalUpload" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                <asp:TextBox ID="txContractUpl" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Document</label>
                                            <div class="col-md-9">
                                                <asp:FileUpload ID="upl" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="modal-footer">
                                <asp:Button ID="btUpload" runat="server" Text="Upload Document" CssClass="btn blue" OnClick="btUpload_Click" UseSubmitBehavior="false" />
                            </div>
                        </div>
                    
            </div>
        </div>

            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myDriver" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModalDriver" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" id="closeDR" class="close" data-dismiss="modal" aria-hidden="true" runat="server" onserverclick="close_ServerClick">&times;</button>
                                    <h4 class="modal-title"><asp:Label ID="lbModalTitleDR" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                             <div class="form-group">
                                                <label class="col-md-3 control-label">Contract No</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txcontractDR" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Receive Date</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="dtDR" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txdateDR_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtDR">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Driver</label>
                                                <div class="col-md-3">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                    <asp:DropDownList ID="cbdriverDR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbdriverDR_SelectedIndexChanged" CssClass="form-control input-sm" Width="100%">
                                                    </asp:DropDownList>
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>  
                                                <label class="col-md-3 control-label">Vehicle</label>
                                                <div class="col-md-3">
                                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                    <ContentTemplate>
                                                    <asp:DropDownList ID="cbvehicleDR" runat="server" CssClass="form-control input-sm" Width="100%">
                                                    </asp:DropDownList>
                                                    </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cbdriverDR" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnDR" runat="server" Text="Driver Receipt" CssClass="btn blue" OnClick="btnDR_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myCustomer" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModalCustomer" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" id="closeCR" class="close" data-dismiss="modal" aria-hidden="true" runat="server" onserverclick="close_ServerClick">&times;</button>
                                    <h4 class="modal-title"><asp:Label ID="lbModalTitleCR" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                             <div class="form-group">
                                                <label class="col-md-3 control-label">Contract No</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txcontractCR" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Receive Date</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="dtCR" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="dtCR_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtCR">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnCR" runat="server" Text="Customer Receipt" CssClass="btn blue" OnClick="btnCR_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myPrint" role="dialog" aria-hidden="true" >
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModalPrint" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title"><asp:Label ID="lbModalTitlePrint" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grdscheduleprint" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdschedule_RowDataBound" OnRowCommand="grdschedule_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seq No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>   
                                                        <asp:TemplateField>
                                                            <ItemTemplate>                
                                                                <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btn default" CommandName="reprint" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>            
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="grdschedulegprint" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdscheduleg_RowDataBound" OnRowCommand="grdscheduleg_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seq No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer">
                                                            <ItemTemplate><asp:Label ID="lbcustomer" runat="server" Text='<%# Eval("customer") %>'></asp:Label></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>   
                                                        <asp:TemplateField>
                                                            <ItemTemplate>                
                                                                <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btn default" CommandName="reprint" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>            
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                             <div class="form-group">
                                                <label class="col-md-3 control-label">Contract No</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txagreeprint" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myReject" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModalReject" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title"><asp:Label ID="lbModalTitleReject" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                         <div class="form-group">
                                            <label class="col-md-3 control-label">Contract No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txagreeReject" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Remarks</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txremarkReject" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn blue" OnClick="btnReject_Click" />
                                <%--<button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>--%>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myReturn" role="dialog" aria-hidden="true" >
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModalReturn" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title"><asp:Label ID="lbModalTitleReturn" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body">                                    
                                    <div class="row">
                                        <div class="col-md-12">
                                             <div class="form-group">
                                                <label class="col-md-3 control-label">Contract No</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txagreeReturn" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grdschedulereturn" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seq No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>             
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="grdschedulegreturn" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seq No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer">
                                                            <ItemTemplate><asp:Label ID="lbcustomer" runat="server" Text='<%# Eval("customer") %>'></asp:Label></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>              
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnReturn" runat="server" Text="Return" CssClass="btn blue" OnClick="btnReturn_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

             <!-- Bootstrap Modal Dialog -->
            <div class="modal fade data-popup-modal" id="modalAjax" role="dialog" aria-hidden="true" >
                <div class="modal-dialog bg-white">
                     <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Payment For Business Agreement Detail</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                  
                </div>
            </div>

        </div>

    </div>

</asp:Content>


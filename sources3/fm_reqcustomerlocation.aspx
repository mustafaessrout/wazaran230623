<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqcustomerlocation.aspx.cs" Inherits="fm_reqcustomerlocation" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script>
        function openwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=800,height=600");
            mywindow.moveTo(400, 200);
        }
        function vEnableShow() {
            $get('showmessagex').className = "showmessage";
        }

        function vDisableShow() {
            $get('showmessagex').className = "hidemessage";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
    
        <div class="page-header">
            <h3>Request Multiple Location Customer</h3>
        </div>

        <div class="form-horizontal">

            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="status" class="col-xs-2 col-form-label col-form-label-sm">Status</label>    
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cbstatus" runat="server" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%" OnClientClick="vEnableShow();">
                                <asp:ListItem Value="0">Not Yet Approve</asp:ListItem>
                                <asp:ListItem Value="1">Approved</asp:ListItem>
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
                        <div class="col-xs-4">    
                            <asp:LinkButton ID="btnSearch" 
                                            runat="server" 
                                            CssClass="btn btn-info btn-sm"    
                                            OnClick="btnSearch_Click" OnClientClick="vEnableShow();"> <span aria-hidden="true" class="glyphicon glyphicon-search"></span>Search </asp:LinkButton>  
                        </div>
                        <div class="col-xs-6">                               
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
                            <asp:GridView ID="grdlocation" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" OnRowCommand="grdlocation_RowCommand" AllowPaging="true" DataKeyNames="customer_cd" OnPageIndexChanging="grdlocation_PageIndexChanging" >  
                                <Columns> 
                                    <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lbbranch" runat="server" Text='<%# Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Code">
                                            <ItemTemplate>
                                                <a href="javascript:openwindow('fm_reqcustomerlocation_lookup.aspx?dc=<%# Eval("cust_cd") %>&sp=<%# Eval("salespointcd") %>');">
                                                <asp:Label ID="lbcustcd" runat="server" Text='<%# Eval("customer_cd") %>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcustnm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Outlet">
                                            <ItemTemplate>
                                                <asp:Label ID="lbotlcd" runat="server" Text='<%# Eval("otlcd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgroup" runat="server" Text='<%# Eval("cust_group") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("is_multiple_loc").ToString() == "0" ? "Not Yet Approve" : "Approved" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbloctype" runat="server" Text='<%# Eval("loc_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnView" runat="server" Text='View' CssClass="btn default btn-sm" CommandName='View' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:Button ID="btnApprove" runat="server" Text='Approve' CssClass="btn primary btn-sm" CommandName='Approve' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:Button ID="btnReject" runat="server" Text='Reject' CssClass="btn danger btn-sm" CommandName='Reject' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbstatus" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbbranch" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </div>  
                </div>
            </div>

        </div>
            
    </div>
</asp:Content>


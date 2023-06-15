<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_po_ho.aspx.cs" Inherits="fm_po_ho" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function openwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=1000,height=600");
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

    <asp:HiddenField ID="txremarkreject" runat="server" />
    <asp:HiddenField ID="lbproposalrej" runat="server" />
    <asp:HiddenField ID="lbdiscrej" runat="server" />
    <asp:Button ID="btrejectremark" runat="server" Text="Reject" Style="display: none"  OnClick="btrejectremark_Click" />

    <div class="divheader">Branch Order Request</div>
    <div class="h-divider"></div>
    <div class="container">

        <div class="row">
            <div class="clearfix ">
                <div class="clearfix">
                    <label for="branch" class="col-md-1 col-sm-2 control-label control-label-sm">Branch</label>    
                    <div class="col-md-4 col-sm-8 drop-down margin-bottom">
                        <asp:DropDownList ID="cbbranch" runat="server" CssClass="form-control input-sm"></asp:DropDownList> 
                    </div>
                </div>

                <div class="form-group clearfix">
                    <label for="status" class="col-md-1 col-sm-2 control-label  control-label-sm">Status</label>    
                    <div class="col-sm-4 drop-down">
                        <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="clearfix">
                    <label for="vendor" class="col-md-1 col-sm-2 control-label control-label-sm">PO No</label>    
                    <div class="col-md-9 col-sm-8 margin-bottom">
                        <asp:TextBox ID="txpo" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 margin-bottom">
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary btn-search" OnClick="btnSearch_Click">
                            <i aria-hidden="true" class="fa fa-search"></i>Search
                        </asp:LinkButton>  
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="form-group">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="margin-top">
                <ContentTemplate>
                <div class="overflow-y" style="max-height:270px;">
                    <asp:GridView ID="grdpo" runat="server" CellPadding="0"  CssClass="table table-fix table-striped table-hover table-page-fix mygrid" data-table-page="#copy-fst" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" OnRowCommand="grdpo_RowCommand" OnPageIndexChanging="grdpo_PageIndexChanging" AllowPaging="True" PageSize="30" >  
                        <Columns>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespoint") %>'></asp:Label>
                                    <asp:HiddenField ID="hdsalespoint" Value='<%#Eval("salespointcd") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbpono" runat="server" Text='<%# Eval("po_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Date"><ItemTemplate><%# Eval("po_dt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivery Date"><ItemTemplate><%# Eval("po_delivery_dt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Destination"><ItemTemplate><%# Eval("to_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot Qty."><ItemTemplate><%# Eval("tot_qty") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot Amount."><ItemTemplate><%# Eval("tot_amt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Status"><ItemTemplate><%# Eval("status") %></ItemTemplate></asp:TemplateField>

                            <asp:ButtonField ButtonType="Link"  Text="<i aria-hidden='true' class='fa fa-print'></i> Print" ControlStyle-CssClass="btn default btn-xs purple" CommandName="print">
                                <ControlStyle CssClass="btn default btn-xs purple" />
                            </asp:ButtonField>  
                            <asp:ButtonField ButtonType="Link" Text="<i aria-hidden='true' class='fa fa-message'></i> Process" ControlStyle-CssClass="btn default btn-xs purple" CommandName="process">
                                <ControlStyle CssClass="btn danger btn-xs purple" />
                            </asp:ButtonField>  
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer"  />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />

                    </asp:GridView>
                </div>
                <div class="table-copy-page-fix" id="copy-fst"></div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </div>  
        </div>

    </div>


</asp:Content>


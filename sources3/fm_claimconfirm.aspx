<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimconfirm.aspx.cs" Inherits="fm_claimconfirm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="v4-alpha/bootstrap.min.js"></script>
    <script src="v4-alpha/docs.min.js"></script>
    <script type="text/javascript">
        function OpenPop(url, newname, settings, inv_no, received_dt,disc_cd) {
            window.open(url + "?inv=" + inv_no + "&rcv=" + received_dt + "&disc=" + disc_cd, newname, settings);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdopinv" runat="server" />
    <asp:HiddenField ID="hdinv" runat="server" />
    <asp:HiddenField ID="disc" runat="server" />
    <div class="divheader">Claim Daily Confirmation</div>
    <div class="h-divider"></div>

     <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                <label for="search" class="col-xs-12 text-lg control-label control-label-sm">Search</label>
                <div class="col-sm-12 well well-sm">
                    <div class="col-md-4 col-sm-3">
                        <asp:TextBox ID="txsearch" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary btn-sm" OnClick="btnsearch_Click" />
                    </div>
                </div> 
            </div>
            </div>
        </div>
        <div class="row" >
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                        <ContentTemplate>
                            <div class="overflow-y" style="max-height:320px; width:100%;">
                                <asp:GridView ID="grdCLAIM" runat="server" CssClass="table table-striped table-page-fix  table-bordered table-fix table-hover" OnPageIndexChanging="grdCLAIM_PageIndexChanging" data-table-page="#copy-fst" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" OnRowDataBound="grdCLAIM_RowDataBound" AllowPaging="True" PageSize="30">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Discount No.">
                                            <ItemTemplate>
                                                <a href="javascript:popupwindow('fm_discountinfo.aspx?dc=<%# Eval("disc_cd") %>');">
                                                    <asp:Label ID="lbdiscount" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No.">
                                            <ItemTemplate>
                                                <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkinv" runat="server" OnClientClick='<%#  String.Format("return OpenPop(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")","fm_invoiceinfo.aspx","newWindow","scrollbars=yes,resizable=yes", Eval("inv_no").ToString().Trim(), Eval("received_dt"),Eval("disc_Cd")) %>'>
                                                            <asp:Label ID="lbinvoice" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                                        </asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkinv" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="received_dt" HeaderText="RECEIVED DATE" DataFormatString="{0:d/M/yyyy}" />
                                        <asp:TemplateField HeaderText="TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="lbType" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="TYPE" HeaderText="TYPE" />--%>
                                        <asp:BoundField DataField="BUDGET" HeaderText="BUDGET" />                                                   <asp:TemplateField HeaderText="EXCLUDE INV">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chexclude" runat="server" value='<%# Eval("inv_no") %>'></asp:CheckBox></a> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SIGNATURE">
                                            <ItemTemplate>
                                                <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="approve" runat="server" value='<%# Eval("disc_Cd") %>' AutoPostBack="true" OnCheckedChanged="chapprove_CheckedChanged"></asp:CheckBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="approve" EventName="CheckedChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STAMP">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chsign" runat="server" value='<%# Eval("disc_Cd") %>'></asp:CheckBox></a> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txremark" runat="server" TextMode="MultiLine"></asp:TextBox></a> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="POSTPONE">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chpostpone" runat="server" value='<%# Eval("disc_Cd") %>'></asp:CheckBox></a> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Upload Invoice Scan">
                                            <ItemTemplate>
                                                <span><strong>Order Invoice</strong></span><asp:FileUpload ID="uplo" runat="server" />
                                                <span><strong><asp:Label runat="server" id="lbFreeInv" Text="Free Invoice" /></strong></span><asp:FileUpload ID="uplf" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                    </Columns>
                                    <EditRowStyle CssClass="table-edit" />
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page" />
                                    <RowStyle  CssClass="row-pop-up" />
                                    <SelectedRowStyle CssClass="table-edit" />
                                </asp:GridView>
                            </div>
                            <div class="table-copy-page-fix" id="copy-fst"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="navi margin-bottom">
                    <asp:Button ID="btconfirm" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btconfirm_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>



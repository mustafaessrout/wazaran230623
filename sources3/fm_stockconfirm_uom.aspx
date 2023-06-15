<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockconfirm_uom.aspx.cs" Inherits="fm_stockconfirm_uom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        Stock Confirmation as 
        <asp:TextBox ID="dtstock" runat="server" AutoPostBack="True" Enabled="False" BorderStyle="None" CssClass="text-red"></asp:TextBox>
        <asp:CalendarExtender ID="dtstock_CalendarExtender" runat="server" TargetControlID="dtstock" Format="d/M/yyyy" CssClass="black">
        </asp:CalendarExtender>
    </div>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row">
            <div class=" center">
                <div class="overflow-y">
                    <asp:GridView ID="grd" runat="server" CssClass="table table-striped table-fix mygrid" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" GridLines="None">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemnm" runat="server" Text='<%# Eval("item_shortname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Bin Cd">
                                <ItemTemplate>
                                    <asp:Label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_cd") %>'></asp:Label>
                                    <asp:HiddenField ID="hdwhs_cd" runat="Server" Value='<%# Eval("whs_cd") %>' />
                                    <asp:HiddenField ID="hdbin_cd" runat="Server" Value='<%# Eval("bin_cd") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="opening">
                                <ItemTemplate>
                                    <asp:Label ID="lbopening_conv" runat="server" Text='<%# Eval("opening_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="lbopening" runat="server" Value='<%# Eval("opening") %>' />
                                    <asp:HiddenField ID="hdopening_ctn" runat="server" Value='<%# Eval("opening_ctn") %>' />
                                    <asp:HiddenField ID="hdopening_pcs" runat="server" Value='<%# Eval("opening_pcs") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PurchaseIn">
                                <ItemTemplate>
                                    <asp:Label ID="lbpo_conv" runat="server" Text='<%# Eval("po_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="lbpo" runat="server" Value='<%# Eval("po") %>' />
                                    <asp:HiddenField ID="hdpo_ctn" runat="server" Value='<%# Eval("po_ctn") %>' />
                                    <asp:HiddenField ID="hdpo_pcs" runat="server" Value='<%# Eval("po_pcs") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BranchIn">
                                <ItemTemplate>
                                    <asp:Label ID="lbbranchin_conv" runat="server" Text='<%# Eval("branchin_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="lbbranchin" runat="server" Value='<%# Eval("branchin") %>' />
                                    <asp:HiddenField ID="hdbranchin_ctn" runat="server" Value='<%# Eval("branchin_ctn") %>' />
                                    <asp:HiddenField ID="hdbranchin_pcs" runat="server" Value='<%# Eval("branchin_pcs") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Return">
                                <ItemTemplate>
                                    <asp:Label ID="lbret" runat="server" Text='<%# Eval("retur") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Sales">
                                <ItemTemplate>
                                    <asp:Label ID="lbsales_conv" runat="server" Text='<%# Eval("sales_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="lbsales" runat="server" Value='<%# Eval("sales") %>' />
                                    <asp:HiddenField ID="hdsales_ctn" runat="server" Value='<%# Eval("sales_ctn") %>' />
                                    <asp:HiddenField ID="hdsales_pcs" runat="server" Value='<%# Eval("sales_pcs") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free">
                                <ItemTemplate>
                                    <asp:Label ID="lbfree_conv" runat="server" Text='<%# Eval("free_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="lbfree" runat="server" Value='<%# Eval("free") %>' />
                                    <asp:HiddenField ID="hdfree_ctn" runat="server" Value='<%# Eval("free_ctn") %>' />
                                    <asp:HiddenField ID="hdfree_pcs" runat="server" Value='<%# Eval("free_pcs") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Return">
                                <ItemTemplate>
                                    <asp:Label ID="lbretur_conv" runat="server" Text='<%# Eval("retur_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="lbretur" runat="server" Value='<%# Eval("retur") %>' />
                                    <asp:HiddenField ID="hdretur_ctn" runat="server" Value='<%# Eval("retur_ctn") %>' />
                                    <asp:HiddenField ID="hdretur_pcs" runat="server" Value='<%# Eval("retur_pcs") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BranchOut">
                                <ItemTemplate>
                                    <asp:Label ID="lbbranchout_conv" runat="server" Text='<%# Eval("branchout_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="lbbranchout" runat="server" Value='<%# Eval("branchout") %>' />
                                    <asp:HiddenField ID="hdbranchout_ctn" runat="server" Value='<%# Eval("branchout_ctn") %>' />
                                    <asp:HiddenField ID="hdbranchout_pcs" runat="server" Value='<%# Eval("branchout_pcs") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing">
                                <ItemTemplate>
                                    <asp:Label ID="lbclosing_conv" runat="server" Text='<%# Eval("closing_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="lbclosing" runat="server" Value='<%# Eval("closing") %>' />
                                    <asp:HiddenField ID="hdclosing_ctn" runat="server" Value='<%# Eval("closing_ctn") %>' />
                                    <asp:HiddenField ID="hdclosing_pcs" runat="server" Value='<%# Eval("closing_pcs") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txqty" runat="server" Style="background-color: #ecec07" CssClass="form-control input-sm"></asp:TextBox>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdqty" runat="server" Value='<%#Eval("qty") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdqty_conv" runat="server" Value='<%#Eval("qty_conv") %>'></asp:HiddenField>
                                    <asp:TextBox ID="txqty_ctn" Text='<%#Eval("qty_ctn") %>' runat="server" TextMode="Number" Width="15%"></asp:TextBox>
                                    <asp:DropDownList runat="server" ID="cbuom_ctn" Width="10%"></asp:DropDownList>
                                    <asp:TextBox ID="txqty_pcs" Text='<%#Eval("qty_pcs") %>' runat="server" TextMode="Number" Width="15%"></asp:TextBox>
                                    <asp:DropDownList runat="server" ID="cbuom_pcs" Width="10%"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle />
                        <SortedAscendingHeaderStyle />
                        <SortedDescendingCellStyle />
                        <SortedDescendingHeaderStyle />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <%--  <div class="row" runat="server" id="vUpload">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="control-label col-md-1">Stock Jared</label>
                    <div class="col-md-6">
                        <asp:FileUpload id="fup" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="row">
            <div class="alert alert-anger text-center">
                <asp:Label ID="lbcap" runat="server" CssClass="text-bold text-red" Text="Hereby state that all stock in this day is CORRECT"></asp:Label>
            </div>
        </div>

        <div class="h-divider"></div>

        <div class="row">
            <div class="navi margin-bottom text-center">
                <asp:Button ID="btpostpone" runat="server" OnClientClick="ShowProgress();" CssClass="btn-danger btn btn-cancel" OnClick="btpostpone_Click" Text="Postpone (you have chance until 5 days)" />
                <asp:Button ID="btconfirm" OnClientClick="ShowProgress();" runat="server" CssClass="btn-primary btn btn-confirm" OnClick="btconfirm_Click" Text="Confirm" />
                <asp:Button ID="btprint" runat="server" Text="Print" OnClientClick="ShowProgress();" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            </div>
        </div>

    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


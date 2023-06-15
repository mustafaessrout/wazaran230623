<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_app_stockadj.aspx.cs" Inherits="fm_app_stockadj" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
    <script>    
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
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
    <asp:HiddenField ID="hdtrfno" runat="server" />
    <asp:HiddenField ID="hdsalespoint" runat="server" />

    <div id="container">
        <h4 class="jajarangenjang">Approval - Stock Adjustment</h4>
        <div class="h-divider"></div>

        <div class="clearfix">
            <div class="overflow-x overflow-y">
                <asp:GridView ID="grdtrf" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" OnPageIndexChanging="grdtrf_PageIndexChanging" data-table-page="#copy-fst" AutoGenerateColumns="False" OnSelectedIndexChanging="grdtrf_SelectedIndexChanging" OnRowCommand="grdtrf_RowCommand" AllowPaging="True" PageSize="50">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Salespoint">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label>
                                -
                                <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sys No.">
                            <ItemTemplate>
                                <asp:Label ID="lbstkadjno" runat="server" Text='<%#Eval("stkadjno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbstkadjdate" runat="server" Text='<%#Eval("stkadjdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No.">
                            <ItemTemplate>
                                <asp:Label ID="lbstkadjmanno" runat="server" Text='<%#Eval("stkadjmanno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Warehouse">
                            <ItemTemplate>
                                <asp:Label ID="lbwhs_cd" runat="server" Text='<%#Eval("whs_cd") %>'></asp:Label>
                                -
                                <asp:Label ID="lbwhs_nm" runat="server" Text='<%#Eval("whs_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnApprove" OnClientClick="ShowProgress();" runat="server" Text="Approve" CssClass="btn btn-default" CommandName="approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnReject" runat="server" Text="Reject" OnClientClick="ShowProgress();" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" SelectText="Detail" />
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <div class="table-copy-page-fix" id="copy-fst"></div>
        </div>

        <div class="h-divider"></div>
        <div style="width: 100%; height: 400px; overflow: scroll">
            <asp:GridView ID="grdtrfdtl" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" ShowFooter="True" OnRowDataBound="grdtrfdtl_RowDataBound">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Item From">
                        <ItemTemplate>
                            <asp:Label ID="lbitemcode_fr" runat="server" Text='<%# Eval("item_cd_fr") %>'></asp:Label>
                            -
                            <asp:Label ID="lbitemnm_fr" runat="server" Text='<%# Eval("item_shortname_fr") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item To">
                        <ItemTemplate>
                            <asp:Label ID="lbitemcode_to" runat="server" Text='<%# Eval("item_cd_to") %>'></asp:Label>
                            -
                            <asp:Label ID="lbitemnm_to" runat="server" Text='<%# Eval("item_shortname_to") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bin">
                        <ItemTemplate><%# Eval("bin_cd_fr") %> -> <%# Eval("bin_cd_to") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uom">
                        <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <div>
                                <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div>
                                <asp:Label ID="lblTotalqty" runat="server" />
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate><%# Eval("reason") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle CssClass="table-edit" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

            </asp:GridView>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_app_stockaddloss.aspx.cs" Inherits="fm_app_stockaddloss" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdtrfno" runat="server" />
    <asp:HiddenField ID="hdsalespoint" runat="server" />

    <div class="alert alert-info text-bold">Approval for Stock Add | Loss  |Destroy</div>
     <div id="container">
        <%--<h4 class="jajarangenjang">Approval - Stock Add | Loss | Destroy</h4>
        <div class="h-divider"></div>--%>

         <div class="clearfix">
            <div class="overflow-x overflow-y">
                <asp:GridView ID="grdtrf" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" OnPageIndexChanging="grdtrf_PageIndexChanging" data-table-page="#copy-fst"  AutoGenerateColumns="False" OnSelectedIndexChanging="grdtrf_SelectedIndexChanging" OnRowCommand="grdtrf_RowCommand" AllowPaging="True" PageSize="50">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Salespoint">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label> - <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sys No.">
                            <ItemTemplate>
                                <asp:Label ID="lbtrnstkno" runat="server" Text='<%#Eval("trnstkno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbtrnstkDate" runat="server" Text='<%#Eval("trnstkDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No.">
                            <ItemTemplate>
                                <asp:Label ID="lbtrnstkmanno" runat="server" Text='<%#Eval("trnstkmanno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Warehouse">
                            <ItemTemplate>
                                <asp:Label ID="lbwhs_cd" runat="server" Text='<%#Eval("whs_cd") %>'></asp:Label> - <asp:Label ID="lbwhs_nm" runat="server" Text='<%#Eval("whs_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bin">
                            <ItemTemplate>
                                <asp:Label ID="lbbin_cd" runat="server" Text='<%#Eval("bin_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                                <asp:Label ID="lbtrnstkremark" runat="server" Text='<%#Eval("trnstkremark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lbinvtyp_nm" runat="server" Text='<%#Eval("invtyp_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document">
                            <ItemTemplate>
                                <div  class="data-table-popup">
                                    <a class="example-image-link" href="/images/stock/<%# Eval("fileAttachment") %>" data-lightbox="example-1<%# Eval("fileAttachment") %>">
                                        <asp:Label ID="lbfileloc" runat="server" Text='Download'></asp:Label>
                                    </a>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>                
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-default" CommandName="approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>          
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" SelectText="Detail" />
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
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
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <%# Eval("item_shortname") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                            <FooterTemplate>
				            <div >
				            <asp:Label ID="lbtotuom" runat="server" Text="CTN" />
				            </div>
			                </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <div >
                                <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
				            <div >
				            <asp:Label ID="lblTotalqty" runat="server" />
				            </div>
			                </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                </asp:GridView>
            </div>


    </div>

</asp:Content>


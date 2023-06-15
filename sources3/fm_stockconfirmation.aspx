<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockconfirmation.aspx.cs" Inherits="fm_stockconfirmation" %>

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

     <div id="container">
        <h4 class="jajarangenjang">Approval - Stock Confirmation</h4>
        <div class="h-divider"></div>

         <div class="clearfix">
            <div class="overflow-x overflow-y">
                <asp:GridView ID="grd" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" OnPageIndexChanging="grd_PageIndexChanging" data-table-page="#copy-fst"  AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging" AllowPaging="True" PageSize="50">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespointnm" Text='<%# Eval("salespoint_nm") %>' runat="server"></asp:Label>
                                <asp:HiddenField ID="hdsalespoint" Value='<%# Eval("salespointcd") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock Date">
                            <ItemTemplate>
                                <asp:Label ID="lbstock_dt" runat="server" Text='<%# Eval("stock_dt","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Confirm By">
                            <ItemTemplate>
                                <%# Eval("emp_desc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document">
                            <ItemTemplate>
                                <div class="data-table-popup">
                                    <a class="example-image-link" href="/images/stock/stock_confirm/<%# Eval("file_doc") %>" data-lightbox="example-1<%# Eval("file_doc") %>">
                                        <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                    </a>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" ShowFooter="True" OnRowDataBound="grddtl_RowDataBound">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# Eval("item_shortname") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size" ItemStyle-Width="10%">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Warehouse" ItemStyle-Width="5%">
                            <ItemTemplate><%# Eval("whs_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bin" ItemStyle-Width="5%">
                            <ItemTemplate><%# Eval("bin_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <div>
                                <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty_conv") %>'></asp:Label>
                                <asp:HiddenField ID="hdqty" runat="server" Value='<%# Eval("qty") %>' />
                                <asp:HiddenField ID="hdqty_ctn" runat="server" Value='<%# Eval("qty_ctn") %>' />
                                <asp:HiddenField ID="hdqty_pcs" runat="server" Value='<%# Eval("qty_pcs") %>' />
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

            <div class="h-divider"></div>
            <div class="navi margin-bottom">
                <asp:Button ID="btapprove" runat="server" Text="Approve Stock Confirm" CssClass="btn-success btn btn-add" OnClick="btapprove_Click"/>
                <asp:Button ID="btcancel" runat="server" Text="Cancel Stock Confirm" CssClass="btn-danger btn btn-delete" OnClick="btcancel_Click"/>
            </div>

    </div>

</asp:Content>


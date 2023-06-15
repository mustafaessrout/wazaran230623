<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_app_internaltransfer.aspx.cs" Inherits="fm_app_internaltransfer" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdtrfno" runat="server" />
    <asp:HiddenField ID="hdsalespoint" runat="server" />

    <div class="alert alert-info text-bold">Approval - Internal Transfer</div>
    <div class="container">
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <div class="overflow-x overflow-y">
                    <asp:GridView ID="grdtrf" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" OnPageIndexChanging="grdtrf_PageIndexChanging" data-table-page="#copy-fst" AutoGenerateColumns="False" OnSelectedIndexChanging="grdtrf_SelectedIndexChanging" AllowPaging="True" PageSize="50">
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
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <asp:Label ID="lb_trf_no" runat="server" Text='<%# Eval("trf_no") %>'></asp:Label>
                                    <asp:HiddenField ID="hdsalespointcd" Value='<%# Eval("salespointcd") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbtrf_dt" runat="server" Text='<%# Eval("trf_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salesman">
                                <ItemTemplate>
                                    <%# Eval("emp_desc") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="WHS CD">
                                <ItemTemplate>
                                    <asp:Label ID="lbwhs_cd" runat="server" Text='<%# Eval("whs_cd_from") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bin CD">
                                <ItemTemplate>
                                    <asp:Label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_from") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VHC CD">
                                <ItemTemplate>
                                    <asp:Label ID="lbvhc_cd" runat="server" Text='<%# Eval("whs_cd_to") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bin CD vhc">
                                <ItemTemplate>
                                    <asp:Label ID="lbbin_cd_vhc" runat="server" Text='<%# Eval("bin_to") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tranfer type">
                                <ItemTemplate>
                                    <asp:Label ID="lbfrf_typ" runat="server" Text='<%# Eval("trf_typ_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual No">
                                <ItemTemplate>
                                    <asp:Label ID="lbmanual_no" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalesman_cd" runat="server" Text='<%# Eval("salesman_cd") %>' Visible="false"></asp:Label>
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
        </div>
        <div class="h-divider"></div>
        <div style="width: 100%; height: 400px; overflow: scroll">
            <asp:GridView ID="grdtrfdtl" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" OnRowCancelingEdit="grdtrfdtl_RowCancelingEdit" OnRowEditing="grdtrfdtl_RowEditing" OnRowUpdating="grdtrfdtl_RowUpdating" OnSelectedIndexChanging="grdtrfdtl_SelectedIndexChanging" ShowFooter="True" OnRowDataBound="grdtrfdtl_RowDataBound">
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
                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <div>
                                <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty_conv") %>'></asp:Label>
                                <asp:HiddenField ID="hdqty" runat="server" Value='<%# Eval("qty") %>' />
                                <asp:HiddenField ID="hdqty_ctn" runat="server" Value='<%# Eval("qty_ctn") %>' />
                                <asp:HiddenField ID="hdqty_pcs" runat="server" Value='<%# Eval("qty_pcs") %>' />
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <div>
                                <asp:Label ID="lblTotalqty" runat="server" />
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="False" />
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
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btapprove" runat="server" Text="Approve Internal Transfer" CssClass="btn-success btn btn-sm" OnClientClick="ShowProgress();" OnClick="btapprove_Click">Approve</asp:LinkButton>
                <asp:LinkButton ID="btcancel" runat="server" Text="Cancel Internal Transfer" CssClass="btn-danger btn btn-sm" OnClientClick="ShowProgress();" OnClick="btcancel_Click">Reject</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


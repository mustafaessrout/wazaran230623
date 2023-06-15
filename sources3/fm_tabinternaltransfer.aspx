<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_tabinternaltransfer.aspx.cs" Inherits="fm_tabinternaltransfer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet" />


    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/index.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>

    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="full">
        <div class="containers bg-white">
            <div class="divheader">
                tablet internal transfer transaction
            </div>
            <div class="h-divider"></div>
            <div class="divgrid clearfix margin-bottom">
                <label class="control-label col-sm-2">Status</label>
                <div class="col-sm-6 drop-down">
                    <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" onchange="ShowProgress();" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    <asp:HiddenField ID="hdtrfno" runat="server" />
                </div>
            </div>
            <div class="clearfix">
                <div class="overflow-x overflow-y" style="max-height: 400px;">
                    <asp:GridView ID="grdtab" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" OnPageIndexChanging="grdtab_PageIndexChanging" data-table-page="#copy-fst" AutoGenerateColumns="False" OnSelectedIndexChanging="grdtab_SelectedIndexChanging" AllowPaging="True" PageSize="50">
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
                            <asp:TemplateField HeaderText="Tab No">
                                <ItemTemplate>
                                    <asp:Label ID="lbtab_trf_no" runat="server" Text='<%# Eval("tab_trf_no") %>'></asp:Label>
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
                                    <asp:Label ID="lbwhs_cd" runat="server" Text='<%# Eval("whs_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bin CD">
                                <ItemTemplate>
                                    <asp:Label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VHC CD">
                                <ItemTemplate>
                                    <asp:Label ID="lbvhc_cd" runat="server" Text='<%# Eval("vhc_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bin CD vhc">
                                <ItemTemplate>
                                    <asp:Label ID="lbbin_cd_vhc" runat="server" Text='<%# Eval("bin_cd_vhc") %>'></asp:Label>
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

            <div class="h-divider"></div>
            <div style="width: 100%; height: 400px; overflow: scroll">
                <asp:GridView ID="grdtabdtl" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" OnRowCancelingEdit="grdtabdtl_RowCancelingEdit" OnRowEditing="grdtabdtl_RowEditing" OnRowUpdating="grdtabdtl_RowUpdating" OnSelectedIndexChanging="grdtabdtl_SelectedIndexChanging" ShowFooter="True" OnRowDataBound="grdtabdtl_RowDataBound">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <%# Eval("item_nm") %>
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
                                <asp:HiddenField ID="hdqty" runat="server" Value='<%# Eval("qty") %>' />
                                <asp:HiddenField ID="hdqty_ctn" runat="server" Value='<%# Eval("qty_ctn") %>' />
                                <asp:HiddenField ID="hdqty_pcs" runat="server" Value='<%# Eval("qty_pcs") %>' />
                                <asp:TextBox ID="txqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <div>
                                    <asp:Label ID="lblTotalqty" runat="server" />
                                </div>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StockAvl" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="lbstockavl" runat="server" Text='<%# Eval("stockqty_conv") %>'></asp:Label>
                                    <asp:HiddenField ID="hdstockqty" runat="server" Value='<%# Eval("stock_qty") %>' />
                                    <asp:HiddenField ID="hdstockqty_ctn" runat="server" Value='<%# Eval("stockqty_ctn") %>' />
                                    <asp:HiddenField ID="hdstockqty_pcs" runat="server" Value='<%# Eval("stockqty_pcs") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div>
                                    <asp:Label ID="lbtotstockavl" runat="server" />
                                </div>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
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
            <div class="h-divider"></div>
            <div class="navi margin-bottom">
                <asp:LinkButton ID="btapply" runat="server" Text="Transfer To Back Office" CssClass="btn-success btn btn-sm" OnClick="btapply_Click" />
                <asp:LinkButton ID="btcancel" runat="server" Text="Cancel Internal Transfer" CssClass="btn-danger btn btn-sm" OnClick="btcancel_Click" />
                <asp:LinkButton ID="btpostphone" runat="server" Text="Postphone to next day" CssClass="btn-primary btn btn-sm" OnClick="btpostphone_Click" />

            </div>
        </div>
    </form>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>

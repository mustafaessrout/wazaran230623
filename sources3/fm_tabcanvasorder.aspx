<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_tabcanvasorder.aspx.cs" Inherits="fm_tabcanvasorder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/index.js"></script>
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />

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
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="divheader">TABLET CANVAS ORDER</div>
            <div class="h-divider"></div>
            <div class="container">

                <div class="margin-bottom clearfix">
                    <label class="control-label col-md-1">Status</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbstatus" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Salesman</label>
                    <div class="col-md-4 drop-down">
                        <asp:DropDownList ID="cbsalesman" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-12">
                        <div class="text margin-bottom">Summary Order</div>
                        <asp:GridView ID="grdorder" runat="server" AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" GridLines="None" CssClass="table table-striped mygrid" ShowFooter="True" OnRowDataBound="grdorder_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_shortname") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Order">
                                    <ItemTemplate>
                                        <%# Eval("qty_conv") %>
                                        <asp:HiddenField ID="hdqty" runat="server" Value='<%# Eval("qty") %>' />
                                        <asp:HiddenField ID="hdqty_ctn" runat="server" Value='<%# Eval("qty_ctn") %>' />
                                        <asp:HiddenField ID="hdqty_pcs" runat="server" Value='<%# Eval("qty_pcs") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotorder" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Price">
                                    <ItemTemplate>
                                        <%# Eval("unitprice") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subtotal">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal","{0:F2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                    </FooterTemplate>
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
                        <div class="text margin-bottom">Summary Free/Bonus</div>
                        <asp:GridView ID="grdfree" runat="server" AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" GridLines="None" CssClass="table table-striped mygrid" ShowFooter="True" OnRowDataBound="grdfree_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_shortname") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Free">
                                    <ItemTemplate>
                                        <%# Eval("freeqty_conv") %>
                                        <asp:HiddenField ID="hdqtyfree" runat="server" Value='<%# Eval("freeqty") %>' />
                                        <asp:HiddenField ID="hdqtyfree_ctn" runat="server" Value='<%# Eval("freeqty_ctn") %>' />
                                        <asp:HiddenField ID="hdqtyfree_pcs" runat="server" Value='<%# Eval("freeqty_pcs") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotfree" runat="server"></asp:Label>
                                    </FooterTemplate>
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

                <div class="margin-bottom ">
                    <div class="overflow-y" style="max-height: 450px;">
                        <asp:GridView ID="grdtab" runat="server" CellPadding="0" GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanging="grdtab_SelectedIndexChanging" CssClass="table table-striped table-fix mygrid" OnRowDataBound="grdtab_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tab No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbso_cd" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbpayment_no" runat="server" Text='<%# Eval("payment_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manual No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbmanual_no" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbso_dt" runat="server" Text='<%# Eval("so_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salesman">
                                    <ItemTemplate>
                                        <%# Eval("emp_desc") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcust" runat="server" Text='<%# Eval("customer") %>'></asp:Label>
                                        <%--<asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label Text="Total Amount:" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Disc">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtaxdisc" runat="server" Text='<%# Eval("disc_tax_amt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbtaxdiscsum" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtotamt" runat="server" Text='<%# Eval("total_amt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbtotamtsum" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtotqty" runat="server" Text='<%# Eval("qty_conv") %>'></asp:Label>
                                        <%--<asp:Label ID="lbtotqty" runat="server" Text='<%# Eval("sales_qty") %>'></asp:Label>--%>
                                        <%--<asp:HiddenField ID="hdtotqty" runat="server" Value='<%# Eval("sales_qty") %>' />--%>
                                        <asp:HiddenField ID="hdqty_ctn" runat="server" Value='<%# Eval("qty_ctn") %>' />
                                        <asp:HiddenField ID="hdqty_pcs" runat="server" Value='<%# Eval("qty_pcs") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbtotqtysum" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Qty">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lbfreeqty" runat="server" Text='<%# Eval("free_qty") %>'></asp:Label>--%>
                                        <asp:Label ID="lbfreeqty" runat="server" Text='<%# Eval("free_qty_conv") %>'></asp:Label>
                                        <%--<asp:HiddenField ID="hdfreeqty" runat="server" Value='<%# Eval("free_qty") %>' />--%>
                                        <asp:HiddenField ID="hdfreeqty_ctn" runat="server" Value='<%# Eval("free_qty_ctn") %>' />
                                        <asp:HiddenField ID="hdfreeqty_pcs" runat="server" Value='<%# Eval("free_qty_pcs") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbfreeqtysum" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc On/Off">
                                    <ItemTemplate>
                                        <asp:Label ID="lbrdonoff" runat="server" Text='<%# Eval("rdonoff") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtype" runat="server" Text='<%# Eval("payment_type") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label Text="Count:" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lbloc" runat="server" Text='<%# Eval("location") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbcounttran" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>' Visible="false"></asp:Label>
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
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbvhc_cd" runat="server" Text='<%# Eval("vhc_cd") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_cd") %>' Visible="false"></asp:Label>
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
                </div>
                <div class="margin-bottom">
                    <div class="col-md-12">
                        <asp:GridView ID="grdtabdtl" runat="server" AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" GridLines="None" CssClass="table table-striped mygrid" ShowFooter="True" OnRowDataBound="grdtabdtl_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_shortname") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Order">
                                    <ItemTemplate>
                                        <%# Eval("qty_conv") %>
                                        <asp:HiddenField ID="hdqty" runat="server" Value='<%# Eval("qty") %>' />
                                        <asp:HiddenField ID="hdqty_ctn" runat="server" Value='<%# Eval("qty_ctn") %>' />
                                        <asp:HiddenField ID="hdqty_pcs" runat="server" Value='<%# Eval("qty_pcs") %>' />
                                        <%--<%# Eval("qty") %>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotorder" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Price">
                                    <ItemTemplate>
                                        <%# Eval("price_conv") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT">
                                    <ItemTemplate>
                                        <%# Eval("vat_amt") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subtotal">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal","{0:F2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock Cust">
                                    <ItemTemplate>
                                        <%# Eval("stock_cust") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <%# Eval("uom") %>
                                    </ItemTemplate>
                   
                                </asp:TemplateField>--%>
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
                <div class="form-group">
                    <div class="col-md-12">
                        <div class="text-bold margin-bottom">Discount Information</div>
                        <asp:GridView ID="grddisc" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped mygrid">
                            <AlternatingRowStyle />
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
            </div>
            <div class="navi">
                <asp:Button ID="btapply" OnClientClick="ShowProgress();" runat="server" Text="Transfer To Back Office" CssClass="btn btn-success add" OnClick="btapply_Click" />
                <asp:Button ID="btcancel" runat="server" OnClientClick="ShowProgress();" Text="Cancel Canvas Order" CssClass="btn btn-danger delete" OnClick="btcancel_Click" />
                <asp:Button ID="btpostpone" runat="server" Text="Postpone to next day" OnClientClick="ShowProgress();" CssClass="btn btn-primary edit" OnClick="btpostpone_Click" />
            </div>
        </div>

    </form>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>

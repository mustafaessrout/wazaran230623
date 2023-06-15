<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_tabsalesreturn.aspx.cs" Inherits="fm_tabsalesreturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Return</title>

    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/sweetalert.min.js"></script>


    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />

    <script>
        var d = $(window).height();
        $("#form1").addClass('asdasdasd');
        $("#form1").css({ "max-height": d, "overflow-y": "scroll" });
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

</head>
<body>
    <form id="form1" runat="server" style="max-height: 670px; overflow-y: auto">
        <div class="containers bg-white">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div class="divheader">TABLET SALES RETURN TRANSACTION</div>
            <div class="h-divider"></div>

            <div class="divgrid clearfix margin-bottom">
                <label class="titik-dua col-sm-1 control-label">Status</label>
                <div class="col-sm-5 drop-down">
                    <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    <asp:HiddenField ID="hdreturno" runat="server" />
                    <asp:HiddenField ID="hdretur_typ" runat="server" />
                </div>
            </div>

            <div class=" margin-bottom">
                <div class="overflow-x " style="width: 100%; max-height: 500px;">
                    <asp:GridView ID="grdtab" runat="server" CellPadding="0" GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanging="grdtab_SelectedIndexChanging" CssClass="table table-striped table-fix table-page-fix mygrid" data-table-page="#copy-fst" OnPageIndexChanging="grdtab_PageIndexChanging" AllowPaging="True" PageSize="30">
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
                                    <asp:Label ID="lbtabretur_no" runat="server" Text='<%# Eval("tabretur_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual No">
                                <ItemTemplate>
                                    <asp:Label ID="lbmanual_no" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbretur_dt" runat="server" Text='<%# Eval("retur_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salesman">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalesman_cd" runat="server" Text='<%# Eval("emp_desc")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust CD">
                                <ItemTemplate>
                                    <asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retur type">
                                <ItemTemplate>
                                    <asp:Label ID="lbretur_typ" runat="server" Text='<%# Eval("retur_typ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lbremark" runat="server" Text='<%# Eval("remark") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tab Payment RTV">
                                <ItemTemplate>
                                    <asp:Label ID="lbpayment_no" runat="server" Text='<%# Eval("payment_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Man No">
                                <ItemTemplate>
                                    <asp:Label ID="lbcustmanual_no" runat="server" Text='<%#Eval("custmanual_no") %>'></asp:Label>
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
            <div class=" margin-bottom">
                <div class="overflow-x" style="width: 100%; max-height: 500px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdtabdtl" runat="server" AllowPaging="True" AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="0" CssClass="table tables-striped table-page-fix mygrid" data-table-page="#copy-scd" EmptyDataText="NO DATA FOUND" GridLines="None" OnPageIndexChanging="grdtabdtl_PageIndexChanging" OnRowCancelingEdit="grdtabdtl_RowCancelingEdit" OnRowEditing="grdtabdtl_RowEditing" OnRowUpdating="grdtabdtl_RowUpdating" PageSize="30">
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
                                        <ItemTemplate>
                                            <%# Eval("size") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <%# Eval("branded_nm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <%# Eval("qty") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" Text='<%# Eval("qty") %>' Width="5em"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <%# Eval("uom") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exp Date">
                                        <ItemTemplate>
                                            <%# Eval("exp_dt","{0:d/M/yyyy}") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="dtexp" runat="server" CssClass="form-control input-sm" Text='<%# Eval("exp_dt") %>'></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="date" TargetControlID="dtexp">
                                            </asp:CalendarExtender>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Price">
                                        <ItemTemplate>
                                            <%# Eval("unitprice") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust Price">
                                        <ItemTemplate>
                                            <%# Eval("custprice") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="sub Total">
                                        <ItemTemplate>
                                            <%# Eval("subtotal") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Whs">
                                        <ItemTemplate>
                                            <asp:Label ID="lbwhs_cd" runat="server" Text='<%# Eval("whs_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bin">
                                        <ItemTemplate>
                                            <asp:Label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cbbin_cd" runat="server" Width="90px" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField HeaderText="Action" ShowEditButton="True" />
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="table-copy-page-fix" id="copy-scd"></div>
            </div>
            <p class="well well-sm  danger text-white">Bin Code : GS=Good Stock, BS=Bad Stock , NE=Near Expiration</p>

            <div class="navi margin-bottom">
                <asp:Button ID="btapply" runat="server" Text="Transfer To Back Office" CssClass="btn-success btn add" OnClick="btapply_Click" />
                <asp:Button ID="btcancel" runat="server" Text="Cancel Tablet Salesreturn" CssClass="btn-danger btn delete" OnClick="btcancel_Click" />
                <asp:Button ID="btpostpone" runat="server" Text="Postpone to next day" CssClass="btn-warning btn edit" OnClick="btpostpone_Click" />
            </div>
        </div>

    </form>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_mst_Supplier.aspx.cs" Inherits="lookup_mst_Supplier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="form-horizontal">
                    <div class="form-group">
                        <table>
                            <tr>
                                <td>Supplier Name / Code</td>
                                <td>
                                    <asp:TextBox ID="txtSupplier" runat="server" Text="" CssClass="form-control input-group-sm"></asp:TextBox>
                                    <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary" Style="float: right; margin-top: -33px;" OnClick="btsearch_Click">Search</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                            </tr>

                        </table>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" CellPadding="0"
                                OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Supplier Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierCode" runat="server" Text='<%#Eval("supplier_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("supplier_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Registration">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyRegistration" runat="server" Text='<%#Eval("companyRegistration") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax No">
                                        <ItemTemplate>
                                            <asp:Label ID="txtSupplierTax_no" runat="server" Text='<%#Eval("supplierTax_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                                </Columns>
                                <HeaderStyle CssClass="header"></HeaderStyle>

                                <PagerStyle CssClass="pager"></PagerStyle>

                                <RowStyle CssClass="rows"></RowStyle>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>


        </div>
        <div>
        </div>
    </form>
</body>
</html>

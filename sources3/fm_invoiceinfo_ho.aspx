<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_invoiceinfo_ho.aspx.cs" Inherits="fm_invoiceinfo_ho" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script>
        function openreport(url) {
            window.open(url, url, "toolbar=no;fullscreen=yes;scrollbars=yes", true);
        }
    </script>


    <style type="text/css">
        .auto-style1 {
            height: 19px;
        }

        .auto-style2 {
            width: 20%;
        }

        .auto-style3 {
            height: 19px;
            width: 20%;
        }
    </style>
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>

</head>
<body style="font-family: Calibri; font-size: small;">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
        <div style="background-color: silver; padding: 1em 1em 1em 1em">
            <strong>Invoice Information
            </strong>
        </div>
        <table style="width: 100%">
            <tr>
                <td class="auto-style2">Invoice NO</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lbinvoice" runat="server" Style="font-weight: 700"></asp:Label></td>
                <td>Customer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbcust" runat="server" Style="font-weight: 700; color: #000000"></asp:Label></td>
            </tr>
            <tr>
                <td class="auto-style2">Cust Type</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lbotlcd" runat="server"></asp:Label>
                </td>

                <td>Discount No</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lbdisc" runat="server"></asp:Label>
                </td>

            </tr>
            <tr>
                <td class="auto-style2">Invoice Date</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lbinvoicedt" runat="server" Text=""></asp:Label>
                </td>

                <td>Received Date</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lbreceiveddt" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            <tr>
                <td class="auto-style2">Manual_no</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lbmanual" runat="server"></asp:Label>
                </td>
                <td>Free Manual_no</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbfree" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Order No</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lborder" runat="server"></asp:Label>
                </td>
                <td>Order Type</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbtype" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top" class="auto-style3">Invoice Detail</td>
                <td style="vertical-align: top">:</td>
                <td colspan="4" style="vertical-align: top">
                    <%# Eval("salespointcd") %>
                    <asp:GridView ID="grdinvoice" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="mygrid" OnRowCancelingEdit="grdinvoice_RowCancelingEdit" OnRowEditing="grdinvoice_RowEditing" OnRowUpdating="grdinvoice_RowUpdating">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lbitem" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="UOM" HeaderText="UOM" ReadOnly="true" />--%>
                            <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txqty" runat="server" Text='<%# Eval("qty") %>' Width="4em"></asp:TextBox>
                            </EditItemTemplate>                            
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unitprice">
                                <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="unitprice" HeaderText="Unitprice" />--%>
                            <%--<asp:BoundField DataField="disc_amt" HeaderText="Disc" />--%>
                            <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lbdisc_amt" runat="server" Text='<%# Eval("disc_amt") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txdisc_amt" runat="server" Text='<%# Eval("disc_amt") %>' Width="4em"></asp:TextBox>
                            </EditItemTemplate>                            
                        </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="subtotal" />--%>
                            <asp:CommandField HeaderText="Action" ShowEditButton="True" ShowHeader="True" />
                            <%--<asp:TemplateField HeaderText="Scratch">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Scratch" runat="server" value='<%# Eval("item_cd") %>'></asp:CheckBox></a>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <%# Eval("item_nm") %>
                   
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top" class="auto-style3">&nbsp;</td>
                <td style="vertical-align: top" class="auto-style1">&nbsp;</td>
                <td colspan="4" style="vertical-align: top" class="navi">
                    &nbsp;</td>
                </tr>
            <div id="orderUpload" runat="server">
            <tr>
                <td style="text-align: left; vertical-align: top" class="auto-style3">
                    <asp:Label ID="lbupinv" runat="server" Text="Order Invoice" Visible="False"></asp:Label>
                </td>
                <td style="vertical-align: top" class="auto-style1">:</td>
                <td colspan="4" style="vertical-align: top" class="auto-style1">
                    <asp:FileUpload ID="uplo" runat="server" />
                </td>
            </tr>
            </div>
            <div id="freeUpload" runat="server">
            <tr>
                <td style="text-align: left; vertical-align: top" class="auto-style3">
                    <asp:Label ID="lbupfree" runat="server" Text="Free Invoice" Visible="False"></asp:Label>
                </td>
                <td style="vertical-align: top" class="auto-style1">:</td>
                <td colspan="4" style="vertical-align: top" class="auto-style1">
                    <asp:FileUpload ID="uplf" runat="server" Visible="False" />
                </td>
            </tr>
            </div>
            <tr>
                <td style="vertical-align: top" class="auto-style3">Invoice Free</td>
                <td style="vertical-align: top">:</td>
                <td colspan="4">
                    <asp:GridView ID="grdfree" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" ShowHeaderWhenEmpty="True" CssClass="mygrid" OnRowCancelingEdit="grdfree_RowCancelingEdit" OnRowEditing="grdfree_RowEditing" OnRowUpdating="grdfree_RowUpdating">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lbitem" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="UOM" />--%>
                            <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txqty" runat="server" Text='<%# Eval("qty") %>' Width="4em"></asp:TextBox>
                            </EditItemTemplate>                            
                        </asp:TemplateField>
                            <asp:CommandField HeaderText="Action" ShowEditButton="True" />
                            <%--<asp:TemplateField HeaderText="Scratch">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Scratch" runat="server" value='<%# Eval("item_cd") %>'></asp:CheckBox></a>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </td>
            </tr>
            <tr>
                <td style="vertical-align: top" class="auto-style3">
                    <asp:Label ID="Label1" runat="server" Text="Attachment"></asp:Label>
                </td>
                <td style="vertical-align: top">&nbsp;</td>
                <td colspan="4">
                    <asp:GridView ID="grdfree0" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" ShowHeaderWhenEmpty="True" CssClass="mygrid">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField HeaderText="INVOICE" />
                            <asp:BoundField HeaderText="FREE" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </td>
            </tr>
        </table>

        <img src="div2.png" class="divid" />
        <div class="navi">
            <%--<asp:Button ID="btcalc" runat="server" Text="Disc Calculation" CssClass="button2 save" OnClick="btcalc_Click" />--%>
            <asp:Button ID="btscratch" runat="server" Text="Update" CssClass="button2 save" OnClick="btscratch_Click" />
            <%--<asp:Button ID="btclose" runat="server" Text="No Scratch" CssClass="button2 delete" OnClick="btclose_Click" />--%>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_invoiceinfoCO.aspx.cs" Inherits="fm_invoiceinfoCO" %>

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
        .auto-style4 {
            height: 17px;
        }
        </style>
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>

</head>
<body style="font-family: Calibri; font-size: small;">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
        <div style="background-color: silver; padding: 1em 1em 1em 1em">
            <strong>Cashout Information
            </strong>
        </div>
        <table style="width: 100%">
            <tr>
                <td class="auto-style2">Cashout NO</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lbclaimco" runat="server" Style="font-weight: 700"></asp:Label></td>
                <td>Proposal No</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbproposal" runat="server" Style="font-weight: 700; color: #000000"></asp:Label></td>
            </tr>
            <tr>
                <td class="auto-style2">CDV No</td>
                <td>:</td>
                <td class="auto-style1">
                    <asp:Label ID="lbcdv" runat="server"></asp:Label>
                </td>

                <td class="auto-style2">Cashout Date</td>
                <td>:</td>
                <td class="auto-style4">
                    <asp:Label ID="lbcashoutdt" runat="server" Text=""></asp:Label>
                </td>

            </tr>            
            <tr>
                <td style="text-align: left; vertical-align: top" class="auto-style3">Cashout Detail</td>
                <td style="vertical-align: top">:</td>
                <td colspan="4" style="vertical-align: top">
                    <%# Eval("salespointcd") %>
                    <asp:GridView ID="grdcashout" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="mygrid">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Cashout Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbco" runat="server" Text='<%# Eval("cashout_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemco" runat="server" Text='<%# Eval("itemco_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("itemco_nm") %></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vat">
                                <ItemTemplate><%# Eval("vat_amt") %></ItemTemplate>
                            </asp:TemplateField>
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
                <td style="text-align: left; vertical-align: top" class="auto-style3">&nbsp;</td>
                <td style="vertical-align: top" class="auto-style1">&nbsp;</td>
                <td colspan="4" style="vertical-align: top" class="navi">
                    &nbsp;</td>
                </tr>
            <div id="orderUpload" runat="server">
            <tr>
                <td style="text-align: left; vertical-align: top" class="auto-style3">
                    Cashout Document</td>
                <td style="vertical-align: top" class="auto-style1">:</td>
                <td colspan="4" style="vertical-align: top" class="auto-style1">
                    <asp:FileUpload ID="uplo" runat="server" />
                </td>
            </tr>
            </div>
            
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

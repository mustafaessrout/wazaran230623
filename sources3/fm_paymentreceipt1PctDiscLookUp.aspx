<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentreceipt1PctDiscLookUp.aspx.cs" Inherits="fm_paymentreceipt1PctDiscLookUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <style>
        .ContentPlaceHolder1_txcust_AutoCompleteExtender_completionListElem {
            overflow-x: scroll;
            overflow-y: scroll;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">1 Percent Discount Details</h4>
        <div class="h-divider"></div>
        <div class="container">

            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">



                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdInvoice" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                                    ForeColor="#333333" EmptyDataText="No records Found">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="salespointcd" HeaderText="salespointcd" />
                                        <asp:BoundField DataField="custName" HeaderText="Cust Name" />
                                        <asp:BoundField DataField="groupName" HeaderText=" Group Name" />
                                        <asp:BoundField DataField="payment_no" HeaderText="Pay No" />
                                        <asp:BoundField DataField="inv_no" HeaderText="Inv No" />
                                        <asp:BoundField DataField="inv_amt" HeaderText="Inv Amt" />
                                        <asp:BoundField DataField="inv_disc" HeaderText="Disc On Inv" />
                                        <asp:BoundField DataField="disc1pct_cash" HeaderText="Disc Cash 1%" />
                                        <asp:BoundField DataField="collectionAfter_disc1pct" HeaderText="Collection After Dis" />
                                        <asp:BoundField DataField="vat_in" HeaderText="Vat In" />
                                        <asp:BoundField DataField="vat_out" HeaderText="Vat Out" />
                                    </Columns>

                                    <FooterStyle CssClass="table-footer" BackColor="Yellow" Font-Bold="True" Font-Size="Larger" />
                                    <EditRowStyle BackColor="#999999" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>


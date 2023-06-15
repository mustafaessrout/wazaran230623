<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimEntry.aspx.cs" Inherits="fm_claimEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script>
        function RefreshData(proposal_no, benefit, disc_cd) {
            $get('<%=txtProposal.ClientID%>').value = proposal_no;
            $get('<%=lhDiscCd.ClientID%>').value = disc_cd;
            $get('<%=btrefresh.ClientID%>').click();
        }
        function RefreshData1(prop_no) {
            $get('<%=txtProposal.ClientID%>').value = prop_no;
            $get('<%=lhDiscCd.ClientID%>').value = prop_no;
            $get('<%=btrefresh.ClientID%>').click();
        }
        function OpenDetails(so_cd, type) {
            popupwindow("lookup_invoice.aspx?number=" + so_cd + "&type=" + type);
            // $get('<%=btInvDetails.ClientID%>').click();
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
    .auto-style2 {
        width: 317px;
    }
    .auto-style3 {
        height: 20px;
        width: 317px;
    }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Claim Entry</div>
    <div class="h-divider"></div>

     <div class="container-fluid">
        <div class="row">
            <div class="clearfix margin-bottom">
                 <div class="col-sm-6">
                     <label class="control-label col-sm-4 titik-dua">Claim Type</label>
                     <div class="col-sm-8 radio radio-space-around no-margin">
                         <asp:RadioButtonList ID="rdbtrxn" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbtrxn_SelectedIndexChanged">
                            <asp:ListItem Value="IV">INVOICES</asp:ListItem>
                            <asp:ListItem Value="CSH">CASH OUT</asp:ListItem>
                            <asp:ListItem Value="CNDN">CN / DN</asp:ListItem>
                            <asp:ListItem Value="BA">BUS. AG.</asp:ListItem>
                        </asp:RadioButtonList>
                     </div>
                 </div>
             </div>
            <div class="clearfix margin-bottom">
                <div class="col-sm-6">
                    <label class="control-label col-sm-4 titik-dua">Claim No.</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtClaimNo" runat="server" CssClass="form-control ro" ReadOnly="True" Enabled="false">NEW</asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6">
                    <label class="control-label col-sm-4 titik-dua">CCNR No.</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtCCNR" runat="server" ReadOnly="false" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-sm-6">
                    <label class="control-label col-sm-4 titik-dua">Claim Date</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDate" runat="server"  CssClass="ro form-control" ReadOnly="True" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    <label class="control-label col-sm-4 titik-dua">Budget</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txBudget" runat="server" CssClass="ro form-control" ReadOnly="True" Enabled="false"></asp:TextBox>
                    </div>
                    </ContentTemplate>       
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-sm-6">
                    <label class="control-label col-sm-4 titik-dua">Transaction Period</label>
                    <div class="col-sm-8">
                        <label class="control-label col-sm-12">Month</label>
                        <div class="col-sm-12 no-padding-left drop-down">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                            <asp:DropDownList ID="ddMonth" runat="server" CssClass="form-control">
                                
                            </asp:DropDownList>
                            <asp:HiddenField ID="lhMonth" runat="server" />
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="col-sm-offset-2 col-sm-4">
                    <label class="control-label col-sm-12">Year</label>
                        <div class="col-sm-12 no-padding-left drop-down">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                            <asp:DropDownList ID="ddYear" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:HiddenField ID="lhYear" runat="server" />
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-xs-12">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <asp:GridView ID="grdcate" runat="server" AutoGenerateColumns="False" GridLines="None" PageSize="5" OnRowDataBound="grdcate_RowDataBound" CssClass="table table-striped mygrid">
                        <AlternatingRowStyle  />
                        <Columns>
                            <asp:TemplateField HeaderText="Document Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbdocname" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Preview">
                                <ItemTemplate>
                                    <a class="example-image-link" href="/images/claim_doc/sample/<%# Eval("doc_cd") %>.pdf" data-lightbox="example-1<%# Eval("doc_cd") %>.pdf">
                                        <asp:Label ID="lbdocfilename" runat="server" Text='Preview'></asp:Label>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Upload" HeaderStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:FileUpload ID="upl" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="BY">
                                <ItemTemplate>
                                    <asp:Label ID="lbdic" runat="server" Text='<%# Eval("dic") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
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
                
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-sm-6">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <label class="control-label col-sm-4 titik-dua">Proposal Number</label>
                    <div class="col-sm-8">
                        <div class="input-group">                            
                            <asp:TextBox ID="txtProposal" runat="server" CssClass="makeitreadonly ro form-control" Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="lhDiscCd" runat="server" />
                            <asp:HiddenField ID="lhDiscountMec" runat="server" />
                            <asp:HiddenField ID="lhProposalNumber" runat="server" />
                            <asp:TextBox ID="so_cd" runat="server" Style="display: none"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" Style="display: none" />
                                <asp:Button ID="btInvDetails" runat="server" Text="Button" Style="display: none" />
                                <asp:Button ID="btsearchso" runat="server" CssClass="btn btn-primary btn-search" OnClick="btsearchso_Click" Text="search" />
                            </div>
                            <asp:HiddenField ID="hdto" runat="server" />                            
                        </div>                        
                    </div>
                    </ContentTemplate>       
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-6">
                    <asp:UpdatePanel ID="lblRemarkWel" runat="server" class="well well-sm primary text-center text-bold" Visible="false">
                        <ContentTemplate>
                            <asp:Label ID="lblRemark" runat="server" ></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="clearfix">
        <div class="col-sm-12 clearfix">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
            <table class="col-sm-12">
                <tr>
                    <td>
                        <asp:Label ID="lbltotitem" runat="server" style="font-weight: 700" Text="Total Item Claim :"></asp:Label>
                        <asp:Label ID="lbltotcashoutdesc" runat="server" style="font-weight: 700" Text="Total Cashout Claim :"></asp:Label>
                        <asp:Label ID="lbltotcndndesc" runat="server" style="font-weight: 700" Text="Total CN/DN Claim :"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lbltotcndn" runat="server"></asp:Label>
                        <asp:Label ID="lbltotcashout" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblTQtyOrder" runat="server" Style="font-weight: 700" Visible="False"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbAvgPrice" runat="server" Style="font-weight: 700" Visible="False"></asp:Label>
                    </td>
                    <td><strong>
                        <asp:Label ID="lbtotqtyorder" runat="server" Text="Total Quantity :"></asp:Label>
                
                        &nbsp;<asp:Label ID="lbltotcashoutdescex" runat="server" style="font-weight: 700" Text="Total Cashout Claim :"></asp:Label>
                        <asp:Label ID="lbltotcashoutex" runat="server"></asp:Label>
                        <asp:Label ID="lblTValue" runat="server" Style="font-weight: 700" Visible="False"></asp:Label>
                        &nbsp;<asp:Label ID="lbltotcndndescex" runat="server" style="font-weight: 700" Text="Total CN/DN Claim :"></asp:Label>
                        <asp:Label ID="lbltotcndnex" runat="server"></asp:Label>
                    </strong></td>
                </tr>
                <tr>
                    <td class="auto-style1"><strong>
                        <asp:Label ID="lbtotfree" runat="server" Text="Total Free Item :"></asp:Label>
                        </strong></td>
                    <td class="auto-style3">
                        <asp:Label ID="lblTFreeItem" runat="server" Style="font-weight: 700" Visible="False"></asp:Label>
                    </td>
                    <td class="auto-style1"><strong>
                        <asp:Label ID="lbtotdiscount" runat="server" Text="Total Discount :"></asp:Label>
                        &nbsp;<asp:Label ID="lblTFreeCash" runat="server" Style="font-weight: 700" Visible="False"></asp:Label>
                        &nbsp;</strong></td>
                </tr>
            </table>
            </ContentTemplate>       
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
            <asp:Label ID="Label1" runat="server" CssClass="text-bold text-capitalize"></asp:Label>
            <asp:GridView ID="gridClaimSO" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover mygrid" ShowFooter="True" GridLines="None" OnRowDataBound="gridClaimSO_RowDataBound" CellPadding="4"  Caption="Canvas Order Free Cash">

                <AlternatingRowStyle />

                <Columns>
                    <asp:TemplateField HeaderText="Invoice No.">
                        <ItemTemplate>
                            <a href="javascript:OpenDetails('<%# Eval("so_cd") %>','TO');">
                                <asp:Label ID="so_cd" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Manual No.">
                        <ItemTemplate>
                            <asp:Label ID="manual_no" runat="server" Text='<%# Eval("manual_inv_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                            &nbsp;|&nbsp;<asp:Label ID="cust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="total" runat="server" Text="Total &nbsp; "></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="otlcd" HeaderText="Type" />
                    <asp:TemplateField HeaderText="Qty Order">
                        <ItemTemplate>
                            <asp:Label ID="qtyCashSO" runat="server" Text='<%# Eval("qtyorder","{0:N2}") %>'></asp:Label>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("uomorder") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbTotQtyOrderSO" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount Discount">
                        <ItemTemplate>
                            <asp:Label ID="amtSO" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbtotsubtotalSO" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="table-edit"/>
                <FooterStyle CssClass="table-footer"/>
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle  />
                <SelectedRowStyle CssClass="table-edit"/>
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
                
            <br />


            <asp:Label ID="Label2" runat="server" Visible="False" CssClass="text-bold text-capitalize"></asp:Label>
            <asp:GridView ID="gridClaimSOItem" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridClaimSOItem_RowDataBound" PageSize="30" CellPadding="4" GridLines="None" Caption="Take Order Free Item" CssClass="table table-striped mygrid">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Invoice No.">
                        <ItemTemplate>
                            <a href="javascript:OpenDetails('<%# Eval("so_cd") %>','TO');">
                                <asp:Label ID="so_cd" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Manual No.">
                        <ItemTemplate>
                            <asp:Label ID="manual_no" runat="server" Text='<%# Eval("free_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                            &nbsp;|&nbsp;<asp:Label ID="cust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="otlcd" HeaderText="Type" />
                    <asp:TemplateField HeaderText="Free Item">
                        <ItemTemplate><%# Eval("item_nm") %> <%# Eval("size") %></ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="total" runat="server" Text="Total Free &nbsp; "></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty Free" FooterText="Total">
                        <ItemTemplate>
                            <asp:Label ID="priceBuySOItem" Style="display: none;" runat="server" Text='<%# Eval("price_buy") %>'></asp:Label>
                            <asp:Label ID="qty" Style="display: none;" runat="server" Text='<%# Eval("qtyfree") %>'></asp:Label>
                            <%# Eval("qtyfree","{0:N2}") %> <%# Eval("uom_free") %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subtotal">
                        <ItemTemplate>
                            <asp:Label ID="lbQtySO" Style="display: none;" runat="server" Text='<%# Eval("qtyorder") %>'></asp:Label>
                            <%# Eval("subtotal","{0:N2}") %>
                            <asp:Label ID="subtotal" Style="display: none;" runat="server" Text='<%# Eval("subtotal") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbtotQtySO" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="table-edit"/>
                <FooterStyle CssClass="table-footer"/>
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle  />
                <SelectedRowStyle CssClass="table-edit"/>
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

            </ContentTemplate>       
            </asp:UpdatePanel>
        </div>
    </div>


    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
            <div>
                <asp:Label ID="Label3" runat="server"></asp:Label>
                <asp:HiddenField ID="totalQtyCO" runat="server" />
                <asp:GridView ID="gridClaimCO" CssClass="table table-striped table-hover mygrid" runat="server" AutoGenerateColumns="False"  ShowFooter="True" GridLines="None" OnRowDataBound="gridClaimCO_RowDataBound" CellPadding="4" ForeColor="#333333" Caption="Canvas Order Free Cash" CaptionAlign="Top">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Invoice No.">
                            <ItemTemplate>
                                <a href="javascript:OpenDetails('<%# Eval("so_cd") %>','CO');">
                                    <asp:Label ID="so_cd" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No.">
                            <ItemTemplate>
                                <asp:Label ID="manual_no" runat="server" Text='<%# Eval("manual_inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                &nbsp;|&nbsp;<asp:Label ID="cust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="total" runat="server" Text="Total &nbsp; "></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="otlcd" HeaderText="Type" />
                        <asp:TemplateField HeaderText="Qty Order">
                            <ItemTemplate>
                                <asp:Label ID="qtyCashCO" runat="server" Text='<%# Eval("qtyorder","{0:N2}") %>'></asp:Label>
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("uomorder") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbTotQtyOrderCO" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount Discount">
                            <ItemTemplate>
                                <asp:Label ID="amtCO" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotsubtotalCO" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer"/>
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit"/>
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>

            <div>
                <asp:Label ID="Label4" runat="server"></asp:Label>
                <asp:GridView ID="gridClaimCOItem" CssClass="table table-striped table-hover mygrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridClaimCOItem_RowDataBound" CellPadding="4" GridLines="None" Caption="Canvas Order Free Item">
                    <AlternatingRowStyle  />
                    <Columns>
                        <asp:TemplateField HeaderText="Invoice No.">
                            <ItemTemplate>
                                <a href="javascript:OpenDetails('<%# Eval("so_cd") %>','CO');">
                                    <asp:Label ID="so_cd" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No.">
                            <ItemTemplate>
                                <asp:Label ID="manual_no" runat="server" Text='<%# Eval("free_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                &nbsp;|&nbsp;<asp:Label ID="cust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="otlcd" HeaderText="Type" />
                        <asp:TemplateField HeaderText="Free Item">
                            <ItemTemplate><%# Eval("item_nm") %> <%# Eval("size") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="total" runat="server" Text="Total Free &nbsp; "></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Free">
                            <ItemTemplate>
                                <asp:Label ID="qtyCO" Style="display: none;" runat="server" Text='<%# Eval("qtyfree") %>'></asp:Label>
                                <%# Eval("qtyfree","{0:N2}") %> <%# Eval("uom_free") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotsubtotalCO" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subtotal">
                            <ItemTemplate>
                                <asp:Label ID="priceBuyCOItem" Style="display: none;" runat="server" Text='<%# Eval("price_buy") %>'></asp:Label>
                                <asp:Label ID="qtyOrderCO" Style="display: none;" runat="server" Text='<%# Eval("subtotal") %>'></asp:Label>
                                <%# Eval("subtotal","{0:N2}") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="QtyOrderCO" runat="server" Text='<%# Eval("subtotal","{0:00}") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer"/>
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit"/>
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            
            <div>
                <asp:GridView ID="grdcst" CssClass="table table-striped table-hover mygrid" runat="server" BorderStyle="None"  CellPadding="3" ShowFooter="True"  Caption="Internal Cashout Claim" AutoGenerateColumns="False" OnRowDataBound="grdcst_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="prop_no" HeaderText="Proposal No" />
                        <asp:BoundField DataField="schedule_dt" DataFormatString="{0:dd/MM/yyyy}"  HeaderText="Schedule Date" />
                        <asp:BoundField DataField="paid_dt" DataFormatString="{0:dd/MM/yyyy}"  HeaderText="Paid date" />
                        <asp:BoundField DataField="receivedby" HeaderText="receivedby" />
                        <asp:BoundField DataField="emp_nm" HeaderText="Salesman" />
                        <asp:BoundField DataField="phone_no" HeaderText="Phone No" />
                         <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbsubamt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer"/>
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit"/>
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>

            <div>
                <asp:GridView ID="grdgr" runat="server" CssClass="table table-striped table-hover mygrid" CellPadding="3" ShowFooter="True"  Caption="External Cashout Claim" AutoGenerateColumns="False" OnRowDataBound="grdgr_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="prop_no" HeaderText="Proposal No" />
                        <asp:BoundField DataField="schedule_dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Schedule Date" />
                        <asp:BoundField DataField="paid_dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Paid date" />
                        <asp:BoundField DataField="receivedby" HeaderText="Received by" />
                        <asp:BoundField DataField="fld_desc" HeaderText="Receiver Name" />
                        <asp:BoundField DataField="phone_no" HeaderText="Phone No" />
            
                        <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbsubamt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer"/>
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit"/>
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>

            <div>
                <asp:GridView ID="grdcndn" runat="server" CssClass="table table-striped table-hover mygrid" CellPadding="3" ShowFooter="True"  Caption="CN / DN Claim" AutoGenerateColumns="False" OnRowDataBound="grdcndn_RowDataBound">
                    <Columns>
                    <asp:BoundField DataField="prop_no" HeaderText="Proposal No" />
                    <asp:BoundField DataField="cndn_dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" />
                    <asp:BoundField DataField="cndn_cd" HeaderText="CN/DN" />
                    <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbsubamt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                    </Columns>
                   <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer"/>
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit"/>
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
            </div>

            <div>
                <asp:GridView ID="grdBA" runat="server" CssClass="table table-striped table-hover mygrid" CellPadding="3" ShowFooter="True"  Caption="Business Agreement Claim" AutoGenerateColumns="False" OnRowDataBound="grdBA_RowDataBound">
                    <Columns>
                    <asp:BoundField DataField="contract_no" HeaderText="Contract No" />
                    <asp:BoundField DataField="start_dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date" />
                    <asp:BoundField DataField="end_dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="End Date" />
                    <asp:BoundField DataField="customer" HeaderText="Customer" />
                    <asp:TemplateField HeaderText="FreeQty">
                        <ItemTemplate>
                            <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbsubqty" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbsubamt" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer"/>
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit"/>
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>

            </ContentTemplate>       
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
    <ContentTemplate>
    <asp:Label ID="lblTotalFreeCash" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblTotalFreeItem" runat="server"></asp:Label>
    <br />
    </ContentTemplate>       
    </asp:UpdatePanel>
    
    <div class="navi margin-bottom">
        <asp:Button ID="btnNew" runat="server" CssClass="btn-success btn btn-add" OnClick="btnNew_Click" Text="NEW CLAIM" Visible="False" />
        <asp:Button ID="btprint" runat="server" Text="SAVE CLAIM" CssClass="btn-warning btn btn-save" OnClick="btSave_Click" />
        <asp:Button ID="btnCancel" runat="server" CssClass="btn-danger btn btn-delete" OnClick="btnCancel_Click" Text="CANCEL CLAIM" Visible="False" />
    </div>


</asp:Content>

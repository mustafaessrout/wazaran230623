<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_invByProposal.aspx.cs" Inherits="fm_invByProposal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script>
        function RefreshData(claim_no) {
            $get('<%=btrefresh.ClientID%>').click();
        }

        function OpenDetails(so_cd, type) {
            popupwindow("lookup_invoice.aspx?number=" + so_cd + "&type=" + type);
        }

        function closeWindow() {
            closeWindow();
        }

        function openFile(filename) {
            popupwindow("images/claim_doc/" + filename)
        }
    </script>
    <style>
        a:visited {
            color: blue;
        }

        a:active {
            color: blue;
        }

        .auto-style1 {
            width: 255px;
        }

        .auto-style2 {
            width: 101px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        Claim Details
        <asp:Label ID="lblClaimStatus" runat="server" CssClass="badge danger" ></asp:Label>
        <asp:HiddenField ID="hstatus" runat="server" />
    </div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="clearfix row">
            <div  class="margin-bottom col-sm-6 ">
                <label class="control-label col-sm-4 titik-dua">Claim No.</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtClaimNo"  runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div  class="margin-bottom col-sm-6 ">
                <label class="control-label col-sm-4 titik-dua">CCNR No.</label>
                <div class="col-sm-8 no-padding clearfix">
                     <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCCNR" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:Button ID="txupdate" runat="server" CssClass="btn btn-primary col-sm-4" OnClick="txupdate_Click" Text="Update CCNR" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="margin-bottom col-sm-6">
                <label class="control-label col-sm-4 titik-dua">Proposal Number</label>
                <div class="col-sm-8">
                     <asp:TextBox ID="txtProposal" runat="server" CssClass="form-control ro"  Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="margin-bottom col-sm-6 clearfix">
                <asp:Label ID="lblRemark" runat="server"  BorderColor="Red" BorderStyle="Solid"></asp:Label>
            </div>
        </div>
         <div class="clearfix row">
            <div  class="margin-bottom col-sm-6 ">
                <label class="control-label col-sm-4 titik-dua">Transaction Period</label>
                <div class="col-sm-8">
                    <div class="clearfix margin-bottom">
                        <label class="col-sm-4 titik-dua padding-top-4">Month</label>
                        <div class="col-sm-8 drop-down">
                            <asp:DropDownList ID="ddMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddMonth_SelectedIndexChanged" CssClass="form-control input-sm" Enabled="False">
                                <%-- <asp:ListItem Text="=All=" Value="0"></asp:ListItem>--%>
                                <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:HiddenField ID="lhMonth" runat="server" />
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="col-sm-4 titik-dua padding-top-4">Year</label>
                        <div class="col-sm-8 drop-down">
                            <asp:DropDownList ID="ddYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddYear_SelectedIndexChanged" CssClass="ro form-control input-sm" Enabled="False">
                                
                            </asp:DropDownList>
                            <asp:HiddenField ID="lhYear" runat="server" />
                            <asp:HiddenField ID="lhDiscountMec" runat="server" />
                            <asp:HiddenField ID="lhDiscCd" runat="server" />
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="col-sm-4 titik-dua padding-top-4">Claim Date</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="ro form-control input-sm" Enabled="false" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix">
            <div class="well well-sm primary flex space-between no-margin">
                <div>
                    <asp:Label ID="lbclaimtyp" runat="server" Style="font-weight: 700"></asp:Label>
                    :<asp:Label ID="lblTQtyOrder" runat="server" Style="font-weight: 700"></asp:Label>
                   <asp:HiddenField ID="hdto" runat="server" />
                </div>
                <div>
                    <strong>Total Free Item/Discount :</strong>
                     <asp:Label ID="lblTFreeItem" runat="server" Style="font-weight: 700"></asp:Label>
                    / <asp:Label ID="lblTFreeCash" runat="server" Style="font-weight: 700"></asp:Label>
                </div>
            </div>
        </div>
        <div class="clearfix">
            <asp:GridView ID="grdcate" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered mygrid" GridLines="None" PageSize="5" OnRowDataBound="grdcate_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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

                        <asp:TemplateField HeaderText="Upload">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:FileUpload ID="upl" runat="server" />  
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BY">
                            <ItemTemplate>
                                <asp:Label ID="lbdic" runat="server" Text='<%# Eval("dic") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3"  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            <div class="navi">
                <asp:Button ID="btupload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btupload_Click" />
            </div>
        </div>
        <div class="clearfix">
            <asp:GridView ID="grddoc" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" CssClass="table table-striped table-bordered mygrid" OnRowDataBound="grddoc_RowDataBound" OnRowCommand="grddoc_RowCommand">
                    <AlternatingRowStyle  />
                    <Columns>
                        <asp:TemplateField HeaderText="Document Code">
                            <ItemTemplate>
                                <asp:Label ID="lbdoc_cd" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lbdoc_nm" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File location">
                            <ItemTemplate>
                                <a class="example-image-link" href="/images/claim_doc/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>" style="color: blue;">
                                    <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                </a>
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-default" CommandName="view" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3"  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
        </div>
        <div class="row">
            <div class="h-divider"></div>
        </div>
        <div class="clearfix">
            <div class="">
                <asp:Label ID="Label1" runat="server" Visible="False" CssClass="text-bold "></asp:Label>

                <asp:GridView ID="gridClaimSO" runat="server" AutoGenerateColumns="False" ShowFooter="True" GridLines="None" OnRowDataBound="gridClaimSO_RowDataBound" CellPadding="4" CssClass="table table-striped mygrid table-bordered">
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
                                <asp:Label ID="qtyOrderSO" runat="server" Text='<%# Eval("qtyorder") %>'></asp:Label>
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
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle Font-Bold="True" BackColor="#507CD1" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
            <div>
                 <asp:Label ID="Label2" runat="server" Visible="False" CssClass="text-bold "></asp:Label>

                <asp:GridView ID="gridClaimSOItem" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridClaimSOItem_RowDataBound" PageSize="30" CellPadding="4" GridLines="None" CssClass="table table-striped table-bordered mygrid">
                    <AlternatingRowStyle  />
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
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                    <RowStyle BackColor="#F7F6F3"  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
        <div class="clearfix">
            <div>
                <asp:Label ID="Label3" runat="server" Visible="False" CssClass="text-bold "></asp:Label>
                <asp:GridView ID="gridClaimCO" runat="server" AutoGenerateColumns="False"  ShowFooter="True" GridLines="None" OnRowDataBound="gridClaimCO_RowDataBound" CellPadding="4" CssClass="table table-striped table-bordered mygrid">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                                    <asp:Label ID="qtyOrderCO" runat="server" Text='<%# Eval("qtyorder") %>'></asp:Label>
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

                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle Font-Bold="True" BackColor="#5D7B9D" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3"  />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
            </div>
            <div>
                <asp:Label ID="Label4" runat="server" Visible="False" CssClass="text-bold"></asp:Label>

                <asp:GridView ID="gridClaimCOItem" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridClaimCOItem_RowDataBound" CellPadding="4" CssClass="table table-striped table-bordered mygrid" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                                <%# Eval("qtyfree") %> <%# Eval("uom_free") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotsubtotalCO" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subtotal">
                            <ItemTemplate>
                                <asp:Label ID="qtyOrderCO" Style="display: none;" runat="server" Text='<%# Eval("qtyorder") %>'></asp:Label>
                                <%# Eval("subtotal","{0:N2}") %>
                                <asp:Label ID="subtotal1" Style="display: none;" runat="server" Text='<%# Eval("subtotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="QtyOrderCO" runat="server" Text='<%# Eval("qtyorder","{0:00}") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                    <RowStyle BackColor="#F7F6F3"  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:GridView ID="grdcst" runat="server" CssClass="table table-striped table-bordered mygrid" BorderStyle="None" CellPadding="3" ShowFooter="True" Caption="Internal Cashout Claim" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="prop_no" HeaderText="Proposal No" />
                        <asp:BoundField DataField="schedule_dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Schedule Date" />
                        <asp:BoundField DataField="paid_dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Paid date" />
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
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                    <RowStyle BackColor="#F7F6F3"  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

                <asp:GridView ID="grdgr" runat="server"  CssClass="table table-striped table-bordered mygrid"  ShowFooter="True" Caption="External Cashout Claim" AutoGenerateColumns="False">
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
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                    <RowStyle BackColor="#F7F6F3"  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

                <asp:GridView ID="grdcndn" runat="server"  CssClass="table table-striped table-bordered mygrid"  ShowFooter="True" Caption="CN / DN Claim" AutoGenerateColumns="False">
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
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                    <RowStyle BackColor="#F7F6F3"  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

                <asp:GridView ID="grdBA" runat="server"  CssClass="table table-striped table-bordered mygrid"  CellPadding="3" ShowFooter="True"  Caption="Business Agreement Claim" AutoGenerateColumns="False" OnRowDataBound="grdBA_RowDataBound">
                <Columns>
                <asp:BoundField DataField="so_cd" HeaderText="Contract No" />
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
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                        <RowStyle BackColor="#F7F6F3"  />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

                
                <asp:GridView ID="grdadjso" runat="server" AutoGenerateColumns="False"  Caption="Sales order price adjustment Claim" ShowFooter="True" GridLines="None" OnRowDataBound="gridClaimSO_RowDataBound" CellPadding="4" CssClass="table table-striped mygrid table-bordered">
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
                                <asp:Label ID="qtyOrderSO" runat="server" Text='<%# Eval("qtyorder") %>'></asp:Label>
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
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle Font-Bold="True" BackColor="#507CD1" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

                  
                <asp:GridView ID="grdadjco" runat="server" AutoGenerateColumns="False"  Caption="Canvas order price adjustment Claim" ShowFooter="True" GridLines="None" OnRowDataBound="gridClaimCO_RowDataBound" CellPadding="4" CssClass="table table-striped table-bordered mygrid">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                                    <asp:Label ID="qtyOrderCO" runat="server" Text='<%# Eval("qtyorder") %>'></asp:Label>
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

                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle Font-Bold="True" BackColor="#5D7B9D" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3"  />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                
            </div>
            <div>              

            </div>
        </div>
        <div class="clearfix margin-bottom">
            <label class="control-label titik-dua col-sm-2">Remark Group</label>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="drop-down col-sm-5">
                        <asp:DropDownList ID="cbissuegrp" runat="server" CausesValidation="True" OnSelectedIndexChanged="cbissuegrp_SelectedIndexChanged" CssClass="form-control input-sm" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbissuegrp" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="clearfix margin-bottom">
            <label class="control-label titik-dua col-sm-2">Remark description</label>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="drop-down col-sm-5">
                        <asp:DropDownList ID="cbissuedesc" runat="server" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="clearfix margin-bottom">
            <label class="control-label titik-dua col-sm-2">Remark</label>
            <div class="col-sm-5">
                <asp:TextBox ID="txremark" runat="server" ForeColor="Red" TextMode="MultiLine" CssClass="form-control input-sm "></asp:TextBox>
            </div>
        </div>
        <div class="clearfix margin-bottom">
            <asp:GridView ID="grdremark" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" CssClass="table table-striped table-bordered mygrid">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="claim_no" HeaderText="Claim No" />
                        <asp:BoundField DataField="remark" HeaderText="Remark" />
                        <asp:BoundField DataField="userid" HeaderText="User" />
                        <asp:BoundField DataField="remarkdt" HeaderText="Date" />
                        <asp:BoundField DataField="fld_desc" HeaderText="Status" />
                        <asp:BoundField DataField="issue_no" HeaderText="Issue No" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                    <RowStyle BackColor="#F7F6F3"  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
        </div>
        <div class="clearfix">
            <asp:Label ID="lblTotalFreeCash" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblTotalFreeItem" runat="server"></asp:Label>
            <br />
        </div>
    </div>
    <div class="h-divider"></div>
    <div class="navi margin-bottom">
        <asp:Button ID="btClose" runat="server" Text="CLOSE" CssClass="btn btn-danger flag" OnClientClick="javascrip:window.close()" />
        <asp:Button ID="btApprove" runat="server" Text="APPROVE CLAIM" CssClass="btn btn-warning save" OnClick="btApprove_Click" OnClientClick="x=confirm ('Approve this Claim ?');if (x==true) return true;" />
        <asp:Button ID="btrefresh" runat="server" Text="btn btn-primary" Style="display: none" />
        <asp:Button ID="btnremarkreturn" runat="server" CssClass="btn btn-danger " OnClick="btnCancel_Click" Text="Return CLAIM" />
        <asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger " OnClick="btncancel2_Click" Text="Cancel CLAIM" />
        <asp:Button ID="btPrint" runat="server" Text="PRINT CCNR" CssClass="btn btn-info" OnClick="btPrint_Click" />
        <asp:Button ID="btPaid" runat="server" Text="PAY THIS CLAIM" CssClass="btn btn-warning save" OnClick="btPaid_Click" OnClientClick="x=confirm ('Pay this Claim ?');if (x==true) return true;" />
    </div>


</asp:Content>

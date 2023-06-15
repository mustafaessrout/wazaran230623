<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_transactionconfirmation.aspx.cs" Inherits="fm_transactionconfirmation" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdtrfno" runat="server" />
    <asp:HiddenField ID="hdsalespoint" runat="server" />

     <div id="container">
        <h4 class="jajarangenjang">Branch Transaction - Cancel / Reject </h4>
        <div class="h-divider"></div>

        <div id="paramtrans" runat="server">
            <div class="row">
                <div class="col-sm-6">
                    <div class="clearfix form-group" runat="server" id="listSalespoint">
                        <label class="control-label col-sm-2">Salespoint</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbSalesPointCD" runat="server" CssClass="form-control  " AutoPostBack="true" OnSelectedIndexChanged="cbSalesPointCD_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>                         
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="clearfix form-group" runat="server" id="typeTrans">
                        <label class="control-label col-sm-2">Transaction Type</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbtypetrans" runat="server" CssClass="form-control  " AutoPostBack="true" OnSelectedIndexChanged="cbtypetrans_SelectedIndexChanged">
                                        <asp:ListItem Value="t1"> Take Order with Payment</asp:ListItem>
                                        <asp:ListItem Value="t2"> Canvas Order with Payment</asp:ListItem>
                                        <asp:ListItem Value="t6"> Internal Transfer </asp:ListItem>
                                        <asp:ListItem Value="t3"> Payment Receipt</asp:ListItem>
                                        <asp:ListItem Value="t4"> Cashout Request </asp:ListItem>
                                        <asp:ListItem Value="t5"> Stock Opname </asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>                         
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <label class="control-label col-sm-2">Branch Date</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtbranch" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-sm-6">
                    <label class="control-label col-sm-2">Transaction No</label>
                    <div class="col-sm-10">
                        <div class="input-group">
                            <asp:textbox id="txtrans_no" runat="server" CssClass="form-control"></asp:textbox>
                            <div class="input-group-btn">
                                <asp:button id="btn_search" runat="server" cssclass="btn btn-primary btn-search" Text="Search" onclick="btn_search_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>   

        <div class="h-divider"></div>
        <div class="clearfix" id="listtrans" runat="server">

            <div class="overflow-x overflow-y">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:GridView ID="grdto" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" data-table-page="#copy-fst"  AutoGenerateColumns="False" OnSelectedIndexChanging="grdto_SelectedIndexChanging" OnPageIndexChanging="grdto_PageIndexChanging" OnRowCommand="grdto_RowCommand" ShowFooter="false" AllowPaging="True" PageSize="10">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Transaction No.">
                            <ItemTemplate>
                                <asp:Label ID="lbso_cd" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbso_dt" runat="server" Text='<%# Eval("so_dt","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice No.">
                            <ItemTemplate>
                                <asp:Label ID="lbinv_no" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No">
                            <ItemTemplate>
                                <asp:Label ID="lbmanual_no" runat="server" Text='<%# Eval("manual_inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate>
                                <asp:Label ID="lbsalesman" runat="server" Text='<%# Eval("emp_nm1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:Label ID="lbcustomer" runat="server" Text='<%# Eval("cust_nm1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbtotamt" runat="server" Text='<%# Eval("totamt","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("balance","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate> <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>' Visible="false"></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate> <asp:Label ID="lbsalesman_cd" runat="server" Text='<%# Eval("salesman_cd") %>' Visible="false"></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" SelectText="Payment Details" />
                        <asp:TemplateField>
                            <ItemTemplate>                
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="table-header" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

               


                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_search" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cbSalesPointCD" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbtypetrans" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                <ContentTemplate>
                     <asp:GridView ID="grdit" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" data-table-page="#copy-fst"  AutoGenerateColumns="False" OnSelectedIndexChanging="grdit_SelectedIndexChanging" OnPageIndexChanging="grdit_PageIndexChanging" OnRowCommand="grdit_RowCommand" ShowFooter="false" AllowPaging="True" PageSize="10">
                    <AlternatingRowStyle />
                    <Columns>
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
                            <ItemTemplate> <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>' Visible="false"></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate> <asp:Label ID="lbsalesman_cd" runat="server" Text='<%# Eval("salesman_cd") %>' Visible="false"></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" SelectText="Item Details" />
                        <asp:TemplateField>
                            <ItemTemplate>                
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="table-header" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_search" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cbSalesPointCD" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbtypetrans" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="table-copy-page-fix" id="copy-fst"></div>
        </div>
            
        <div class="h-divider"></div>
        <div id="listdtltrans" runat="server">
            <div class="overflow-x overflow-y">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdtodtl" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND"  ShowFooter="True" OnRowDataBound="grdtodtl_RowDataBound">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Transaction No.">
                            <ItemTemplate>
                                <asp:Label ID="lbso_cd" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice No.">
                            <ItemTemplate>
                                <asp:Label ID="lbinv_no" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment No.">
                            <ItemTemplate>
                                <asp:Label ID="lbpayment_no" runat="server" Text='<%# Eval("payment_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbpayment_dt" runat="server" Text='<%# Eval("payment_dt","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No">
                            <ItemTemplate>
                                <asp:Label ID="lbmanual_no" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Payment">
                            <ItemTemplate>
                                <asp:Label ID="lbtotamt" runat="server" Text='<%# Eval("totamt","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbamount" runat="server" Text='<%# Eval("amt","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbdiscount_amt" runat="server" Text='<%# Eval("discount_amt","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Round Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbround_amt" runat="server" Text='<%# Eval("round_amt","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

                    <asp:GridView ID="grditdtl" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND"  ShowFooter="True" OnRowDataBound="grditdtl_RowDataBound">
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
                                <div >
                                <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
				            <div >
				            <asp:Label ID="lblTotalqty" runat="server" />
				            </div>
			                </FooterTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
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


    </div>

</asp:Content>


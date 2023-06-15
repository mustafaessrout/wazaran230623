<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_upload_transaction.aspx.cs" Inherits="fm_upload_transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <h4 class="jajarangenjang">Branch Transaction - Upload </h4>
        <div class="h-divider"></div>

        <div class="form-group">
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
                    <label class="control-label col-sm-2">File Upload</label>
                    <div class="col-sm-10">
                        <div class="input-group">
                            <asp:FileUpload ID="fup" CssClass="form-control" runat="server" />
                            <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click"><i class="fa fa-upload"></i>Upload</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

         <div class="form-group">
            <div class="row">
                <div class="col-md-12 center">
                    <asp:LinkButton ID="btexport" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-info" OnClick="btexport_Click">Export To Wazaran </asp:LinkButton>
                </div>
            </div>
        </div>


        <div class="h-divider"></div>
        <div class="clearfix" id="listtrans" runat="server">
            <div class="overflow-x overflow-y">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:GridView ID="grdto" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" data-table-page="#copy-fst"  AutoGenerateColumns="False" ShowFooter="false" AllowPaging="False" >
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:Label ID="lbsp" runat="server" Text='<%# Eval("Branch_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate>
                                <asp:Label ID="lbsalesman" runat="server" Text='<%# Eval("Salesman_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:Label ID="lbcustomer" runat="server" Text='<%# Eval("Customer_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbso_dt" runat="server" Text='<%# Eval("transaction_date","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment">
                            <ItemTemplate>
                                <asp:Label ID="lbpayment" runat="server" Text='<%# Eval("Payment_Type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ST 1%">
                            <ItemTemplate>
                                <asp:Label ID="lbst" runat="server" Text='<%# Eval("source_tax") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Type">
                            <ItemTemplate>
                                <asp:Label ID="lbordertyp" runat="server" Text='<%# Eval("order_type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:Label ID="lbitem" runat="server" Text='<%# Eval("item_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="lbqty" runat="server" Text='<%# String.Format("{0} | {1}", Eval("qty_ctn"), Eval("qty_pcs")) %>'></asp:Label>
                                <asp:HiddenField ID="hdqty_ctn" runat="server" Value='<%# Eval("qty_ctn") %>' />
                                <asp:HiddenField ID="hdqty_pcs" runat="server" Value='<%# Eval("qty_pcs") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lbprice" runat="server" Text='<%# String.Format("{0} | {1}", Eval("price_ctn"), Eval("price_pcs")) %>'></asp:Label>
                                <asp:HiddenField ID="hdprice_ctn" runat="server" Value='<%# Eval("price_ctn") %>' />
                                <asp:HiddenField ID="hdprice_pcs" runat="server" Value='<%# Eval("price_pcs") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subtotal">
                            <ItemTemplate>
                                <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("Subtotal") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Invoice">
                            <ItemTemplate>
                                <asp:Label ID="lbtot_inv" runat="server" Text='<%# Eval("total_invoice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Payment">
                            <ItemTemplate>
                                <asp:Label ID="lbtot_payment" runat="server" Text='<%# Eval("total_payment") %>'></asp:Label>
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
                        <%--<asp:AsyncPostBackTrigger ControlID="btn_upload" EventName="Click" />--%>
                        <asp:AsyncPostBackTrigger ControlID="cbSalesPointCD" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbtypetrans" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>


       
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


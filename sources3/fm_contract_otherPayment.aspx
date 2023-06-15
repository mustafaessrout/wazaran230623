<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_contract_otherPayment.aspx.cs" Inherits="fm_contract_otherPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btShowInvoice.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Contract Payment</h4>
        <div class="h-divider"></div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Contract No</label>
                    <div class="col-md-2">
                        <div class="input-group">
                            <asp:UpdatePanel ID="UpdatePanel0" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdcust" runat="server" />

                                    <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                    <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" CompletionListElementID="divw" runat="server"
                                        TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                        MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True"
                                        ContextKey="true">
                                    </asp:AutoCompleteExtender>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="btShowInvoice" runat="server" CssClass="btn btn-success" OnClick="btShowInvoice_Click">Show</asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div id="divw" class="auto-text-content"></div>
                        </div>
                </div>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                            OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowDataBound="grd_RowDataBound" ForeColor="#333333"
                            EmptyDataText="No records Found">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Contract No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontract_no" runat="server" Text='<%#Eval("contract_no") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfcontract_no" runat="server" Value='<%#Eval("contract_no") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcustName" runat="server" Text='<%#Eval("custName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prop No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprop_no" runat="server" Text='<%#Eval("prop_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salesman">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempName" runat="server" Text='<%#Eval("empName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatusv" runat="server" Text='<%#Eval("statusv") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tot VAT">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotVAT" runat="server" Text='<%#Eval("totamt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance VAT">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotamt" runat="server" Text='<%#Eval("balance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="true" />
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
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Contract No</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" Value="" ID="hdfContractNumber" />
                            <asp:Label runat="server" ID="lblContractNumber" Text="" CssClass="form-control ro"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Prop Number </label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <asp:Label runat="server" ID="lblprop_no" Text="" CssClass="form-control ro"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>

        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Salesman </label>
                <div class="col-md-5">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdfSalesman" runat="server" />
                            <asp:Label runat="server" ID="lblSalesman" Text="" CssClass="form-control ro"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Customer </label>
                <div class="col-md-5">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="llbCustomer" Text="" CssClass="form-control ro"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Status </label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblstatusv" Text="" CssClass="form-control ro"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Balance Amount </label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblBalance" Text="" CssClass="form-control ro"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Payment Mode </label>
                <div class="col-md-2    drop-down">
                    <asp:UpdatePanel ID="UpdatePane21" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbpaymenttype" runat="server" Height="2em" AutoPostBack="True" OnSelectedIndexChanged="cbpaymenttype_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Cheque/BT Date</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtcheque" runat="server" CssClass="form-control" Height="2em"></asp:TextBox>
                            <asp:CalendarExtender ID="dtcheque_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtcheque">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <label class="control-label col-md-1">Cheque No</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txcheque" runat="server" CssClass="form-control" Height="2em" Width="100%"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Bank</label>
                <div class="col-md-2   drop-down">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbbank" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Payment </label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtPayment" runat="server" Text='0'></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Payment Type </label>
                <div class="col-md-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlPaymentype" CssClass="form-control input-sm" runat="server">
                                <asp:ListItem Text="Vat" Value="VAT"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Reason</label>
                <div class="col-md-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlReason" CssClass="form-control input-sm" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Remarks</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtRemarks" Style="height: 50px;"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        
 <div class="navi row margin-bottom">
            <asp:LinkButton ID="btSave" runat="server" CssClass="btn btn-primary" OnClick="btSave_Click">Save Payment</asp:LinkButton>
        </div>
            <h4 class="jajarangenjang">Contract Payment History</h4>
        <div class="container">
            <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdDetails" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                            ForeColor="#333333"
                            EmptyDataText="No records Found">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Payment No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltopID" runat="server" Text='<%#Eval("topID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="Contract No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontract_no" runat="server" Text='<%#Eval("contract_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcustName" runat="server" Text='<%#Eval("custName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="Pay VAT amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontractAmountPay" runat="server" Text='<%#Eval("contractAmountPay") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="Pay Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpayment_typ" runat="server" Text='<%#Eval("payment_typ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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


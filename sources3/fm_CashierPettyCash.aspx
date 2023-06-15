<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_CashierPettyCash.aspx.cs" Inherits="fm_CashierPettyCash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <script>
        function openwindow() {
            var oNewWindow = window.open("lookup_CashierPettyCash.aspx", "lookup", "height=700,width=1200,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
        function CashSelected(sender, e) {
            $get('<%=hdCashOut.ClientID%>').value = e.get_value();
            $get('<%=btnCashOutType.ClientID%>').click();

        }
        function EmployeeSelected(sender, e) {
            $get('<%=hdfEmployee.ClientID%>').value = e.get_value();
            $get('<%=btShowItemCashout.ClientID%>').click();
        }
        function SelectData(sVal) {
            $get('<%=hdfCashAdvancedID.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Cashier Petty Cash</h4>
        <div class="h-divider"></div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Cashier Petty Cash No</label>
                    <div class="col-md-2">

                        <asp:Label ID="lbsysno" CssClass="form-control input-group-sm" runat="server" Text=""></asp:Label>

                    </div>
                    <label class="control-label col-md-1">Cashout Approved</label>
                    <div class="col-md-2">
                        <div class="input-group">
                            <asp:Label ID="txtCashoutNumber" CssClass="form-control input-group-sm" runat="server" Text=""></asp:Label>
                            <%--                            <asp:TextBox ID="txtCashoutNumber" runat="server" CssClass="form-control input-group-sm" Enabled="false"></asp:TextBox>--%>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" OnClientClick="openwindow();return(false);" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <label class="control-label col-md-1">Operation Type</label>
                    <div class="col-md-2   drop-down">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCRDR" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCRDR_SelectedIndexChanged">
                                    <asp:ListItem Text="Debit" Selected="True" Value="DR"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Value="CR"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Dept</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" Text="ACCOUNTING" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2" style="display: none;">
                        <label class="control-label col-md-1">Employee</label>


                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfEmployee" runat="server" />
                                <div class="input-group">
                                    <div>
                                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" CompletionListElementID="divwEmployee" runat="server"
                                            TargetControlID="txtEmployee" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="EmployeeSelected" ServiceMethod="GetCompletionListEmployee" UseContextKey="True"
                                            ContextKey="true" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover"
                                            CompletionListItemCssClass="auto-complate-item">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btShowItemCashout" runat="server" OnClick="btShowItemCashout_Click" Text="Button" Style="display: none" />
                                        <asp:Button ID="btnCashOutType" runat="server" OnClick="btnCashOutType_Click" Text="Button" Style="display: none" />

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div id="divwEmployee" class="auto-text-content"></div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">CashOut Type</label>
                    <div class="col-md-2">

                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdCashOut" runat="server" />
                                <asp:HiddenField ID="hdfCashAdvancedID" runat="server" />
                                <div class="input-group">
                                    <div>
                                        <asp:TextBox ID="txItemCashout" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender ID="txItemCashout_AutoCompleteExtender" CompletionListElementID="divw" runat="server"
                                            TargetControlID="txItemCashout" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="CashSelected" ServiceMethod="GetCompletionList" UseContextKey="True"
                                            ContextKey="true" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover"
                                            CompletionListItemCssClass="auto-complate-item">
                                        </asp:AutoCompleteExtender>
                                    </div>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div id="divw" class="auto-text-content"></div>
                    </div>
                    <label class="control-label col-md-1">In Out</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtInOut" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Routine</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtRoutine" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Type</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Employee</label>
                    <div class="col-md-5">
                        <asp:TextBox ID="txtemp" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <label class="control-label col-md-1">Employee Debit</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtEmpDebit" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <label class="control-label col-md-1">Employee Credit</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtEmpCredit" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Approval</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbapproval" runat="server" CssClass="form-control"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Post Date</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="dtpost" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtprop_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtpost">
                        </asp:CalendarExtender>
                    </div>
                    <label class="control-label col-md-1">Manual Number</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtManualNumber" runat="server" CssClass="form-control input-sm"></asp:TextBox>

                    </div>
                    <label class="control-label col-md-1">Amount</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtAmt" FilterType="Custom" ValidChars=".0123456789" />
                    </div>


                    <label class="control-label col-md-1">Balance</label>
                    <div class="col-md-2">
                        <asp:Label ID="lblBalance" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                    </div>


                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-5">
                        <asp:Label ID="lblExpreid" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                    </div>


                </div>
            </div>

            <div class="navi row margin-bottom">

                <asp:LinkButton ID="btNew" runat="server" CssClass="btn btn-primary" OnClick="btNew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btSave" runat="server" CssClass="btn btn-primary" OnClick="btSave_Click">Save Petty Cash</asp:LinkButton>
                <asp:LinkButton ID="btnReport" CssClass="btn btn-danger" runat="server" OnClick="btnReport_Click"><i class="fa fa-print">&nbsp;Report</i></asp:LinkButton>
                <asp:LinkButton ID="btnRaw" CssClass="btn btn-danger" runat="server" OnClick="btnRaw_Click"><i class="fa fa-print">&nbsp;Raw Report</i></asp:LinkButton>
                <asp:Button ID="btlookup" runat="server" OnClick="btlookup_Click" Text="Button" Style="display: none" />
            </div>
            <h4 class="jajarangenjang">Employee Petty Cash</h4>
            <div class="form-group">
                <div class="row">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                                ForeColor="#333333" EmptyDataText="No records Found" OnRowDataBound="grd_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Cashout ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldoc_cd" runat="server" Text='<%#Eval("doc_cd") %>'></asp:Label>
                                            <asp:HiddenField ID="hdfdoc_cd" runat="server" Value='<%#Eval("doc_cd") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Date">
                                        <ItemTemplate>
                                            <%# Eval("transactionDate","{0:d/M/yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cashout Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemcoName" runat="server" Text='<%#Eval("itemcoName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebit" runat="server" Text='<%#Eval("debit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbTotDebit" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%#Eval("credit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbTotCredit" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
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

</asp:Content>


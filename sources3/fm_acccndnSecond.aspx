<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acccndnSecond.aspx.cs" Inherits="fm_acccndnSecond" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btShowInvoice.ClientID%>').click();
        }
        function openwindow() {
            var oNewWindow = window.open("lookup_acccndn.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="form-horizontal">
        <h4 class="jajarangenjang">CN DN Adjustment</h4>
        <div class="h-divider"></div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">CN/DN No</label>
                    <div class="col-md-2">
                        <div class="input-group">
                            <asp:Label ID="lbsysno" CssClass="form-control input-group-sm" runat="server" Text=""></asp:Label>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" OnClientClick="openwindow();return(false);" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <label class="control-label col-md-1">Customer</label>
                    <div class="col-md-2">

                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <div class="input-group">
                                    <div>
                                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" CompletionListElementID="divw" runat="server"
                                            TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True"
                                            ContextKey="true">
                                        </asp:AutoCompleteExtender>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btShowInvoice" runat="server" CssClass="btn btn-success" OnClick="btShowInvoice_Click">Show</asp:LinkButton>
                        </div>
                        <div id="divw" class="auto-text-content"></div>
                    </div>
                    <label class="control-label col-md-1">Operation Type</label>
                    <div class="col-md-2   drop-down">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCNDN" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCNDN_SelectedIndexChanged">
                                    <asp:ListItem Text="CN" Selected="True" Value="CN"></asp:ListItem>
                                    <asp:ListItem Text="DN" Value="DN"></asp:ListItem>
                                </asp:DropDownList>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>

            <div class="form-group">
                <div class="row">

                    <label class="control-label col-md-1">Manual / Automatic</label>
                    <div class="col-md-2   drop-down">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlOperation" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged">
                                    <asp:ListItem Text="Manual" Selected="True" Value="Manual"></asp:ListItem>
                                    <asp:ListItem Text="Automatic" Value="Automatic"></asp:ListItem>
                                </asp:DropDownList>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div id="dvAutomactic" runat="server">
                                <label class="control-label col-md-1">Automatic Amount</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtAutomaticAmount" runat="server"></asp:TextBox>

                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div id="dvbtnAutomatic" runat="server">
                                <asp:LinkButton ID="btnAutomatic" runat="server" CssClass="btn btn-success" OnClick="btAutomatic_Click">Automatic CNDN</asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfAutomaticAmount" runat="server" />
                        <div class="row" id="dvCNDNVat" runat="server">
                            <label class="control-label col-md-1">CN DN Type</label>
                            <div class="col-md-2  drop-down">
                                <asp:DropDownList ID="ddlCNDNType" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCNDNType_SelectedIndexChanged">
                                    <asp:ListItem Text="Non Vat" Value="NonVAT" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Vat" Value="VAT"></asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <div id="dvCNDNVatCal" runat="server">

                                <label class="control-label col-md-1">Vat Amount</label>
                                <div class="col-md-2">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblVat" runat="server" Style="color: red;" Text="0"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">HO Ref No</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtRef" runat="server" Text=""></asp:TextBox>

                    </div>
                    <label class="control-label col-md-1">Reason</label>
                    <div class="col-md-2 drop-down">

                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlReason" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Post Date</label>
                    <div class="col-md-2">

                        <asp:TextBox ID="dtpost" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtprop_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtpost">
                        </asp:CalendarExtender>
                    </div>
                    <label class="control-label col-md-1">CNDN Date</label>
                    <div class="col-md-2">

                        <asp:TextBox ID="dtCNDNDate" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtCNDNDate">
                        </asp:CalendarExtender>
                    </div>
                </div>

            </div>



            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Remarks</label>
                    <div class="col-md-4">

                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <table class="table mygrid">
                        <tr>
                            <th style="width: 9%">Inv No</th>
                            <th style="width: 13%">Inv Dt</th>
                            <th style="width: 10%">Manual Number</th>
                            <th style="width: 24%">Sales Man</th>
                            <th style="width: 11%">Status</th>
                            <th style="width: 7%">Inv Amt</th>
                            <th style="width: 8%">VAT in Inv</th>
                            <th style="width: 9%">CNDN Amount</th>
                            <th style="width: 7%">Balance</th>
                            <th style="width: 7%">Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                            <asp:Label ID="lblInvNo" runat="server" CssClass="form-control input-sm"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                            <asp:Label ID="lblInvDt" runat="server" CssClass="form-control input-sm" ></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <strong>
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblManualNumber" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </strong>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                            <asp:Label ID="lblSalesMan" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:Label>
                                            <asp:HiddenField ID="hdfsalesman_cd" runat="server" Value=''></asp:HiddenField>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                            <asp:Label ID="lblStatus" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:Label>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                            <asp:Label ID="lblAmount" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:Label>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                            <asp:Label ID="lblVATInInv" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:Label>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                            <asp:TextBox ID="lblCNDNAmount" runat="server"  ></asp:TextBox>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                            <asp:Label ID="lblBalance" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:Label>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="btadd" runat="server" CssClass="btn btn-block btn-success btn-sm" OnClick="btadd_Click" Text="Add" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" CssClass="table table-striped mygrid table-hover" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                                OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" 
                                 OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowDataBound="grd_RowDataBound"
                                ForeColor="#333333" EmptyDataText="No records Found">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Inv No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_no" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>
                                            <asp:HiddenField ID="hdfsalesman_cd" runat="server" Value='<%#Eval("salesman_cd") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Dt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_dt" runat="server" Text='<%#Eval("inv_dt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manual Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmanual_no" runat="server" Text='<%#Eval("manual_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Man">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsalesMan" runat="server" Text='<%#Eval("salesMan") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatusVal" runat="server" Text='<%#Eval("statusVal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotamt" runat="server" Text='<%#Eval("tbalance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT in Inv">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotVat" runat="server" Text='<%#Eval("totVat") %>' Style="color: red"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbVat" runat="server" Style="color: red"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CNDN Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCNDN" runat="server" Text='0'></asp:Label>
                                        </ItemTemplate>
                                        
                                        <FooterTemplate>
                                            <asp:Label ID="lbCNDNAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lbtotBalance" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="true"  />
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
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdDetails" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                                OnRowDataBound="grdDetails_RowDataBound" ForeColor="#333333" EmptyDataText="No records Found">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="HO Ref.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHO_Ref" runat="server" Text='<%#Eval("refho_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_no" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Dt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_dt" runat="server" Text='<%#Eval("inv_dt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manual Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmanual_no" runat="server" Text='<%#Eval("manual_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Man">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsalesMan" runat="server" Text='<%#Eval("salesMan") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatusVal" runat="server" Text='<%#Eval("statusVal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inv Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotamt" runat="server" Text='<%#Eval("inv_noAmount") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CN Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCN" runat="server" Text='<%#Eval("inv_CNAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotDtlCNAmount" runat="server" Text='0'></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DN Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDN" runat="server" Text='<%#Eval("inv_DNAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotDtlDNAmount" runat="server" Text='0'></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:CommandField />
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
            <div class="navi row margin-bottom">

                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnew_Click" CssClass="btn btn-success bt-add "><span >New </span></asp:LinkButton>
                <asp:LinkButton ID="btSave" runat="server" CssClass="btn btn-primary" OnClick="btSave_Click">Save CNDN</asp:LinkButton>
                <asp:LinkButton ID="btPrint" runat="server" CssClass="btn btn-primary" OnClick="btPrint_Click">Print CNDN</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>


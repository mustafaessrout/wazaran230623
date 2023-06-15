<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acccndn.aspx.cs" Inherits="fm_acccndn" %>

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
            var oNewWindow = window.open("lookup_acccndn.aspx", "lookup", "height=700,width=1200,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

        function SelectData(sVal) {
            $get('<%=hdfCNDNID.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }

        function TaxSelected(sender, e) {
            $get('<%=hdtaxall.ClientID%>').value = e.get_value();
            $get('<%=bttax.ClientID%>').click();
        }
    </script>
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
    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdtaxall" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="form-horizontal">
        <div class="alert alert-info text-bold">CNDN Adjustment</div>
        <%--   <h4 class="jajarangenjang">CN DN Adjustment</h4>
        <div class="h-divider"></div>--%>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">CN/DN No</label>
                    <div class="col-sm-2">
                        <div class="input-group">
                            <asp:Label ID="lbsysno" CssClass="form-control input-group-sm" runat="server" Text=""></asp:Label>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" OnClientClick="openwindow();return(false);" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <label class="control-label col-sm-1">Customer</label>
                    <div class="col-sm-2">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdcust" runat="server" />
                                <asp:HiddenField ID="hdfCNDNID" runat="server" />
                                <div class="input-group">
                                    <div class="input-group-sm">
                                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" CompletionListElementID="divw" runat="server"
                                            TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True"
                                            ContextKey="true">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btShowInvoice" runat="server" Text="Show" CssClass="btn btn-primary btn-sm" OnClick="btShowInvoice_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div id="divw" class="auto-text-content"></div>
                    </div>
                    <label class="control-label col-sm-1">Operation Type</label>
                    <div class="col-sm-2 drop-down">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCNDN" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCNDN_SelectedIndexChanged">
                                    <asp:ListItem Text="CN" Selected="True" Value="CN"></asp:ListItem>
                                    <asp:ListItem Text="DN" Value="DN"></asp:ListItem>
                                </asp:DropDownList>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-sm-1">PIC</label>
                    <div class="col-sm-2   drop-down">

                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbapproval" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="row">

                    <label class="control-label col-sm-1">Manual / Automatic</label>
                    <div class="col-sm-2   drop-down">

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
                                <label class="control-label col-sm-1">Automatic Amount</label>
                                <div class="col-sm-2">
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
                            <label class="control-label col-sm-1">CN DN Type</label>
                            <div class="col-sm-2  drop-down">
                                <asp:DropDownList ID="ddlCNDNType" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCNDNType_SelectedIndexChanged">
                                    <asp:ListItem Text="Non Vat" Value="NonVAT" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Vat" Value="VAT"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                <ContentTemplate>
                                    <div runat="server" id="showTax">
                                        <label class="control-label col-sm-1">Tax </label>
                                        <div class="col-sm-2">
                                            <table class="mGrid">
                                                <tr>
                                                    <th>Tax Name:</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtaxall" runat="server" Width="100%"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="txtaxall_AutoCompleteExtender" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="TaxSelected" runat="server" ServiceMethod="GetCompletionListTax" TargetControlID="txtaxall" UseContextKey="True">
                                                                </asp:AutoCompleteExtender>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdtax" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="No tax found" ShowHeaderWhenEmpty="True" OnRowDeleting="grdtax_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Tax Name">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="tax_cd" Value='<%#Eval("tax_cd") %>' runat="server" />
                                                                    <asp:Label ID="lbtaxnm" runat="server" Text='<%#Eval("tax_desc") %>' CssClass="control-label-sm"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbvalue" runat="server" Text='<%#Eval("amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlCNDNType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <div id="dvCNDNVatCal" runat="server">
                                <label class="control-label col-sm-1">Vat Amount</label>
                                <div class="col-sm-2">
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
                    <label class="control-label col-sm-1">HO Form Type</label>
                    <div class="col-sm-2   drop-down">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlHOFormType" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHOFormType_SelectedIndexChanged">
                                    <asp:ListItem Text="HODIFF" Selected="True" Value="HODIFF"></asp:ListItem>
                                    <asp:ListItem Text="HOCM" Value="HOCM"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-sm-1">Clearance No</label>
                    <div class="col-sm-2">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtRef" runat="server" MaxLength="7" Text="" AutoPostBack="true" OnTextChanged="txtRef_TextChanged"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRef" FilterType="Numbers" ValidChars="0123456789" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-sm-1">Additional</label>
                    <div class="col-sm-2">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtAdditional" runat="server" Text="" MaxLength="2" AutoPostBack="true" OnTextChanged="txtAdditional_TextChanged"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtAdditional" FilterType="Numbers" ValidChars="0123456789" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-sm-1">Sample</label>
                    <div class="col-sm-2">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <table class=" table-bordered">
                                    <thead style="padding: 2px; background-color: #337ab7; color: #fff">
                                        <td>Form Type</td>
                                        <td>Clearance</td>
                                        <td>Additional</td>
                                    </thead>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFormType" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblClearance" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAdditional" runat="server" Text="1"></asp:Label></td>
                                    </tr>
                                    <tr style="align-items: center;">
                                        <td colspan="1">HO Ref No
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="lblSample" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">CN DN Type</label>
                    <div class="col-sm-2 drop-down">

                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlReason" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-sm-1">On the Account</label>
                    <div class="col-sm-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlOnAccount" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdfFileName" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-sm-1">Post Date</label>
                    <div class="col-sm-2">

                        <asp:TextBox ID="dtpost" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtprop_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtpost">
                        </asp:CalendarExtender>
                    </div>
                    <label class="control-label col-sm-1">CNDN Date</label>
                    <div class="col-sm-2 require">
                        <asp:TextBox ID="dtCNDNDate" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtCNDNDate">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Document</label>
                    <div class="col-sm-4">
                        <asp:FileUpload ID="upl" runat="server" />

                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="row">

                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                            OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing"
                            OnRowUpdating="grd_RowUpdating" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowDataBound="grd_RowDataBound" ForeColor="#333333" EmptyDataText="No records Found">
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

                                <asp:TemplateField HeaderText="Balance Amount">
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
                                <asp:TemplateField HeaderText="Already CN Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlreadyCN" runat="server" Text='0'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbCNAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Already DN Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlreadyDN" runat="server" Text='0'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbDNAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CNDN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCNDNCurrent" runat="server" Text='0'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCNDN" runat="server" Text='0'></asp:TextBox>

                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbCNDNAmountCurrent" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lbtotBalance" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>
                                <asp:CommandField ShowSelectButton="true" ShowCancelButton="true" ShowEditButton="true" />
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
            <div class="col-sm-12">
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



            <asp:Button ID="btnew" OnClientClick="ShowProgress();" runat="server" Text="NEW" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
            <asp:Button ID="btSave" runat="server" OnClientClick="ShowProgress();" Text="Save CNDN" CssClass="btn-primary btn btn-primary" OnClick="btSave_Click" />
            <asp:Button ID="btnViewCNDN" runat="server" OnClientClick="ShowProgress();" Text="View CNDN" CssClass="btn-primary btn btn-primary" OnClick="btnViewCNDN_Click" />
            <asp:Button ID="btPrint" runat="server" OnClientClick="ShowProgress();" Text="Print CNDN" CssClass="btn-info btn btn-print" OnClick="btPrint_Click" />
            <asp:Button ID="btlookup" runat="server" OnClientClick="ShowProgress();" OnClick="btlookup_Click" Text="Button" Style="display: none" />
            <asp:Button ID="bttax" runat="server" OnClientClick="ShowProgress();" OnClick="bttax_Click" Text="Button" Style="display: none" />
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


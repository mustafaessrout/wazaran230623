<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqreturnexpired.aspx.cs" Inherits="fm_reqreturnexpired" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            <%--$get('<%=btsearchcust.ClientID%>').click();--%>
            $('#<%=txcustomer.ClientID%>').addClass("form-control ro");

        }
        function InvSelected(sender, e) {
            $get('<%=hdinvoice.ClientID%>').value = e.get_value();
            $get('<%=btinvselect.ClientID%>').click();
            $('#<%=txinvoicesearch.ClientID%>').addClass("form-control ro");

        }

        function EmpSelected(sender, e) {
          <%--  $get('<%=hdsalesman.ClientID%>').value = e.get_value();--%>
            $get('<%=hdemp.ClientID%>').value = e.get_value();
            $('#<%=txsalesman.ClientID%>').addClass('form-control ro');
            $get('<%=btemp.ClientID%>').click();
        }

        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $('#<%=txitemsearch.ClientID%>').addClass("form-control ro");
            $get('<%=cbuom.ClientID%>').value = '';
            $get('<%=btcheckprice.ClientID%>').click();
        }

        function SelectReturn(sRetNo) {
            <%--$get('<%=hdreturno.ClientID%>').value = sRetNo;--%>
            <%--$get('<%=btrefresh.ClientID%>').click();--%>

        }

        function ConfirmDelete() {
            var i = confirm('Do you want delete retur ?');
            if (i == true) {
                <%--$get('<%=btcancel2.ClientID%>').click();--%>
            }
        }

        function DateSelect() {
            alert('test saja');
        }
        function getsubtotal() {
            $('#<%=btsubtotal.ClientID%>').click();
        }

        function ReturSelected(val) {
            $get('<%=hdreturno.ClientID%>').value = val;
            $get('<%=btreturn.ClientID%>').click();
        }

        $(document).ready(function () {
            $('#<%=txqty.ClientID%>').attr('onchange', 'getsubtotal()');
        });
    </script>
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <asp:HiddenField ID="hdinvoice" runat="server" />
        <asp:HiddenField ID="hditem" runat="server" />
        <asp:HiddenField ID="hdcust" runat="server" />
        <asp:HiddenField ID="hdsalesman" runat="server" />
        <asp:HiddenField ID="hdemp" runat="server" />
        <asp:HiddenField ID="hdreturno" runat="server" />
        <h4 class="jajarangenjang">Request Return Expired Product</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Type</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="rdreturtype" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdreturtype_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Request No</label>
                <div class="col-md-2">
                    <div class="input-group">
                        <asp:TextBox ID="txreqno" CssClass="form-control input-group-sm ro" runat="server">NEW</asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label col-md-1">Date</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="dtrequest" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtrequest_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtrequest">
                    </asp:CalendarExtender>
                </div>
                <label class="control-label col-md-1">Man No</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txmanualno" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

            </div>
            <div class="row margin-bottom">
                <%--<label class="control-label col-md-1">Cost</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbreturcost" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>--%>
                <label class="control-label col-md-1">Cust</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txcustomer" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" MinimumPrefixLength="1" ServiceMethod="GetCompletionList " UseContextKey="true" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="CustSelected" runat="server" TargetControlID="txcustomer">
                    </asp:AutoCompleteExtender>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Remark</label>
                <div class="col-md-5 drop-down">
                    <asp:DropDownList ID="cbremark" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Driver</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbdriver" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <label class="control-label col-md-1">Approval</label>
                <div class="col-md-2 drop-down">
                    <asp:TextBox ID="txapproval" CssClass="form-control ro" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <table class="mGrid">
                        <tr>
                            <th style="width: 10%">Item</th>
                            <th style="width: 10%">Invoice</th>
                            <th style="width: 5%">Qty Avl Rtn</th>
                            <th style="width: 10%;">UOM</th>
                            <th style="width: 5%">Qty</th>
                            <th style="width: 5%">Unit Price</th>
                            <th style="width: 5%">Salesman Unit Price</th>
                            <th style="width: 5%">Sub Total </th>
                            <th style="width: 5%">VAT</th>
                            <th style="width: 10%">Expired Date</th>
                            <th style="width: 5%">Status</th>
                            <th style="width: 10%">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbwhs" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </th>
                            <th style="width: 10%">Bin</th>
                            <th style="width: 10%">VAT</th>
                            <th>Add</th>
                            <th>Reset</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txitemsearch" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetCompletionList2" UseContextKey="True" OnClientItemSelected="ItemSelected" CompletionListElementID="divitem">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txinvoicesearch" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:AutoCompleteExtender OnClientItemSelected="InvSelected" ID="txinvoicesearch_AutoCompleteExtender" UseContextKey="true" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetInvoiceList" CompletionSetCount="1" runat="server" TargetControlID="txinvoicesearch">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbqtyavl" CssClass="form-control" runat="server"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbuom" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control" onchange="javascript:ShowProgress();" AutoPostBack="True" OnSelectedIndexChanged="cbuom_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txqty_TextChanged"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbprice" runat="server" CssClass="control-label" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbuom" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txcustprice" runat="server" CssClass="form-control" OnTextChanged="txcustprice_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="grd" EventName="SelectedIndexChanging" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbtotprice" runat="server" CssClass="control-label" Font-Size="X-Large" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txqty" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbvat" runat="server" Text="0" Font-Size="X-Large" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>

                            <td>
                                <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="dtexp" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="dtexp_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="dtexp_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtexp">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbexp" Text="EXPIRED" runat="server" CssClass="form-control input-sm text-primary text-bold block padding-top-4"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dtexp" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbwhs" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rdreturtype" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbbin" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td class="drop-down">
                                <asp:DropDownList ID="cbvat" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbvat_SelectedIndexChanged">
                                    <asp:ListItem Value="1">With VAT</asp:ListItem>
                                    <asp:ListItem Value="0">Non VAT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btadd" OnClientClick="ShowProgress();" runat="server" Text="Add" CssClass="btn btn-success" OnClick="btadd_Click" /></td>
                            <td>
                                <asp:Button ID="btReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btReset_Click" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" OnRowDataBound="grd_RowDataBound" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowDeleting="grd_RowDeleting" GridLines="None" ShowFooter="True" OnSelectedIndexChanging="grd_SelectedIndexChanging" CssClass="mGrid">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdvat" Value='<%#Eval("isvat")%>' runat="server" />
                                            <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branded">
                                        <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv">
                                        <ItemTemplate>
                                            <asp:Label ID="lbinvno" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lbunitprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txunitprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcustprice" runat="server" Text='<%# Eval("custprice") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotcustprice" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subtotal">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsubtotal" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT">
                                        <ItemTemplate>
                                            <asp:Label ID="lbvat" runat="server" Text='<%#Eval("vat")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotvat" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lbuom" runat="server" Text='<%# Eval("uom") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exp">
                                        <ItemTemplate>
                                            <asp:Label ID="lbexp" runat="server" Text='<%# Eval("exp_dt","{0:d/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcondition" runat="server" Text='<%# Eval("condition") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Whs/Vhc">
                                        <ItemTemplate>
                                            <asp:Label ID="lbwhs" runat="server" Text='<%# Eval("whs_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Bin">
                                        <ItemTemplate>
                                            <asp:Label ID="lbbin" runat="server" Text='<%# Eval("bin_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" BackColor="Yellow" Font-Bold="True" Font-Size="X-Large" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle />
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
            <div class="row margin-bottom">
                <div class="col-md-6">
                    <table class="mGrid table-title-sm">
                        <tr>
                            <th>Employee</th>
                            <th>Job Title</th>
                            <th>Amount</th>
                            <th>Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txsalesman" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txsalesman_AutoCompleteExtender" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="1" UseContextKey="true" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetEmployeeList" OnClientItemSelected="EmpSelected" runat="server" TargetControlID="txsalesman">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:Label ID="lbjobtitle" CssClass="control-label" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txempamt" CssClass="form-control" Width="5em" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton ID="btaddamt" CssClass="btn btn-primary" runat="server" OnClick="btaddamt_Click">Add</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-6">
                    <asp:GridView ID="grdemp" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdemp_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Emp Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbempcode" runat="server" Text='<%#Eval("emp_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <%#Eval("emp_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Title">
                                <ItemTemplate><%#Eval("job_title_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate><%#Eval("amt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="row margin-bottom">
                <label class="col-md-1">Remarks</label>
                <div class="col-sm-6">
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="row margin-bottom">
                <label class="col-md-1">Document Supported</label>
                <div class="col-sm-2">
                    <asp:FileUpload ID="upl" runat="server"></asp:FileUpload>
                    <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                    <asp:Label ID="lblocfile" runat="server" Text='SalesReturn Document'></asp:Label></asp:HyperLink>
                </div>
            </div>

            <div class="h-divider"></div>
            <div class="row center">
                <asp:Button ID="btprice" runat="server" Text="Button" OnClick="btprice_Click" Style="display: none" />
                <asp:Button ID="btsubtotal" runat="server" OnClick="btsubtotal_Click" Text="Button" Style="display: none" />
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary" runat="server" OnClick="btnew_Click">New</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="btsave" CssClass="btn btn-info" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="btprint" CssClass="btn btn-success" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>&nbsp;
                <asp:Button ID="btsearchcust" runat="server" Text="Button" Style="display: none" />
                <asp:Button ID="btreturn" runat="server" OnClick="btreturn_Click" Text="Button" Style="display: none" />
                <asp:Button ID="btemp" runat="server" OnClick="btemp_Click" style="display:none" OnClientClick="javascript:ShowProgress();" Text="Button" />
                <asp:Button ID="btcheckprice" runat="server" OnClick="btcheckprice_Click" OnClientClick="javascript:ShowProgress();" Text="Button" Style="display: none" />
                <asp:Button ID="btinvselect" runat="server" Style="display: none" OnClick="btinvselect_Click" Text="Button" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


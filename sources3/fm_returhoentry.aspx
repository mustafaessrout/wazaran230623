<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_returhoentry.aspx.cs" Inherits="fm_returhoentry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function Left(str, n) {
            if (n <= 0)
                return "";
            else if (n > String(str).length)
                return str;
            else
                return String(str).substring(0, n);
        }

        function Right(str, n) {
            if (n <= 0)
                return "";
            else if (n > String(str).length)
                return str;
            else {
                var iLen = String(str).length;
                return String(str).substring(iLen, iLen - n);
            }
        }
        function Len(string_variable) {

            return string_variable.length;

        }
        function Search(str, p) {
            return str.search(p);
        }
    </script>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_returHO.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=txqty.ClientID%>').focus();
        }

        function EmpSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();
            $get('<%=btemp.ClientID%>').click();
        }
    </script>
    <script>
        function Selectedinvoice_no(sender, e) {
            $get('<%=hdinvoice_no.ClientID%>').value = e.get_value();
        }
    </script>
    <script>
        function SetContextKey() {
            if ($get("<%=cbreturho_type.ClientID%>").value == "REG") {
                $find('<%=txitemcode_AutoCompleteExtender.ClientID%>').set_contextKey($get("<%=hdemp.ClientID%>").value);
            }
            else {
                $find('<%=txitemcode_AutoCompleteExtender.ClientID%>').set_contextKey($get("<%=hdinvoice_no.ClientID%>").value);
            }
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
     //$(document).ready(function () {
     //    $('#pnlmsg').hide();
     //});

    </script>
    <style>
        .status.success {
            background-color: #4cae4c !important;
        }

        .status.warning {
            background-color: #eea236 !important;
        }

        .status.danger {
            background-color: #f44455 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4 class="jajarangenjang">Return To HO</h4>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <div>
                    <label class="col-sm-2 control-label titik-dua">Return No.</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txreturno" runat="server" CssClass="makeitreadonly ro form-control input-sm" Enabled="false"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:Button ID="btsearch" runat="server" CssClass="btn btn-primary btn-sm btn-search" Text="Search" OnClientClick="openwindow();return(false);" />
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" Style="display: none" />
                    </div>
                </div>

                <div>
                    <label class="col-sm-2 control-label titik-dua">Manual Return No.</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txreturho_manual_no" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <label class="col-sm-2 control-label titik-dua">Date</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" class="drop-down-date">
                            <ContentTemplate>
                                <asp:TextBox ID="dtretur" runat="server" CssClass="makeitreadonly form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dtretur_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtretur">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <label class="col-sm-2 control-label titik-dua">Driver Name</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txdriver_nm" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <label class="col-sm-2 control-label titik-dua">Warehouse</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbwhs_cd" runat="server" OnSelectedIndexChanged="cbwhs_cd_SelectedIndexChanged" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <label class="col-sm-2 control-label titik-dua">Vihicle No.</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txvehicle_no" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <label class="col-sm-2 control-label titik-dua">Bin</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbbin_cd" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <label class="col-sm-2 control-label titik-dua">Phone No.</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txphone_no" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel13" runat="server" class="clearfix">
                    <ContentTemplate>
                        <div>
                            <label class="col-sm-2 control-label titik-dua">Return Type Type</label>
                            <div class="col-sm-4 margin-bottom">
                                <div class="drop-down">
                                    <asp:DropDownList ID="cbreturho_type" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="cbreturho_type_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:Label ID="lbRTmark" runat="server" CssClass="text-red col-sm-6" Text="regular return should be approve by product supervisor"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div>
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" class="col-sm-2 no-padding titik-dua">
                        <ContentTemplate>
                            <asp:Label ID="lbempInv" runat="server" CssClass=" control-label " Text="Product Supervisor"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdemp" runat="server" />
                                <asp:TextBox ID="txemployee" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txemployee_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="EmpSelected" ServiceMethod="GetCompletionList" TargetControlID="txemployee" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hdinvoice_no" runat="server" />
                                <asp:TextBox ID="txinvoice_no" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txinvoice_no_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txinvoice_no_AutoCompleteExtender" runat="server" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" TargetControlID="txinvoice_no" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="Selectedinvoice_no" ServiceMethod="GetCompletionListinvoice_no" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <label class="col-sm-2 control-label titik-dua">Reason</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbreason" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <%-- <div>
                    <label class="col-sm-2 control-label titik-dua">Amount Available</label>
                    <div class="col-sm-4 margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:Label ID="lbamount" runat="server" Text="0" CssClass="control-label-sm" style="background-color:silver"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>--%>
            </div>

            <div class="h-divider"></div>

            <div>
                <table class="table table-striped mygrid">
                    <tr>
                        <th>Item Name</th>
                        <th>UOM</th>
                        <th>Stk Avl</th>
                        <th>Qty
                        </th>

                        <th>Price</th>
                        <th>Amount</th>
                        <th>Expiry Date
                        </th>
                        <th>Good Status
                        </th>
                        <th></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="hditemPnl">
                                        <asp:HiddenField ID="hditem" runat="server" />
                                        <asp:TextBox ID="txitemcode" runat="server" AutoPostBack="True" OnKeyUp="SetContextKey()" OnTextChanged="txitemcode_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txitemcode_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ServiceMethod="GetItemList" TargetControlID="txitemcode" UseContextKey="True">
                                        </asp:AutoCompleteExtender>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 100px;" class="drop-down">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="cbuomPnl">
                                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbuom_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txstkavl" type="number" runat="server" CssClass="form-control input-sm text-primary"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txqty_TextChanged"></asp:TextBox>
                                   <%-- <asp:FilteredTextBoxExtender ID="txqty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txqty" FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>

                        <td>
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txRetHO_price" runat="server" type="number" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txRetHO_price_TextChanged"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txRetHO_Amount" runat="server" CssClass="form-control input-sm makeitreadonly" Enabled="False"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>

                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="dtexpiry" runat="server" AutoPostBack="True" OnTextChanged="dtexpiry_TextChanged" CssClass="form-control input-sm "></asp:TextBox>
                                    <asp:CalendarExtender ID="dtexpiry_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtexpiry">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbstatus" runat="server" Style="line-height: 12px;" CssClass="form-control input-sm text-white"></asp:Label>
                                            <asp:DropDownList ID="cbsubstatus" CssClass="form-control input-sm " runat="server" Visible="False">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dtexpiry" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </td>
                        <td>
                            <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-success btn btn-sm btn-add" OnClick="btadd_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="divgrid">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" OnRowDeleting="grd_RowDeleting" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" CellPadding="0" GridLines="None" ShowFooter="True" OnRowDataBound="grd_RowDataBound">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="txseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <%# Eval("item_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate>
                                        <asp:Label ID="txbranded_nm" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="size">
                                    <ItemTemplate>
                                        <asp:Label ID="txsize" runat="server" Text='<%# Eval("size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stk Avl">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblstkavl" runat="server" Text='<%# Eval("stkavl") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtstkavl" runat="server" Text='<%# Eval("stkavl") %>' CssClass="form-control input-sm" type="number"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm" type="number"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="drop-down-sm">
                                            <asp:DropDownList ID="cboUOM" runat="server" Width="90px" CssClass="form-control input-sm"></asp:DropDownList>
                                        </div>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <%# Eval("RetHO_price") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRetHO_price" runat="server" Text='<%# Eval("RetHO_price") %>' CssClass="form-control input-sm" type="number"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbRetHO_Amount" runat="server" Text='<%# Eval("RetHO_Amount") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalRetHO_Amount" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expire Date">
                                    <ItemTemplate>
                                        <%# Eval("exp_dt","{0:d/M/yyyy}") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtexp_dt" runat="server" Text='<%# Eval("exp_dt","{0:d/M/yyyy}") %>' CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="dtretur_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="txtexp_dt" PopupPosition="TopLeft">
                                        </asp:CalendarExtender>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="txitem_status" runat="server" Text='<%# Eval("item_status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Condition">
                                    <ItemTemplate>
                                        <%# Eval("item_cond_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbIDS" runat="server" Text='<%# Eval("IDS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
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

            <div class="navi margin-bottom margin-top">
                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnew" runat="server" CssClass="btn-success btn btn-add" OnClick="btnew_Click" Text="NEW" />
                        <asp:Button ID="btsave" runat="server" OnClientClick="javascript:ShowProgress();" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" Text="Save" />
                        <asp:Button ID="btDelete" runat="server" CssClass="btn btn-danger btn-delete" OnClick="btDelete_Click" Text="Delete" Visible="False" />
                        <asp:Button ID="btprint" runat="server" CssClass="btn btn-info btn-print" OnClick="btprint_Click" Text="Print" />
                        <asp:Button ID="btemp" runat="server" OnClick="btemp_Click" Text="Button" Style="display: none" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>

    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>

</asp:Content>


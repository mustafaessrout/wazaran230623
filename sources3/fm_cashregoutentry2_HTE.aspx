<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashregoutentry2_HTE.aspx.cs" Inherits="fm_cashregoutentry2_HTE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <style>
        .completionListX
        {
          width: auto !important;    
        }
    </style>
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
    <script>
        function SuppSelected(sender, e) {
            $get('<%=hdtax.ClientID%>').value = e.get_value();
            $get('<%=bt.ClientID%>').click();
        }

        function LookupSupplierSelected(sender, e) {
            $get('<%=hdlookuptax.ClientID%>').value = e.get_value();

        }
    </script>
    <script>
        function EmpSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();
        }
        function lookupitemcashoutpurposeSelected(sender, e) {
            $get('<%=hdpurpose.ClientID%>').value = e.get_value();
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
        function EmpAdvSelected(sender, e) {
            $get('<%=hdempadv.ClientID%>').value = e.get_value();
        }

        function DataSelected(sVar) {
            $get('<%=hdcashout.ClientID%>').value = sVar;
            //var bal = GetBalance(sVar);
            //window.alert(bal);
           
            $get('<%=btrefresh.ClientID%>').click();
        }
        function SelectPcPIC() {
            $get('<%=btPcPIC.ClientID%>').click();
        }

        //function GetBalance(sVar) {
        //    var balancePCO;
        //    $.ajax({
        //        type: "POST",
        //        url: "fm_cashregoutentry2.aspx/GetBalancePerCashOut",
        //        data: "{'sVar':'" + sVar + "'}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (response) {
        //            balancePCO = response.d;
        //        },
        //        error: function (jqXHR, textStatus, errorThrown) {
        //            $("#output").append("<li>error " + response.d + "<li>");
        //        },
        //        async: false
        //    });
        //    return balancePCO;
        //};
    </script>
<%--    <script>
        $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
                xRefreshPage();
                //PopupCenter('lookupcashout.aspx', 'xtf', '900', '500');
            });
        });

        function xRefreshPage() {
            PageMethod.sbtsysno_RefreshPage();
        };
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdemp" runat="server" />
            <asp:HiddenField ID="hdtax" runat="server" />
            <asp:HiddenField ID="hdcust" runat="server" />
            <asp:HiddenField ID="hdcashout" runat="server" />
            <asp:HiddenField ID="hditemco" runat="server" />
            <asp:HiddenField ID="hdlookuptax" runat="server" />
            <asp:HiddenField ID="hdpurpose" runat="server" />
            <asp:HiddenField ID="hdempadv" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Cash Out Request for Employee Advance</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Sys No</label>
                <div class="col-md-2">
                    <div class="input-group">
                        <asp:Label ID="txsysno" placeholder="Auto Number" CssClass="form-control ro input-group-sm" runat="server"></asp:Label>

                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsysno_Click"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label col-md-1">Out</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbinout" onchange="javascript:ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinout_SelectedIndexChanged">
                        <asp:ListItem  runat="server" AutoPostBack="True" Selected="True" Value="O" Text="HO To Employee"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Routine</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbroutine" CssClass="form-control" runat="server" AutoPostBack="True" onchange="javascript:ShowProgress();" OnSelectedIndexChanged="cbroutine_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Date</label>
                <div class="col-md-2 drop-down-date">
                    <asp:TextBox ID="dtcashout" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtcashout_CalendarExtender" runat="server" TargetControlID="dtcashout" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div id="yellowLine1" runat="server" class="row margin-bottom" style="background-color: yellow">
                <label class="control-label col-md-1">Type</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbcategory" runat="server" CssClass="form-control" AutoPostBack="True" onchange="ShowProgress();" OnSelectedIndexChanged="cbcategory_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Item CO</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbitem" runat="server" onchange="javascript:ShowProgress();" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cbitem_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1" id="lbCashoutInfo" runat="server">Cashout Info</label>
                <div class="col-md-5">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdattr" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="No attribute found" ShowHeaderWhenEmpty="True" OnRowDataBound="grdattr_RowDataBound" OnRowEditing="grdattr_RowEditing" OnRowUpdating="grdattr_RowUpdating" OnRowCancelingEdit="grdattr_RowCancelingEdit">
                                <Columns>
                                    <asp:TemplateField HeaderText="Attribute Name">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="IDS" Value='<%#Eval("IDS") %>' runat="server" />
                                            <asp:Label ID="lbattribute" runat="server" Text='<%#Eval("attribute_nm") %>' CssClass="control-label-sm"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lbvalue" runat="server" Text='<%#Eval("attributevalue") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txvalue" Text='<%#Eval("attributevalue") %>' runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="clvalue" runat="server" TargetControlID="txvalue"></asp:CalendarExtender>
                                            <asp:DropDownList ID="cbvalue" runat="server"></asp:DropDownList>
                                            <asp:TextBox ID="txvalue2" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txvalue3" runat="server"></asp:TextBox>
                                            <asp:AutoCompleteExtender UseContextKey="true" ID="AutoCompleteExtender_txvalue3" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="CustSelected" TargetControlID="txvalue3" ServiceMethod="GetCompletionList3" runat="server"></asp:AutoCompleteExtender>
                                            <asp:TextBox ID="txvalueemp" runat="server"></asp:TextBox>
                                            <asp:AutoCompleteExtender UseContextKey="true" ID="AutoCompleteExtender_txvalueemp" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="EmpAdvSelected" TargetControlID="txvalueemp" ServiceMethod="GetCompletionList" runat="server"></asp:AutoCompleteExtender>
                                            <asp:TextBox ID="txlookupsupplier" runat="server"></asp:TextBox>
                                            <asp:AutoCompleteExtender UseContextKey="true" ID="AutoCompleteExtender_txlookupsupplier" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="LookupSupplierSelected" TargetControlID="txlookupsupplier" ServiceMethod="GetCompletionListSup" runat="server"></asp:AutoCompleteExtender>
                                            <asp:TextBox ID="txlookupitemcashoutpurpose" runat="server" Height="50"></asp:TextBox>
                                            <asp:AutoCompleteExtender UseContextKey="true" ID="AutoCompleteExtender_txlookupitemcashoutpurpose" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="lookupitemcashoutpurposeSelected" TargetControlID="txlookupitemcashoutpurpose" ServiceMethod="GetCompletionListItemcashoutpurpose" runat="server" CompletionListCssClass="completionListX"></asp:AutoCompleteExtender>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbitem" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-md-1 align-right">Manual No</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txmanualno" CssClass="form-control" runat="server" placeholder="Enter Manual No"></asp:TextBox>
                </div>
                <label class="control-label col-md-1 align-right">Remark</label>
                <div class="col-md-8 require">
                    <asp:TextBox ID="txremark" CssClass="form-control" runat="server" placeholder="Enter Remark Cashout"></asp:TextBox>

                </div>
            </div>

            <div class="row margin-bottom">
                <label class="control-label col-md-1">Reason</label>
                <div class="col-md-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbremark" runat="server" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbitem" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Approval</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbapproval" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1" id="lbPIC" runat="server">PIC</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txpic" runat="server" CssClass="form-control small" placeholder="PIC"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txpic_AutoCompleteExtender" OnClientItemSelected="EmpSelected" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" ContextKey="true" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txpic" UseContextKey="True">
                    </asp:AutoCompleteExtender>
                </div>
                <label class="control-label col-md-1">Dept</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbdept" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Amount</label>
                <div class="col-md-2 require">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txamt" CssClass="form-control require" runat="server" placeholder="Amount" OnTextChanged="txamt_TextChanged"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chvat" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">VAT included</label>
                <div class="col-md-2 drop-down">
                    <div class="input-group">
                        <div class="input-group-sm">
                            <asp:DropDownList ID="chvat" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chvat_SelectedIndexChanged">
                                <asp:ListItem Value="NONVAT">NON VAT</asp:ListItem>
                                <asp:ListItem Value="VAT">WITH VAT</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="input-group-sm">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbvat" CssClass="control-label" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chvat" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <label class="control-label col-md-1">Supplier</label>
                <div class="col-md-5">
                    <table class="mGrid">
                        <tr>
                            <th>Supplier Tax No:Suppler Name</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtaxno" runat="server"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txtaxno_AutoCompleteExtender" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="SuppSelected" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txtaxno" UseContextKey="True">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="ClaimCashout" runat="server" visible="false">
                    <label class="control-label col-md-1">Paid Claim Cashout</label>
                    <div class="col-md-2 drop-down">
                            <asp:DropDownList ID="ddlClaimCashout" CssClass="form-control" runat="server">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                 </div>
            </div>
<%--            <div id="yellowLine2" runat="server" class="row margin-bottom" style="background-color: yellow">
                <div class="col-md-2">
                    LAST BALANCE CASHIER
                </div>
                <div class="col-md-2">
                    <strong style="color: red; font-size: large">
                        <asp:Label ID="lbcashier" CssClass="control-label" runat="server" Text=""></asp:Label>
                    </strong>
                </div>
            </div>--%>
<%--            <div id="yellowLine3" runat="server" class="row margin-bottom" style="background-color: yellow">
                <div class="col-md-2">
                    BALANCE SELECTED CASH IN / OUT
                </div>
                <div class="col-md-2">
                    <strong style="color: red; font-size: large">
                        <asp:Label ID="lbbalanceco" CssClass="control-label" runat="server" Text=""></asp:Label>
                    </strong>
                </div>
                <label class="control-label col-md-1">Status</label>
                <div class="col-md-7" style="text-align: left">
                    <asp:Label ID="lbstatus" Font-Bold="true" Font-Size="X-Large" ForeColor="Red" runat="server" Text=""></asp:Label>
                </div>
            </div>--%>
            <div class="row margin-bottom">
                <label class="control-label col-md-1">File</label>
                <div class="col-md-2">
                    <asp:FileUpload ID="fucashout" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="h-divider"></div>
            <div class="row margin-bottom" style="text-align: center">
                <div class="col-md-12" style="text-align: center">
                    <asp:LinkButton ID="New" CssClass="btn btn-primary" runat="server" OnClick="New_Click"><i class="fa fa-plus">&nbsp;New</i></asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn btn-success" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn btn-warning" runat="server" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>

                    <asp:Button ID="bt" runat="server" OnClick="bt_Click" Text="Button" Style="display: none" />
                    <asp:Button ID="btrefresh" runat="server" Style="display: none" OnClick="btrefresh_Click" Text="Button" />
                    <asp:Button ID="btPcPIC" runat="server" Style="display: none" OnClick="btPcPIC_Click" Text="Button" />
                </div>
            </div>
        </div>
    </div>
    <div id="output">
        <%--tes--%>
    </div>
    <div class="divmsg loading-cont" style="display: none" id="pnlmsg">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


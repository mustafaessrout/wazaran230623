<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashregout_pettycash.aspx.cs" Inherits="fm_cashregout_pettycash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <style>
        .completionListX {
            width: auto !important;
        }

        .label {
            margin-bottom: 0px;
            font-weight: 700;
            display: flex;
            justify-content: space-between;
            width: 100%;
            align-items: center;
            text-align: center;
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
            $get('<%=btsearchPlate.ClientID%>').click();
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
            //document.getElementById('<%=lbbalanceco.ClientID %>').value = bal;
            $get('<%=btrefresh.ClientID%>').click();
        }
        function SelectPcPIC() {
            $get('<%=btPcPIC.ClientID%>').click();
        }
        function SelectData(x) {
            $get('<%=hdcashout.ClientID%>').value = x;
            //var bal = GetBalance(sVar);
            //window.alert(bal);
            //document.getElementById('<%=lbbalanceco.ClientID %>').value = bal;
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
    <script>
        function v_sendmessage(phoneno, msg) {

            var DTO = { 'phone': phoneno, 'body': msg };
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: 'https://eu36.chat-api.com/instance103604/sendMessage?token=kkfkbdouwv4ecmgk',
                data: JSON.stringify(DTO),
                datatype: 'json',
                success: function (result) {
                    //do something
                    //    window.alert('SUCCESS = ' + result.phone);
                    //console.log(result);
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    //     sweetAlert(textstatus);
                    //console.log("error: " + errorthrown);
                }
            });//end of $.ajax()

        }

        function v_sendfile(phoneno, msg, filename) {

            var DTO = { 'phone': phoneno, 'body': msg, 'filename': filename };
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: 'https://eu36.chat-api.com/instance103604/sendFile?token=kkfkbdouwv4ecmgk',
                data: JSON.stringify(DTO),
                datatype: 'json',
                success: function (result) {
                    //do something
                    //    window.alert('SUCCESS = ' + result.phone);
                    //console.log(result);
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    sweetAlert(textstatus);
                }
            });//end of $.ajax()
        }
    </script>
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

    <div class="alert alert-info"><strong>Petty Cash Request</strong></div>
    <div class="container">
        <div class="form-horizontal">
            <div class="form-group">
                <label class="control-label col-sm-1 input-sm">Cashier</label>
                <div class="col-sm-4 drop-down">
                    <asp:DropDownList ID="cbcashregister" onchange="javascript:ShowProgress();" CssClass="form-control input-sm" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="cbcashregister_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-2">
                    Select cashier pettycash in branch
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-1 input-sm">Sys No</label>
                <div class="col-sm-2">
                    <div class="input-group-sm input-group">
                        <asp:Label ID="txsysno" placeholder="Auto Number" CssClass="input-group-sm input-sm form-control" runat="server"></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btsysno_Click"><span class="fa fa-search"></span></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label col-sm-1 input-sm">In Out</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbinout" onchange="javascript:ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinout_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <label class="control-label col-sm-1 input-sm">Routine</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbroutine" CssClass="form-control input-sm" runat="server" AutoPostBack="True" onchange="javascript:ShowProgress();" OnSelectedIndexChanged="cbroutine_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-sm-1 input-sm">Date</label>
                <div class="col-sm-2 drop-down-date">
                    <asp:TextBox ID="dtcashout" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtcashout_CalendarExtender" runat="server" TargetControlID="dtcashout" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="row">
                <label class="control-label col-sm-1 input-sm">Type</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbcategory" runat="server" CssClass="form-control input-sm" AutoPostBack="True" onchange="ShowProgress();" OnSelectedIndexChanged="cbcategory_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-sm-1 input-sm">Item CO</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbitem" runat="server" onchange="javascript:ShowProgress();" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="cbitem_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Label Font-Bold="true" ForeColor="Red" Font-Size="Larger" ID="EmployeeAdvanceOutNote" runat="server">(OHDA)</asp:Label>
                    <asp:Label Font-Bold="true" ForeColor="Red" Font-Size="Larger" ID="EmployeeAdvanceInNote" runat="server">(Return OHDA)</asp:Label>
                </div>
                <label class="control-label col-sm-1 input-sm" id="lbCashoutInfo" runat="server">Petty Cashout Info</label>
                <div class="col-sm-5">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdattr" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="No attribute found"
                                ShowHeaderWhenEmpty="True" OnRowDataBound="grdattr_RowDataBound" OnRowEditing="grdattr_RowEditing" OnRowUpdating="grdattr_RowUpdating"
                                OnRowCancelingEdit="grdattr_RowCancelingEdit">
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
             <%--   <label class="control-label col-sm-1 align-right input-sm">Remark</label>
                <div class="col-sm-2 require drop-down">
                    <asp:DropDownList ID="cbsubitem" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                </div>--%>
                <label class="control-label col-sm-1 align-right input-sm">Ref No</label>
                <div class="col-sm-2 require">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txmanualno" CssClass="form-control input-sm" runat="server" placeholder="Enter Manual No"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1 align-right input-sm">Remark</label>
                <div class="col-sm-8 require">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server" placeholder="Enter Remark Pettycash"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1 input-sm">Approval</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbapproval" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1 input-sm" id="lbPIC" runat="server">PIC</label>
                <div class="col-sm-2 require">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txpic" runat="server" CssClass="form-control small input-sm" placeholder="PIC"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txpic_AutoCompleteExtender" OnClientItemSelected="EmpSelected" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" ContextKey="true" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txpic" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1 input-sm">For Dept</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbdept" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
               <%-- <label class="control-label col-sm-1 input-sm">Account Department</label>
                <div class="col-sm-2 drop-down require">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbacctdept" CssClass="form-control input-sm" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>--%>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1 input-sm">VAT Rate</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbvatrate" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbvatrate_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1 input-sm">Supplier</label>
                <div class="col-sm-2">
                    <div class="input-group">
                        <div class="input-group-sm">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtaxno" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:AutoCompleteExtender CompletionListCssClass="input-sm" ID="txtaxno_AutoCompleteExtender" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="SuppSelected" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txtaxno" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdatesupplier" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btupdatesupplier_Click" runat="server">NAV Update</asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1 input-sm">Amount (Inclusive VAT)</label>
                <div class="col-sm-2 require">
                    <div class="input-group-sm require">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamt" CssClass="form-control input-sm" onchange="ShowProgress();" runat="server" placeholder="Amount" AutoPostBack="true" OnTextChanged="txamt_TextChanged"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <label class="control-label col-sm-1 input-sm">VAT Amt</label>
                <div class="col-sm-2 input-group-sm">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="lbvat" AutoPostBack="true" runat="server" onchange="ShowProgress();" CssClass="form-control input-sm" Text="0" Font-Bold="True" Font-Size="Large" ForeColor="Red" OnTextChanged="lbvat_TextChanged"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1 input-sm">Based Amountt</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txbasedprice" CssClass="form-control ro input-sm" runat="server" placeholder="Total"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>


            <%-- <div class="row margin-bottom">
            </div>--%>
            <div class="alert alert-info">

                <div class="col-sm-2">
                    LAST BALANCE PETTY CASHIER
                </div>
                <div class="col-sm-2">
                    <strong style="color: red; font-size: large">
                        <asp:Label ID="lbcashier" CssClass="control-label input-lg" runat="server" Text=""></asp:Label>
                    </strong>
                </div>
            </div>
            <div class="alert alert-info">
                <div class="col-sm-2">
                    BALANCE SELECTED PETTY CASH IN / OUT
                </div>
                <div class="col-sm-2">
                    <strong style="color: red; font-size: large">
                        <asp:Label ID="lbbalanceco" CssClass="control-label" runat="server" Text=""></asp:Label>
                    </strong>
                </div>
                <label class="control-label col-sm-1">Status</label>
                <div class="col-sm-7" style="text-align: left">
                    <asp:Label ID="lbstatus" Font-Bold="true" Font-Size="Large" ForeColor="Red" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1">File</label>
                <div class="col-sm-2">
                    <asp:FileUpload ID="fucashout" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="h-divider"></div>
            <div class="row margin-bottom" style="text-align: center">
                <div class="col-sm-12" style="text-align: center">
                    <asp:LinkButton ID="New" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="New_Click">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn btn-success btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn btn-warning btn-sm" runat="server" OnClientClick="ShowProgress();" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:Button ID="bt" runat="server" OnClick="bt_Click" Text="Button" Style="display: none" />
                    <asp:Button ID="btrefresh" OnClientClick="ShowProgress();" runat="server" Style="display: none" OnClick="btrefresh_Click" Text="Button" />
                    <asp:Button ID="btPcPIC" runat="server" OnClientClick="ShowProgress();" Style="display: none" OnClick="btPcPIC_Click" Text="Button" />
                    <asp:Button ID="btsearchPlate" runat="server" Text="Button" onclin="ShowProgress();" OnClick="btsearchPlate_Click" CssClass="divhid" />
                </div>
            </div>
        </div>
    </div>
    <div id="output">
        <%--tes--%>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


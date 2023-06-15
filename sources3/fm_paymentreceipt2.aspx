<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentreceipt2.aspx.cs" Inherits="fm_paymentreceipt2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/styles.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearchcust.ClientID%>').click();
        }

        function InvSelected(sender, e) {
            $get('<%=hdinv.ClientID%>').value = e.get_value();
            $get('<%=btsearchinv.ClientID%>').click();
        }

        function RefreshData(dt) {
            $get('<%=hdpaid.ClientID%>').value = dt;
            $get('<%=btlookup.ClientID%>').click();

        }

        function ItemSelected(sender, e) {

            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btinv.ClientID%>').click();

        }

        function clicksearch(tabno) {
            $get('<%=txtabno.ClientID%>').value = tabno;
           <%-- $get('<%=bttab.ClientID%>').click();--%>
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

    <style type="text/css">
        .divhid {
            display: none;
        }

        body {
            margin: 0;
            padding: 0;
            font-family: Calibri;
        }

        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=1);
            opacity: 0.5;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 140px;
            height: 140px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 128px;
                width: 128px;
            }

        .radio label {
            padding-top: 2px !important;
        }

        .auto-complate-list {
            width: 450px !important;
        }
    </style>


    <style type="text/css">
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            /* Hide default HTML checkbox */
            .switch input {
                display: none;
            }

        /* The slider */
        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 2px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(16px);
            -ms-transform: translateX(16px);
            transform: translateX(16px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 25px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .isDiscount {
            color: green;
            font-size: large;
        }

        .noDiscount {
            color: red;
            font-size: large;
        }

        .divmsg {
            /*position:static;*/
            top: 30%;
            right: 50%;
            left: 50%;
            width: 200px;
            height: 200px;
            position: fixed;
            opacity: 0.9;
            overflow-y: auto;
            /*-webkit-transition: background-color 0;
    transition: background-color 0;*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdpaid" runat="server" />
            <asp:HiddenField ID="hdfilename" runat="server" />
            <asp:HiddenField ID="hdinv" runat="server" />
            <asp:HiddenField ID="hdcust" runat="server" />
            <asp:HiddenField ID="hddisconepct" runat="server" />
            <asp:HiddenField ID="hdfinalcust" runat="server" />
            <asp:HiddenField ID="hdfAutoBalance" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--  <h4 class="jajarangenjang">Payment Received</h4>
    <div class="h-divider"></div>--%>
    <div class="alert alert-info text-bold">Payment Received</div>
    <div class="container">
        <div class="form-horizontal">
            <div class="form-group" style="background-color: yellow">
                <label class="control-label col-sm-1">Source</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsource" onchange="ShowProgress();" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsource_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <label class="control-label col-sm-1">Pay Date</label>
                <div class="col-sm-2 drop-down-date">
                    <asp:TextBox ID="dtpayment" runat="server" CssClass="form-control ro"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtpayment_CalendarExtender" runat="server" TargetControlID="dtpayment" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
                <label class="control-label col-sm-1">P Type</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbpaymnttype" onchange="ShowProgress();" runat="server" CssClass="form-control" AutoPostBack="True" PlaceHolder="Leave it empty, it mandatory on Cleareance" OnSelectedIndexChanged="cbpaymnttype_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <label class="control-label col-sm-1">Doc No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txdocno" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpaymnttype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-1">Tab No</label>
                <div class="col-sm-2">
                    <asp:Panel runat="server" ID="txtabnoPnl" CssClass="input-group">
                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtabno" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:Panel runat="server" ID="bttabnoPnl" CssClass="input-group-btn">
                            <asp:LinkButton ID="bttabno" CssClass="btn btn-primary" runat="server" OnClick="bttabno_Click"><i class="fa fa-search"></i></asp:LinkButton>
                        </asp:Panel>
                    </asp:Panel>
                </div>
                <label class="control-label col-sm-1">Rcp No</label>
                <div class="col-sm-2">
                    <asp:Panel runat="server" ID="txreceiptnoPnl" CssClass="input-group">
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txreceiptno" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Panel runat="server" ID="btreceiptnoPnl" CssClass="input-group-btn">
                            <asp:LinkButton ID="btreceiptno" OnClick="btreceiptno_Click" CssClass="btn btn-primary" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                        </asp:Panel>
                    </asp:Panel>
                </div>
                <label class="control-label col-sm-1">Manual No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txmanualno" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <label class="control-label col-sm-1">Date</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtcheque" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="txpaymentdate_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtcheque">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpaymnttype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-1">Due Dt</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtdue" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="txduedate_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtdue" Format="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpaymnttype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">BankCQ: </label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbbankcq" runat="server" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpaymnttype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>

                <label class="control-label col-sm-1">BankHO</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbbankho" placeholder="Leave it empty" runat="server" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpaymnttype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <label class="control-label col-sm-1">HO REC Date</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dthorec" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" CssClass="date" runat="server" TargetControlID="dthorec" Format="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpaymnttype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>


            </div>
            <div class="form-group">
                <label class="control-label col-sm-1">HO VCR No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txhovoucher" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpaymnttype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Remark</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txrenark" CssClass="form-control" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-sm-1">Paid For</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="rdpaid" CssClass="form-control" OnChange="javascript:ShowProgress();" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdpaid_SelectedIndexChanged">
                                <asp:ListItem Value="C">Customer</asp:ListItem>
                                <asp:ListItem Value="G">Group Customer</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Group</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbgroup" runat="server" CssClass="form-control" AutoPostBack="True" OnChange="javascript:ShowProgress();" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rdpaid" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Cust</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel88" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txcust" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server"
                                ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" CompletionSetCount="10"
                                CompletionInterval="1" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="CustSelected">
                            </asp:AutoCompleteExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Salesman</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="rdpaid" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
          <%--  <h5 class="jajarangenjang">Invoice To Be Paid</h5>
            <div class="h-divider"></div>--%>
            <div class="alert alert-info">Invoice To Be Paid</div>
            <div class="form-group">
                <label class="control-label col-sm-1">Tot Amount Paid</label>
                <div class="col-sm-2 require">
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txamount" CssClass="form-control" runat="server" Font-Bold="True" AutoPostBack="true" OnTextChanged="txamount_TextChanged"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Promise Payment</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbpromised" runat="server" Font-Size="Medium" CssClass="form-control" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Man/Auto</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                        <ContentTemplate>

                            <asp:DropDownList ID="cbmanual" CssClass="form-control" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="cbmanual_SelectedIndexChanged" onchange="javascript:ShowProgress();">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Payment Status (BT/CQ)</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                        <ContentTemplate>

                            <asp:DropDownList ID="ddlPaymentAttribute" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3">
                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                        <ContentTemplate>
                            <i>
                                <asp:CheckBox ID="chAdvance" CssClass="form-control" runat="server" AutoPostBack="true" OnCheckedChanged="chAdvance_CheckedChanged" Text="* Tick This if Payment as Advance!!!" />
                            </i>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                        <ContentTemplate>
                            <table class="mGrid" runat="server" id="tblInvoice">
                                <tr style="background-color: silver">
                                    <th>invoice No</th>
                                    <th>Manual No</th>
                                    <th>Cust Code</th>
                                    <th>System No</th>
                                    <th>Inv Date</th>
                                    <th>Received Date</th>
                                    <th>Total Amt</th>

                                    <th>Temp Balance</th>
                                    <th>Has Paid</th>
                                    <th>Discount 1%</th>
                                    <th>VAT Fr 1%</th>
                                    <th>Switch Disc 1%</th>
                                    <th>Remain</th>
                                    <th>Amount to be paid</th>
                                    <th>Discount Fraction</th>
                                    <th>Round Fraction</th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txsearchinv" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txsearchinv_AutoCompleteExtender" FirstRowSelected="false"
                                                    EnableCaching="false" CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1"
                                                    OnClientItemSelected="InvSelected" runat="server" ServiceMethod="GetCompletionList2"
                                                    TargetControlID="txsearchinv" UseContextKey="True" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover"
                                                    CompletionListItemCssClass="auto-complate-item">
                                                </asp:AutoCompleteExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </td>
                                    <td>
                                        <asp:Label ID="lbmanualno" CssClass="control-label" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCustCD" CssClass="control-label" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbsysno" CssClass="control-label" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbinvoicedate" CssClass="control-label" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbreceived" CssClass="control-label" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbamt" runat="server" CssClass="control-label" Text=""></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lbtempbalance" runat="server" CssClass="control-label" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbhaspaid" CssClass="control-label" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbdisc1pct" CssClass="control-label" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbvatdisc1pct" CssClass="control-label" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td>
                                        <label class="switch-slider" style="position: relative; width: 50px;">
                                            <%--onclick="javascript:ShowProgress();" --%>
                                            <asp:CheckBox ID="chonepct" runat="server"
                                                AutoPostBack="True" OnCheckedChanged="chonepct_CheckedChanged" />
                                            <span class="slider round"></span>
                                        </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbbalance" CssClass="control-label" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txamtpaid" CssClass="form-control" runat="server" placeholder="Enter amount paid"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txdisc" CssClass="form-control" Text="0.00" runat="server" placeholder="Enter discount less than SAR 0.50"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtRound" CssClass="form-control" Text="0.00" runat="server" placeholder="Enter discount less than SAR 0.50"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                    </td>
                                    <td>
                                        <%--OnClientClick="javascript:ShowProgress();" --%>
                                        <asp:LinkButton ID="btadd" OnClientClick="ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btadd_Click">Add Paid</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-12">
                    <asp:Label ID="lbalert" runat="server" Text=""></asp:Label>
                </div>

            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdInvoice" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                                OnRowCancelingEdit="grdInvoice_RowCancelingEdit" OnRowDeleting="grdInvoice_RowDeleting" OnRowEditing="grdInvoice_RowEditing"
                                OnRowUpdating="grdInvoice_RowUpdating" OnSelectedIndexChanging="grdInvoice_SelectedIndexChanging" OnRowDataBound="grdInvoice_RowDataBound"
                                ForeColor="#333333" EmptyDataText="No records Found">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsnumber" runat="server" Text='<%#Eval("snumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_type" runat="server" Text='<%#Eval("paymentfor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Manual no">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmanual_no" runat="server" Text='<%#Eval("manual_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sys No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_no" runat="server" Text='<%#Eval("inv_noID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_dt" runat="server" Text='<%#Eval("inv_dt") %>' DataFormatString="{0:d/M/yyyy}"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblreceived" runat="server" Text='<%#Eval("received_dt") %>' DataFormatString="{0:d/M/yyyy}"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tot Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotamt" runat="server" Text='<%#Eval("totamt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="blbGrndTot" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Temp Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTempbalance" runat="server" Text='<%#Eval("empbalance") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Haspaid">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblhaspaid" runat="server" Text='<%#Eval("haspaid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount 1%">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlbdisc1pct" runat="server" Text="0"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT Fr 1%">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlbvatdisc1pct" runat="server" Text="0"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaing">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlbbalance" runat="server" Text='<%#Eval("balance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotBalance" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Eligible">
                                        <ItemTemplate>
                                            <%-- <%# ((bool)Eval("DiscBit") == true) ? "Red" : Eval("Green") %>--%>
                                            <asp:Label ID="lblIsEligible" runat="server" Text='<%#Eval("DiscBit") %>' CssClass="jajal"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disc1pct">
                                        <ItemTemplate>
                                            <%--onclick="javascript:ShowProgress();"--%>
                                            <label class="switch-slider" style="position: relative; width: 40px;">
                                                <asp:CheckBox ID="grdchonepct" runat="server"
                                                    AutoPostBack="True" OnCheckedChanged="chonepctAutomatic_CheckedChanged" Enabled='<%#Eval("DiscBit") %>' />
                                                <span class="slider round"></span>
                                                <%----%>
                                            </label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount to be paid">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrdamtpaid" runat="server" Text='0'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrdamtpaid" CssClass="form-control" runat="server" placeholder="Enter amount paid"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount Fraction">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrddisc" runat="server" Text='0'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrddisc" CssClass="form-control" runat="server" placeholder="Enter discount less than SAR 1"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" CssClass="table table-striepd mygrid" EmptyDataText="No Invoice Found" ShowHeaderWhenEmpty="True" OnRowDataBound="grd_RowDataBound" ShowFooter="True" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowDeleting="grd_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Cust">
                                        <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sys No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv No">
                                        <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Date">
                                        <ItemTemplate><%# Eval("inv_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rcvd Date">
                                        <ItemTemplate><%# Eval("received_dt", "{0:d/M/yyyy}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt Inv">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdtotamt" runat="server" Value='<%# Eval("totamt") %>' />
                                            <asp:Label ID="lbtotamt" runat="server" Text='<%# Eval("totamt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Has Paid">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpaid" runat="server" Text='<%# Eval("totpaid","{0:F2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Temp Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lbremain" runat="server" Text='<%# Eval("remain","{0:F2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlbbalance" runat="server" Text='<%# Eval("tbalance","{0:F2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disc1%">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdisconepct" runat="server" Text='<%# Eval("disconepct") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OFF-ON1%">
                                        <ItemTemplate>
                                            <%-- <label class="switch-slider">--%>
                                            <%--<asp:CheckBox ID="chdisc" runat="server" AutoPostBack="true" OnCheckedChanged="chdisc_CheckedChanged" Checked='<%#Eval("onoff")%>' />--%>
                                            <asp:CheckBox ID="chdisc" runat="server" AutoPostBack="false" Checked='<%#Eval("onoff")%>' />
                                            <%--<input type="checkbox" <%#((Convert.ToInt32(Eval("onoff")) == 1)?"checked":"")%> id="chdisc" style="pointer-events:none"/>--%>
                                            <%--<div class="slider round"></div>--%>
                                            </label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT 1%">
                                        <ItemTemplate>
                                            <asp:Label ID="lbvatonepct" runat="server" Text='<%#Eval("vatonepct") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Be Paid">
                                        <ItemTemplate>
                                            <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>' Font-Size="Medium" Font-Bold="True"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txamt" runat="server" Width="5em" Text='<%# Eval("amt") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbtotamt" runat="server"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disc Fraction">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdisc" runat="server" Text='<%# Eval("disc_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txdisc" runat="server" Width="5em" Text='<%# Eval("disc_amt") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotdisc" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Round Fraction">
                                        <ItemTemplate>
                                            <asp:Label ID="lbRound" runat="server" Text='<%# Eval("round_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRound" runat="server" Width="5em" Text='<%# Eval("round_amt") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotRound" runat="server"></asp:Label>
                                        </FooterTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Outstanding">
                                        <ItemTemplate>
                                            <asp:Label ID="lboutstanding" runat="server" Text='<%# Eval("outstanding") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotOutStand" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <FooterStyle BackColor="Silver" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btsearchcust" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-offset-8 col-sm-1">Suspense</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbsuspense" runat="server" CssClass="control-label" Font-Bold="true" Font-Size="X-Large" Text="0"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="h-divider"></div>
    <div class="container margin-bottom">
        <div class="form-group">
            <label class="control-label col-sm-1">File</label>
            <div class="col-sm-3">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="fup" runat="server" CssClass="form-control" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btsave" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-sm-1">Total Paid</label>
            <div class="col-sm-2">
                <strong style="font-weight: bolder; font-size: x-large">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtotalpayment" runat="server" BackColor="Yellow" BorderColor="Yellow"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </strong>
            </div>
        </div>
    </div>
    <div class="h-divider"></div>
    <div class="margin-bottom navi text-center">
        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="btnew" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" runat="server" OnClientClick="ShowProgress();" CssClass="btn btn-warning btn-sm" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:LinkButton ID="btprintall" runat="server" CssClass="btn btn-info btn-sm" OnClick="btprintall_Click">Print All</asp:LinkButton>
                <asp:Button ID="btinv" runat="server" OnClick="btinv_Click" OnClientClick="ShowProgress();" Text="Button" Style="display: none" />
                <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" Style="display: none" OnClientClick="javascript:dvshow.setAttribute('class','divmsg');" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--OnClientClick="ShowProgress();" --%>
        <asp:Button ID="btsearchcust" runat="server" Text="Button"
            OnClick="btsearchcust_Click" Style="display: none" />
        <%--OnClientClick="javascript:ShowProgress();" --%>
        <asp:Button ID="btsearchinv" runat="server" Style="display: none" OnClick="btsearchinv_Click"
            Text="Button" />
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


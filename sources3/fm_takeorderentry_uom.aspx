<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_takeorderentry_uom.aspx.cs" Inherits="fm_takeorderentry_uom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <%--        <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery-1.12.4.js"></script>--%>
    <%--<script src="https://code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script>
        function sManualNo() {
            var s = prompt("Enter Manual No.");
            $get("<%=hdmanualno.ClientID%>").value = s;
            $get("<%=btupdman.ClientID%>").click();
        }
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function ProFormaSelected(sender, e) {
            $get('<%=hdproforma.ClientID%>').value = e.get_value();
            $get('<%=btsearchproinvoice.ClientID%>').click();
        }

        function ItemSelected(sender, e) {
            if ($('#' + '<%=lbcusttype.ClientID%>').text() == 'Key Account' && ($('#' + '<%=lblPO.ClientID%>').text() == 'Enter PO Number')) {


                $get('<%=hditem.ClientID%>').value = '';
                $get('<%=txitemsearch.ClientID%>').value = '';

                swal({
                    title: "Please enter PO Number",
                    text: "PO Number",
                    type: "warning",
                    showOKButton: true,
                },
                    function () {

                    });
                return;

            }
            if ($('#' + '<%=lbcusttype.ClientID%>').text() == 'Key Account' && ($('#' + '<%=lblPO.ClientID%>').text() == 'Enter PO Number' || $('#' + '<%=lblPO.ClientID%>').text() == 'Invalid PO Number')) {
                alert('Please enter correct PO number');
                function alert() {
                    swal({
                        title: "Please enter correct PO number",
                        text: "PO Number",
                        type: "warning",
                        showOKButton: true,
                    },
                        function () {

                        });
                }
                $get('<%=hditem.ClientID%>').value = '';
                $get('<%=txitemsearch.ClientID%>').value = '';
                console.log('sd2');
            }
            else {
                console.log($('#' + '<%=lbcusttype.ClientID%>').text());
                var dat = $('#' + '<%=lbcredit.ClientID%>').value;

                $get('<%=hditem.ClientID%>').value = e.get_value();
                $get('<%=txqty.ClientID%>').focus();
            }
            $get('<%=btreset.ClientID%>').click();

        }

        function SetDeliver() {
            $get('<%=btprice.ClientID%>').click();

        }

        function openwindow(url) {
            window.open(url, url, "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=800px, height=400px, top=200px, left=400px", true);
        }

        function btfreeclick() {
            //alert('test');
            $get('<%=btfree.ClientID%>').click();
        }

        function btitemclick() {
            //alert('test');
            $get('<%=btitem.ClientID%>').click();
        }

        function RefreshData(socd) {
            $get('<%=hdto.ClientID%>').value = socd;
            $get('<%=txorderno.ClientID%>').value = socd;
            $get('<%=btrefresh.ClientID%>').click();

        }

        function HideLoading() {
            dvshow.className = "divhid";
        }

        function fn_getmanual() {
            var i = window.prompt('ENTER MANUAL NO.');
            $get('<%=txmanualno.ClientID%>').value = i;
            if (i != null) {
                $get('<%=btprintloading.ClientID%>').click();
            }
            // return i;
        }

        function fn_getmanualinv() {
            var i = window.prompt('ENTER MANUAL INVOICE NO.');
            $get('<%=txmanualinv.ClientID%>').value = i;
            if (i != null) {
                $get('<%=btprintinvoice2.ClientID%>').click();
            }
            // return i;
        }

        function fn_getmanualfreeinv() {
            var i = window.prompt('ENTER MANUAL FREE INVOICE NO.');
            $get('<%=txmaninvfreeno.ClientID%>').value = i;
            if (i != null) {
                $get('<%=btprintfreeinv2.ClientID%>').click();
            }
        }

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
    <style type="text/css">
        .divmsg {
            /*position:static;*/
            top: 7%;
            right: 36%;
            left: 55%;
            width: 200px;
            height: 200px;
            position: fixed;
            opacity: 0.9;
            overflow-y: auto;
            /*-webkit-transition: background-color 0;
    transition: background-color 0;*/
        }

        .divhid {
            display: none;
        }

        .divnormal {
            display: normal;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 500px;
            height: 200px;
            font-family: Calibri;
            font-size: small;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 70px;
            height: 35px;
        }

        .checkbox label {
            padding-top: 2px !important;
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
                height: 34px;
                width: 34px;
                left: 3px;
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
            -webkit-transform: translateX(32px);
            -ms-transform: translateX(32px);
            transform: translateX(32px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 25px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel43" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdto" runat="server" />
            <asp:HiddenField ID="hdcust" runat="server" />
            <asp:HiddenField ID="hdcust_otlcd" runat="server" />
            <asp:HiddenField ID="hditem" runat="server" />
            <asp:HiddenField ID="hdproforma" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--  <h4 class="jajarangenjang">Take Order Entry</h4>
    <div class="h-divider"></div>--%>
    <div class="alert alert-info text-bold">Take Order Entry</div>
    <div class="container">
        <div class="form-horizontal">
            <div class="row margin-bottom" style="background-color: yellow">
                <label class="control-label col-sm-1">Status</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbstatus" runat="server" CssClass="well well-sm no-margin danger text-bold text-white radius5 badge"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Disc On-Off</label><strong> </strong>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                        <ContentTemplate>
                            <label class="switch">
                                <asp:CheckBox ID="chdisc" runat="server" AutoPostBack="true" OnCheckedChanged="chdisc_CheckedChanged" />
                                <div class="slider round"></div>
                            </label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Paid Cash</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel51" runat="server">
                        <ContentTemplate>
                            <label class="switch">
                                <asp:CheckBox ID="chcash" runat="server" />
                                <div class="slider round"></div>
                            </label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Direct Sales</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                        <ContentTemplate>
                            <label class="switch">
                                <asp:CheckBox ID="chdirect" runat="server" />
                                <div class="slider round"></div>
                            </label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%--<div class="col-sm-3">
                    <asp:CheckBox ID="chcash" CssClass="checkbox-inline" runat="server" Text="Cust Paid Cash" />
                </div>--%>
                <label class="control-label col-sm-1">Salespoint</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbsalespoint" CssClass="form-control " runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <label class="control-label col-sm-1">
                    Tax Disc
                    <asp:Label runat="server" ID="lbtotDiscTax"></asp:Label>
                    %</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                        <ContentTemplate>
                            <label class="switch">
                                <asp:CheckBox ID="chdisctax" runat="server" AutoPostBack="true" OnCheckedChanged="chdisctax_CheckedChanged" />
                                <div class="slider round"></div>
                            </label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1">Source</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsourceorder" onchange="javascript:ShowProgress();" CssClass="form-control " runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsourceorder_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Tablet No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="txtabnoPnl" CssClass="input-group">
                                <asp:TextBox ID="txtabno" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Panel runat="server" CssClass="input-group-btn" ID="bttabsearchPnl">
                                    <%--  <button id="bttabsearch" class="btn btn-primary btn-sm" type="submit" runat="server" onserverclick="bttabsearch_Click">
                                        <i class="fa fa-search" aria-hidden="true"></i>
                                    </button>--%>
                                    <asp:LinkButton ID="bttabsearch" CssClass="btn btn-primary" OnClientClick="PopupCenter('fm_tabsalesorder.aspx','Device Transfer',1000,800);" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">TO No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" CssClass="input-group" ID="txordernoPnl">
                                <asp:TextBox ID="txorderno" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Panel runat="server" ID="btsearchsoPnl" CssClass="input-group-btn">
                                    <%--<button id="btsearchso" onclientclick="ShowProgress();" class="btn btn-primary" type="submit" runat="server" onserverclick="btsearchso_Click">
                                        <i class="fa fa-search" aria-hidden="true"></i>
                                    </button>--%>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="btsearchso" OnClientClick="ShowProgress();" OnClick="btsearchso_Click" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                    </div>

                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Date</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtorder" runat="server" CssClass="form-control "></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtorder_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtorder">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1">Source Info</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsourseinfo" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsourseinfo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Cust Category</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbcustcate" runat="server" CssClass="well well-sm no-margin danger text-bold text-white radius5 badge"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1"></label>
                <div class="col-sm-2">
                </div>
                <label class="control-label col-sm-1">Delivery Date</label>
                <div class="col-sm-2 drop-down-date">
                    <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                        <ContentTemplate>
                            <div class="input-group">
                                <asp:TextBox ID="dtdelivery" runat="server" CssClass="form-control input-group-sm" AutoPostBack="True" OnTextChanged="dtdelivery_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtdelivery">
                                </asp:CalendarExtender>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btupdatedeliverydate" OnClientClick="ShowProgress();" OnClick="btupdatedeliverydate_Click" CssClass="btn btn-primary" runat="server">Update</asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1">Inv No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txinvoiceno" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Loading No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hddo" runat="server" />
                            <asp:TextBox ID="txmanualno" placeholder="Manual Loading" CssClass="form-control required" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hdmanualno" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Man Inv No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txmanualinv" placeholder="Manual Inv Number" runat="server" CssClass="form-control  ro"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Man Inv Free No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txmaninvfreeno" placeholder="Manual Free Invoice" runat="server" CssClass="form-control  ro" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1">Warehouse</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhs" onchange="ShowProgress();" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Bin</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <div class="input-group">
                                <asp:DropDownList ID="cbbin" onchange="ShowProgress();" CssClass="form-control input-group-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbin_SelectedIndexChanged">
                                </asp:DropDownList>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btupdatebin" CssClass="btn btn-primary" OnClientClick="ShowProgress();" runat="server" OnClick="btupdatebin_Click">Update</asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-sm-1">Sample</label>
                <div class="col-sm-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                              <asp:DropDownList ID="cbsample" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  
                </div>

            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1">Remark</label>
                <div class="col-sm-11">
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txremark" runat="server" CssClass="form-control require" placeholder="Put remark"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-sm-1">Cust</label>
                <div class="col-sm-4">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server" class="input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txcustomer" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" TargetControlID="txcustomer" CompletionSetCount="1" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetCompletionList" UseContextKey="True" OnClientItemSelected="CustSelected" ShowOnlyCurrentWordInCompletionListItem="true">
                            </asp:AutoCompleteExtender>

                            <span class="input-group-btn">
                                <button id="btsearchx" class="btn btn-primary" type="submit" runat="server">
                                    <i class="fa fa-search" aria-hidden="true"></i>
                                </button>
                            </span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-4">
                    <span style="color: red;" class="padding-top-4 block">* Customer should be in RPS (Route Plan Salesman)</span>
                </div>
                <label class="control-label col-sm-1">Promised No</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbpromisedno" runat="server" Font-Bold="True"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="col-sm-1 control-label">Cust PO</label>
                <div class="col-sm-4">
                    <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txpocust" placeholder="PO From Cust" CssClass="form-control" runat="server" Style="margin-left: -16px"></asp:TextBox>
                            </div>

                            <asp:Panel runat="server" CssClass="input-group-btn" ID="Panel2">

                                <%--   <button id="btCustPO" class="btn btn-primary" type="submit" runat="server" onserverclick="btCustPO_ServerClick">
                                    <i class="fa fa-user" aria-hidden="true">Validate PO</i>
                                </button>--%>
                                <asp:LinkButton ID="btCustPO" OnClientClick="ShowProgress();" OnClick="btCustPO_ServerClick" runat="server" CssClass="btn btn-primary btn-sm">Validate</asp:LinkButton>
                            </asp:Panel>
                            <asp:Label ID="lblPO" runat="server" Text="" Style="margin-left: -186px;"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <label class="col-sm-1 control-label">Cust PO Date</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                        <ContentTemplate>

                            <asp:TextBox ID="dtcustpo" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="dtcustpo_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtcustpo">
                            </asp:CalendarExtender>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="col-sm-1 control-label">Logistic Discount</label>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="updpaneldisclog" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txdisclogistic" placeholder="Put % disc" CssClass="form-control input-sm" TextMode="Number" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                <ContentTemplate>
                    <div class="row margin-bottom" runat="server" id="showProforma">
                        <label class="col-sm-1 control-label">Pro-Forma Invoice</label>
                        <div class="col-sm-4">
                            <div class="col-sm-6">
                                <asp:TextBox ID="txproforma" placeholder="Pro-forma Invoice" CssClass="form-control" runat="server" Style="margin-left: -16px">
                                </asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txproforma" CompletionSetCount="1" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetListProForma" UseContextKey="True" OnClientItemSelected="ProFormaSelected" ShowOnlyCurrentWordInCompletionListItem="true">
                                </asp:AutoCompleteExtender>
                            </div>
                            <asp:Label ID="lblProForma" runat="server" Text="" Style="margin-left: -186px;"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row margin-bottom">
                <div class="col-sm-12" style="background-color: yellowgreen">
                    <div class="clearfix margin-bottom">
                        <div class="well well-sm ">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="clearfix">
                                        <div class="col-sm-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Address</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbaddress" runat="server" CssClass="padding-top-4 block text-primary">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Salesman</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-6 control-button titik-dua">Customer Type</label>
                                            <div class="col-sm-5">
                                                <asp:Label ID="lbcusttype" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">City</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbcity" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Term Payment</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbterm" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-6 control-button titik-dua">Customer Group</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lbcustgroup" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Contact</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbcontact" runat="server" CssClass=" block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua no-padding" style="padding-left: 15px !important;">VAT No.</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbvatno" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Blue"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-sm-6 clearfix no-padding" style="margin-bottom: 10px;">
                                            <label class="col-sm-6 control-button titik-dua">Sales Block</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lbsalesblock" runat="server" CssClass="text-primary block padding-top-4">NO</asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua" style="font-size: 13px;">Vat English </label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lblVatEnglish" runat="server" Text="-" CssClass="text-primary block padding-top-4"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Vat Arabic</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblVatArabic" runat="server" Text="-" CssClass="text-primary block padding-top-4"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-6 control-button titik-dua">Credit Type</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lbcredittype" runat="server" Text="-" CssClass="text-primary block padding-top-4"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">
                                                <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbcapcredit" runat="server" Text="CL"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lbcredit" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">CL Remain</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="txclremain" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-6 control-button titik-dua">Max Avl Trns</label>
                                            <div class="col-sm-6">
                                                <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbmaxtrans" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-2 control-button titik-dua">Pay Promised Amt</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbpromised" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                                        <span class="text-bold block padding-top-4 inline-block padding-right">,Overdue Amt : </span>
                                                        <asp:Label ID="lboverdue" runat="server" CssClass="text-primary block padding-top-4 inline-block padding-right" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                                        <asp:Label ID="lbcapbalance" runat="server" Text="Balance" CssClass="text-bold block padding-top-4 inline-block padding-right"></asp:Label>
                                                        <asp:Label ID="lbbalance" runat="server" CssClass="text-primary block padding-top-4 inline-block padding-right" Style="color: Red; font-size: X-Large; font-weight: bold;" Text="0"></asp:Label>
                                                        <span class="text-bold block padding-top-4 inline-block padding-right">,Cust Suspense : </span>
                                                        <asp:Label ID="lbsuspense" runat="server" CssClass="text-primary block padding-top-4 inline-block padding-right" Style="color: Red; font-size: X-Large; font-weight: bold;" Text="0"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row margin-bottom">
                <div class="col-sm-12">
                    <table class="mGrid">
                        <tr>
                            <th style="width: 30%">Item</th>
                            <th style="width: 35%" colspan="4">Qty Order</th>
                            <th style="width: 5%">Price</th>
                            <th style="width: 5%">Stk Cust</th>
                            <th style="width: 10%">Stock Available</th>
                            <th style="width: 10%">Qty Shipment</th>
                            <th style="width: 5%">Add</th>

                        </tr>
                        <tr>
                            Veuillez utiliser TAB au lieu de SOURIS
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control input-sm" OnTextChanged="txitemsearch_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" CompletionListCssClass="input-smt" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitemsearch" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ShowOnlyCurrentWordInCompletionListItem="true">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" TextMode="Number" OnTextChanged="txqty_TextChanged"></asp:TextBox>
                                                <asp:HiddenField ID="hdqtytotal" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="cbuom_SelectedIndexChanged" AutoPostBack="True" OnClientClick="HideLoading();">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel55" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel56" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txqty2" runat="server" CssClass="form-control input-sm" TextMode="Number"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel57" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbuom2" runat="server" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbprice" CssClass="form-control input-sm text-bold text-red" runat="server" Text="0" BorderStyle="Dotted"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btprice" EventName="Click" />
                                    </Triggers>
                                    <%--<Triggers><asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" /></Triggers>--%>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txitemsearch" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:Button ID="btprice" runat="server" OnClientClick="ShowProgress();" Text="Button" OnClick="btprice_Click" Style="display: none" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblItemBlock" CssClass="form-control input-sm text-bold text-red" runat="server" Text=""></asp:Label>
                                                <asp:TextBox ID="txstockcust" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbstock" CssClass="form-control input-sm text-bold text-red" runat="server" Text="0"></asp:Label>
                                        <asp:HiddenField ID="hdstock" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txshipmen" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm ro"></asp:TextBox>
                                                <asp:HiddenField ID="hdshipment" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" runat="server" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" OnClick="btadd_Click">Add</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <%--Screen for multi uom--%>


            <!-- End of Form Group-->
            <div class="row margin-bottom">
                <div class="col-sm-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="grd_RowDeleting" OnRowDataBound="grd_RowDataBound" ShowFooter="True" OnSelectedIndexChanging="grd_SelectedIndexChanging" CssClass="mGrid" CellPadding="0">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate><%# Container.DataItemIndex+1 %>.</ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbitemname" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <%# Eval("size") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stk Cust">
                                        <ItemTemplate>
                                            <asp:Label ID="lbstockcust" runat="server" Text='<%# Eval("stock_cust","{0:G92}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order">
                                        <ItemTemplate>
                                            <asp:Label ID="lbqtyconv" runat="server" Text='<%# Eval("qty_conv") %>'></asp:Label>
                                            <asp:HiddenField ID="lbqtyorder" runat="server" Value='<%# Eval("qty") %>' />
                                            <asp:HiddenField ID="hdqty_ctn" runat="server" Value='<%# Eval("qty_ctn") %>' />
                                            <asp:HiddenField ID="hdqty_pcs" runat="server" Value='<%# Eval("qty_pcs") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txqtyorder" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotqtyorder" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Av">
                                        <ItemTemplate>
                                            <asp:Label ID="lbstock_qty" runat="server" Text='<%# Eval("stockqty_conv") %>'></asp:Label>
                                            <asp:HiddenField ID="lbstockamt" runat="server" Value='<%# Eval("stock_amt") %>' />
                                            <asp:HiddenField ID="hdstockqty_ctn" runat="server" Value='<%# Eval("stockqty_ctn") %>' />
                                            <asp:HiddenField ID="hdstockqty_pcs" runat="server" Value='<%# Eval("stockqty_pcs") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ship">
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotshipment" runat="server" Font-Size="X-Large" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbshipment_qty" runat="server" Text='<%# Eval("qtyshipment_conv") %>'></asp:Label>
                                            <asp:HiddenField ID="lbshipment" runat="server" Value='<%# Eval("qty_shipment") %>' />
                                            <asp:HiddenField ID="hdqtyshipment_ctn" runat="server" Value='<%# Eval("qtyshipment_ctn") %>' />
                                            <asp:HiddenField ID="hdqtyshipment_pcs" runat="server" Value='<%# Eval("qtyshipment_pcs") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lbuom" runat="server" Text='<%# Eval("uom") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprice0" runat="server" Text='<%# Eval("price_conv") %>'></asp:Label>
                                            <asp:HiddenField ID="hdprice0" runat="server" Value='<%# Eval("unitprice") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disc">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdisc" runat="server" Text='<%#Eval("disc_cash","{0:N2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubTotal">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal","{0:N2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotsubtotal" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT">
                                        <ItemTemplate><%#Eval("vat") %></ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotvat" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="False" ShowDeleteButton="True" />
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle BackColor="Yellow" Font-Bold="True" />
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
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btdisc" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="col-sm-4 col-sm-offset-8">
                            <table class="table tab-content">
                                <tr>
                                    <th style="text-align: right; width: 50%">VAT
                                        <asp:Label ID="lbtitlevat" runat="server" Text="5"></asp:Label>
                                        % :                       
                                <th style="text-align: left; width: 50%">
                                    <strong>
                                        <asp:Label ID="lbvat" runat="server" Text=""></asp:Label>
                                    </strong>
                                </th>
                                    </th>
                                </tr>
                                <tr>
                                    <th style="text-align: right; width: 50%">Net Subtotal
                                        <asp:Label ID="lbtitlediscvat" runat="server" Text=""></asp:Label>
                                        <th style="text-align: left; width: 50%">
                                            <strong>
                                                <asp:Label ID="lbdiscountvat" runat="server" Text=""></asp:Label>
                                            </strong>
                                        </th>
                                    </th>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-group">
                <div class="col-sm-12">
                    <div style="text-align: center">
                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btdisc" runat="server" OnClientClick="javascript:ShowProgress();" Text="DISCOUNT CALCULATION " CssClass="btn btn-primary" OnClick="btdisc_Click" TabIndex="18" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chdisc" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <!-- End Of Group -->
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="margin-bottom">
                        <div style="width: 100%">
                            <div style="float: left; width: 50%; vertical-align: top; padding: 10px 10px 10px 10px">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grddisc" runat="server" AutoGenerateColumns="False" Caption="Discount Applied" CaptionAlign="Top" GridLines="None" ShowHeaderWhenEmpty="True" CellPadding="0" OnRowEditing="grddisc_RowEditing" OnRowCancelingEdit="grddisc_RowCancelingEdit" OnRowDataBound="grddisc_RowDataBound" OnRowUpdating="grddisc_RowUpdating" OnSelectedIndexChanging="grddisc_SelectedIndexChanging1" CssClass="mGrid" EmptyDataText="No Discount Hit">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Discount Code">
                                                    <ItemTemplate>
                                                        <a href="javascript:popupwindow('fm_discountinfo.aspx?dc=<%# Eval("disc_cd") %>');">
                                                            <asp:Label ID="lbdisccode" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label></a>
                                                        <asp:HiddenField ID="hddiscuse" runat="server" Value='<%# Eval("discount_use") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mechanism">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbmec" runat="server" Text=' <%# Eval("discount_mec_nm") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdmec" runat="server" Value='<%# Eval("discount_mec") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Free Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbfreeqty" runat="server" Text='<%# Eval("free_qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txfreeqty" runat="server" Width="4em" Text='<%# Eval("free_qty") %>' CssClass="form-control input-sm"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbuomfree" runat="server" Text='<%# Eval("uom_free") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Free Cash">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbfreecash" runat="server" Text='<%# Eval("free_cash") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txfreecash" runat="server" Width="4.5em" Text='<%# Eval("free_cash") %>' CssClass="form-control input-sm"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowSelectButton="True" HeaderText="Free Item" SelectImageUrl="~/add_item.png" SelectText="Free Item" ButtonType="Image" InsertVisible="False">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:CommandField>
                                                <asp:CommandField EditImageUrl="~/edit.png" HeaderText="Edit" ShowEditButton="True" ButtonType="Image">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <%--    <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btdisc" EventName="Click" />
                        </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>
                            <div style="float: left; width: 50%; position: relative; padding: 10px 10px 10px 10px; vertical-align: top; font-size: small; font-family: Calibri">
                                <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grddirect" runat="server" AutoGenerateColumns="False" Caption="Cash Discount as Selected Goods" ShowHeaderWhenEmpty="True" GridLines="None" CellPadding="0" CssClass="mygrid" OnSelectedIndexChanging="grddirect_SelectedIndexChanging" Width="100%">
                                            <AlternatingRowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Mechanism">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbmec" runat="server" Text=' <%# Eval("discount_mec_nm") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdmec" runat="server" Value='<%# Eval("discount_mec") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Free Cash">
                                                    <ItemTemplate><%# Eval("free_cash") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txfreecash" CssClass="txfreecash" runat="server" Text='<%# Eval("free_cash") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/add_item.png" HeaderText="Action" SelectText=" Additional Item">
                                                    <ItemStyle Wrap="False" />
                                                </asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btdisc" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel54" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grddirectitem" runat="server" AutoGenerateColumns="False" Caption="Item Discount" ShowHeaderWhenEmpty="True" GridLines="None" CellPadding="0" CssClass="mGrid" OnRowDeleting="grddirectitem_RowDeleting" Width="100%">
                                            <AlternatingRowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate>
                                                        <%# Eval("item_shortname") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branded">
                                                    <ItemTemplate>
                                                        <%# Eval("branded_nm") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size">
                                                    <ItemTemplate>
                                                        <%# Eval("size") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate><%# Eval("free_qty") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="grddirect" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="btitem" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdfree" runat="server" AutoGenerateColumns="False" Caption="Item Free" ShowHeaderWhenEmpty="True" GridLines="None" CellPadding="0" OnRowDeleting="grdfree_RowDeleting" CssClass="mGrid" EmptyDataText="No Free Item Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Discount No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldisc_cd" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate>
                                                        <%# Eval("item_nm") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branded">
                                                    <ItemTemplate>
                                                        <%# Eval("branded_nm") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size">
                                                    <ItemTemplate>
                                                        <%# Eval("size") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate><%# Eval("free_qty") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="grddisc" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="btfree" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-1">PO Cust</label>
                <div class="col-sm-2">
                    <input type="file" name="uplpo" class="form-control" />
                </div>
                <div class="col-sm-2">
                    <strong style="color: red">(Optional)</strong>
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-sm-4">
                    <asp:Label ID="lbsourseinfo" runat="server" Visible="False"></asp:Label>
                </div>
                <asp:Panel runat="server" ID="cbmerchendizerPnl" CssClass="col-sm-8 drop-down">
                    <asp:DropDownList ID="cbmerchendizer" CssClass="form-control" runat="server" Visible="False">
                    </asp:DropDownList>
                </asp:Panel>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="navi margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" Style="display: none" OnClientClick="javascript:ShowProgress();" />
                                <asp:Button ID="btupdman" runat="server" OnClick="btupdman_Click" Text="Button" CssClass="divhid" />
                                <asp:Button ID="btprintloading" runat="server" OnClick="btprintloading_Click" Text="Button" CssClass="divhid" />
                                <asp:Button ID="btprintinvoice2" runat="server" CssClass="divhid" OnClick="btprintinvoice2_Click" Text="Button" />
                                <asp:Button ID="btprintfreeinv2" runat="server" OnClick="btprintfreeinv2_Click" Text="Button" CssClass="divhid" />
                                <asp:LinkButton ID="btnew" runat="server" OnClientClick="ShowProgress();" OnClick="btnew_Click" CssClass="btn btn-primary">New</asp:LinkButton>
                                <asp:LinkButton ID="btedit" runat="server" OnClick="btedit_Click" CssClass="btn btn-default ">Edit</asp:LinkButton>
                                <asp:LinkButton ID="btsave" runat="server" OnClick="btsave_Click" CssClass="btn btn-success" OnClientClick="javascript:ShowProgress();">&nbsp;Save&nbsp;</asp:LinkButton>
                                <asp:LinkButton ID="btcancel" runat="server" OnClick="btcancel_Click" CssClass="btn btn-default ">Cancel Order</asp:LinkButton>
                                <asp:Button ID="btfree" runat="server" Text="Button" OnClick="btfree_Click" Style="display: none" />
                                <asp:Button ID="btitem" runat="server" Text="Button" OnClick="btitem_Click" Style="display: none" />
                                <asp:LinkButton ID="btprint" runat="server" OnClientClick="javascript:fn_getmanual();return false;" OnClick="btprint_Click" CssClass="btn btn-default ">Print Loading</asp:LinkButton>
                                <asp:LinkButton ID="btprintinvoice" runat="server" OnClick="btprintinvoice_Click" OnClientClick="javascript:fn_getmanualinv();return false;" CssClass="btn btn-default ">Print Invoice</asp:LinkButton>
                                <asp:LinkButton ID="btprintvat" CssClass="btn btn-info" runat="server" OnClick="btprintvat_Click" Style="display: none">Print VAT</asp:LinkButton>
                                <asp:LinkButton ID="btprintfreeinv" runat="server" OnClick="Button1_Click" CssClass="btn btn-default " OnClientClick="javascript:fn_getmanualfreeinv();return false;" Style="display: none">Print Invoice Free</asp:LinkButton>
                                <asp:Button ID="btcopyorder" runat="server" Text="Copy Order" OnClick="btcopyorder_Click" Visible="False" />
                                <asp:LinkButton ID="btpanda" runat="server" CssClass="btn btn-primary" Style="display: none" OnClick="btpanda_Click">Edited KA Price</asp:LinkButton>
                                <asp:Button ID="btreset" runat="server" Style="display: none" OnClick="btreset_Click" Text="Button" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Of container-->

    <div id="divmsg" style="display: none">
        Please type code or name customer | 
يرجى كتابة كود أو اسم العميل
    </div>
    <table>
        <tr>
            <td style="position: relative">
                <div id="divwidth"></div>
                <div id="divwidthc" style="font-family: Calibri; font-size: small"></div>
            </td>
        </tr>
    </table>


    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="popupcust" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                <ContentTemplate>
                    Document need make completed for Customer :
                    <asp:Label ID="lbcust" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>
            <img src="div2.png" class="divid" />
        </div>
        <div class="body">
            <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbmessage" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>

            <br />
            <asp:Button ID="btnHide" runat="server" Text="Back To Order" />
            <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" OnClientClick="ShowProgress();" />
            <asp:Button ID="btsearchproinvoice" runat="server" CssClass="button2 search" OnClick="btsearchproinvoice_Click" OnClientClick="javascript:ShowProgress();" />
        </div>
    </asp:Panel>

    <div id="divwidthi" style="font-family: Calibri; font-size: small; position: relative">
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


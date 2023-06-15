<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_canvasorder.aspx.cs" Inherits="fm_canvasorder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>

        function senddata2(thedata) {
            $get('<%=hdcanvas.ClientID%>').value = thedata;
           <%-- $get('<%=txorderno.ClientID%>').value=thedata;--%>

        }
        function senddata(thedata) {
            $get('<%=hdcanvas.ClientID%>').value = thedata;
            $get('<%=btrefresh.ClientID%>').click();

        }
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function ItemSelected(sender, e) {
            var dat = $('#' + '<%=lbcredit.ClientID%>').value;
           <%-- if ($get('<%=lbcredittype.ClientID%>').value != 'CASH') {

                if (parseInt(document.getElementById('<%=lbcredit.ClientID%>').innerHTML) < 5000) {
                    document.getElementById('<%=txitemsearch.ClientID%>').value = '';
                    if (document.getElementById('<%=lblVatEnglish.ClientID%>').innerHTML == '') {
                        document.getElementById('<%=txitemsearch.ClientID%>').value = '';
                        document.getElementById('<%=hditem.ClientID%>').value = '';
                        swal({
                            title: "Item Selection!",
                            text: "Please update english name (vat).",
                            type: "warning",
                            showOKButton: true,
                        },
                            function () {

                            });
                        //alert('Please update english name (vat).');
                    }
                    else if (document.getElementById('<%=lblVatArabic.ClientID%>').innerHTML == '') {
                        document.getElementById('<%=txitemsearch.ClientID%>').value = '';
                        document.getElementById('<%=hditem.ClientID%>').value = '';
                        //alert('Please update arabic name (vat).');
                        swal({
                            title: "Item Selection!",
                            text: "Please update arabic name (vat).",
                            type: "warning",
                            showOKButton: true,
                        },
                            function () {

                            });
                    }
                    else if (document.getElementById('<%=lbvatno.ClientID%>').innerHTML == '') {
                        document.getElementById('<%=txitemsearch.ClientID%>').value = '';
                        document.getElementById('<%=hditem.ClientID%>').value = '';
                        //alert('Please update vat number.');
                        swal({
                            title: "Item Selection!",
                            text: "Please update vat number.",
                            type: "warning",
                            showOKButton: true,
                        },
                            function () {

                            });
                    }
                }

               
            }--%>
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btreset.ClientID%>').click();
        }
        function SetDeliver() {
            <%--$get('<%=btprice.ClientID%>').click();--%>
            $get('<%=btadd.ClientID%>').focus();
        }

        function openwindow(url) {
            window.open(url, url, "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=1, resizable=no, copyhistory=no, width=800px, height=700px, top=200px, left=400px", true);
        }

        function btfreeclick() {
            //alert('test');
            $get('<%=btfree.ClientID%>').click();
        }

        function CanvasSelected(sender, e) {
            $get('<%=hdcanvas.ClientID%>').value = e.get_value();
                $get('<%=btsearchso.ClientID%>').click();
        }

        //function scrollmv() {
        //    $('html, body').animate({
        //        scrollTop: $("#mv").offset().top - 80
        //    }, 500);
        //}

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
    <%--  <style type="text/css">
        .alert-syalala {
            display: none;
            position: fixed;
            z-index: 99;
            bottom: 60px;
            right: 35px;
            background: #6e88a7;
            width: 300px;
            height: 100px;
            box-shadow: 0 0 6px rgba(0,0,0,0.5);
            text-align: center;
            padding: 10px;
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
    </style>--%>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 70px;
            height: 35px;
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

        .table td {
            border-top: none !important;
        }

            .table td:nth-child(2n-1) {
                font-weight: bold;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcanvas" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
  <%--  <h4 class="jajarangenjang">Canvas Order Entry</h4>
    <div class="h-divider"></div>--%>
    <div class="alert alert-info text-bold">Canvass Order Entry ver 2.0</div>
    <div class="container">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Status</label>

                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbstatus" runat="server" CssClass="control-label"></asp:Label>
                                <asp:HiddenField ID="hdcust_otlcd" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Use Disc</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                            <ContentTemplate>
                                <label class="switch">
                                    <asp:CheckBox ID="rdonoff" runat="server" AutoPostBack="true" OnCheckedChanged="chdisc_CheckedChanged" />
                                    <div class="slider round"></div>
                                </label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <label class="control-label col-md-1">Cust Category</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbcuscate" runat="server" CssClass="border well well-sm danger text-bold text-white no-margin badge" Text="Cust Category"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Salespoint</label>
                    <div class="col-md-2">
                        <asp:Label ID="lbsalespoint" runat="server" Text="" CssClass="control-label"></asp:Label>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <label class="control-label col-md-1">Source Order</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbsourceorder" onchange="javascript:ShowProgress();" AutoPostBack="true" OnSelectedIndexChanged="cbsourceorder_SelectedIndexChanged" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Canvas No</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txorderno" runat="server" CssClass="txorderno form-control"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txorderno_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txorderno" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" ServiceMethod="GetCompletionList3" UseContextKey="True" OnClientItemSelected="CanvasSelected">
                                </asp:AutoCompleteExtender>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearchso" OnClientClick="javascript:ShowProgress();" runat="server" CssClass="btn btn-primary btn-search" OnClick="btsearchso_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Tablet No</label>
                    <div class="col-md-2">
                        <div class="input-group">
                            <asp:TextBox ID="txtabno" CssClass="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="bttabsearch" runat="server" CssClass="btn btn-primary btn-search" OnClick="bttabsearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <label class="control-label col-md-1">Invoice No</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txinvoiceno" runat="server" placeholder="Inv Number" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <label class="control-label col-md-1">Manual No</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txmanualinv" placeholder="Manual Inv Number" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Free Inv No</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txmanualfreeinv" placeholder="Manual Free Number" runat="server" CssClass="form-control txmanualfreeinv"></asp:TextBox>
                    </div>
                    <label class="control-label col-md-1">Order Date</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="dtorder" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtorder_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtorder">
                        </asp:CalendarExtender>
                    </div>
                    <label class="control-label col-md-1">Paid By Cash</label>
                    <div class="col-md-2">
                        <label class="switch">
                            <asp:CheckBox ID="chcash" runat="server" />
                            <div class="slider round"></div>
                        </label>
                        <%--<asp:CheckBox ID="chcash" runat="server" Font-Overline="False" CssClass="control-label" />--%>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <label class="control-label col-md-1">Remark</label>
                    <div class="col-md-8">
                        <asp:TextBox ID="txremark" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="h-divider"></div>
                <div class="row margin-bottom">
                    <label class="control-label col-md-1">Cust</label>
                    <div class="col-md-5">
                        <div class="input-group">
                            <asp:TextBox ID="txcustomer" runat="server" CssClass="form-control input-group-sm"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustomer" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected">
                            </asp:AutoCompleteExtender>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btsearch" OnClientClick="javascript:ShowProgress();" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <span class="text-bold text-red block padding-top-4">* Customer should be in RPS (Route Plane Salesman)</span>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <div class="col-md-12" style="background-color: yellow">
                        <table class="mGrid">
                            <tr style="background-color: yellow">
                                <th>Address</th>
                                <th>
                                    <asp:Label ID="lbaddress" runat="server" Font-Bold="True" ForeColor="White"></asp:Label></th>
                                <th>Credit Limit </th>
                                <th>
                                    <asp:Label ID="lbcredit" runat="server" Font-Bold="True" ForeColor="White"></asp:Label></th>
                                <th>Customer Type </th>
                                <th>
                                    <asp:Label ID="lbcusttype" runat="server" Font-Bold="True" ForeColor="White"></asp:Label></th>
                            </tr>
                            <tr>
                                <th>City</th>
                                <th>
                                    <asp:Label ID="lbcity" runat="server" Font-Bold="True" ForeColor="White"></asp:Label></th>
                                <th>Remain Credit</th>
                                <th>
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbremain" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </th>
                                <th>Sales Block</th>
                                <th>
                                    <asp:Label ID="lbsalesblock" runat="server" Font-Bold="True" ForeColor="White">NO</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <th>Contact</th>

                                <th>
                                    <asp:Label ID="lbcontact" runat="server"></asp:Label>
                                </th>
                                <th>Term Payment</th>

                                <th>

                                    <asp:Label ID="lbterm" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>

                                </th>
                                <th>Cust Group</th>
                                <th>
                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbcustcate" runat="server" ForeColor="White"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </th>
                              

                            </tr>
                            <tr>
                                <th>Van</th>

                                <th>
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField ID="hdvan" runat="server" />
                                            <asp:Label ID="lbvan" runat="server" ForeColor="White" Font-Bold="True"></asp:Label>


                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbsalesman" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </th>
                                <th>Bin</th>

                                <th>
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbbin" runat="server" CssClass="form-control input-sm" Style="min-width: 150px;">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbsalesman" EventName="SelectedIndexChanged" />
                                        </Triggers>

                                    </asp:UpdatePanel>
                                </th>
                                <th>Salesman Name</th>
                                <th>
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbsalesman" runat="server" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged" AutoPostBack="True" Enabled="False" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </th>
                            </tr>
                            <tr>
                                <th>Payment Promised</th>

                                <th>
                                    <asp:Label ID="lbpromised" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="White"></asp:Label>
                                </th>
                                <th>Over due amt</th>

                                <th>
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lboverdue" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="White"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                </th>
                                <th>Max Amt Trans</th>
                                <th>
                                    <asp:Label ID="lbmaxamt" runat="server" Text="0" ForeColor="White" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <th>VAT No</th>
                                <th>
                                    <asp:Label ID="lbvatno" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="White"></asp:Label>
                                </th>
                                <th>Vat English</th>
                                <th>
                                    <asp:Label ID="lblVatEnglish" runat="server" Text="" CssClass="text-primary block padding-top-4"></asp:Label></th>
                                <th>Vat Arabic</th>
                                <th>
                                    <asp:Label ID="lblVatArabic" runat="server" Text="" CssClass="text-primary block padding-top-4"></asp:Label></th>
                            </tr>
                            <tr>
                                <th>Credit Type</th>
                                <th>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbcredittype" runat="server" ForeColor="White"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <div class="col-md-12">
                        <table class="mGrid" id="mv">
                            <tr>
                                <th style="width: 30%">Item</th>
                                <th style="width: 5%">UOM</th>
                                <th style="width: 10%">Item Block PCS</th>
                                <th style="width: 5%">Unit Price</th>
                                <th style="width: 5%">Stk Cust</th>
                                <th style="width: 5%">Qty Order</th>
                                <th style="width: 5%">Stock AV</th>
                                <th style="width: 5%">Qty Ship</th>
                                <th style="width: 5%">ADD</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control input-sm input-item txitemsearch"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" runat="server" TargetControlID="txitemsearch" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="10" CompletionInterval="1" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionList2" UseContextKey="True" OnClientItemSelected="ItemSelected">
                                            </asp:AutoCompleteExtender>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:HiddenField ID="hditem" runat="server" />
                                </td>
                                <td class="drop-down">
                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbuom" CssClass="form-control" runat="server" onChange="javascript:ShowProgress();" OnSelectedIndexChanged="cbuom_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                                        <ContentTemplate>
                                             <asp:Label ID="lblItemBlock" CssClass="form-control input-sm text-bold text-red" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbprice" runat="server" Text="0" CssClass="form-control input-sm text-bold text-red"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbuom" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btprice" EventName="Click" />
                                        </Triggers>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                                        </Triggers>

                                    </asp:UpdatePanel>

                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txstockcust" runat="server" CssClass="form-control input-sm txstockcust" TabIndex="2"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txqty" runat="server" CssClass="form-control" TabIndex="3" AutoPostBack="True" OnTextChanged="txqty_TextChanged"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbstock" runat="server" Text="0" CssClass="form-control"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txshipmen" runat="server" CssClass="form-control ro"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txqty" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </td>

                                <td>
                                    <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-primary" OnClick="btadd_Click" TabIndex="6">ADD</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="grd_RowDeleting" OnRowUpdating="grd_RowUpdating" ShowFooter="True" OnRowDataBound="grd_RowDataBound" CellPadding="0" CssClass="mGrid">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>.
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <%# Eval("item_shortname") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branded">
                                            <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lbqtyorder" runat="server" Text='<%# Eval("qty")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbtotqtyorder" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stck Av">
                                            <ItemTemplate>
                                                <asp:Label ID="lbstockamt" runat="server" Text='<%# Eval("stock_amt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ship">
                                            <FooterTemplate>
                                                <asp:Label ID="lbtotshipment" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbshipment" runat="server" Text='<%# Eval("qty_shipment") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lbprice0" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Disc Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lbdiscamt" runat="server" Text='<%# Eval("disc_cash") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SubTotal">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal","{0:#,###.00}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbTotdiscount" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VAT">
                                            <ItemTemplate><%#Eval("vat") %></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbtotvat" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="Yellow" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="grd" EventName="RowDeleting"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                <ContentTemplate>
                <div class="col-md-4 col-md-offset-8">
                    <table class="table tab-container">
                        <tr>
                            <th>VAT (<asp:Label ID="lbtitlevat" runat="server" Text=""></asp:Label> %) : </th>
                            <th>
                                <asp:Label ID="lbvat" runat="server" Text=""></asp:Label>
                            </th>
                        </tr>
                    </table>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row margin-bottom">
                    <div class="col-md-12" style="text-align: center">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btdisc" runat="server" OnClick="btdisc_Click" CssClass="btn btn-primary "><span >Discount Calculation</span></asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="h-divider"></div>
                <div class="row margin-bottom">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grddisc" runat="server" AutoGenerateColumns="False" Caption="Discount Applied" CaptionAlign="Top" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" OnSelectedIndexChanging="grddisc_SelectedIndexChanging" CellPadding="0" OnRowCancelingEdit="grddisc_RowCancelingEdit" OnRowEditing="grddisc_RowEditing" CssClass="mGrid" OnRowUpdating="grddisc_RowUpdating">
                                        <AlternatingRowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Discount Code">
                                                <ItemTemplate>
                                                    <a style="color: blue" href="javascript:openwindow('fm_discountinfo.aspx?dc=<%# Eval("disc_cd")%>');">
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
                                                    <asp:TextBox ID="txfreeqty" CssClass="txfreeqty" runat="server" Text='<%# Eval("free_qty") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbuomfree" runat="server" Text='<%# Eval("uom_free") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Free Cash">
                                                <ItemTemplate><%# Eval("free_cash") %></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txfreecash" CssClass="txfreecash" runat="server" Text='<%# Eval("free_cash") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" EditImageUrl="~/edit.png" SelectImageUrl="~/add_item.png" ShowEditButton="True" HeaderText="Action" SelectText="Free Item">
                                                <ItemStyle Wrap="False" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btdisc" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="grdfree" runat="server" AutoGenerateColumns="False" Caption="Item Free" ShowHeaderWhenEmpty="True" GridLines="None" CellPadding="0" CssClass="mGrid" OnRowDeleting="grdfree_RowDeleting">
                                        <AlternatingRowStyle />
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
                <!--End Of Form Hori -->
            </div>
        </div>
    </div>
    <div style="text-align: center;" class="navi margin-bottom">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Style="display: none" />
                <asp:LinkButton ID="btnew" runat="server" OnClick="btnew_Click" OnClientClick="ShowProgress();" CssClass="btn btn-success bt-add "><span >New Canvas</span></asp:LinkButton>
                <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-warning" OnClientClick="ShowProgress();" OnClick="btsave_Click"><span >Save</span></asp:LinkButton>
                <asp:LinkButton ID="btcancel" runat="server" CssClass="btn btn-danger " Visible="false" OnClick="btcancel_Click"><span >Cancel</span></asp:LinkButton>
                <asp:Button ID="btfree" runat="server" Text="Button" OnClick="btfree_Click" Style="display: none" />
                <asp:LinkButton ID="btprintinvoice" OnClientClick="ShowProgress();" runat="server" OnClick="btprintinvoice_Click" CssClass="btn btn-info ">Print Invoice</asp:LinkButton>
                <asp:LinkButton ID="btprintinvfree" runat="server" OnClick="btprintinvfree_Click" CssClass="btn btn-info ">Print Free Invoice</asp:LinkButton>
                <asp:LinkButton ID="btprintall" OnClientClick="ShowProgress();" runat="server" OnClick="btprintall_Click" CssClass="btn btn-info">Print All</asp:LinkButton>
                <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="Button" Style="display: none" />
                <asp:Button ID="btprice" runat="server" Text="Button" OnClick="btprice_Click" OnClientClick="javascript:ShowProgress();" Style="display: none" />
                <asp:Button ID="btreset" runat="server" Style="display: none" OnClientClick="javascript:ShowProgress();" OnClick="btreset_Click" Text="Button" />
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>

</asp:Content>


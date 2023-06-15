<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesreturn_wrk.aspx.cs" Inherits="fm_salesreturn_wrk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearchcust.ClientID%>').click();
            
        }
        
        function InvSelected(sender, e) {
            $get('<%=hdinvoice.ClientID%>').value = e.get_value();
            $get('<%=btinvselect.ClientID%>').click();
            $('#<%=txinvoicesearch.ClientID%>').addClass("form-control ro");

        }
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btcheckprice.ClientID%>').click();
            $('#<%=txitemsearch.ClientID%>').addClass("form-control ro");
        }

        function cndnselected(sender, e) {
            $get('<%=hdcndn.ClientID%>').value = e.get_value();            
        }

        
        function SelectReturn(sRetNo) {
            $get('<%=hdreturno.ClientID%>').value = sRetNo;
            $get('<%=btrefresh.ClientID%>').click();

        }
        function DateSelect() {
            alert('test saja');
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

        .auto-complate-list {
            font-size: 12px !important;
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
    </style>

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

        .checkbox label, .radio label {
            min-height: 0;
        }

        fieldset[disabled] input[type=checkbox],
        fieldset[disabled] input[type=radio],
        input[type=checkbox].disabled,
        input[type=checkbox][disabled],
        input[type=radio].disabled,
        input[type=radio][disabled] {
            margin-top: 4px !important;
        }

        .line-hight10 {
            line-height: 10px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdinvoice" runat="server" />
    <asp:HiddenField ID="hdcndn" runat="server" />
    <div class="jajarangenjang">Return Destroyed At Customer Side</div>
    <div class="h-divider"></div>
    <div class="container">
        <div class="form-group">
            <div class="row margin-bottom">
                <label class="col-md-1 control-label">Return Type </label>
                <div class="col-md-3">
                    <asp:RadioButtonList ID="rdreturtype" onchange="javascript:ShowProgress();" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rdreturtype_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="well well-sm radio radio-inline no-margin" BackColor="Yellow">
                        <asp:ListItem Value="I">Retur Depo In</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-8">
                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdreturno" runat="server" />
                            <asp:HiddenField ID="hdcust" runat="server" />
                            <asp:HiddenField ID="hditem" runat="server" />
                            <asp:Label ID="lbretursta" runat="server" CssClass="badge badge-lg danger" Style="margin-left: 10px;"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="col-md-1 control-label">Manual No.</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txmanualno" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="col-md-1 control-label">System No.</label>
                <div class="col-md-2 ">
                    <div class="input-group">
                        <asp:TextBox ID="txreturno" runat="server" CssClass="form-control ro">NEW</asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearchret" OnClick="btsearchret_Click" CssClass="btn btn-primary" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>

                <label class="col-md-1 control-label">Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtretur" Width="100%" runat="server" CssClass="form-control drop-down-date"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtretur_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtretur" OnClientDateSelectionChanged="DateSelect">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="col-md-1 control-label">Cust Manual No.</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txcustmanualno" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="col-md-1 control-label">Reason</label>
                <div class="col-md-5 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbremark" runat="server" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="col-md-1 control-label">Cust Manual Date</label>
                <div class="col-md-2 drop-down-date">
                    <asp:TextBox ID="dtcustman" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtcustman_CalendarExtender" runat="server" TargetControlID="dtcustman">
                    </asp:CalendarExtender>
                </div>
                <label class="col-md-1 control-label">Customer</label>
                <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" class="input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txsearchcust" runat="server" CssClass="form-control ro"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btsearchcust" OnClick="btsearchcust_Click" CssClass="btn btn-primary" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                            <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover"
                                CompletionListItemCssClass="auto-complate-item" ID="txsearchcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList"
                                TargetControlID="txsearchcust" UseContextKey="True" CompletionListElementID="divwidthc" FirstRowSelected="false" EnableCaching="false"
                                MinimumPrefixLength="1" CompletionSetCount="1" CompletionInterval="10" OnClientItemSelected="CustSelected">
                            </asp:AutoCompleteExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-6" style="background-color: yellow; width: 40%;">
                    <div class="row">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="well well-sm clearfix margin-top">
                            <ContentTemplate>
                                <div class="col-sm-6 clearfix no-padding">
                                    <label class="col-sm-6">Address</label>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lbaddress" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-6 clearfix no-padding">
                                    <label class="col-sm-3">City</label>
                                    <div class="col-sm-9">
                                        <asp:Label ID="lbcity" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-6 clearfix no-padding">
                                    <label class="col-sm-6">Credit Limit</label>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lbcl" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-6 clearfix no-padding">
                                    <label class="col-sm-3">Type</label>
                                    <div class="col-sm-9">
                                        <asp:Label ID="lbcusttype" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <label class="col-md-1">Salesman</label>
                    <div class="col-sm-2">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="col-md-1">Return By Driver</label>
                    <div class="col-sm-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbDriver" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    
                </div>
            </div>

                    <div class="row margin-bottom">
                        <label class="col-md-1">CNDN CD</label>
                        <div class="col-sm-2">
                            <asp:UpdatePanel ID="UpdatePanel100" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txsalesmanPnl">
                                        <asp:TextBox ID="txcndn" runat="server" CssClass="form-control ro"></asp:TextBox>
                                    </asp:Panel>                                                                 
                                    <ajaxToolkit:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcndn_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList3" TargetControlID="txcndn" UseContextKey="True"
                                        CompletionListElementID="divwidthcndn" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="cndnselected">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
            <div class="h-divider"></div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <table class="mGrid" title="Invoice automatically selected with FIFO">
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
                                        <asp:Label ID="lbexp" runat="server" CssClass="form-control input-sm text-primary text-bold block padding-top-4"></asp:Label>
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
            <!-- end of form-horizontal -->
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6">
        </div>
    </div>
    <div class="row margin-top">
        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
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

    <div class=" margin-top">
        <div class="row">
            <asp:Label ID="lbamt" runat="server" Visible="False"></asp:Label>
            <asp:Button ID="btpaid" runat="server" Text="P A I D" CssClass="button2 btn add" OnClick="btpaid_Click" Visible="False" />
        </div>
    </div>
    <div class="divheader subheader subheader-bg">List Of Available Invoice</div>

    <div class="margin-bottom">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdinv" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" CaptionAlign="Top" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="True" OnSelectedIndexChanging="grdinv_SelectedIndexChanging" CssClass="table table-striped mygrid" AllowPaging="True" OnPageIndexChanging="grdinv_PageIndexChanging">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sys No.">
                            <ItemTemplate>
                                <asp:Label ID="lbinvno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inv No.">
                            <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# Eval("inv_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remain">
                            <ItemTemplate>
                                <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("balance") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <EmptyDataRowStyle Font-Bold="False" />
                    <FooterStyle CssClass="table-footer" />
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btsearchcust" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

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
                <asp:Label ID="lblocfile" runat="server" Text='SalesReturn Document'></asp:Label>
            </asp:HyperLink>
        </div>
    </div>

    <div class="row">
        <div class="navi margin-bottom">
            <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="refresh" Style="display: none" />

            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-new" OnClick="btnew_Click" />
            <asp:Button ID="btsave" runat="server" OnClientClick="javascript:ShowProgress();" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            <asp:Button ID="btcheckprice" runat="server" OnClick="btcheckprice_Click" OnClientClick="javascript:ShowProgress();" Text="Button" Style="display: none" />
            <asp:Button ID="btinvselect" runat="server" Style="display: none" OnClick="btinvselect_Click" Text="Button" />
        </div>
    </div>
    <table>
        <tr>
            <td style="position: relative">
                <div id="divwidthc" style="font-size: small; font-family: Calibri"></div>
                <div id="divwidthcndn" style="font-size: small; font-family: Calibri"></div>
                <div id="divitem" style="font-size: smaller; font-family: Calibri"></div>
            </td>

        </tr>
    </table>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


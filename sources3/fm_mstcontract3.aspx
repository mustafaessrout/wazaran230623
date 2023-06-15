<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcontract3.aspx.cs" Inherits="fm_mstcontract3" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }
        function ItemSelected(sender, e) {            
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=cbuom.ClientID%>').value = "";
            $get('<%=btreset.ClientID%>').click();

        }
        function SetDeliver() {
            $get('<%=btprice.ClientID%>').click();

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
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel43" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdto" runat="server" />
            <asp:HiddenField ID="hdcust" runat="server" />
            <asp:HiddenField ID="hdcust_otlcd" runat="server" />
            <asp:HiddenField ID="hditem" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="container">
        <h4 class="jajarangenjang">Order Free / Bonus Entry</h4>
        <div class="h-divider"></div>

        <div class="form-horizontal">

            <div class="row margin-bottom" style="background-color: yellow">
                <div class="col-md-12">
                    <label class="control-label col-md-1">Status</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbstatus" runat="server" CssClass="well well-sm no-margin danger text-bold text-white radius5 badge"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Salespoint</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbsalespoint" CssClass="form-control " runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">System No</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" CssClass="input-group" ID="txordernoPnl">
                                    <asp:TextBox ID="txorderno" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Panel runat="server" ID="btsearchsoPnl" CssClass="input-group-btn">
                                        <button id="btsearchso" class="btn btn-primary" type="submit" runat="server" onserverclick="btsearchso_ServerClick">
                                            <i class="fa fa-search" aria-hidden="true"></i>
                                        </button>
                                    </asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Date</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtorder" runat="server" CssClass="form-control "></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dtorder_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtorder">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <label class="control-label col-md-1">Warehouse</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbwhs" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Bin</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbbin" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbin_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Driver</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbdriver" CssClass="form-control " runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbdriver_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Delivery Date</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtdelivery" runat="server" CssClass="form-control " AutoPostBack="True" OnTextChanged="dtdelivery_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtdelivery">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <label class="control-label col-md-1">Loading No</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hddo" runat="server" />
                                <asp:TextBox ID="txmanualno" placeholder="Manual Loading" CssClass="form-control ro" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdmanualno" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Man Inv No</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txmanualinv" placeholder="Manual Inv Number" runat="server" CssClass="form-control  ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <label class="control-label col-md-1">Remark</label>
                    <div class="col-md-11">
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txremark" runat="server" CssClass="form-control require" placeholder="Put remark"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <label class="control-label col-md-1">Cust</label>
                    <div class="col-md-4">
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
                </div>
            </div>

            <div class="row margin-bottom">
                <div class="col-md-12" style="background-color: yellowgreen">
                    <div class="clearfix margin-bottom">
                        <div class="well well-sm ">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="clearfix">
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Address</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbaddress" runat="server" CssClass="padding-top-4 block text-primary">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Salesman</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-6 control-button titik-dua">Customer Type</label>
                                            <div class="col-sm-5">
                                                <asp:Label ID="lbcusttype" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">City</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbcity" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Term Payment</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbterm" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-6 control-button titik-dua">Customer Group</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lbcustgroup" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Contact</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbcontact" runat="server" CssClass=" block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua no-padding" style="padding-left: 15px !important;">VAT No.</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbvatno" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Blue"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 clearfix no-padding" style="margin-bottom: 10px;">
                                            <label class="col-sm-6 control-button titik-dua">Sales Block</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lbsalesblock" runat="server" CssClass="text-primary block padding-top-4">NO</asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua" style="font-size: 13px;">Vat English </label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lblVatEnglish" runat="server" Text="-" CssClass="text-primary block padding-top-4"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Vat Arabic</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblVatArabic" runat="server" Text="-" CssClass="text-primary block padding-top-4"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-6 control-button titik-dua">Credit Type</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lbcredittype" runat="server" Text="-" CssClass="text-primary block padding-top-4"></asp:Label>
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

            <div class="h-divider"></div>

            <div class="row margin-bottom">
                <div class="col-md-12">
                    <label class="control-label col-md-1">Promotion</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbdisctyp" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Type</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbtype_SelectedIndexChanged">
                                    <asp:ListItem Value="FG">By Quantity</asp:ListItem>
                                    <asp:ListItem Value="PC">By Percentage</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <strong>
                        <div runat="server" id="vTotalQty">
                            <label class="control-label col-md-1">Actual Sales</label>
                            <div class="col-md-1">
                            <asp:TextBox ID="txtotalsales" runat="server" CssClass="form-control ro"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-1">CTN</label>
                            <label class="control-label col-md-1">Total Free</label>
                            <div class="col-md-1">
                            <asp:TextBox ID="txtotalqty" runat="server" CssClass="form-control ro" AutoPostBack="true" OnTextChanged="txtotalqty_TextChanged"></asp:TextBox>
                            <asp:HiddenField ID="hdtotalqty" runat="server" />
                            </div>
                            <label class="control-label col-md-1">CTN</label>
                        </div>
                        </strong>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btcalc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
            <div class="row margin-bottom" runat="server" id="vPercentage">
                <div class="col-md-12">
                    <label class="control-label col-md-1">Sales Date</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtstartsales" runat="server" CssClass="form-control "></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender2" runat="server" Format="d/M/yyyy" TargetControlID="dtstartsales">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Between</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtendsales" runat="server" CssClass="form-control "></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender3" runat="server" Format="d/M/yyyy" TargetControlID="dtendsales">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Percentage</label>
                    <div class="col-md-1">
                        <asp:TextBox ID="txpercentage" runat="server" CssClass="form-control " ></asp:TextBox>                        
                    </div>
                    <label class="control-label col-md-1">%</label>
                    <div class="col-md-1">
                        <asp:Button ID="btcalc" Text="Calculation" runat="server" CssClass="btn-success btn btn-add" OnClick="btcalc_Click" OnClientClick="javascript:ShowProgress();"/>
                    </div>
                </div>
            </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbtype" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <div class="h-divider"></div>

            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div class="row margin-bottom" runat="server" id="vItem">
                    <div class="col-md-12">
                        <table class="mGrid">
                        <tr>
                            <th style="width: 30%">Item</th>
                            <th style="width: 5%">UOM</th>
                            <th style="width: 5%">Item Block PCS</th>
                            <th style="width: 5%">Price</th>
                            <th style="width: 5%">Qty</th>
                            <th style="width: 5%">Stock Available</th>
                            <th style="width: 5%">Qty Shipment</th>
                            <th style="width: 5%">Add</th>

                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control input-sm" OnTextChanged="txitemsearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitemsearch" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ShowOnlyCurrentWordInCompletionListItem="true">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbuom" onchange="javascript:ShowProgress();" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="cbuom_SelectedIndexChanged" AutoPostBack="True" OnClientClick="HideLoading();">
                                        </asp:DropDownList>
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
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
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
                                <asp:Button ID="btprice" runat="server" Text="Button" OnClick="btprice_Click" Style="display: none" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbstock" CssClass="form-control input-sm text-bold text-red" runat="server" Text="0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txshipmen" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm ro"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btadd_Click">Add</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lbqtyorder" runat="server" Text='<%# Eval("qty")%>'></asp:Label>
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
                                            <asp:Label ID="lbstockamt" runat="server" Text='<%# Eval("stock_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ship">
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotshipment" runat="server" Font-Size="X-Large" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbshipment" runat="server" Text='<%# Eval("qty_shipment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lbuom" runat="server" Text='<%# Eval("uom") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprice0" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubTotal">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal","{0:f2}") %>'></asp:Label>
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
                                    <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" />
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
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btcalc" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

    </div>

    <div class="h-divider"></div>
    <div class="navi margin-bottom">        
        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
        <ContentTemplate>
        <asp:LinkButton ID="btnew" runat="server" OnClick="btnew_Click" CssClass="btn btn-primary">New</asp:LinkButton>
        <asp:LinkButton ID="btsave" runat="server" OnClick="btsave_Click" CssClass="btn btn-success" OnClientClick="javascript:ShowProgress();">&nbsp;Save&nbsp;</asp:LinkButton>
        <asp:LinkButton ID="btprint" runat="server" OnClientClick="javascript:fn_getmanual();return false;" OnClick="btprint_Click" CssClass="btn btn-default ">Print Loading</asp:LinkButton>
        <asp:LinkButton ID="btprintinvoice" runat="server" OnClick="btprintinvoice_Click" OnClientClick="javascript:fn_getmanualinv();return false;" CssClass="btn btn-default ">Print Invoice</asp:LinkButton>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="btprintloading" runat="server" OnClick="btprintloading_Click" Text="Button" CssClass="divhid" />
        <asp:Button ID="btprintinvoice2" runat="server" CssClass="divhid" OnClick="btprintinvoice2_Click" Text="Button" />
        <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" OnClientClick="javascript:ShowProgress();"  Style="display: none"/>
        <asp:Button ID="btreset" runat="server" Style="display: none" OnClick="btreset_Click" Text="Button" />
    </div>
   
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


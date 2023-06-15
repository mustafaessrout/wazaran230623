<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_po.aspx.cs" Inherits="fm_po" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--  <script src="js/jquery-1.9.1.min.js"></script>--%>

    <script src="assets/bootstrap/js/bootstrap.min.js"></script>

    <style type="text/css">
        .divmsg {
            /*position:static;*/
            top: 30%;
            right: 50%;
            left: 50%;
            width: 200px;
            height: 200px;
            position: fixed;
            /*background-color:greenyellow;*/
            overflow-y: auto;
        }

        .divhid {
            display: none;
        }

        .divnormal {
            display: normal;
        }
    </style>
    <style>
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left: 10px;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
            width: 250px;
            padding: 0px;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
            width: 250px;
        }

        #divwidth {
            width: 250px !important;
        }

            #divwidth div {
                width: 250px !important;
            }

        .type1 {
            width: 20px;
            height: 20px;
            display: inline-block;
            background: yellow;
        }

            .type1.green {
            }

            .type1.red {
            }

        .lblHOStat {
            vertical-align: top;
            line-height: 20px;
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>
    <script>
        function openwindow3() {
            var oNewWindow = window.open("fm_lookup_po.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl3() {
            document.getElementById('<%=bttmp3.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function openwindow1() {
            var oNewWindow = window.open("fm_lookupitem.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function openwindow(sUrl) {
            var oNewWindow = window.open(sUrl, "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

       <%-- function updpnl()
        {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }--%>

        function updpnl2(itemid) {
            document.getElementById('<%=cbmaster.ClientID%>').value = itemid;
            document.getElementById('<%=bttmp2.ClientID%>').focus()

            return (false);
        }

        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btstock.ClientID%>').click();
           <%-- $get('<%=txqty.ClientID%>').focus();--%>
            //sGetStockAmount();
        }

        <%--function ItemSelected1(sender, e) {
            $get('<%=hditem1.ClientID%>').value = e.get_value();
            $get('<%=btstock.ClientID%>').click();
            $get('<%=txqty.ClientID%>').focus();
            //sGetStockAmount();
        }--%>

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearchpo.ClientID%>').click();
        }

        function ClearItem() {
            $get('<%=txitemname.ClientID%>').ClearItem();
            <%--$get('<%=txitemname1.ClientID%>').ClearItem();--%>
        }
    </script>
    <script>
        function scrollWin() {
            window.scrollTo(0, 2000);
        }
    </script>
    <%--<script type="text/javascript">
        function sGetStockAmount() {
            $.ajax({
                type: "POST",
                url: "fm_po.aspx/sGetStockAmount",
                data: '{name: "' + $("#<%=txstock.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    $get('<%=txstock.ClientID%>').value = response.d;
                }
            });
            }
            function OnSuccess(response) {
                //alert(response.d);
                $get('<%=txstock.ClientID%>').value = response.d;
              }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4 class="divheader">Branch Order Request</h4>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row clearfix">
            <div class="clearfix no-padding margin-bottom col-md-3 col-sm-6">
                <label class="control-label col-sm-4">Salespoint</label>
                <div class="col-sm-8 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <strong>
                                <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                                <%--<asp:Label ID="lbsalespoint" runat="server" Text="" CssClass="form-control"></asp:Label>--%>
                            </strong>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix no-padding margin-bottom col-md-3 col-sm-6">
                <label class="control-label col-sm-4">Status</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbstatus" runat="server" Text="NEW ENTRY" CssClass="well well-sm no-margin danger text-white badge"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix no-padding margin-bottom col-md-3 col-sm-6">
                <label class="control-label col-sm-4">PO No.</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <div class="input-group">
                                <asp:TextBox ID="txpono" runat="server" CssClass="form-control " Enabled="False"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearch3" runat="server" OnClientClick="openwindow3();return(false);" CssClass="btn btn-primary btn-search"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <%-- <asp:Button ID="btsearch3s" runat="server" CssClass="button2 search" OnClientClick="openwindow3();return(false);" />--%>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bttmp3" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:Button ID="bttmp3" runat="server" Text="Button" OnClick="bttmp3_Click" Style="display: none" />
                </div>
            </div>
            </div>

            <div class="row clearfix">
                <div class="clearfix no-padding margin-bottom col-md-3 col-sm-6">
                    <label class="control-label col-sm-4">Order Type</label>
                    <div class="col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <strong>
                                    <asp:DropDownList ID="cbordertype" runat="server" CssClass="form-control" ></asp:DropDownList>
                                </strong>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix no-padding margin-bottom col-md-3 col-sm-6">
                    <label class="control-label col-sm-4">Date</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="dtpo" runat="server" CssClass="form-control ro"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtpo_CalendarExtender" runat="server" Format="d/MM/yyyy" TargetControlID="dtpo" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            
                <div class="clearfix no-padding margin-bottom col-md-3 col-sm-6">
                    <label class="control-label col-sm-4">Delivery</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="dtpo_delivery_dt" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtpo_delivery_dt_CalendarExtender" runat="server" Format="d/MM/yyyy" TargetControlID="dtpo_delivery_dt" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
            <div class="clearfix no-padding margin-bottom col-md-6 col-sm-6">
                <label class="control-label col-sm-2">Destination</label>
                <div class="col-sm-6 drop-down">
                    <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbtype_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4">
                    <ol class="no-padding">
                        <li>1. Depo / Branch</li>
                        <li>2. Direct Customer</li>
                        
                        <%--<li>3. Sub Depo</li>--%>
                    </ol>
                </div>
            </div>
            <div class="clearfix no-padding margin-bottom col-md-3 col-sm-6">
                <label class="control-label col-sm-4">To</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbmaster" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbmaster_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Button ID="bttmp2" runat="server" OnClick="bttmp2_Click" Text="Button" Style="display: none" />
                            <asp:HiddenField ID="hdcust" runat="server" />
                            <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txsearch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txsearch" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionListCust" UseContextKey="True" CompletionListElementID="divwidth1" ShowOnlyCurrentWordInCompletionListItem="true">
                            </asp:AutoCompleteExtender>
                            <%--  <asp:Button ID="btsearchpox" runat="server" OnClick="btsearchpo_Click" style="left: 0px; top: 0px" />--%>
                            <asp:LinkButton ID="btsearchpo" Style="display: none" runat="server" OnClick="btsearchpo_Click">Search</asp:LinkButton>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbtype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="clearfix ">
            <div class="row no-padding margin-bottom col-sm-6">
                <label class="control-label col-sm-2 no-padding-left">PO Customer</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txpocust" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="control-label col-sm-2">PO Date</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="dtpocust" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtpocust_CalendarExtender" runat="server" Format="dd/M/yyyy" TargetControlID="dtpocust">
                    </asp:CalendarExtender>
                </div>
                
            </div>
        </div>

        <div class="row">
            <div class="h-divider"></div>
        </div>

        <div class="row clearfix">
            <div class="clearfix">
                <label class="control-label col-md-1 col-sm-2">Address</label>
                <div class="col-md-11 col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <table class="table table-bordered mygrid no-margin-top">
                                <tr>
                                    <th>Address</th>
                                    <th>City</th>
                                    <th>Phone</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbaddress" runat="server" CssClass="form-control input-sm"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbcity" runat="server" CssClass="form-control input-sm"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbphone" runat="server" CssClass="form-control input-sm"></asp:Label></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btsearchpo" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="h-divider"></div>
        </div>
        <div class="row clearfix">
            <div class="clearfix no-padding margin-bottom col-md-4 col-sm-6">
                <label class="control-label col-sm-3">Supplier</label>
                <div class="col-sm-9 drop-down">
                    <asp:DropDownList ID="cbvendor" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clearfix no-padding margin-bottom col-md-8 col-sm-6">
                <label class="control-label col-md-1 col-sm-2">Remark</label>
                <div class="col-md-11 col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txremark" runat="server" CssClass="form-control" Height="95" Style="resize: none;" TextMode="MultiLine"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <div class="h-divider"></div>
            <h1 class="divheader">Summary Loading Request</h1>
            <div class="row" runat="server" id="vReqLoading">
                <div class="form-group">
                    <div class="col-md-12">
                        <asp:GridView ID="grdloading" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid table-hover" CellPadding="0" ShowFooter="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate><%# Container.DataItemIndex + 1 %>.</ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
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
                                        <ItemTemplate>
                                            <%# Eval("branded_nm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <%# Eval("uom") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotqty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Current">
                                        <ItemTemplate>
                                            <asp:Label ID="lbqty_stock" runat="server" Text='<%# Eval("qty_stock") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="table-header" />
                                <FooterStyle CssClass="table-footer" />
                                <EditRowStyle CssClass="table-edit" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle />
                            </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>

        <div class="h-divider"></div>
        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
        <div class="row" runat="server" id="vNotSpecial" >
            <div class="clearfix margin-bottom">
                <div class="col-md-12">
                    <table class="table mygrid">
                        <tr>
                            <th style="width: 60%">Item</th>
                            <th style="width: 10%">Uom</th>
                            <th style="width: 10%">Qty</th>
                            <th style="width: 10%">Avl Stk</th>
                            <th style="width: 10%">Max Stk</th>
                            <th style="width: 10%">Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="txitemnamePnl">
                                            <asp:TextBox ID="txitemname" runat="server" CssClass="form-control input-sm" AutoPostBack="True"></asp:TextBox>
                                            <div id="divwidthi"></div>
                                            <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txitemname_AutoCompleteExtender" runat="server" ServiceMethod="GetItemList" TargetControlID="txitemname" UseContextKey="True" OnClientItemSelected="ItemSelected" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidthi">
                                            </asp:AutoCompleteExtender>
                                        </asp:Panel>
                                        <asp:HiddenField ID="hditem" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 100px;" class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="cbuomPnl">
                                            <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbuom_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="txqtyPnl">
                                            <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" type="number"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txqty"
                                                FilterType="Numbers" ValidChars="0123456789" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <strong>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txstock" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdstock" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btstock" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </strong>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                            <asp:TextBox ID="txmax_stock" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="btadd" runat="server" CssClass="btn btn-block btn-success btn-sm" OnClick="btadd_Click" Text="Add" AutoPostBack="true"  />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <asp:GridView ID="grdpo" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdpo_RowDeleting" OnRowEditing="grdpo_RowEditing" OnRowUpdating="grdpo_RowUpdating" CssClass="table table-striped mygrid table-hover" CellPadding="0" ShowFooter="True" OnRowCreated="grdpo_RowCreated" OnRowDataBound="grdpo_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %>.</ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
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
                                <ItemTemplate>
                                    <%# Eval("branded_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Old Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" type="number" Text='<%# Eval("qty") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotqty" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GS">
                                <ItemTemplate><%# Eval("stock_gs") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FA">
                                <ItemTemplate><%# Eval("stock_fa") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NE">
                                <ItemTemplate><%# Eval("stock_ne") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemTemplate>
                                    <asp:Label ID="lbunitprice" runat="server" Text='<%# Eval("unit_price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub Total">
                                <ItemTemplate>
                                    <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                        </Columns>
                        <HeaderStyle CssClass="table-header" />
                        <FooterStyle CssClass="table-footer" />
                        <EditRowStyle CssClass="table-edit" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                    </asp:GridView>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btsearchpo" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="bttmp3" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>

        <div class="row">
            <div class="form-group">
                <div class="navi margin-bottom padding-bottom margin-top">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <%--<asp:LinkButton ID="btnew" OnClick="btedit_Click1" CssClass="btn btn-primary" runat="server">New</asp:LinkButton>--%>
                            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn btn-success btn-new" OnClick="btnew_Click" />
                            <asp:Button ID="btedit" runat="server" Text="Edit" CssClass="btn btn-primary btn-edit" OnClick="btedit_Click1" />
                            <asp:Button ID="btsave" runat="server" Text="Save" OnClick="btsave_Click" CssClass="btn btn-warning btn-save" />
                            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info btn-print" OnClick="btprint_Click" />
                            <asp:Button ID="btstock" runat="server" Text="Button" OnClick="btstock_Click" style="display:none"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <div class="divmsg loading-cont" id="dvshow">
        <div>
            <i class="fa fa-spinner spiner fa-spin fa-3x fa-fw" aria-hidden="true"></i>
        </div>
    </div>


</asp:Content>


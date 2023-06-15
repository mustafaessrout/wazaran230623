<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_takeorderentry_ho2.aspx.cs" Inherits="fm_takeorderentry_ho2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=cbuom.ClientID%>').value = "";
            $get('<%=cbuom.ClientID%>').focus();
        }
        function RefreshData(socd) {
            $get('<%=hdto.ClientID%>').value = socd;
            $get('<%=txorderno.ClientID%>').value = socd;
            $get('<%=btrefresh.ClientID%>').click();

        }
        function btfreeclick() {
            //alert('test');
            $get('<%=btfree.ClientID%>').click();
        }

        function openwindow(url) {
            window.open(url, url, "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=800px, height=400px, top=200px, left=400px", true);
        }
        function HideLoading() {
            dvshow.className = "divhid";
        }
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdto" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hditem" runat="server" />

    <h4 class="jajarangenjang">Take Order Entry (HO)</h4>
    <div class="h-divider"></div>
    <div class="container">
        <div class="form-horizontal">

            <div class="row margin-bottom" style="background-color: yellow">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsalespoint" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Status</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbstatus" runat="server" CssClass="well well-sm no-margin danger text-bold text-white radius5 badge"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Source</label>
                <div class="col-md-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsourceorder" CssClass="form-control " runat="server">
                                <asp:ListItem Value="MAN" Text="MANUAL"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">TO No</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" CssClass="input-group" ID="txordernoPnl">
                                <asp:TextBox ID="txorderno" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Panel runat="server" ID="btsearchsoPnl" CssClass="input-group-btn">
                                    <button id="btsearchso" class="btn btn-primary" type="submit" runat="server" onserverclick="btsearchso_Click">
                                        <i class="fa fa-search" aria-hidden="true"></i>
                                    </button>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Source Info</label>
                <div class="col-md-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsourseinfo" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsourseinfo_SelectedIndexChanged">
                            </asp:DropDownList>
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
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Inv No</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txinvoiceno" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Manual/Ref No</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txmanualinv" placeholder="Manual Inv Number" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Man Inv Free No</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txmaninvfreeno" placeholder="Manual Free Invoice" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Remark</label>
                <div class="col-md-11">
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txremark" runat="server" CssClass="form-control require" placeholder="Put remark"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
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
                <label class="col-md-1 control-label">Cust PO</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txpocust" placeholder="PO From Cust" CssClass="form-control" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <label class="col-md-1 control-label">Cust PO Date</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtcustpo" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="dtcustpo_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtcustpo">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                                            <label class="col-sm-4 control-button titik-dua">Customer Type</label>
                                            <div class="col-sm-8">
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
                                            <label class="col-sm-4 control-button titik-dua">Customer Group</label>
                                            <div class="col-sm-8">
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
                                            <label class="col-sm-4 control-button titik-dua">VAT No.</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbvatno" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Blue"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Sales Block</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbsalesblock" runat="server" CssClass="text-primary block padding-top-4">NO</asp:Label>
                                            </div>
                                        </div>              
                                        
                                                                  
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Credit Type</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbcredittype" runat="server" Text="-" CssClass="text-primary block padding-top-4"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">
                                                <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbcapcredit" runat="server" Text="CL"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lbcredit" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">CL Remain</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="txclremain" runat="server" CssClass="text-primary block padding-top-4">-</asp:Label>
                                            </div>
                                        </div>
                                         
                                        <div class="col-md-3 col-sm-6 clearfix no-padding">
                                            <label class="col-sm-4 control-button titik-dua">Max Avl Trns</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbmaxtrans" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 clearfix no-padding ">
                                            <label class="col-sm-4 control-button titik-dua">Pay Promised Amt</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lbpromised" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                                        <span class="text-bold block padding-top-4 inline-block padding-right">,Overdue Amt : </span>
                                                        <asp:Label ID="lboverdue" runat="server" CssClass="text-primary block padding-top-4 inline-block padding-right" Text="0" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                                        <asp:Label ID="lbcapbalance" runat="server" Text="Balance" CssClass="text-bold block padding-top-4 inline-block padding-right"></asp:Label>
                                                        <asp:Label ID="lbbalance" runat="server" CssClass="text-primary block padding-top-4 inline-block padding-right" Text="0"></asp:Label>
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
                <div class="col-md-12">
                    <table class="mGrid">
                        <tr>
                            <th style="width: 20%">Item</th>
                            <th style="width: 10%">UOM</th>
                            <th style="width: 5%">Price</th>
                            <th style="width: 5%">Stk Cust</th>
                            <th style="width: 5%">Qty order</th>
                            <th style="width: 5%">Stock Available</th>
                            <th style="width: 5%">Qty Shipment</th>
                            <th style="width: 5%">Add</th>

                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control input-sm" OnTextChanged="txitemsearch_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitemsearch" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ShowOnlyCurrentWordInCompletionListItem="true">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="cbuom_SelectedIndexChanged" AutoPostBack="True">
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
                                <asp:Button ID="btprice" runat="server" Text="Button" OnClick="btprice_Click" Style="display: none" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txstockcust" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbstock" CssClass="form-control input-sm text-bold text-red" runat="server" Text="0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
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
                                    <asp:TemplateField HeaderText="Disc">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdisc" runat="server"></asp:Label>
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
                            <asp:AsyncPostBackTrigger ControlID="btdisc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-4 col-md-offset-8">
                    <table class="table tab-content">
                        <tr>
                            <th style="text-align: right; width: 50%">VAT 5 % :                       
                                <th style="text-align: left; width: 50%">
                                    <strong>
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lbvat" runat="server" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                    </strong>
                                </th>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <div style="text-align: center">
                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btdisc" runat="server" Text="DISCOUNT CALCULATION " CssClass="btn btn-primary" OnClick="btdisc_Click" TabIndex="18" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-12">
                    <div class="margin-bottom">
                        <div style="width: 100%">
                            <div style="float: left; width: 50%; vertical-align: top; padding: 10px 10px 10px 10px">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grddisc" runat="server" AutoGenerateColumns="False" Caption="Discount Applied" CaptionAlign="Top" GridLines="None" ShowHeaderWhenEmpty="True" CellPadding="0" OnRowEditing="grddisc_RowEditing" OnRowCancelingEdit="grddisc_RowCancelingEdit" OnRowDataBound="grddisc_RowDataBound" OnRowUpdating="grddisc_RowUpdating" OnSelectedIndexChanging="grddisc_SelectedIndexChanging" CssClass="mGrid" EmptyDataText="No Discount Hit">
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
                                </asp:UpdatePanel>
                            </div>
                            <div style="float: left; width: 50%; position: relative; padding: 10px 10px 10px 10px; vertical-align: top; font-size: small; font-family: Calibri">
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
            <div class="h-divider"></div>

            <div class="form-group">
                <div class="col-md-12">
                    <div class="navi margin-bottom">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btsearch" runat="server" OnClick="btsearch_Click" Style="display: none" />
                                <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" Style="display: none" />
                                <asp:LinkButton ID="btnew" runat="server" OnClick="btnew_Click" CssClass="btn btn-primary">New</asp:LinkButton>
                                <asp:LinkButton ID="btedit" runat="server" OnClick="btedit_Click" CssClass="btn btn-default ">Edit</asp:LinkButton>
                                <asp:LinkButton ID="btsave" runat="server" OnClick="btsave_Click" CssClass="btn btn-success">&nbsp;Save&nbsp;</asp:LinkButton>
                                <asp:LinkButton ID="btcancel" runat="server" OnClick="btcancel_Click" CssClass="btn btn-default ">Cancel</asp:LinkButton>

                                <%--<asp:Button ID="btprintinvoice2" runat="server" CssClass="divhid" OnClick="btprintinvoice2_Click" Text="Button" />
                                <asp:LinkButton ID="btprintinvoice" runat="server" OnClick="btprintinvoice_Click" OnClientClick="javascript:fn_getmanualinv();return false;" CssClass="btn btn-default ">Print Invoice</asp:LinkButton>
                                <asp:LinkButton ID="btprintvat" CssClass="btn btn-info" runat="server" OnClick="btprintvat_Click">Print VAT</asp:LinkButton>--%>


                                <asp:Button ID="btupdman" runat="server" OnClick="btupdman_Click" Text="Button" CssClass="divhid" />
                                <asp:Button ID="btfree" runat="server" Text="Button" OnClick="btfree_Click" Style="display: none" />

                                <%--<asp:Button ID="btprintloading" runat="server" OnClick="btprintloading_Click" Text="Button" CssClass="divhid" />
                                <asp:Button ID="btprintfreeinv2" runat="server" OnClick="btprintfreeinv2_Click" Text="Button" CssClass="divhid" />--%>

                                <%--<asp:Button ID="btfree" runat="server" Text="Button" OnClick="btfree_Click" Style="display: none" />
                                <asp:LinkButton ID="btprint" runat="server" OnClientClick="javascript:fn_getmanual();return false;" OnClick="btprint_Click" CssClass="btn btn-default ">Print Loading</asp:LinkButton>
                                <asp:LinkButton ID="btprintfreeinv" runat="server" OnClick="Button1_Click" CssClass="btn btn-default " OnClientClick="javascript:fn_getmanualfreeinv();return false;">Print Invoice Free</asp:LinkButton>
                                <asp:Button ID="btcopyorder" runat="server" Text="Copy Order"  OnClick="btcopyorder_Click" Visible="False" />
                                <asp:LinkButton ID="btpanda" runat="server" CssClass="btn btn-primary" style="display:none" OnClick="btpanda_Click">Edited KA Price</asp:LinkButton>--%>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>        
    </div>

    <div id="divwidthi" style="font-family: Calibri; font-size: small; position: relative">
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


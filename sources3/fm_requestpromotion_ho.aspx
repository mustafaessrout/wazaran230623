<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_requestpromotion_ho.aspx.cs" Inherits="fm_requestpromotionho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }
        function ItemFreeSelected(sender, e) {
            $get('<%=hditemfree.ClientID%>').value = e.get_value();
        }
        function PropSelected(sender, e) {
            $get('<%=hdproposal.ClientID%>').value = e.get_value();
        }
    </script>
    <script>
        function PopupCenter(url, title, w, h) {
            // Fixes dual-screen position                         Most browsers      Firefox
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;
            var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

            // Puts focus on the newWindow
            if (window.focus) {
                newWindow.focus();
            }
        }

        function SelectDiscount(sVal) {
            ShowProgress();
            $get('<%=hdpromo.ClientID%>').value = sVal;
            $get('<%=btdiscount.ClientID%>').click();
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
    <style>
        .product-border{
            font-size:16px;
            margin-top: -10px;
            background-color:#fff;
            padding: 5px;
            font-weight: bold;
        }
        fieldset.product-border {
            border: 1px solid #bebebe !important;
            padding: 0 1.4em 0 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            border-radius: 5px;
        }
        .content-hover{
            /*display:none;*/
        }
        .hover-title .subheader{
            cursor:pointer;
            position:relative;
        }
        .hover-title .subheader::after{
            content: "\f063";
            font-family: fontawesome;
            position: absolute;
            color: #fff;
            font-size: 14px;
            right: 20px;
            top: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdproposal" runat="server" />
    <asp:HiddenField ID="hdpromo" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hditemfree" runat="server" />
    <asp:Button ID="btdiscount" runat="server" Text="Button" Style="display: none" OnClick="btdiscount_Click" />

    <div class="form-horizontal" style="font-family: Calibri">
        <h4 class="jajarangenjang">
            <asp:Label ID="lbtitle" runat="server" Text="Request Promotion (Approval)"></asp:Label>
        </h4>
        <div class="h-divider"></div>

        <fieldset class="hover-title">
            
            <div class="content-hover" id="promo1">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row margin-top">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="Proposal" class="col-xs-4 col-form-label col-form-label-sm">Proposal No</label>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="txproposal" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetListProposal" TargetControlID="txproposal" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="PropSelected" >
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="salespoint" class="col-xs-4 col-form-label col-form-label-sm">Type</label>
                                    <div class="col-xs-8">
                                        <asp:RadioButtonList ID="rdpromotion" runat="server" CssClass="col-form-label-sm" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" >
                                            <asp:ListItem Value="BB">Promotion By Branch</asp:ListItem>
                                            <asp:ListItem Value="PD">Promotion Deal</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="salespoint" class="col-xs-4 col-form-label col-form-label-sm">Branch Request</label>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="lbSp" runat="server" CssClass="form-control input-sm" Font-Bold="true" ReadOnly="true" Enabled="false" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">                        
                                <div class="form-group">
                                    <label for="promotionNo" class="col-xs-4 col-form-label col-form-label-sm">Promotion No</label>
                                    <div class="col-xs-8">
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <div class="input-group">
                                                <asp:Label ID="lbpromotionno" runat="server" CssClass="form-control input-sm"   BorderStyle="Solid" BorderWidth="1px">New</asp:Label>
                                                <div class="input-group-btn">
                                                    <asp:LinkButton ID="btsearchpromotion" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchpromotion_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="startDate" class="col-xs-4 col-form-label col-form-label-sm">Start Date</label>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtstart">
                                            </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="endDate" class="col-xs-4 col-form-label col-form-label-sm">End Date</label>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtend">
                                            </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="deliveryDate" class="col-xs-4 col-form-label col-form-label-sm">Delivery Date</label>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="dtdelivery" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="dtdeliveryd_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtdelivery">
                                            </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="cbp" class="col-xs-4 col-form-label col-form-label-sm">CBP</label>
                                    <div class="col-xs-2">
                                        <asp:TextBox ID="txcbp" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="cbp_uom" runat="server" CssClass="form-control input-sm" Width="100%" >
                                            <asp:ListItem Value="CTN">CTN</asp:ListItem>
                                            <asp:ListItem Value="PCS">PCS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="kindpromotion" class="col-xs-4 col-form-label col-form-label-sm">Kinds of Promotion</label>
                                    <div class="col-xs-8">
                                        <asp:DropDownList ID="cbpromokind" runat="server" CssClass="form-control input-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="cbpromokind_SelectedIndexChanged">
                                            <asp:ListItem Value="ATL">ATL</asp:ListItem>
                                            <asp:ListItem Value="BTL">BTL</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="promocd" class="col-xs-4 col-form-label col-form-label-sm">Promotion Group</label>
                                    <div class="col-xs-8">
                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                        <ContentTemplate>
                                        <asp:DropDownList ID="cbpromogroup" runat="server" CssClass="form-control input-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="cbpromogroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbpromokind" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="promotyp" class="col-xs-4 col-form-label col-form-label-sm">Promotion Type</label>
                                    <div class="col-xs-8">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        <asp:DropDownList ID="cbpromotype" runat="server" CssClass="form-control input-sm" Width="100%">
                                        </asp:DropDownList>
                                        </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="txRemark" class="col-xs-2 col-form-label col-form-label-sm">Remarks</label>
                                    <div class="col-xs-10">
                                        <asp:TextBox ID="txremark" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="customer" class="col-xs-4 col-form-label col-form-label-sm">Customer</label>
                                    <div class="col-xs-8">
                                        <asp:DropDownList ID="rdcust" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdcust_SelectedIndexChanged" CssClass="form-control input-sm"  Width="100%">
                                            <asp:ListItem Value="C">Customer</asp:ListItem>
                                            <asp:ListItem Value="G">Customer Group</asp:ListItem>
                                            <asp:ListItem Value="T">Customer Type</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <div class="col-xs-8">
                                            <asp:TextBox ID="txsearchcust" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txsearchcust_AutoCompleteExtender" runat="server" ServiceMethod="GetListCustomer" TargetControlID="txsearchcust" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="CustSelected" >
                                                </asp:AutoCompleteExtender>
                                            <asp:DropDownList ID="cbcusgrcd" runat="server" AutoPostBack="True" CssClass="form-control input-sm"  >
                                            </asp:DropDownList>
                                            </div>
                                            <div class="col-xs-4">
                                                <asp:Button ID="btaddcust" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btaddcust_Click"  />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdcust" AllowPaging="True" 
                                            OnPageIndexChanging="grdcust_PageIndexChanging" OnRowDeleting="grdcust_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Cust Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cust Name">
                                                    <ItemTemplate>
                                                        <%# Eval("cust_nm") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cust Type">
                                                    <ItemTemplate>
                                                        <%# Eval("otlcd") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salespoint Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsalespointcd" runat="server" Text='<%# Eval("salespoint_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="grdcusgrcd" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="grdcusgrcd_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Group Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbcusgrcd" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Group Name">
                                                    <ItemTemplate><%# Eval("cusgrcd_nm") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="grdcusttype" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="grdcusttype_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbcusttype" runat="server" Text='<%# Eval("otlcd") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Type">
                                                    <ItemTemplate>
                                                        <%# Eval("custtyp_nm") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btaddcust" EventName="click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="row" runat="server" id="branch">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="customer" class="col-xs-4 col-form-label col-form-label-sm">Branch</label>
                                    <div class="col-xs-6">
                                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control input-sm"  Width="100%" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Button ID="btaddbranch" runat="server" Text="Add" CssClass="btn btn-default" Enabled="false"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                    <div class="table-responsive">
                                         <asp:GridView ID="grdsalespoint" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Salespoint Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespoint_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salespoint Name">
                                                    <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btaddbranch" EventName="click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="customer" class="col-xs-4 col-form-label col-form-label-sm">Item/Product</label>
                                    <div class="col-xs-4">
                                        <asp:DropDownList ID="rditem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rditem_SelectedIndexChanged" CssClass="form-control input-sm"  Width="100%">
                                            <asp:ListItem Value="I">Item</asp:ListItem>
                                            <asp:ListItem Value="G">Group Product</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xs-4">
                                        <asp:updatepanel runat="server">
                                           <ContentTemplate>                                  
                                               <asp:TextBox ID="txsearchitem" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                               <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" ServiceMethod="GetListItem" TargetControlID="txsearchitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="1" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected">
                                               </asp:AutoCompleteExtender>
                                                <asp:DropDownList ID="cbgroup" runat="server" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                           </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="rditem" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:updatepanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="col-xs-2">
                                            <asp:DropDownList ID="cbbgitem" runat="server" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="cbbgitem_SelectedIndexChanged">
                                                <asp:ListItem Value="A">Type 1</asp:ListItem>
                                                <asp:ListItem Value="B">Type 2</asp:ListItem>
                                                <asp:ListItem Value="C">Type 3</asp:ListItem>
                                                <asp:ListItem Value="D">Type 4</asp:ListItem>
                                                <asp:ListItem Value="E">Type 5</asp:ListItem>
                                            </asp:DropDownList> 
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="col-xs-8">
                                                <div runat="server" id="bga">
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitemax" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lbbgitemaplus" runat="server" CssClass="col-xs-1 col-form-label col-form-label-sm">+</asp:Label>
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitemay" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lbbgitemalimit" runat="server" CssClass="col-xs-1 col-form-label col-form-label-sm">Limit</asp:Label>
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitemalimit" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div runat="server" id="bgb">
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitembx" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lbbgitembpct" runat="server" CssClass="col-xs-3 col-form-label col-form-label-sm">%, Target</asp:Label>
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitemby" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <asp:DropDownList ID="cbbgitembtarget" runat="server" CssClass="form-control input-sm" >
                                                            <asp:ListItem Value="Q">QTY</asp:ListItem>
                                                            <asp:ListItem Value="S">SAR</asp:ListItem>
                                                        </asp:DropDownList> 
                                                    </div>
                                                </div>                                                
                                                <div runat="server" id="bgc">
                                                    <asp:Label ID="lbbgitemcdisc" runat="server" CssClass="col-xs-1 col-form-label col-form-label-sm">Disc Item</asp:Label>
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitemcx" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lbbgitemcmax" runat="server" CssClass="col-xs-1 col-form-label col-form-label-sm">SAR, Max</asp:Label>
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitemcy" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lbbgitemcqty" runat="server" CssClass="col-xs-1 col-form-label col-form-label-sm">Qty</asp:Label>
                                                </div>
                                                <div runat="server" id="bgd">
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitemdx" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lbbgitemdpct" runat="server" CssClass="col-xs-1 col-form-label col-form-label-sm">%</asp:Label>
                                                </div>
                                                <div runat="server" id="bge">
                                                    <div class="col-xs-3">
                                                        <asp:TextBox ID="txbgitemex" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lbbgitemesar" runat="server" CssClass="col-xs-1 col-form-label col-form-label-sm">SAR</asp:Label>
                                                </div> 
                                            </div>
                                        </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbbgitem" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="col-xs-2">
                                            <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btadd_Click"  />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="grditem" AllowPaging="True" OnRowDeleting="grditem_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate><%# Eval("item_bg") %></ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="x">
                                                    <ItemTemplate><%# Eval("x") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="y">
                                                    <ItemTemplate><%# Eval("y") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="target">
                                                    <ItemTemplate><%# Eval("target") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RBP - Before">
                                                    <ItemTemplate><%# Eval("price_rbp_before") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RBP - After">
                                                    <ItemTemplate><%# Eval("price_rbp") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="grdgroup" AllowPaging="True" OnRowDeleting="grdgroup_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Group Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgroupcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Group Name">
                                                    <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate><%# Eval("prod_bg") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="X">
                                                    <ItemTemplate><%# Eval("x") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Y">
                                                    <ItemTemplate><%# Eval("y") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Target">
                                                    <ItemTemplate><%# Eval("target") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RBP - Before">
                                                    <ItemTemplate><%# Eval("price_rbp_before") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RBP - After">
                                                    <ItemTemplate><%# Eval("price_rbp") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btadd" EventName="click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="lbfreegood" class="col-xs-3 col-form-label col-form-label-sm">Have Freegoods ?</label>
                                    <asp:RadioButtonList ID="rdFreegood" runat="server" CssClass="col-xs-1 col-form-label-sm" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdFreegood_SelectedIndexChanged" >
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:updatepanel runat="server">
                                    <ContentTemplate>      
                                    <div id="viewFreegood" runat="server">
                                        <div class="col-xs-2">
                                            <asp:DropDownList ID="rditemfree" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rditemfree_SelectedIndexChanged" CssClass="form-control input-sm"  Width="100%">
                                                <asp:ListItem Value="I">Item</asp:ListItem>
                                                <asp:ListItem Value="G">Product</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:updatepanel runat="server">
                                               <ContentTemplate>                                  
                                                   <asp:TextBox ID="txsearchitemfree" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                   <asp:AutoCompleteExtender ID="txsearchitemfree_AutoCompleteExtender" runat="server" ServiceMethod="GetListItem" TargetControlID="txsearchitemfree" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="1" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="ItemFreeSelected">
                                                   </asp:AutoCompleteExtender>
                                                    <asp:DropDownList ID="cbgroupfree" runat="server" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                               </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="rditemfree" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:updatepanel>
                                        </div>
                                        <div class="col-xs-2">
                                            <asp:Button ID="btaddfree" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btaddfree_Click"  />
                                        </div>
                                    </div>
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rdFreegood" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:updatepanel>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="grditemfree" AllowPaging="True" OnRowDeleting="grditemfree_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="grdgroupfree" AllowPaging="True" OnRowDeleting="grdgroupfree_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Group Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgroupcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Group Name">
                                                    <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btaddfree" EventName="click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="budgetLimit" class="col-xs-4 col-form-label col-form-label-sm">Budget Limit</label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txbudget" runat="server" CssClass="form-control input-sm"></asp:TextBox>                                        
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                        <asp:Label ID="lbsar" runat="server" Text="CTN" class="col-xs-2 col-form-label col-form-label-sm"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbpaymenttype" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="paymentType" class="col-xs-4 col-form-label col-form-label-sm">Payment Type</label>
                                    <div class="col-xs-8">
                                        <asp:DropDownList ID="cbpaymenttype" runat="server" CssClass="form-control input-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="cbpaymenttype_SelectedIndexChanged">
                                            <asp:ListItem Value="ATL">ATL</asp:ListItem>
                                            <asp:ListItem Value="BTL">BTL</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="approveby" class="col-xs-4 col-form-label col-form-label-sm">Approve By</label>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="txapproval" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </fieldset>

        <div class="h-divider"></div>
        <div class="clearfix margin-bottom">
            <label class="col-md-2 col-sm-4 control-label">Remarks (if Rejected.)</label>
            <div class="col-md-8 col-sm-8">
                <asp:TextBox ID="txnoted" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

    </div>

    <div class="row margin-bottom">
      <div class="col-sm-12">
        <div class="text-center navi">
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
            <ContentTemplate>
            <button type="submit" class="btn btn-success btn-sm" runat="server" id="btnew" onserverclick="btnew_ServerClick" >
              <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> New</button>
            <asp:Button ID="btedit" runat="server" Text="Edit" class="btn btn-info btn-sm" OnClick="btedit_ServerClick" />
            <asp:Button ID="btapprove" runat="server" Text="Approve" class="btn btn-primary btn-sm" OnClick="btapprove_Click" />
            <asp:Button ID="btreject" runat="server" Text="Reject" class="btn btn-warning btn-sm" OnClick="btreject_Click" />

            <button type="submit" class="btn btn-info btn-sm" runat="server" id="btprint" onserverclick="btprint_ServerClick" >
              <span class="glyphicon glyphicon-print" aria-hidden="true"></span> Print
            </button>         
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
      </div>
    </div>  

</asp:Content>


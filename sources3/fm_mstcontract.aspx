<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcontract.aspx.cs" Inherits="fm_mstcontract" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script>
        function PropSelected(sender, e) {
            $get('<%=hdprop.ClientID%>').value = e.get_value();
            $get('<%=btlookup.ClientID%>').click();
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }

        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }

        function FreeItemSelected(sender, e) {
            $get('<%=hdfreeitem.ClientID%>').value = e.get_value();
        }
        function ContractSelected(dt) {
            $get('<%=hdcontract.ClientID%>').value = dt;
            $get('<%=btlookcontract.ClientID%>').click();
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

    <asp:HiddenField ID="hdcontract" runat="server" />
    <asp:HiddenField ID="hdprop" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hdfreeitem" runat="server" />
    <div class="divheader">Business Agreement <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red"></asp:Label></div> 
    <div class="h-divider"></div>
    
    <div class="container-fluid">
        <div class="form-horizontal">
            <fieldset class="hover-title">
                <div class="row ">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Business Agreement 1</div>
                </div>
                <div class="content-hover" id="ba1">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class=" row margin-top">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="salespoint" class="col-xs-4 col-form-label col-form-label-sm">Branch</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="lbSp" runat="server" CssClass="form-control input-sm" Font-Bold="true" ReadOnly="true" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">                        
                                    <div class="form-group">
                                        <label for="contractNo" class="col-xs-4 col-form-label col-form-label-sm">Contract No</label>
                                        <div class="col-xs-8">
                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:Label ID="lbconractno" runat="server" CssClass="form-control input-sm"   BorderStyle="Solid" BorderWidth="1px">New</asp:Label>
                                                            <div class="input-group-btn">
                                                                <asp:LinkButton ID="btsearchcontract" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchcontract_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <%--<asp:TextBox ID="txContractNo" runat="server" CssClass="" Font-Bold="true" ReadOnly="true"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="proposalNo" class="col-xs-4 col-form-label col-form-label-sm">Proposal No</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txPropNo" runat="server" CssClass="form-control input-sm"  ></asp:TextBox>
                                            <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txPropNo_AutoCompleteExtender" runat="server" ServiceMethod="GetListProposal" TargetControlID="txPropNo" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="PropSelected" >
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="typeContract" class="col-xs-4 col-form-label col-form-label-sm">Type</label>
                                        <div class="col-xs-4">
                                            <asp:DropDownList ID="cbType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbType_SelectedIndexChanged" CssClass="form-control-static input-sm" Width="100%">
                                                <asp:ListItem Text="Display Rent BTL" Value="DR"></asp:ListItem>
                                                <asp:ListItem Text="Display Rent ATL" Value="DS"></asp:ListItem>
                                                <asp:ListItem Text="Tactical Bonus" Value="TB"></asp:ListItem>
                                                <asp:ListItem Text="Sign Board" Value="SB"></asp:ListItem>
                                                <asp:ListItem Text="Others" Value="OT"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:RadioButtonList ID="rdTypePayment" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"  OnSelectedIndexChanged="rdTypePayment_SelectedIndexChanged" CssClass="form-control-static input-sm" Width="100%" >
                                                <asp:ListItem Value="CH">Cash</asp:ListItem>
                                                <asp:ListItem Value="FG">Freegood</asp:ListItem>
                                                <asp:ListItem Value="CN">Credit Note</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="manualNo" class="col-xs-4 col-form-label col-form-label-sm">Manual No</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txManualNo" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="contractDate" class="col-xs-4 col-form-label col-form-label-sm">Contract Date</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="dtContract" runat="server" CssClass="form-control input-sm"  ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="startDate" class="col-xs-4 col-form-label col-form-label-sm">Start Date</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                            <asp:CalendarExtender CssClass="date" ID="dtStart_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtstart">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="endDate" class="col-xs-4 col-form-label col-form-label-sm">End Date</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                            <asp:CalendarExtender CssClass="date" ID="dtEnd_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtend">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="firstParty" class="col-xs-4 col-form-label col-form-label-sm">First Party</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txFirst" runat="server" CssClass="form-control input-sm" Font-Bold="true" ForeColor="Red" ReadOnly="true" Enabled="false"  ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="secondParty" class="col-xs-4 col-form-label col-form-label-sm">Second Party</label>
                                        <div class="col-sm-4 drop-down">
                                            <asp:DropDownList ID="cbCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbCustomer_SelectedIndexChanged" CssClass="form-control input-sm"  >
                                                <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                                <asp:ListItem Text="Customer Group" Value="G"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txCustomer" runat="server" CssClass="form-control input-sm"  ></asp:TextBox>
                                                <asp:AutoCompleteExtender  CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item"  ID="txCustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetListCustomer" TargetControlID="txCustomer" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="CustSelected" >
                                                </asp:AutoCompleteExtender>
                                                <asp:DropDownList ID="cbCusGrp" runat="server" CssClass="form-control input-sm"   ></asp:DropDownList>
                                            </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbCustomer" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="contractFor" class="col-xs-4 col-form-label col-form-label-sm">Second Party</label>
                                        <div class="col-xs-8">
                                            <asp:DropDownList ID="cbCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbCustomer_SelectedIndexChanged" >
                                                <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                                <asp:ListItem Text="Customer Group" Value="G"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txCustomer" runat="server" CssClass=""></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txCustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetListCustomer" TargetControlID="txCustomer" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="CustSelected" >
                                                </asp:AutoCompleteExtender>
                                                <asp:DropDownList ID="cbCusGrp" runat="server" ></asp:DropDownList>
                                            </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbCustomer" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset class="product-border">
                                    <div class="product-border ">Product for Agreement</div>
                                    <div class="">
                                        <div class="row">
                                            <div class="">
                                                <label for="item" class="col-xs-4 control-label control-label-sm">Product / Item</label>
                                                <div class="col-xs-8">
                                                    <asp:RadioButtonList ID="rditem" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"  OnSelectedIndexChanged="rditem_SelectedIndexChanged" CssClass="radio radio-inline no-margin">
                                                        <asp:ListItem Value="I">Item</asp:ListItem>
                                                        <asp:ListItem Value="P">Product</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="">
                                                <div class="col-xs-10">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cbprod" runat="server" CssClass="form-control input-sm" >
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txitem" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetListItem" TargetControlID="txitem" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected">
                                                        </asp:AutoCompleteExtender>
                                                    </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="rditem" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-xs-2">
                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btaddprod" Text="Add" OnClick="btaddprod_ServerClick" />
                                                    <%--<button type="submit" class="btn btn-default btn-xs" runat="server" id="btaddprod" onserverclick="btaddprod_ServerClick" >
                                                    <span class="fa fa-plus" aria-hidden="true"></span> Add</button>--%>
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-xs-12">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="table-responsive">
                                                        <asp:GridView ID="grditem" AllowPaging="True" OnRowDeleting="grditem_RowDeleting" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("itemcode") %>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item / Group Product">
                                                                    <ItemTemplate><%# Eval("itemname") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField ShowDeleteButton="True" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </fieldset>                        
                                </div>
                                <div id="lblFree" runat="server">
                                    <div class="col-md-6">
                                        <fieldset class="product-border">
                                            <div class="product-border ">Free Product for Agreement</div>
                                            <div class="">
                                                <div class="row">
                                                    <div class=" ">
                                                        <label for="item" class="col-xs-4 control-label control-label-sm">Free Product / Item</label>
                                                        <div class="col-xs-8">
                                                            <asp:RadioButtonList ID="rdfreeitem" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"  OnSelectedIndexChanged="rdfreeitem_SelectedIndexChanged" CssClass="no-margin radio radio-inline " >
                                                                <asp:ListItem Value="I">Item</asp:ListItem>
                                                                <asp:ListItem Value="P">Product</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div id="dtFree" runat="server">
                                                        <div class=" ">
                                                            <div class="col-xs-10">
                                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="cbfreeprod" runat="server" CssClass="form-control input-sm">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txfreeitem" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txfreeitem_AutoCompleteExtender" runat="server" ServiceMethod="GetListItem" TargetControlID="txfreeitem" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="FreeItemSelected">
                                                                    </asp:AutoCompleteExtender>
                                                                </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="rdfreeitem" EventName="SelectedIndexChanged" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-xs-2">
                                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Button CssClass="btn btn-success btn-sm " runat="server" ID="btaddfreeprod" Text="Add" OnClick="btaddfreeprod_ServerClick" />
                                                                <%--<button type="submit" class="btn btn-default btn-xs" runat="server" id="btaddfreeprod" onserverclick="btaddfreeprod_ServerClick" >
                                                                <span class="fa fa-plus" aria-hidden="true"></span> Add</button>--%>
                                                                </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-xs-12">
                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                            <ContentTemplate>
                                                                <div class="table-responsive">
                                                                <asp:GridView ID="grdfreeitem" AllowPaging="True" OnRowDeleting="grdfreeitem_RowDeleting" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("itemcode") %>'></asp:Label></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item / Group Product">
                                                                            <ItemTemplate><%# Eval("itemname") %></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField ShowDeleteButton="True" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                                </div>
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>                        
                                    </div>
                                </div>
                            </div>
                            </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
            <fieldset runat="server" id="secGD" class="hover-title">
                <div class="row">
                    <h5 class="divheader subheader subheader-bg " style="margin-bottom: 0 !important;">
                        <asp:Label ID="lbtitlesecGD" runat="server" Text="Gondola Section"></asp:Label>
                    </h5>
                </div>
                <div class="content-hover" id="GondolaSecHv">
                    <asp:Panel runat="server" CssClass="" ID="GondolaSec" >
                        <div class="row margin-top">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="payTerm" class="col-xs-4 col-form-label col-form-label-sm">Payment Term</label>
                                    <div class="col-xs-8 ">
                                        <div class="drop-down">
                                            <asp:DropDownList ID="cbcontractterm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbcontractterm_SelectedIndexChanged" CssClass="form-control input-sm" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group" runat="server" id="cbpaytype">
                                    <label for="payType" class="col-xs-4 col-form-label col-form-label-sm">Payment Type</label>
                                    <div class="col-xs-8">
                                        <div class="drop-down">
                                            <asp:DropDownList ID="cbpaymenttype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbpaymenttype_SelectedIndexChanged" CssClass="form-control input-sm" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <%--<div class="col-md-6"></div>--%>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grdschedule" AllowPaging="True" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdschedule_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sequence No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="dtpayment" runat="server" Text='<%# Eval("payment_dt","{0:dd/MM/yyyy}") %>' CssClass="form-control input-sm"></asp:TextBox>
                                                        <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" TargetControlID="dtpayment" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txamt" runat="server" Text="0" CssClass="form-control input-sm"></asp:TextBox></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txqty" runat="server" Text="0" CssClass="form-control input-sm"></asp:TextBox></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm"></asp:DropDownList></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </fieldset>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbType" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
            <fieldset runat="server" id="secTB"  class="hover-title">
                <div class="row">
                    <h5 class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Tactical Bonus Section</h5>
                </div>
                <div class="content-hover"  >
                    <div class="row margin-top">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="periodSelection" class="col-xs-4 col-form-label col-form-label-sm">Periode</label>
                                <div class="col-xs-8">
                                    <asp:DropDownList ID="cbperiodtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbperiodtype_SelectedIndexChanged" CssClass="form-control input-sm"  >
                                        <asp:ListItem Text="Q1" Value="Q1"></asp:ListItem>
                                        <asp:ListItem Text="Q2" Value="Q2"></asp:ListItem>
                                        <asp:ListItem Text="Q3" Value="Q3"></asp:ListItem>
                                        <asp:ListItem Text="Q4" Value="Q4"></asp:ListItem>
                                        <asp:ListItem Text="Yearly" Value="Yearly"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="previousSold" class="col-xs-4 col-form-label col-form-label-sm">Actual Sales</label>
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grdPrevSold" AllowPaging="True" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" ShowFooter="True" OnRowDataBound="grdPrevSold_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# String.Format("{0}_{1}", Eval("item_cd"), Eval("item_nm")) %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Sales (CTN)">
                                                    <ItemTemplate><asp:Label ID="lbtotal" runat="server" Text='<%# Eval("total") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                        <asp:TextBox ID="txprevsold" runat="server" CssClass="form-control"></asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group">
                                    <label for="bonusPct" class="col-xs-4 col-form-label col-form-label-sm">Bonus Pct (%)</label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txpct" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txpct_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>
                                            <asp:Button CssClass="btn btn-primary btn-sm" runat="server" ID="btncalc" Text="Calculation" OnClick="btncalc_Click" />
                                        <%--<button type="submit" class="btn btn-default btn-xs" runat="server" id="btaddprod" onserverclick="btaddprod_ServerClick" >
                                        <span class="fa fa-plus" aria-hidden="true"></span> Add</button>--%>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="viewCalc">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                    <ContentTemplate>
                                    <label for="calcPct" class="col-xs-4 col-form-label col-form-label-sm">Calculation</label>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="txcalcpct" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btncalc" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="salesAchievement" class="col-xs-4 col-form-label col-form-label-sm">Sales Achievement</label>
                                <div class="col-xs-8">
                                    <asp:TextBox ID="txachievement" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="increasingTarget" class="col-xs-4 col-form-label col-form-label-sm">Sales Target</label>
                                <div class="col-xs-8">
                                    <asp:TextBox ID="txincreasing" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="increasingTarget" class="col-xs-4 col-form-label col-form-label-sm">Increasing Target Sold Pct (%)</label>
                                <div class="col-xs-8">
                                    <asp:TextBox ID="txincreasingpct" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6"></div>
                    </div>
                </div>
            </fieldset>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbType" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
            <fieldset runat="server" id="secSS"  class="hover-title">
                <div class="row">
                    <h5 class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Shop Sign Section</h5>
                </div>
                <div class="content-hover"  >
                    <div class="row margin-top">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="electricityBill" class="col-xs-4 col-form-label col-form-label-sm">Electricity (%)</label>
                                <div class="col-xs-8">
                                    <asp:TextBox ID="txelec" runat="server" CssClass=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="municipalityTax" class="col-xs-4 col-form-label col-form-label-sm">Municipality Tax(%)</label>
                                <div class="col-xs-8">
                                    <asp:TextBox ID="txmunicipal" runat="server" CssClass=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="size" class="col-xs-2 col-form-label col-form-label-sm">Size</label>
                                <div class="col-xs-10">
                                    <div class="form-group">
                                        <div class="col-xs-2">
                                            <asp:TextBox ID="txsizex" runat="server" ></asp:TextBox>
                                        </div>
                                        <label for="size" class="col-xs-1 col-form-label col-form-label-sm">x</label>
                                        <div class="col-xs-2">
                                            <asp:TextBox ID="txsizey" runat="server" ></asp:TextBox>
                                        </div>
                                        <label for="size" class="col-xs-1 col-form-label col-form-label-sm">=</label>
                                        <div class="col-xs-2">
                                            <asp:TextBox ID="txsizetot" runat="server" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbType" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
            <fieldset runat="server" id="aggGD"  class="hover-title">
                <div class="row ">
                    <h5 class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Agreement Section</h5>
                </div>
                <div class="content-hover"  >
                    <div class="row margin-top">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="A" class="col-xs-2 col-form-label col-form-label-sm">A. Agreement</label>
                                <div class="col-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grdagreeGD" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Choose">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkselect" runat="server" /></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbagreecode" runat="server" Text='<%# Eval("agree_cd") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agreement">
                                                    <ItemTemplate><%# Eval("agree_desc") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="B" class="col-xs-2 col-form-label col-form-label-sm">
                                
                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                    <ContentTemplate>
                                        <span class="inline-block"> B.</span>
                                            <asp:Label ID="lbBGD" runat="server" Text="Label" CssClass="inline-block"></asp:Label>
                                    </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbType" EventName="SelectedIndexChanged" />
                                </Triggers>
                                </asp:UpdatePanel></label>
                                <div class="col-xs-10">
                                    <div class="form-group">
                                            <div class="col-sm-2 drop-down">
                                                <asp:DropDownList ID="cbtypedisplay" runat="server"  CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                             
                                            </div>
                                            <label for="size" class="col-sm-1 col-form-label col-form-label-sm">Size</label>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txsizeP" runat="server" CssClass="form-control input-sm" ></asp:TextBox> 
                                                    </div>
                                                    <label for="size" class="col-sm-1 col-form-label col-form-label-sm">x</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txsizeL" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                    </div>
                                                </div>                                        
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="size" class="col-sm-4 col-form-label col-form-label-sm">Qty (ctn)</label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txqtyGD" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="locdisplay" class="col-sm-2 col-form-label col-form-label-sm">Location Display</label>
                                    <div class="col-sm-5 drop-down">
                                        <asp:DropDownList ID="cbloc" runat="server"  CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                     
                                    </div>
                                    <div class="col-sm-5"></div>
                                </div>
                            </div>
                        </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="A" class="col-xs-2 col-form-label col-form-label-sm">D. Term of Display Rental</label>
                                <div class="col-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grdtermGD" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Choose">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkselect" runat="server" /></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Terms Of Display">
                                                    <ItemTemplate><%# Eval("agree_desc") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="A" class="col-xs-2 col-form-label col-form-label-sm">E. Damaged or Expired Products Policy</label>
                                <div class="col-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grddamageGD" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Choose">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkselect" runat="server" /></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Damage or Expired Products Policy">
                                                    <ItemTemplate><%# Eval("agree_desc") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbType" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
            <fieldset runat="server" id="aggTB"  class="hover-title">
                <div class="row">
                    <h5 class="divheader subheader subheader-bg " style="margin-bottom: 0 !important;">Agreement Section</h5>
                </div>
                <div class="content-hover"  >
                    <div class="row margin-top">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="A" class="col-xs-2 col-form-label col-form-label-sm">A. Agreement</label>
                                <div class="col-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grdagreeTB" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Choose">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkselect" runat="server" /></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbagreecode" runat="server" Text='<%# Eval("agree_cd") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agreement">
                                                    <ItemTemplate><%# Eval("agree_desc") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="A" class="col-xs-2 col-form-label col-form-label-sm">E. Damaged or Expired Products Policy</label>
                                <div class="col-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grddamageTB" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Choose">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkselect" runat="server" /></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Damage or Expired Products Policy">
                                                    <ItemTemplate><%# Eval("agree_desc") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbType" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
            <fieldset runat="server" id="aggSS" class=" hover-title">
                <h5 >Agreement Section</h5>
            </fieldset>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbType" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <fieldset class="hover-title">
                <div class="row">
                    <h5 class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Contact</h5>
                </div>
                <div class="content-hover"  >
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row margin-top">
                                <div class="col-md-6">
                                    <fieldset class="product-border">
                                        <h5 class="product-border">The First Party</h5>
                                        <div class="row">
                                            <div class="margin-bottom clearfix">
                                                <label for="contact" class="col-xs-4 col-form-label col-form-label-sm">Contact Name</label>
                                                <div class="col-sm-8 drop-down">
                                                    <asp:DropDownList ID="cbContactFirst" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="cbContactFirst_SelectedIndexChanged" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="margin-bottom clearfix">
                                                <label for="position" class="col-xs-4 col-form-label col-form-label-sm">Position</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txPostionFirst" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="margin-bottom clearfix">
                                                <label for="mobile" class="col-xs-4 col-form-label col-form-label-sm">Contact No.</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txMobileFirst" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>                        
                                </div>
                                <div id="Div1" runat="server">
                                    <div class="col-md-6">
                                        <fieldset class="product-border">
                                            <h5 class="product-border">The Second Party</h5>
                                            <div class="row">
                                                <div class="margin-bottom clearfix">
                                                    <label for="name" class="col-xs-4 col-form-label col-form-label-sm">Contact Name</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txNameSecond" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="margin-bottom clearfix">
                                                    <label for="position" class="col-xs-4 col-form-label col-form-label-sm">Position</label>
                                                    <div class="col-xs-8">
                                                        <asp:TextBox ID="txPostionSecond" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                            <div class="margin-bottom clearfix">
                                                <label for="mobile" class="col-xs-4 col-form-label col-form-label-sm">Contact No.</label>
                                                <div class="col-xs-8">
                                                    <asp:TextBox ID="txMobileSecond" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        </fieldset>                        
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>

        </div>

    </div>
      
    <div class="row margin-bottom">
      <div class="col-sm-12">
        <div class="text-center navi">
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
            <ContentTemplate>
            <button type="submit" class="btn btn-success btn-sm" runat="server" id="btnew" onserverclick="btnew_Click" >
              <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> New</button>

            <button type="submit" class="btn btn-primary btn-sm" runat="server" id="btedit" onserverclick="btedit_ServerClick" >
              <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Edit
            </button>
            <button type="submit" class="btn btn-danger btn-sm" runat="server" id="btdelete" onserverclick="btdelete_ServerClick" >
              <span class="glyphicon glyphicon-erase" aria-hidden="true"></span> Delete
            </button>
            <asp:Button ID="btsave" runat="server" Text="Save" class="btn btn-warning btn-sm" OnClick="btsave_Click" />
            <%--<button type="submit" class="btn btn-warning btn-sm" runat="server" id="btsave" onserverclick="btsave_Click" >
              <span class="glyphicon glyphicon-save" aria-hidden="true"></span> Save
            </button>--%>
            <button type="submit" class="btn btn-primary btn-sm" runat="server" id="btupdate" onserverclick="btupdate_ServerClick" >
              <span class="glyphicon glyphicon-save" aria-hidden="true"></span> Update
            </button>
            <button type="submit" class="btn btn-info btn-sm" runat="server" id="btprint" onserverclick="btprint_Click" >
              <span class="glyphicon glyphicon-print" aria-hidden="true"></span> Print
            </button>
            <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none"/>
            <asp:Button ID="btlookcontract" runat="server" Text="Button" OnClick="btlookcontract_Click" style="display:none"/>
            <%--<div id="button" style="display: none">
                <asp:Button ID="btprop" runat="server" Text="Button" OnClick="btprop_Click" />
            </div>--%>                 
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
      </div>
    </div>    
</asp:Content>


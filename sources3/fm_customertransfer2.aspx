<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_customertransfer2.aspx.cs" Inherits="fm_customertransfer2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=Button1.ClientID%>').click();
        }
        function SalesmanSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();

        }
        function RefreshData(sval) {
            $get('<%=hdtrf.ClientID%>').value = sval;
            $get('<%=btrefresh.ClientID%>').click();
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<div class="divheader">Transfer Customer</div>
    <div class="h-divider"></div>--%>
    <div class="alert alert-info text-bold">Transfer Customer</div>
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix margin-bottom padding-bottom">
                <div class="col-sm-6  form-group">
                    <asp:Label ID="lbtransfer" runat="server" Text="Date Transfer" CssClass="col-sm-3 control-label"></asp:Label>
                    <div class="drop-down-date col-md-6 col-sm-9">
                        <asp:TextBox ID="dttransfer" runat="server" CssClass="form-control " Style="left: 4px;"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dttransfer_CalendarExtender" runat="server" BehaviorID="dttransfer_CalendarExtender" TargetControlID="dttransfer" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </div>

                <div class="col-md-offset-2 col-md-4 col-sm-6  form-group">
                    <asp:Label ID="lbtransferno" runat="server" Text="Transfer NO." CssClass="col-sm-4 control-label"></asp:Label>
                    <div class="col-md-8 col-sm-6">
                        <div class="input-group ">
                            <asp:TextBox ID="txtransferno" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="clearfix ">
                <div class="col-md-9 col-sm-12 form-group ">
                    <asp:Label ID="Label1" runat="server" Text="Date Effective" CssClass="col-md-2 col-sm-2 control-label"></asp:Label>
                    <div class="drop-down-date col-md-8 col-sm-10">
                        <asp:TextBox ID="dteff" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="dteff_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dteff" CssClass="date">
                        </asp:CalendarExtender>
                        <asp:HiddenField ID="hdtrf" runat="server" />
                    </div>
                </div>

            </div>

            <div class="clearfix ">
                <div class="col-md-9 col-sm-12 form-group ">
                    <asp:Label ID="Label11" runat="server" Text="End Date" CssClass="col-md-2 col-sm-2 control-label"></asp:Label>
                    <div class="drop-down-date col-md-6 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtend" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend" CssClass="date">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chenddt" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <asp:CheckBox ID="chenddt" runat="server" OnCheckedChanged="chenddt_CheckedChanged" AutoPostBack="true" Text="Check This. if without End Date..." />
                    </div>
                </div>

            </div>

            <div class="clearfix ">
                <div class=" col-md-9 col-sm-12 margin-bottom">
                    <asp:Label ID="lboldsalesman" runat="server" Text="Old Salesman " CssClass="control-label  col-md-2 col-sm-2"></asp:Label>
                    <div class="drop-down col-md-6 col-sm-6">
                        <asp:DropDownList ID="cbsalesman" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="chsalesman" runat="server" OnCheckedChanged="chsalesman_CheckedChanged" AutoPostBack="true" Text="Check This. if Transfer this salesman to another branch..." />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdtransfer" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="clearfix margin-bottom">
                <div class="col-md-9 col-sm-12">
                    <asp:Label ID="lbtypetransfer" runat="server" Text="Type Of Transfer" CssClass="col-md-2 col-sm-2 control-label margin-bottom"></asp:Label>
                    <div class="col-md-8 col-sm-10 margin-bottom">
                        <asp:RadioButtonList ID="rdtransfer" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdtransfer_SelectedIndexChanged" CssClass="well well-sm radio radio-inline no-margin full">
                            <asp:ListItem Value="I">Internal Transfer</asp:ListItem>
                            <asp:ListItem Value="E">Transfer to Another Branch</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-offset-2 col-sm-offset-2 col-md-8 col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbSalesPointCD" runat="server" CssClass="form-control  " AutoPostBack="true" OnSelectedIndexChanged="cbSalesPointCD_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdtransfer" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-9 col-sm-12 ">
                    <div class="col-md-2 col-sm-2 control-label margin-bottom"></div>
                    <div class="col-md-8 col-sm-10 margin-bottom">
                        <asp:RadioButtonList ID="rdcust" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdcust_SelectedIndexChanged" CssClass="well well-sm radio radio-inline no-margin full">
                            <asp:ListItem Value="C">Customer</asp:ListItem>
                            <asp:ListItem Value="A">All Customer</asp:ListItem>
                            <asp:ListItem Value="N">No Customer</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-offset-2 col-sm-offset-2 col-md-8 col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rdtypecustomer" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdtypecustomer_SelectedIndexChanged" CssClass="well well-sm radio radio-inline no-margin full">
                                    <asp:ListItem Value="M">Manual</asp:ListItem>
                                    <asp:ListItem Value="R">RPS</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>

            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <ContentTemplate>
                    <div class="clearfix margin-bottom" runat="server" id="addCustomer">
                        <div class=" col-md-9 col-sm-12 margin-bottom">
                            <asp:Label ID="Label2" runat="server" Text="Customer" CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                            <div class="col-md-6 col-sm-6">
                                <asp:TextBox ID="txcust" runat="server" CssClass="form-control margin-bottom"></asp:TextBox>
                                <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txcust_AutoCompleteExtender" runat="server" TargetControlID="txcust" ServiceMethod="GetCompletionList" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="10" CompletionInterval="1" MinimumPrefixLength="1" UseContextKey="true" CompletionListElementID="divw" OnClientItemSelected="CustSelected">
                                </asp:AutoCompleteExtender>
                                <div id="divw" style="font-size: small; font-family: Calibri"></div>
                                <asp:HiddenField ID="hdcust" runat="server" />
                                <asp:DropDownList ID="cbdaycode" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <asp:Button ID="btaddcust" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btaddcust_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdtypecustomer" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="rdtransfer" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="clearfix margin-bottom" runat="server" id="viewCustomer">
                        <div class="col-md-9 col-sm-12">
                            <div class="col-md-offset-2 col-sm-offset-2 col-md-8 col-sm-10">
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
                                                    <%# Eval("otlcd") %> - <%# Eval("otlnm") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btaddcust" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>


            <div class="clearfix">
                <div class=" col-md-9 col-sm-12 form-group">
                    <asp:Label ID="lbnewsalesman" runat="server" Text="New Salesman" CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                    <div class=" col-md-8 col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdemp" runat="server" />
                                <asp:TextBox ID="txnewsalesman" runat="server" AutoPostBack="True" CssClass="form-control"></asp:TextBox>
                                <div id="divwidths1" style="font-family: Calibri; font-size: small"></div>
                                <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txnewsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txnewsalesman" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths1" OnClientItemSelected="SalesmanSelected">
                                </asp:AutoCompleteExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class=" col-md-9 col-sm-12 form-group">
                    <asp:Label ID="Label4" runat="server" Text=" Type" CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                    <div class=" col-md-8 col-sm-10 drop-down">
                        <asp:DropDownList ID="cbreason" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class=" col-md-9 col-sm-12 form-group">
                    <asp:Label ID="Label3" runat="server" Text=" Remark" CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                    <div class=" col-md-8 col-sm-10">
                        <asp:TextBox ID="txreason" runat="server" TextMode="MultiLine" Width="100%" Height="100" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class=" col-md-9 col-sm-12 form-group">
                    <asp:Label ID="Label6" runat="server" Text="Balance to be transfer" CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                    <div class=" col-md-8 col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txbalancetotransfer" runat="server" TabIndex="3" CssClass="form-control ro" Font-Bold="True"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbsalesman" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class="col-md-9 col-sm-12 form-group">
                    <asp:Label ID="Label5" runat="server" Text="Balance on SOA Signed" CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                    <div class=" col-md-8 col-sm-10">
                        <asp:TextBox ID="txbalancesoa" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class="col-md-9 col-sm-12  form-group">
                    <asp:Label ID="lbconfrimdoc" runat="server" Text="Attach Confirmation 1 " CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                    <div class=" col-md-8 col-sm-10">
                        <asp:FileUpload ID="fuconfirmation1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class="col-md-9 col-sm-12  form-group">
                    <asp:Label ID="Label9" runat="server" Text="Attach Confirmation 2 " CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                    <div class=" col-md-8 col-sm-10">
                        <asp:FileUpload ID="fuconfirmation2" runat="server" />
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class="col-md-9 col-sm-12  form-group">
                    <asp:Label ID="Label10" runat="server" Text="Attach Confirmation 3 " CssClass="control-label col-md-2 col-sm-2"></asp:Label>
                    <div class=" col-md-8 col-sm-10">
                        <asp:FileUpload ID="fuconfirmation3" runat="server" />
                    </div>
                </div>
            </div>
            <div class="clearfix margin-top">
                <div class="col-sm-12  ">
                    <asp:Label ID="lbnote" runat="server" Text="Note :" CssClass="text-bold pull-left"></asp:Label>
                    <div class=" col-md-11 col-sm-10">
                        <asp:Label ID="lbnotetext" runat="server" Style="font-weight: 700; color: #FF0000;" Text="Please make sure all transaction by old salesman posted after transfer all transaction will be post under new salesman."></asp:Label>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-top">
                <div class="col-sm-12  ">
                    <asp:Label ID="Label7" runat="server" Text="Note :" CssClass="text-bold pull-left"></asp:Label>
                    <div class=" col-md-11 col-sm-10">
                        <asp:Label ID="Label8" runat="server" Style="font-weight: 700; color: #FF0000;" Text="File size cannot exceed 1 MB."></asp:Label>
                    </div>
                </div>
            </div>

        </div>
        <div class="row navi margin-bottom padding-top margin-top text-center">
            <asp:LinkButton ID="btrefresh" OnClientClick="ShowProgress();" runat="server" OnClick="btrefresh_Click" Text="Button" Style="display: none" />
            <asp:LinkButton ID="btnew" runat="server" OnClientClick="ShowProgress();" CssClass="btn-success btn btn-add" OnClick="btnew_Click" Text="New" />
            <asp:LinkButton ID="Button1" runat="server" OnClientClick="ShowProgress();" OnClick="Button1_Click" Text="Button" Style="display: none" />
            <asp:LinkButton ID="btsave" runat="server" Text="Process To Transfer" OnClientClick="ShowProgress();" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
            <asp:LinkButton ID="btprint" OnClientClick="ShowProgress();" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />

        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


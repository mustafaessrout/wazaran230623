<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentpromised2.aspx.cs" Inherits="fm_paymentpromised2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="css/anekabutton.css" rel="stylesheet" />--%>

    <script src="js/jquery-1.9.1.min.js"></script>
    <script src="admin/js/bootstrapmin..js"></script>

    <script>
        function PromisedSelected(s) {

            $get('<%=txpromisedno.ClientID%>').value = s;
            $get('<%=btsearchprom.ClientID%>').click();
        }
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function disp() {
            $('#pnlmsg').show();
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
        function SelectData(sVal) {
            $get('<%=hdpromised.ClientID%>').value = sVal;
            $get('<%=btsearchpromised.ClientID%>').click();
        }
    </script>
    <style>
        .btn5 {
            margin-right: 5px;
        }

            .btn5 .btn {
                border-radius: 5px !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdemp" runat="server" />
    <asp:HiddenField ID="hdpromised" runat="server" />
    <h4 class="jajarangenjang">Promise Payment</h4>
    <div class="h-divider"></div>
    <%--<div class="container">--%>
    <div class="row">
        <div class="clearfix">
            <div class="col-sm-12">
                <div class="col-md-3 col-sm-6 no-padding margin-bottom">
                    <label class="control-label col-sm-4">For</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbfor" onchange="javascript:ShowProgress();" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cbfor_SelectedIndexChanged">
                            <asp:ListItem Value="C">Customer</asp:ListItem>
                            <asp:ListItem Value="G">Group Customer</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 no-padding margin-bottom">
                    <%--<label for="customer" class="control-label col-md-1">Cust:</label>--%>
                    <div class="col-md-12">
                        <div class="input-group">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txCustomer" runat="server" CssClass="form-control input-group-sm" Style="border-radius: 5px 0 0 5px;"></asp:TextBox>
                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txCustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txCustomer" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="CustSelected">
                                    </asp:AutoCompleteExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbfor" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server" class="input-group-btn">
                                <ContentTemplate>
                                    <asp:LinkButton ID="btsearchcust" CssClass="btn btn-primary" runat="server" OnClick="btsearchcust_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbcusgrcd" runat="server" CssClass="form-control drop-down" Style="border-radius: 5px;" OnSelectedIndexChanged="cbcusgrcd_SelectedIndexChanged1" AutoPostBack="True"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="col-md-1 col-sm-6 no-padding margin-bottom">
                    <div class="col-md-12 text-center">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:Image ID="img" runat="server" CssClass="form-control input-group-sm" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-2 col-sm-6 no-padding margin-bottom">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbusage" runat="server" CssClass="form-control input-group-sm"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 no-padding margin-bottom">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txpromisedno" runat="server" CssClass="form-control input-group-sm"></asp:TextBox>
                                <div class="input-group-btn">
                                    <button id="btsearchpromised" class="btn btn-primary" type="submit" runat="server" onserverclick="btsearchpromised_ServerClick">
                                        <i class="fa fa-search" aria-hidden="true"></i>
                                    </button>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="h-divider"></div>

        <div class="clearfix">
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4">Balance:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbbalance" runat="server" Text="-" CssClass="form-control input-sm"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbcusgrcd" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <label class="control-label col-sm-4" id="lbcreditlimit" runat="server">CL:</label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbcl" runat="server" CssClass="form-control input-sm">-</asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbcusgrcd" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4" style="font-size: smaller"><span><strong>Remain CL</strong></span>:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbremain" runat="server" Text="-" CssClass="form-control input-sm"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbcusgrcd" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4">Grp:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbcusgrcd" runat="server" Text="-" CssClass="form-control input-sm"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4">TOP:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtop" runat="server" Text="-" CssClass="form-control input-sm"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4">Type:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtype" runat="server" Text="-" CssClass="form-control input-sm"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4">Salesman:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbsalesman" runat="server" Text="-" CssClass="form-control input-sm" Style="font-size: x-small"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4">Overdue: </label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lboverdue" runat="server" Text="-" CssClass="form-control input-sm"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbcusgrcd" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4" style="color: red; font-weight: bold">Promise Amt</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txpromise" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txpromise_TextChanged"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4">Will Paid at:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtpaid" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtpaid_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtpaid">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4">New Trans will paid:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtnewpayment" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtnewpayment_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtnewpayment">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-md-3 col-sm-6 margin-bottom clearfix">
                <label class="control-label col-sm-4" style="font-size: smaller">Max Trans Amt:</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbmaxamt" runat="server" Text="0" CssClass="form-control input-sm" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txpromise" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>

        </div>

        <div class="clearfix margin-bottom">
            <div class="col-sm-12">
                <label class="control-label col-sm-1">Remark</label>
                <div class="col-sm-11">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txremark" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

        <div class="clearfix margin-bottom">
            <div class="col-sm-4 ">
                <label class="control-label col-sm-3">File</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload ID="upl" runat="server" CssClass="form-control input-sm" ToolTip="Please upload SOA with customer sign" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-sm-8">
                <label class="control-label">(SOA with sign from Customer)</label>
            </div>

        </div>

        <div class="h-divider"></div>

        <div class="clearfix">
            <div class="well well-sm danger text-white ">
                1. Order can order less or same with maximum value order .
                <br />
                2. Sales Supervisor can only created ONCE promised for not yet implemented .
                <br />
                3. Maximum amount for make transaction is 40% from over due amount.
                    <br />
                4. SOA must be attached with sign from Supervisor .<br />
                5. Group Credit Limit will applied as total for local customer CL + Group CL.
                <br />
                6. Available maximum transaction is included VAT 5%</div>
        </div>
        <div class="h-divider"></div>

    </div>
    <%--</div>--%>
    <div class="navi margin-bottom">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <asp:Button ID="btsearch" runat="server" Text="btsearch" OnClick="btsearch_Click" CssClass="divhid" OnClientClick="disp();" />
                <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary " OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-warning " OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info " OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:Button ID="btselectpromised" runat="server" OnClick="btselectpromised_Click" style="display:none" Text="Button" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btsearchprom" runat="server" OnClick="btsearchprom_Click" Text="Button" CssClass="divhid" />
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptsummary.aspx.cs" Inherits="fm_rptsummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
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
    <div id="container">
        <%--   <h4 class="jajarangenjang"><asp:Label runat="server" ID="lbTitle"></asp:Label></h4>
        <div class="h-divider"></div>--%>
        <div class="alert alert-info text-bold">
            <asp:Label runat="server" ID="lbTitle"></asp:Label></div>
        <div id="summaryclosing" runat="server">
            <%--<h5 class="jajarangenjang">Daily Closing Summary</h5>--%>
            <div class="row">
                <div class="col-sm-6 ">
                    <div class="clearfix form-group">
                        <label class="control-label col-sm-2">Period</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged" CssClass="form-control  ">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <div class="clearfix form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <div class="no-padding col-xs-5">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txfrom" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txfrom_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="txfrom_CalendarExtender" runat="server" TargetControlID="txfrom" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <p class="col-xs-2 text-center text-bold" style="margin-top: 8px;">To</p>

                            <div class="col-xs-5 no-padding">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txto" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txto_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="txto_CalendarExtender" runat="server" TargetControlID="txto" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div class="clearfix form-group" runat="server" id="listSalespoint">
                        <label class="control-label col-sm-2">Salespoint</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbSalesPointCD" runat="server" CssClass="form-control  ">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="clearfix form-group" runat="server" id="typesales">
                        <label class="control-label col-sm-2">Type</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbtypesales" runat="server" CssClass="form-control  ">
                                        <asp:ListItem Value="salesmanqty"> Summary By Salesman Qty</asp:ListItem>
                                        <asp:ListItem Value="salesmancoll"> Summary By Salesman Collection</asp:ListItem>
                                        <%--<asp:ListItem Value="customer"> Summary By Customer</asp:ListItem>--%>
                                        <%--                                        <asp:ListItem Value="targetsalesman"> Summary By Target Salesman</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="clearfix form-group" runat="server" id="typeStockIn">
                        <label class="control-label col-sm-2">Type</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbtypestockin" runat="server" CssClass="form-control  ">
                                        <asp:ListItem Value="po"> Purchase Order</asp:ListItem>
                                        <asp:ListItem Value="do"> Delivery Order by Factory</asp:ListItem>
                                        <asp:ListItem Value="gr"> GoodReceipt by Branch</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="clearfix form-group" runat="server" id="typeSalesQtySummary">
                        <label class="control-label col-sm-2">Type</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbsumtypesalesqty" runat="server" CssClass="form-control  " AutoPostBack="true" OnSelectedIndexChanged="cbsumtypesalesqty_SelectedIndexChanged">
                                        <asp:ListItem Value="detailbranch"> Detail By Sales Daily Qty (Branch)</asp:ListItem>
                                        <asp:ListItem Value="sumbranch"> Summary By Sales Daily Qty (Branch)</asp:ListItem>
                                        <asp:ListItem Value="sumcategory"> Summary By Sales Daily Qty (Category)</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="clearfix form-group drop-down" runat="server" id="typeOutletCustomer">
                                <label class="control-label col-sm-2">Category</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="cbotlcd" runat="server" CssClass="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="clearfix form-group" runat="server" id="typesumreport">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-1o">
                                    <asp:RadioButtonList ID="rbsumrpt" runat="server" AutoPostBack="True" DataValueField="S" OnSelectedIndexChanged="rbsumrpt_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="radio radio-inline">
                                        <asp:ListItem Value="S" Selected="True">Sales Qty</asp:ListItem>
                                        <asp:ListItem Value="F">Free Qty</asp:ListItem>
                                        <asp:ListItem Value="A">Sales Revenue</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbsumtypesalesqty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="row">
            </div>

        </div>

        <div id="monthlyclosing" runat="server">
            <div class="row">
                <div class="col-sm-6">
                    <div class="clearfix form-group">
                        <label class="control-label col-sm-2">Period</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbmonthperiod" runat="server" CssClass="form-control drop-down" AutoPostBack="true" OnSelectedIndexChanged="cbmonthperiod_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="cbperiodmth" runat="server" CssClass="form-control drop-down ">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbsalespointmth" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <div class="clearfix form-group" runat="server" id="periodDate">
                                <div class="col-sm-offset-2 col-sm-10">
                                    <div class="no-padding col-xs-5">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txstartmth" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txstartmth_TextChanged"></asp:TextBox>
                                                <asp:CalendarExtender ID="txstartmth_CalendarExtender" runat="server" TargetControlID="txstartmth" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                                </asp:CalendarExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <p class="col-xs-2 text-center text-bold" style="margin-top: 8px;">To</p>

                                    <div class="col-xs-5 no-padding">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txendmth" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txendmth_TextChanged"></asp:TextBox>
                                                <asp:CalendarExtender ID="txendmth_CalendarExtender" runat="server" TargetControlID="txendmth" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                                </asp:CalendarExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbsalespointmth" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbmonthperiod" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>


                    <%--<label class="control-label col-sm-2">Period</label>
                    <div class="col-sm-10 ">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" >
                            <ContentTemplate>
                                <asp:DropDownList ID="cbmonthperiod" runat="server" CssClass="form-control drop-down">
                                </asp:DropDownList>
                                <asp:DropDownList ID="cbperiodmth" runat="server" CssClass="form-control drop-down ">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbsalespointmth" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>--%>
                </div>
                <div class="col-sm-6">
                    <label class="control-label col-sm-2">Salespoint</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbsalespointmth" runat="server" CssClass="form-control  " AutoPostBack="true" OnSelectedIndexChanged="cbsalespointmth_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="navi row padding-bottom padding-top margin-top">
            <asp:Button ID="btprint" OnClientClick="ShowProgress();" runat="server" CssClass="btn-info btn btn-print" Text="Print" OnClick="btprint_Click" /><asp:Button ID="ButtonPrintRejected" OnClientClick="ShowProgress();" runat="server" CssClass="btn-info btn btn-print" Text="Print Rejected" Visible="False" OnClick="btprint_Rejected_Click" />
        </div>

    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


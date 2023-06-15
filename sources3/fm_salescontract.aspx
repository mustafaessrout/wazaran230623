<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salescontract.aspx.cs" Inherits="fm_salescontract" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="v4-alpha/docs.min.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e) {

            $get('<%=hdcust_cd.ClientID%>').value = e.get_value();
        }

    </script>
    <script src="v4-alpha/bootstrap.min.js"></script>
    <script src="v4-alpha/docs.min.js"></script>
    <style>
        .main-content #mCSB_2_container {
            min-height: 540px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Sales Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">

        <div class="row clearfix">
            <div class="col-sm-6 clearfix margin-bottom">
                <label class="col-sm-4 col-form-label">Branch</label>
                <div class="col-sm-8 drop-down">
                    <asp:DropDownList ID="cbbranch" CssClass="form-control " runat="server" Enabled="false">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-6 clearfix margin-bottom">
                <label class="col-sm-4 col-form-label">Report</label>
                <div class="col-sm-8 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbreport" CssClass="form-control " OnSelectedIndexChanged="cbreport_SelectedIndexChanged" runat="server" AutoPostBack="true">
                                <asp:ListItem Value="0">SALES FOR BRANCH</asp:ListItem>
                                <asp:ListItem Value="1">SALES FOR SALESMAN</asp:ListItem>
                                <asp:ListItem Value="2">SALES FOR CUTOMER</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbreport" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-6  clearfix margin-bottom">

                <label for="startDate" class="col-xs-4 col-form-label">Customer:</label>
                <div class="col-sm-8 drop-down-date">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txcust" runat="server" CssClass="form-control input-group-lg" Height="2em" Enabled="false"></asp:TextBox>
                            <div id="divwidth1" style="font-size: small; font-family: Calibri; position: absolute;"></div>
                            <asp:HiddenField ID="hdcust_cd" runat="server" />
                            <ajaxToolkit:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" TargetControlID="txcust" CompletionSetCount="1" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetCompletionList" UseContextKey="True" OnClientItemSelected="CustSelected" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidth1">
                            </ajaxToolkit:AutoCompleteExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbreport" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
            <div class="col-sm-6  clearfix margin-bottom">
                <label for="startDate" class="col-sm-4 col-form-label">Start Date</label>
                <div class="col-sm-8 drop-down-date">
                    <asp:TextBox ID="dtstart" runat="server" CssClass="form-control "></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtstart_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtstart">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-sm-6  clearfix margin-bottom">
                <label class="col-sm-4 col-form-label">End Date</label>
                <div class="col-sm-8 drop-down-date">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control "></asp:TextBox>
                    <ajaxToolkit:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" BehaviorID="dtend_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtend">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-sm-6  clearfix margin-bottom">
                <label class="col-sm-4 col-form-label">Group from</label>
                <div class="col-sm-8 drop-down">
                    <asp:DropDownList ID="cbProd_cdFr" CssClass="form-control " runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-6  clearfix margin-bottom">
                <label class="col-sm-4 col-form-label">To</label>
                <div class="col-sm-8 drop-down">
                    <asp:DropDownList ID="cbProd_cdTo" CssClass="form-control " runat="server">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row margin-top padding-top">
                <div class="navi margin-bottom margin-top">
                    <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info btn-print" OnClick="btprint_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>


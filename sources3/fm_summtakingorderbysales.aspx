<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_summtakingorderbysales.aspx.cs" Inherits="fm_summtakingorderbysales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ItemSelectedsalesman_cd(sender, e) {

            $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
    </script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .main-content #mCSB_2_container {
            min-height: 540px;
        }

        .auto-complate-list {
            min-height: 200px;
        }
    </style>
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
    <div class="divheader">Summary Taking Order and Collecting from Outlet by Salesman</div>
    <div class="h-divider"></div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div class="container-fluid ">
                <div class="row">

                    <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                        <label class="col-sm-2 control-label">Salespoint</label>
                        <div class="col-sm-10 drop-down">
                            <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                            </asp:DropDownList>

                        </div>
                    </div>

                    <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                        <label class="col-sm-2 control-label">Type of Report</label>
                        <div class="col-sm-10 drop-down">
                            <asp:DropDownList ID="cbreptype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbreptype_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem Value="0">Summary Taking Order by Salesman</asp:ListItem>
                                <asp:ListItem Value="1">Summary Taking Order by Customer</asp:ListItem>
                                <asp:ListItem Value="2">Summary performance</asp:ListItem>
                                <asp:ListItem Value="3">Summary Taking Order by Customer by branch</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>

                    <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                        <label class="col-sm-2 control-label">salesman</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txsalesman" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hdsalesman_cd" runat="server" />
                                    <ajaxToolkit:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsalesman" UseContextKey="True"
                                        CompletionListElementID="divwidth" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                                    </ajaxToolkit:AutoCompleteExtender>
                                    <div id="divwidth" class="auto-text-content"></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                        <asp:Label ID="lblstart" runat="server" Text="First Date" CssClass="control-label col-sm-2"></asp:Label>
                        <div class="col-sm-10 drop-down-date">
                            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" BehaviorID="dtstart_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtstart">
                            </asp:CalendarExtender>

                        </div>

                    </div>

                    <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                        <asp:Label ID="lblend" runat="server" Text="Second Date" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-10 drop-down-date">
                            <asp:TextBox ID="dtend" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" BehaviorID="dtend_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtend">
                            </asp:CalendarExtender>

                        </div>
                    </div>
                </div>
                <div class="navi row text-center margin-bottom">
                    <asp:Button ID="btprint" OnClientClick="ShowProgress();" runat="server" Text="Print Report" CssClass=" btn btn-info btn-print" OnClick="btprint_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


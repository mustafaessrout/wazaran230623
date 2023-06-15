<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmPrngoodreceiptbyinv.aspx.cs" Inherits="frmPrnStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" href="css/chosen.css" />

    <script src="js/jquery.min.js"></script>
    <script src="css/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
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
    <div class="divheader">Print Stock In by Reference Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-6 clearfix form-group">
                <label class="col-sm-2 control-label">Period</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Sales Pont CD</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly ro form-control  " Enabled="False">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <div class="col-sm-offset-2 col-sm-4">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txfrom" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="txfrom_CalendarExtender" runat="server" TargetControlID="txfrom" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <p class="col-sm-2 no-padding text-center" style="margin-top: 8px;">To</p>
                <div class="col-sm-4 ">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txto" runat="server" CssClass=" form-control "></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="txto_CalendarExtender" runat="server" TargetControlID="txto" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="row margin-top padding-top">
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Item</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbitem_cdFr" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="chosen-select form-control  " data-placeholder="Choose a Item">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">to Item</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbitem_cdTo" runat="server" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Group</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbProd_cdFr" runat="server" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">to Group</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbProd_cdTo" runat="server" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

        <div class="row navi padding-top margin-bottom text-center">
            <asp:Button ID="btprint" OnClientClick="ShowProgress();" runat="server" CssClass="btn-info btn btn-print" Text="Print" OnClick="btprint_Click" />
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


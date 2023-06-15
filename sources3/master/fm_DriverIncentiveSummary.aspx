<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_DriverIncentiveSummary.aspx.cs" Inherits="fm_DriverIncentiveSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        function openreport(url) {
            window.open(url, "myrep");
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Driver Incentive Summary</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Branch</label>
                    <div class="col-sm-4 drop-down">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:ListBox ID="cbsalespoint" runat="server" CssClass="form-control"
                                    SelectionMode="Multiple" Rows="6" Width="300px"></asp:ListBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="chkAllSalespoint" runat="server" Text="All Branch" Checked="false" AutoPostBack="true" OnCheckedChanged="chkAllSalespoint_CheckedChanged" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>

            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Period</label>
                    <div class="col-sm-4 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:ListBox ID="lbPeriod" runat="server" SelectionMode="Multiple" Rows="6" Width="300px"></asp:ListBox>
                                <%--<asp:ListBox ID="cbperiod" SelectMethod="Multiple" runat="server" CssClass="form-control" autogeneratecolumns="False" 
                                    ></asp:ListBox>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Employee</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:ListBox ID="cbemployee" CssClass="form-control" runat="server" Width="300px"
                                    SelectionMode="Multiple" Rows="6" AutoPostBack="true" OnSelectedIndexChanged="cbemployee_SelectedIndexChanged"></asp:ListBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align: center">
                    <asp:LinkButton ID="bprint" CssClass="btn btn-danger" runat="server" OnClick="bprint_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


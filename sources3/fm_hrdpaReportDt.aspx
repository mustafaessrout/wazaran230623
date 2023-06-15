<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_hrdpaReportDt.aspx.cs" Inherits="fm_hrdpaReportDt" %>

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
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <%--<div class="form-horizontal">--%>
        <h4 class="jajarangenjang">Performance Appraisal Report</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Driver Types</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbDriver" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbDriver_SelectedIndexChanged">
                        <asp:ListItem Text="Active Driver" Value="ActiveDriver" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="All Driver" Value="AllDriver"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Start Date</label>
                <div class="col-md-2 drop-down-date">
                    <asp:TextBox ID="dtstart" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeydown="return false;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtfrom_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtfrom_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtstart">
                    </ajaxToolkit:CalendarExtender>
                </div>
                <label class="control-label col-md-1">End Date</label>
                <div class="col-md-2 drop-down-date">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeydown="return false;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtto_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtto_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtend">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label col-md-1">Employee</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbemployee" CssClass="form-control" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12" style="text-align: center">
                    <asp:LinkButton ID="btnPrintDriverHis" runat="server" CssClass="btn btn-success" OnClick="btnPrintDriverHis_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                    <asp:LinkButton ID="btnPrintDriverHisAllDriver" runat="server" CssClass="btn btn-success" OnClick="btnPrintDriverHisAllDriver_Click"><i class="fa fa-print">&nbsp;Print All</i></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
    <%--</div>--%>
</asp:Content>


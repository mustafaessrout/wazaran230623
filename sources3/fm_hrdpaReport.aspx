<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_hrdpaReport.aspx.cs" Inherits="fm_hrdpaReport" %>

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
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Performance Appraisal Report</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Period </label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbperiod" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Level</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cblevel" CssClass="form-control" runat="server" onchange="javascript:ShowProgress();" AutoPostBack="True" OnSelectedIndexChanged="cblevel_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Job Title</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbjobtitle" CssClass="form-control" runat="server" onchange="javascript:ShowProgress();" AutoPostBack="True" OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Employee</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbemployee" AutoPostBack="true"  CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-md-1"></label>
                    <div class="col-md-2 ">
                    </div>
                    <label class="control-label col-md-1"></label>
                    <div class="col-md-2 ">
                    </div>
                    <label class="control-label col-md-1"></label>
                    <div class="col-md-2 ">
                    </div>
                    <label class="control-label col-md-1" style="display: none;">Nationality</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label class="control-label col-md-1" Text="" ID="lblNationalit" runat="server" Style="display: none;"></asp:Label>
                                <asp:HiddenField ID="hdfCustomerAchievement" runat="server" Value="" />
                                <asp:HiddenField ID="hdfCartoonAchievement" runat="server" Value="" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="h-divider"></div>


                <div class="row">
                   <div class="col-md-12" style="text-align: center">
                        <asp:LinkButton ID="btnPrintDriverHis" runat="server" CssClass="btn btn-success" OnClick="btnPrintDriverHis_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                        <asp:LinkButton ID="btnPrintDriverHisAllDriver" runat="server" CssClass="btn btn-success" OnClick="btnPrintDriverHisAllDriver_Click"><i class="fa fa-print">&nbsp;Print All</i></asp:LinkButton>
                        <%--<asp:LinkButton ID="bprint" CssClass="btn btn-danger" runat="server" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print PA</i></asp:LinkButton>
                        <asp:LinkButton ID="btnIncentive" CssClass="btn btn-danger" runat="server" OnClick="btnIncentive_Click"><i class="fa fa-print">&nbsp;Incentive Statement</i></asp:LinkButton>
                        <asp:LinkButton ID="btnValidationSheet" CssClass="btn btn-danger" runat="server" OnClick="btnValidationSheet_Click"><i class="fa fa-print">&nbsp;Validation Sheet</i></asp:LinkButton>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


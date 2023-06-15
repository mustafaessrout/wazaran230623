<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acccndnReport.aspx.cs" Inherits="fm_acccndnReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <script>
        function openwindow() {
            var oNewWindow = window.open("lookup_acccndn.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Cash Discount 1% Report</h4>
        <div class="h-divider"></div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-2">
                        <label>Search By Date</label>
                    </div>
                    <label class="control-label col-md-1">From </label>
                    <div class="col-md-2">

                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtPostFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender3" runat="server" Format="d/M/yyyy" TargetControlID="dtPostFromDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">To From </label>
                    <div class="col-md-2">

                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtPostToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender4" runat="server" Format="d/M/yyyy" TargetControlID="dtPostToDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div class="navi row margin-bottom">


            <%--<asp:LinkButton ID="btPrint" runat="server" CssClass="btn btn-primary" OnClick="btPrintCNDN_Click">Print CNDN</asp:LinkButton>--%>
            <%--<asp:LinkButton ID="btprintDiscount" runat="server" CssClass="btn btn-primary" OnClick="btprintDiscount_Click">Print Cash Discount 1% Report</asp:LinkButton>--%>
            <asp:LinkButton ID="btnInvoice" runat="server" CssClass="btn btn-primary" OnClick="btnInvoice_Click">Print Invoice Report</asp:LinkButton>
            <%--<asp:LinkButton ID="btnInvoiceRaw" runat="server" CssClass="btn btn-primary" OnClick="btnInvoiceRaw_Click">Print 1 % Raw</asp:LinkButton>--%>
            <asp:LinkButton ID="btnsoaRaw" runat="server" CssClass="btn btn-primary" OnClick="btnsoaRaw_Click">SOA Raw</asp:LinkButton>
        </div>
    </div>

</asp:Content>


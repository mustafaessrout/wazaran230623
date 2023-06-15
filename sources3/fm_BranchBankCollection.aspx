<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_BranchBankCollection.aspx.cs" Inherits="fm_BranchBankCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <script>
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
        function openreport(url) {
            window.open(url, "myrep");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Bank Collection</h4>
        <div class="h-divider"></div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Collection From</label>
                    <div class="col-md-2   drop-down">
                        <asp:DropDownList ID="cbfromperiod" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Collection To</label>
                    <div class="col-md-2   drop-down">
                        <asp:DropDownList ID="cbtoperiod" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="navi row margin-bottom">
                <asp:LinkButton ID="btPrint" runat="server" CssClass="btn btn-primary" OnClick="btPrint_Click">Bank Collection</asp:LinkButton>
                 <asp:LinkButton ID="btnOther" runat="server" CssClass="btn btn-primary" OnClick="btnOther_Click">Cash Collection</asp:LinkButton>
                                 <asp:LinkButton ID="btnAll" runat="server" CssClass="btn btn-primary" OnClick="btnAll_Click">Bank Transaction Print</asp:LinkButton>
                <%--<asp:LinkButton ID="btnPayCashDep" runat="server" CssClass="btn btn-primary" OnClick="btnPayCashDep_Click">Cash Deposit</asp:LinkButton>
                 <asp:LinkButton ID="btnPayBank" runat="server" CssClass="btn btn-primary" OnClick="btnPayBank_Click">Bank Payment</asp:LinkButton>
                 <asp:LinkButton ID="btnPayUnpaid" runat="server" CssClass="btn btn-primary" OnClick="btnPayUnpaid_Click">Unpaid Payment</asp:LinkButton>--%>
            </div>
        </div>
    </div>
</asp:Content>


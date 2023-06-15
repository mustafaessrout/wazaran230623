<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_bypassed_tcp.aspx.cs" Inherits="admin_fm_bypassed_tcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" runat="Server">
    <div class="alert alert-info text-bold">By Passed Closing</div>
    <div class="container">
        <div class="row margin-bottom">
            <table class="mGrid">
                <tr style="background-color:silver">
                    <th>By Passed Type</th>
                    <th>Action</th>
                </tr>
                <tr>
                    <td>Bypassed Salesman Deposit</td>
                    <td>
                        <asp:LinkButton ID="btsalesmandeposit" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btsalesmandeposit_Click">Bypass Now</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>Bypassed Payment Customer Cash</td>
                    <td>
                        <asp:LinkButton ID="btpaymetncash" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btpaymetncash_Click">Bypass Now</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>Bypassed Invoice Received</td>
                    <td>
                        <asp:LinkButton ID="btbypassinvoicereceived" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btbypassinvoicereceived_Click">Bypass Now</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


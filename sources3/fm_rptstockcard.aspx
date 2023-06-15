<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptstockcard.aspx.cs" Inherits="fm_rptstockcard" %>

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

    <style>
        th {
            position: sticky;
            top: 0;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Stock Card Report</div>
    <div class="container">
        <div class="row margin-bottom">
            <table class="mGrid">
                <tr>
                    <th>Period</th>
                    <th>Item</th>
                    <th>Warehouse</th>
                    <th>Bin</th>
                    <th>Search</th>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cbperiod" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="cbitem" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="cbwhs" AutoPostBack="true" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="cbbin" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton ID="btsearch" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12  overflow-y" style="max-height: 360px;">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server"></asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btprint" CssClass="btn btn-sm btn-info" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


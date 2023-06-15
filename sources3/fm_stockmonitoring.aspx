<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockmonitoring.aspx.cs" Inherits="fm_stockmonitoring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 106px;
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
    <div class="divheader">Stock Monitoring Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix form-group col-sm-12">
                <label class="col-sm-1 control-label">Type</label>
                <div class="col-sm-11 drop-down">
                    <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control ">
                        <asp:ListItem Value="0">Montoring Good Stock</asp:ListItem>
                        <asp:ListItem>List Order VS Availble Stock</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>

            <div class="clearfix form-group col-sm-12">
                <label class="col-sm-1 control-label">Salespoint</label>
                <div class="col-sm-11 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control ">
                    </asp:DropDownList>

                </div>
            </div>
            <div class="clearfix form-group col-sm-12">
                <label class="col-sm-1 control-label">From Item</label>
                <div class="col-sm-5 drop-down">
                    <asp:DropDownList ID="cbitem_cdFr" runat="server" CssClass="form-control ">
                    </asp:DropDownList>

                </div>
                <label class="col-sm-1 control-label">To Item</label>
                <div class="col-sm-5 drop-down">
                    <asp:DropDownList ID="cbitem_cdTo" runat="server" CssClass="form-control ">
                    </asp:DropDownList>

                </div>
            </div>

        </div>

        <div class="row navi padding-bottom padding-top text-center">
            <asp:Button ID="btprint" runat="server" OnClientClick="ShowProgress();" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


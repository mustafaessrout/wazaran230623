<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_goodreceived_nav.aspx.cs" Inherits="fm_goodreceived_nav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-3.2.1.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
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
    <div class="alert alert-bullet text-bold">Good Received From Navision</div>
    <div class="container block-shadow-info">
        <div class="row">
            <asp:GridView ID="grd" OnRowDataBound="grd_RowDataBound" CssClass="table table-bordered table-condensed input-sm" runat="server" AutoGenerateColumns="False" AllowPaging="True">
                <Columns>
                    <asp:TemplateField HeaderText="DO Number">
                        <ItemTemplate>
                            <%#Eval("DO_No") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Do Date">
                        <ItemTemplate><%#Eval("Order_Date","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch Code">
                        <ItemTemplate><%#Eval("DO_Branch_Code") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch Name">
                        <ItemTemplate><%#Eval("DO_Branch_Name") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <SelectedRowStyle BackColor="Yellow" />
            </asp:GridView>
        </div>
        <div class="alert alert-info input-sm">Detail</div>
        <div class="row">
            <div class="col-sm-12">
                <asp:GridView ID="grddetail" CssClass="table table-bordered table-condensed input-sm" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty"></asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


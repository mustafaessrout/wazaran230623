<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_devicesalesman_closing.aspx.cs" Inherits="fm_devicesalesman_closing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-3.2.1.min.js"></script>
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
    <div class="alert alert-info text-bold">Salesman Device Closing Date</div>
    <div class="container">
        <div class="row margin-bottom">
            <div class="col-sm-12 overflow-y" style="max-height: 360px;">
                <asp:GridView ID="grd" CssClass="table table-bordered table-sm input-sm" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Emp Code">
                            <ItemTemplate>
                                <asp:Label ID="lbemployeecode" runat="server" Text='<%#Eval("emp_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman Name">
                            <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Closing Date">
                            <ItemTemplate>
                                <asp:Label ID="lbtabdate" runat="server" Text='<%#Eval("tab_dt","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btaction" OnClick="btaction_Click" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server">Send Alert</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


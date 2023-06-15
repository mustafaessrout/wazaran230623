<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appcndnsalesman.aspx.cs" Inherits="fm_appcndnsalesman" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Approval CNDN Salesman</div>
    <div class="container">
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="CNDN No">
                            <ItemTemplate>
                                <asp:Label ID="lbcndncode" runat="server" Text='<%#Eval("cndn_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date (Creation)">
                            <ItemTemplate><%#Eval("cndn_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Post Date (Show as transaction date)">
                            <ItemTemplate><%#Eval("post_dt", "{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate><%#Eval("cndn_typ") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate><%#Eval("salesman") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate><%#Eval("amt","{0:N2}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approve">
                            <ItemTemplate>
                                <asp:LinkButton ID="btapprove" CssClass="btn btn-primary btn-sm" OnClick="btapprove_Click" OnClientClick="ShowProgress();" runat="server">Approve</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reject">
                            <ItemTemplate>
                                <asp:LinkButton ID="btreject" CssClass="btn btn-danger btn-sm" OnClientClick="ShowProgress();" OnClick="btreject_Click" runat="server">Reject</asp:LinkButton>
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


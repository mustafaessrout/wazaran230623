<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_returnconfirmation.aspx.cs" Inherits="fm_returnconfirmation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
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
    <div id="container">
        <%--   <h4 class="jajarangenjang">Sales Return - Confirmation</h4>
        <div class="h-divider"></div>--%>
        <div class="alert alert-info text-bold">Sales Return - Confirmation</div>
        <div id="listcash" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdre" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grdre_PageIndexChanging" ShowFooter="False" CellPadding="0" OnRowCommand="grdre_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Salespoint">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label>
                                                -
                                                <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Retur No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbreturno" runat="server" Text='<%#Eval("retur_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbreturdt" runat="server" Text='<%#Eval("retur_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcustcd" runat="server" Text='<%#Eval("cust_cd") %>'></asp:Label>
                                                -
                                                <asp:Label ID="lbcustnm" runat="server" Text='<%#Eval("cust_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salesman">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalesmancd" runat="server" Text='<%#Eval("salesman_cd") %>'></asp:Label>
                                                -
                                                <asp:Label ID="lbsalesmannm" runat="server" Text='<%#Eval("salesman_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnApprove" OnClientClick="ShowProgress();" runat="server" Text="Approve" CssClass="btn btn-primary" CommandName="approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnReject" OnClientClick="ShowProgress();" runat="server" Text="Reject" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


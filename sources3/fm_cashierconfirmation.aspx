<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashierconfirmation.aspx.cs" Inherits="fm_cashierconfirmation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <h4 class="jajarangenjang">Closing Cashier - Confirmation</h4>
        <div class="h-divider"></div>

        <div id="listcash" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">                
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdcash" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grdcash_PageIndexChanging" ShowFooter="False" CellPadding="0" OnRowCommand="grdcash_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Salespoint">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label> - <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbchclosingno" runat="server" Text='<%#Eval("chclosingno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbchclosing_dt" runat="server" Text='<%#Eval("chclosing_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtotamt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbpayment_typ" runat="server" Text='<%#Eval("chclosing_description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Document">
                                            <ItemTemplate>
                                                <div  class="data-table-popup">
                                                    <a class="example-image-link" href="/images/cashier/<%# Eval("cashierfile") %>" data-lightbox="example-1<%# Eval("cashierfile") %>">
                                                        <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>                
                                                <asp:Button ID="btnApprove" runat="server" Text="Confirm" CssClass="btn btn-default" CommandName="approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
</asp:Content>


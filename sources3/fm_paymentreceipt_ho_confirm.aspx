<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentreceipt_ho_confirm.aspx.cs" Inherits="fm_paymentreceipt_ho_confirm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <h4 class="jajarangenjang">Payment Receipt HO - Confirmation</h4>
        <div class="h-divider"></div>

        <div id="listcash" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">                
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grd_PageIndexChanging" ShowFooter="False" CellPadding="0"  OnRowCommand="grd_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Salespoint">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label> - <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbpayment_dt" runat="server" Text='<%#Eval("payment_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sys No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbpaymentno" runat="server" Text='<%#Eval("payment_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manual No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbmanualno" runat="server" Text='<%#Eval("manual_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbinv_no" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salesman">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalesmancd" runat="server" Text='<%#Eval("salesman_cd") %>'></asp:Label> - <asp:Label ID="lbsalesmannm" runat="server" Text='<%#Eval("salesman_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcust_cd" runat="server" Text='<%#Eval("cust_cd") %>'></asp:Label> - <asp:Label ID="lbcust_nm" runat="server" Text='<%#Eval("cust_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtotamt" runat="server" Text='<%#Eval("amt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document">
                                            <ItemTemplate>
                                                <div  class="data-table-popup">
                                                    <a class="example-image-link" href="/images/payment_ho/<%# Eval("docfile") %>" data-lightbox="example-1<%# Eval("docfile") %>">
                                                        <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField>
                                            <ItemTemplate>                
                                                <asp:Button ID="btnReceived" runat="server" Text="Received" CssClass="btn btn-default" CommandName="received" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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


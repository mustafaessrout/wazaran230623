<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="bankcollection.aspx.cs" Inherits="bankcollection" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tbl-cst,
        .tbl-cst td,
        .tbl-cst th {
            font-size: 12px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row clearfix">
            <div class="col-sm-3 no-padding margin-bottom clearfix">
                <label class="control-label col-sm-3 titik-dua">Start Date</label>
                <div class="col-sm-8 drop-down-date">
                    <asp:TextBox ID="dtfrom" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtfrom_CalendarExtender" runat="server" TargetControlID="dtfrom" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="col-sm-3 no-padding margin-bottom clearfix">
                <label class="control-label col-sm-3 titik-dua">End Date</label>
                <div class="col-sm-8 drop-down-date">
                    <asp:TextBox ID="dtto" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtto_CalendarExtender" runat="server" TargetControlID="dtto" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="col-sm-3 no-padding margin-bottom clearfix">
                <label class="control-label col-sm-3 titik-dua">Deposit No</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txdepositno" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-3 no-padding margin-bottom clearfix">
                <label class="control-label col-sm-3 titik-dua">Amount No</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txamount" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="margin-bottom navi ">
            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-success" OnClick="btprint_Click">Get Data</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" CellSpacing="2" CssClass="table table-striepd mygrid tbl-cst" EmptyDataText="No Transactions Found" ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Payment No">
                            <ItemTemplate><%# Eval("payment_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="payment Type">
                            <ItemTemplate><%# Eval("payment_typ") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate><%# Eval("customer") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OTLCD">
                            <ItemTemplate><%# Eval("otlcd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cheque No">
                            <ItemTemplate><%# Eval("btcheq_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Name">
                            <ItemTemplate><%# Eval("acc_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account No">
                            <ItemTemplate><%# Eval("accnoto") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deposit No">
                            <ItemTemplate><%# Eval("deposit_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Amount">
                            <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Clearance Date">
                            <ItemTemplate><%# Eval("clear_dt","{0: dd/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate><%# Eval("attachment") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>

                                            <ItemTemplate>
                                                <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("/images/{0}", HttpUtility.UrlEncode(Eval("attachment").ToString())) %>'
                                                    Text='<%# Eval("attachment").ToString() == "-" ? "" : "Preview" %>' Target="_blank" Enabled='<%# !Eval("attachment").Equals("-") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Mode">
                            <ItemTemplate><%# Eval("Paymentmodule") %></ItemTemplate>
                        </asp:TemplateField>
                       
                            <%--<asp:HyperLinkField DataNavigateUrlFields="attachment" Target="_blank" HeaderText="Document" DataNavigateUrlFormatString="/images/{0}" Text="Download" />--%>
                    </Columns>

                    <FooterStyle BackColor="Silver" />

                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btprint" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimpayment_app.aspx.cs" Inherits="fm_claimpayment_app" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">

        <div class="page-header">
            <h3>Data Claim Payment (PAFL & Misfaco)</h3>
        </div>

        <div class="form-horizontal">
            <div class="row">
                <div class="col-sm-12">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="table-responsive">
                        <asp:GridView ID="grdpayment" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" AllowPaging="true" DataKeyNames="clh_no" OnPageIndexChanging="grdpayment_PageIndexChanging" >  
                            <Columns>
                                <asp:TemplateField HeaderText="Claim Reference No">
                                        <ItemTemplate>
                                            <a href="javascript:openwindow('fm_claimpayment_app_detail.aspx?ch=<%# Eval("clh_no") %>');">
                                            <asp:Label ID="lbclhno" runat="server" Text='<%# Eval("clh_no") %>'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Posted Date.">
                                         <ItemTemplate><%# Eval("created_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Doc Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdoctype" runat="server" Text='<%# Eval("doc_typ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Doc No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdocno" runat="server" Text='<%# Eval("doc_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbamount" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <%# Eval("remark") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File">
                                    <ItemTemplate>
                                        <a class="example-image-link" href="/images/claim_doc/payment/<%# Eval("doc_file") %>" download data-lightbox="example-1<%# Eval("doc_file") %>">
                                        <asp:Label ID="lbfileloc" runat="server" Text='Preview'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>     
                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-default" OnClick="btnApprove_Click" OnClientClick="x=confirm ('Approve this Payment ?');if (x==true) return true;" />
                                    </ItemTemplate>
                                </asp:TemplateField>          
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


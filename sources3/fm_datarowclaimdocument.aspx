<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_datarowclaimdocument.aspx.cs" Inherits="fm_datarowclaimdocument" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="page-header">
            <h3>Document Claim</h3>
        </div>
        <div class="form-horizontal">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                        <div class="table-responsive">
                            <asp:GridView ID="grddocument" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" >  
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Name">
                                         <ItemTemplate>
                                            <asp:Label ID="lbdocname" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Preview">
                                        <ItemTemplate>
                                            <a class="example-image-link" href="/images/faq/claim/doc/<%# Eval("doc_cd") %>.pdf" data-lightbox="example-1<%# Eval("doc_cd") %>.pdf">
                                                <asp:Label ID="lbdocfilename" runat="server" Text='Preview'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>  
                </div>
            </div>
        </div>
    </div>

</asp:Content>


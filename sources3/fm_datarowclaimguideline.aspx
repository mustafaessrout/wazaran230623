<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_datarowclaimguideline.aspx.cs" Inherits="fm_datarowclaimguideline" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="page-header">
            <h3>Claim SOP & Procedures</h3>
        </div>
        <div class="form-horizontal">

            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <label for="ccnr" class="col-sm-4 col-md-1 control-label control-label-sm">Document Type</label>
                            <div class="col-md-4 col-sm-3">
                                <div class=" drop-down">
                                    <asp:DropDownList ID="cbDocument" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="cbDocument_SelectedIndexChanged">
                                        <asp:ListItem Text="SOP" Value="sop"></asp:ListItem>
                                        <asp:ListItem Text="Guideline" Value="guideline"></asp:ListItem>
                                        <asp:ListItem Text="Directory" Value="directory"></asp:ListItem>
                                        <asp:ListItem Text="Flowchart" Value="flowchart"></asp:ListItem>
                                    </asp:DropDownList>
                                </div
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

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
                                            <a class="example-image-link" href="/images/faq/claim/<%# Eval("loc_doc") %>/<%# Eval("doc_cd") %>.pdf" data-lightbox="example-1<%# Eval("doc_cd") %>.pdf">
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


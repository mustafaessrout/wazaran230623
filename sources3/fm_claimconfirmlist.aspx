<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimconfirmlist.aspx.cs" Inherits="fm_claimconfirmlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">List Claim Daily Confirmation</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        
        <div class="row">
            <div class="form-group">
                <label for="period" class="col-xs-12 text-lg control-label control-label-sm">Period</label>
                <div class="col-sm-12 well well-sm">
                    <label for="ccnr" class="col-sm-2 col-md-1 control-label control-label-sm">Month</label>
                    <div class="col-md-4 col-sm-3">
                        <div class=" drop-down">
                            <asp:DropDownList ID="cbMonth" runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Text="-- All Month --" Value="ALL"></asp:ListItem>
                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <label for="ccnr" class="col-sm-2 col-md-1 control-label control-label-sm">Year</label>
                    <div class="col-md-4 col-sm-3">
                        <div class=" drop-down">
                            <asp:DropDownList ID="cbYear" runat="server"  CssClass="form-control input-sm">
                                
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                </div>                    
            </div>
            <div class="form-group">
                <label for="search" class="col-xs-12 text-lg control-label control-label-sm">Search</label>
                <div class="col-sm-12 well well-sm">
                    <div class="col-md-4 col-sm-3">
                        <asp:TextBox ID="txsearch" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary btn-sm" OnClick="btnsearch_Click" />
                    </div>
                </div> 
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <div class="table-responsive">
                        <asp:GridView ID="grdinvoice" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdinvoice_RowDataBound">  
                            <Columns>
                                <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbinvoice" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <%# Eval("disc_cd") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Order (File)">
                                        <ItemTemplate>
                                            <a class="example-image-link" href="/images/invoice_doc/<%# Eval("fileinv") %>" data-lightbox="example-1<%# Eval("fileinv") %>">
                                                <asp:Label ID="lbfileinv" runat="server" Text='<%# Eval("fileinv") %>'></asp:Label>
                                            </a> / 
                                            <a class="example-image-link" href="/images/invoice_doc_copy/<%# Eval("fileinv") %>" data-lightbox="example-1<%# Eval("fileinv") %>">
                                                <asp:Label ID="lbfileinv1" runat="server" Text='<%# Eval("fileinv") %>'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Free (File)">
                                        <ItemTemplate>
                                            <a class="example-image-link" href="/images/invoice_doc/<%# Eval("fileinv_f") %>" data-lightbox="example-1<%# Eval("fileinv_f") %>">
                                                <asp:Label ID="lbfileinv_f" runat="server" Text='<%# Eval("fileinv_f") %>'></asp:Label>
                                            </a> / 
                                            <a class="example-image-link" href="/images/invoice_doc_copy/<%# Eval("fileinv_f") %>" data-lightbox="example-1<%# Eval("fileinv_f") %>">
                                                <asp:Label ID="lbfileinv_f1" runat="server" Text='<%# Eval("fileinv_f") %>'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grdcashout" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdcashout_RowDataBound">  
                            <Columns>
                                <asp:TemplateField HeaderText="Cashout No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcashout" runat="server" Text='<%# Eval("cashout_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manual No">
                                        <ItemTemplate>
                                            <%# Eval("manualno") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="(File)">
                                        <ItemTemplate>
                                            <a class="example-image-link" href="/images/<%# Eval("cashoutfile") %>" data-lightbox="example-1<%# Eval("cashoutfile") %>">
                                                <asp:Label ID="lbfileinv" runat="server" Text='<%# Eval("cashoutfile") %>'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>  
            </div>
        </div>
     
    </div>
</asp:Content>


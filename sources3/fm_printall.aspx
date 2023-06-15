<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_printall.aspx.cs" Inherits="fm_printall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="admin/js/bootstrap.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Document Print</div>
        <div class="h-divider">
    <div class="container">
        
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="table table-striped mygrid" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Doc No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdocno" runat="server" Text='<%# Eval("doc_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tablet No">
                                    <ItemTemplate>
                                        <%# Eval("tab_no") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate><%# Eval("payment_typ") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amt">
                                    <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Man No">
                                    <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salesman">
                                    <ItemTemplate><%# Eval("salesman") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cust">
                                    <ItemTemplate><%# Eval("customer") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="table-edit"/>
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                            <SelectedRowStyle CssClass="table-edit"/>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
       
    </div>
     <div class="navi margin-bottom">
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
</asp:Content>


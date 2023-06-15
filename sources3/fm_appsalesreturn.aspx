<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appsalesreturn.aspx.cs" Inherits="fm_appsalesreturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="admin/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Sales Return Approval</div>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="grdapp" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" OnRowEditing="grdapp_RowEditing" OnRowUpdating="grdapp_RowUpdating" OnRowCancelingEdit="grdapp_RowCancelingEdit" GridLines="None" AllowPaging="True" CellPadding="2" OnPageIndexChanging="grdapp_PageIndexChanging"  OnSelectedIndexChanging="grdapp_SelectedIndexChanging1">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Return NO.">
                            <ItemTemplate>
                                <asp:Label ID="lbretur_no" runat="server" Text='<%# Eval("retur_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual NO.">
                            <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Return Type">
                            <ItemTemplate><%# Eval("retur_typ_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Return Date">
                            <ItemTemplate><%# Eval("retur_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Approve/Reject">
                            <ItemTemplate>
                                <%# Eval("retur_sta_nm") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cbapp" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ACTION" ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" CommandName="Update" Text="Update" OnClientClick="return confirm('Are you sure you want to Update?'); "></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="Linkbutton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ShowSelectButton="True" />

                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="table table-striped mygrid">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branded">
                            <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman Price">
                            <ItemTemplate><%# Eval("custprice") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt">
                            <ItemTemplate><%# Eval("amt","{0:F2}") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>



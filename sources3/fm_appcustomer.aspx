<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appcustomer.aspx.cs" Inherits="fm_appcustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Customer Approval</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <asp:GridView ID="grdapp" CssClass="table table-striped mygrid row-no-padding" runat="server" AutoGenerateColumns="False" OnRowEditing="grdapp_RowEditing" OnRowUpdating="grdapp_RowUpdating" OnRowCancelingEdit="grdapp_RowCancelingEdit" GridLines="None">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Customer Code">
                        <ItemTemplate>
                            <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Short Name">
                        <ItemTemplate><%# Eval("cust_sn") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Arabic">
                        <ItemTemplate><%# Eval("cust_arabic") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate><%# Eval("otlcd") %></ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Credit Limit">
                        <ItemTemplate><%# Eval("credit_limit") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Creator">
                        <ItemTemplate><%# Eval("createdby") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Approve/Reject">
                        <ItemTemplate>
                            <%# Eval("cust_sta_nm") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbapp" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" HeaderText="Action" />

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
</asp:Content>


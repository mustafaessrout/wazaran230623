<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentsuspense.aspx.cs" Inherits="fm_paymentsuspense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Payment Suspense Process</div>
    <div class="h-divider"></div>
    <div class="container">
        <div class="row">
            <asp:GridView ID="grd" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid table table-hover table-striped " Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnPageIndexChanging="grd_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Check">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" CssClass="ro" />
                            <asp:HiddenField ID="hds" runat="server" Value='<%# Eval("ids") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paid Date">
                        <ItemTemplate>
                            <%# Eval("payment_dt","{0:d/M/yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust Cd">
                        <ItemTemplate>
                            <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust Name">
                        <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Source">
                        <ItemTemplate>
                            <asp:Label ID="lbpaymentno" runat="server" Text='<%# Eval("payment_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Manual No">
                        <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Py Type">
                        <ItemTemplate><%# Eval("payment_typ") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Py BY">
                        <ItemTemplate>
                            <asp:Label ID="lbrdpaidfor" runat="server" Text='<%# Eval("rdpaidfor") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sus Amt">
                        <ItemTemplate>
                            <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sus AVL Bal">
                        <ItemTemplate>
                            <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("balance","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust Bal">
                        <ItemTemplate><%# Eval("custbal","{0:F2}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%# Eval("susp_sta_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle CssClass="table-edit" />
            </asp:GridView>
        </div>
    </div>

    <div class="h-divider"></div>

    <div class="divheader">
        List Available Invoice for Paid, AVL amt :
        <asp:Label ID="lbamt" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>&nbsp;<asp:Button ID="btpaid" runat="server" CssClass="btn  btn-primary add" Text="Apply Payment To Invoice" OnClick="btpaid_Click" />
    </div>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row margin-bottom">

            <asp:GridView ID="grdinv" runat="server" CellPadding="0" CssClass="mygrid table table-hover table-striped" AutoGenerateColumns="False" EmptyDataText="No Invoice Found" ShowHeaderWhenEmpty="True" ShowFooter="True">
                <Columns>
                    <asp:TemplateField HeaderText="Inv No">
                        <ItemTemplate>
                            <asp:Label ID="lbinvno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Manual No">
                        <ItemTemplate>
                            <%# Eval("manual_no") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate>
                            <%# Eval("cust_desc") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Totamt">
                        <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Balance">
                        <ItemTemplate>
                            <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("balance") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amt">
                        <ItemTemplate>
                            <div class=" center">
                                <asp:TextBox ID="txamt" runat="server" CssClass="form-control input-sm" Width="80px" Text="0"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbtotamt" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%# Eval("inv_sta_nm") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
            </asp:GridView>

        </div>

        <div class="navi row margin-bottom">
            <asp:Button ID="btnew" runat="server" Text="New" CssClass=" btn btn-success btn-add" OnClick="btnew_Click" />
            <asp:Button ID="btapply" runat="server" Text="Save Payment" CssClass="btn btn-warning btn-save" OnClick="btapply_Click" />
            <asp:Button ID="btprint" runat="server" Text="Print " CssClass="btn btn-info btn-print" OnClick="btprint_Click" />
        </div>
    </div>

</asp:Content>


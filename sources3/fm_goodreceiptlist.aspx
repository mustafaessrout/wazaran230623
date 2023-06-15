<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_goodreceiptlist.aspx.cs" Inherits="fm_goodreceiptlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Good Receipt List</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" CssClass="table table-hover table-striped mygrid">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Receipt No.">
                        <ItemTemplate>
                                    <asp:Label ID="lbreceipt_no" runat="server" Text='<%# Eval("receipt_no") %>'></asp:Label>
                                </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DO No.">
                        <ItemTemplate><%# Eval("do_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Receipt Date">
                        <ItemTemplate><%# Eval("receipt_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate> <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>' Visible="false"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <EditRowStyle CssClass="table-edit"/>
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle CssClass="table-edit" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div class="navi row margin-bottom padding-bottom">
            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
            <asp:Button ID="btclose" runat="server" CssClass="btn-danger btn btn-close" OnClick="btclose_Click" Text="Close" />
        </div>
    </div>

</asp:Content>


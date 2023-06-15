<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_invreceived.aspx.cs" Inherits="fm_invreceived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Invoice Back From Branches</div>
    <div class="h-divider"></div>
    <div class="container-fluid margin-bottom padding-bottom">
        <div class="row center">
            <div class="overflow-x">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server"  class="overflow-y" style="max-height:380px; width:100%;">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="table table-hover table-striped table-fix table-page-fix mygrid" data-table-page="#copy-fst" GridLines="None" OnRowEditing="grd_RowEditing" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging"  PageSize="30">
                            <AlternatingRowStyle/>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>.
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DO No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdono" runat="server" Text='<%# Eval("do_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <%# Eval("do_dt","{0:d/M/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                    <ItemTemplate><%# Eval("do_sta_nm") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="drop-down-sm">
                                            <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                        </div>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle  />
                            <SelectedRowStyle CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="table-copy-page-fix" id="copy-fst"></div>
            </div>
        </div>
    </div>
</asp:Content>


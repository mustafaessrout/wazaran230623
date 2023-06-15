<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appreturho.aspx.cs" Inherits="fm_appreturho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
       List Of New Retur Request
    </div>
    <div class="h-divider"></div>
    <div class="container-fluid margin-bottom">
        <div class="row center">
            <div class="table-page-fixer">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="overflow-y" style="max-height:380px;">
                            <asp:GridView ID="grd" runat="server" OnPageIndexChanging="grd_PageIndexChanging"  AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" CellPadding="0" CssClass="table table-hover table-striped table-fix mygrid table-page-fix" data-table-page="#copytable" AllowPaging="True" PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="Retur HO No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbreturhono" runat="server" Text='<%# Eval("returho_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate><%# Eval("returho_dt") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salespoint">
                                        <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate><%# Eval("retho_sta_nm") %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action Plan">
                                        <ItemTemplate><%# Eval("action_plan") %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cbaction_plan" runat="server"  CssClass="form-control input-sm"></asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deadline Date (dd/mm/yyyy)" HeaderStyle-Width="250px">
                                        <ItemTemplate><%# Eval("deadline_dt") %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="dtdeadline_dt" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" HeaderText="Edit" />
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle  CssClass="row-pop-up" />
                                <SelectedRowStyle CssClass="table-edit" />
                            </asp:GridView>
                        </div>
                        <div class="table-copy-page-fix" id="copytable"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockout_app.aspx.cs" Inherits="fm_stockout_app" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-bullet text-bold">Approval Stock Out</div>
    <div class="container block-shadow-info">
        <div class="row margin-bottom margin-top">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="mGrid table table-striped" runat="server" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowDataBound="grd_RowDataBound" OnRowUpdating="grd_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Stockout Code">
                            <ItemTemplate>
                                <asp:Label ID="lbsamplecode" runat="server" Text='<%#Eval("sample_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <%#Eval("sample_dt","{0:d/M/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Items">
                            <ItemTemplate>
                                <asp:GridView ID="grddetail" CssClass="mGrid" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <%#Eval("item_cd") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <%#Eval("item_nm") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <%#Eval("qty") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Uom">
                                            <ItemTemplate>
                                                <%#Eval("uom") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <%#Eval("sample_sta_nm") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="drop-down">
                                    <asp:DropDownList ID="cbstatus" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                </div>

                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                    <SelectedRowStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>


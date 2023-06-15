<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_fldvalue.aspx.cs" Inherits="maintenance_fm_fldvalue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Master All Data</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Name</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbfldname" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbfldname_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <table class="table tab-pane">
                        <tr><th>Value</th><th>Description</th><th>Arabic</th><th>Order</th><th>Hidden</th><th>IsActive</th><th>Save</th></tr>
                        <tr><td>
                            <asp:TextBox ID="txvalue" CssClass="form-control" runat="server"></asp:TextBox></td>
                            <td>
                            <asp:TextBox ID="txdesc" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                 <asp:TextBox ID="txarabic" runat="server" CssClass="form-control"></asp:TextBox></td>
                            </td>
                            <td>
                            <asp:TextBox ID="txorder" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="cbhidden" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">YES</asp:ListItem>
                                    <asp:ListItem Value="1">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="cbactive" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="btsave" CssClass="btn btn-primary" runat="server" OnClick="btsave_Click">Save</asp:LinkButton></td>
                        </tr>
                     </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" CellPadding="0" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lbfldvalue" runat="server" Text='<%#Eval("fld_valu") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("fld_desc") %>'></asp:Label></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txdesc" CssClass="form-control" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arabic">
                                <ItemTemplate>
                                    <asp:Label ID="lbarabic" runat="server" Text='<%#Eval("fld_arabic") %>'></asp:Label></ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:TextBox ID="txarabic" CssClass="form-control" runat="server"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ordered">
                                <ItemTemplate>
                                    <asp:Label ID="lborder" runat="server" Text='<%#Eval("orderby") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hidden">
                                <ItemTemplate>
                                    <asp:Label ID="lbhiddendata" runat="server" Text='<%#Eval("hiddendata") %>'></asp:Label></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cbhidden" runat="server" CssClass="form-control"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active/Inactive">
                                <ItemTemplate>
                                    <asp:Label ID="lbdeleted" runat="server" Text='<%#Eval("isactive") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <HeaderStyle CssClass="table-header" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


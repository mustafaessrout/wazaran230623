<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_cndn.aspx.cs" Inherits="Adminbranch_fm_cndn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Approval Credit Note Debit Note</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lbarcnno" runat="server" Text='<%#Eval("arcn_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%#Eval("arcn_note") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate><%#Eval("arcnamount") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approval">
                            <ItemTemplate>
                                <%#Eval("sta_id") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cbapproval" runat="server">
                                    <asp:ListItem Value="A">Approve</asp:ListItem>
                                    <asp:ListItem Value="L">Reject</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ref No.">
                            <ItemTemplate>
                                <asp:Label ID="lbcndnrefno" runat="server" Text='<%#Eval("cndnrefno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" HeaderText="ACTION" />
                    </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>


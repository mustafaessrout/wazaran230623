<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_claimcashoutmap.aspx.cs" Inherits="admin_fm_claimcashoutmap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h5>Cashout Mapping For Claim</h5>
            <div class="form-group">
                <label class="control-label col-md-1">ATL/BTL</label>
                <div class="col-md-1">
                    <asp:DropDownList ID="cbpromokind" runat="server" CssClass="form-control-static" AutoPostBack="True" OnSelectedIndexChanged="cbpromokind_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Promotion Group</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbpromogroup" runat="server" CssClass="form-control-static" AutoPostBack="True" OnSelectedIndexChanged="cbpromogroup_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Promo Type</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbpromotype" runat="server" CssClass="form-control-static" AutoPostBack="True" OnSelectedIndexChanged="cbpromotype_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">Cashout</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbcashout" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btupdate" runat="server" Text="Update" CssClass="btn btn-default" OnClick="btupdate_Click" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-3">
                    <label class="control-label col-md-1">Payment Schedule</label>
                    <asp:TextBox ID="dtschedule" runat="server" CssClass="form-control input-sm" Height="100%"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="table" OnSelectedIndexChanging="grd_SelectedIndexChanging" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Kind">
                                <ItemTemplate>
                                    <asp:Label ID="lbpromokind" runat="server" Text='<%# Eval("promokind") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Promo Group">
                                <ItemTemplate>
                                    <asp:Label ID="lbpromocode" runat="server" Text='<%# Eval("promo_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Promo Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbpromotype" runat="server" Text='<%# Eval("promo_typ") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cashout Code">
                                <ItemTemplate><%# Eval("itemco_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


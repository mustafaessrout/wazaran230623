<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_appcashout2.aspx.cs" Inherits="Adminbranch_fm_appcashout2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Approval Cashout</h4>
        <div class="h-divider"></div>
        <div class ="row">
            <div class="col-md-12">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Cashout Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcashoutcode"  runat="server" Text='<%#Eval("cashout_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Cashout">
                            <ItemTemplate><%#Eval("itemco_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee">
                            <ItemTemplate><%#Eval("emp") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate><%#Eval("amt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VAT">
                            <ItemTemplate><%#Eval("vat_amt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approval">
                            <ItemTemplate><%#Eval("approval_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><%#Eval("cashout_sta_nm") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cbstatus" runat="server" ></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>


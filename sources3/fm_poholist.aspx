<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_poholist.aspx.cs" Inherits="fm_poholist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        PO Head Office List
    </div>
    <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="PO No.">
                    <ItemTemplate><%# Eval("po_no") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%# Eval("po_dt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vendor">
                    <ItemTemplate><%# Eval("vendor_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status PO">
                    <ItemTemplate>
                        <%# Eval("po_sta_id") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark">
                    <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="button2 add" OnClick="btnew_Click" />
    </div>
</asp:Content>


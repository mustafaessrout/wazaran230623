<%@ Page Title="" Language="C#" MasterPageFile="~/adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_appcashout.aspx.cs" Inherits="admin_fm_appcashout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="../css/anekabutton.css" rel="stylesheet" />--%>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">

       <h4 class="jajarangenjang">Approval Cashout</h4>
        <div class="h-divider"></div>
        <div class="form-group">
             <div class="col-md-12">
        <asp:GridView ID="grd" runat="server"  CellPadding="0" CssClass="mydatagrid" RowStyle-CssClass="row" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowUpdating="grd_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Cashout No">
                    <ItemTemplate>
                        <asp:Label ID="lbcashoutno" runat="server" Text='<%# Eval("casregout_cd") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate><%#Eval("itemco_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PIC">
                    <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approval">
                    <ItemTemplate><%#Eval("app_cd") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="cbapproval" runat="server"></asp:DropDownList></EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reason">
                    <ItemTemplate></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" HeaderText="Action" />
            </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
        </asp:GridView>
    </div>
        </div>  
 
   
</asp:Content>


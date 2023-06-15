<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_appcanvas.aspx.cs" Inherits="admin_fm_appcanvas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <h3>Approval Canvasser</h3>
        <div class="col-md-8">
            <asp:GridView ID="grd" runat="server" CssClass="mygrid" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="Canvasser not found" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" Width="100%" OnRowDataBound="grd_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sys NO">
                        <ItemTemplate>
                            <asp:Label ID="lbsocd" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate><%# Eval("so_dt") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust">
                        <ItemTemplate><%# Eval("cust_cd") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salesman">
                        <ItemTemplate><%# Eval("salesman_cd") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approval">
                        <ItemTemplate>
                            <%# Eval("app_sta_nm") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbstatus" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate>
                            Please Fill Reason
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txreason" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

